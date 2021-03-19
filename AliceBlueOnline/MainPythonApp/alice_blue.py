import json
import requests
import threading
import websocket
import logging
import enum
import datetime
from time import sleep
from bs4 import BeautifulSoup
from collections import OrderedDict
from protlib import CUInt, CStruct, CULong, CUChar, CArray, CUShort, CString
from collections import namedtuple

class AliceBlue:
    # dictionary object to hold settings
    __service_config = {
      'host': 'https://ant.aliceblueonline.com',
      'routes': {
          'authorize': '/oauth2/auth',
          'access_token': '/oauth2/token',
          'profile': '/api/v2/profile',
          'master_contract': '/api/v2/contracts.json?exchanges={exchange}',
          'holdings': '/api/v2/holdings',
          'balance': '/api/v2/cashposition',
          'positions_daywise': '/api/v2/positions?type=daywise',
          'positions_netwise': '/api/v2/positions?type=netwise',
          'positions_holdings': '/api/v2/holdings',
          'place_order': '/api/v2/order',
          'place_amo': '/api/v2/amo',
          'place_bracket_order': '/api/v2/bracketorder',
          'place_basket_order' : '/api/v2/basketorder',
          'get_orders': '/api/v2/order',
          'get_order_info': '/api/v2/order/{order_id}',
          'modify_order': '/api/v2/order',
          'cancel_order': '/api/v2/order?oms_order_id={order_id}&order_status=open',
          'cancel_bo_order': '/api/v2/order?oms_order_id={order_id}&order_status=open&leg_order_indicator={leg_order_id}',
          'cancel_co_order': '/api/v2/coverorder?oms_order_id={order_id}&order_status=open&leg_order_indicator={leg_order_id}',
          'trade_book': '/api/v2/trade',
          'scripinfo': '/api/v2/scripinfo?exchange={exchange}&instrument_token={token}',
      },
      'socket_endpoint': 'wss://ant.aliceblueonline.com/hydrasocket/v2/websocket?access_token={access_token}'
    }

    def __init__(self, username, password, access_token, master_contracts_to_download = None):
        """ logs in and gets enabled exchanges and products for user """
        self.__access_token = access_token
        self.__username = username
        self.__password = password
        self.__websocket = None
        self.__websocket_connected = False
        self.__ws_mutex = threading.Lock()
        self.__on_error = None
        self.__on_disconnect = None
        self.__on_open = None
        self.__subscribe_callback = None
        self.__order_update_callback = None
        self.__market_status_messages_callback = None
        self.__exchange_messages_callback = None
        self.__oi_callback = None
        self.__dpr_callback = None
        self.__subscribers = {}
        self.__market_status_messages = []
        self.__exchange_messages = []
        self.__exchange_codes = {'NSE' : 1,
                                 'NFO' : 2,
                                 'CDS' : 3,
                                 'MCX' : 4,
                                 'BSE' : 6,
                                 'BFO' : 7}
        self.__exchange_price_multipliers = {1: 100,
                                             2: 100,
                                             3: 10000000,
                                             4: 100,
                                             6: 100,
                                             7: 100}

    @staticmethod
    def login_and_get_access_token(username, password, twoFA, api_secret, redirect_url, app_id):
        """ Login and get access token """
        #Get the Code
        if(app_id is None):
            app_id = username
        r = requests.Session()
        config = AliceBlue.__service_config
        url = f"{config['host']}{config['routes']['authorize']}?response_type=code&state=test_state&client_id={app_id}&redirect_uri={redirect_url}" 
        resp = r.get(url)
        if('OAuth 2.0 Error' in resp.text):
            logger.error("OAuth 2.0 Error occurred. Please verify your api_secret")
            return None
        page = BeautifulSoup(resp.text, features="html.parser")
        csrf_token = page.find('input', attrs = {'name':'_csrf_token'})['value']
        login_challenge = page.find('input', attrs = {'name' : 'login_challenge'})['value']
        resp = r.post(resp.url,data={'client_id':username,'password':password,'login_challenge':login_challenge,'_csrf_token':csrf_token})
        if('Please Enter Valid Password' in resp.text):
            logger.error("Please enter a valid password")
            return
        if('Internal server error' in resp.text):
            logger.error("Got Internal server error, please try again after sometimes")
            return
        question_ids = []
        page = BeautifulSoup(resp.text, features="html.parser")
        err = page.find('p', attrs={'class':'error'})
        if(len(err) > 0):
            logger.error(f"Couldn't login {err}")
            return
        for i in page.find_all('input', attrs={'name':'question_id1'}):
            question_ids.append(i['value'])
        resp = r.post(resp.url,data={'answer1':twoFA,'question_id1':question_ids,'answer2':twoFA,'login_challenge':login_challenge,'_csrf_token':csrf_token})
        if('consent_challenge' in resp.url):
            logger.info("Authorizing app for the first time")
            page = BeautifulSoup(resp.text, features="html.parser")
            csrf_token = page.find('input', attrs = {'name':'_csrf_token'})['value']
            resp = r.post(url=resp.url,data={'_csrf_token':csrf_token, 'consent': "Authorize", "scopes": ""})
            if('Internal server error' in resp.text):
                return
        code = resp.url[resp.url.index('=')+1:resp.url.index('&')]

        #Get Access Token
        params = {'code': code, 'redirect_uri': redirect_url, 'grant_type': 'authorization_code', 'client_secret' : api_secret, "cliend_id": username}
        url = f"{config['host']}{config['routes']['access_token']}?client_id={app_id}&client_secret={api_secret}&grant_type=authorization_code&code={code}&redirect_uri={redirect_url}&authorization_response={resp.url}"
        resp = r.post(url,auth=(app_id, api_secret),data=params)
        resp = json.loads(resp.text)
        if('access_token' in resp):
            access_token = resp['access_token']
            return access_token
        else:
            logger.error(f"Couldn't get access token {resp}")
        return None

