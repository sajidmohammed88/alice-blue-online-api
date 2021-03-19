import logging
import datetime
import statistics
from time import sleep
from alice_blue import *

# Config
username = 'MMY0808'
password = 'Mko@09876'
api_secret = 'XEWJc3HqdnQcap3PccP2W21At2e7YFitX31k9G2ySkTyOCPp073M88ClA2aVpUTA'
twoFA = 'a'
app_id = '18Ne7bOVuT'
redirect_url = 'https://ant.aliceblueonline.com/plugin/callback/'
EMA_CROSS_SCRIP = 'INFY'
logging.basicConfig(level=logging.DEBUG)        # Optional for getting debug messages.
# Config
def main():
    global socket_opened
    global alice
    global username
    global password
    global twoFA
    global api_secret
    global app_id
    global EMA_CROSS_SCRIP
    minute_close = []
    access_token = AliceBlue.login_and_get_access_token(username=username, password=password, twoFA=twoFA,  api_secret=api_secret, redirect_url=redirect_url, app_id=app_id)

main()