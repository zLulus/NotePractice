#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import base64
import hmac
import hashlib
import urllib
import urlparse
import json
import urllib
import sys

def _auth_data():
    psw=sys.argv[1]
    parameters2=sys.argv[2]
    md5 = hashlib.md5()
    md5.update(psw.encode('utf-8'))
    md5.update('hello'.encode('utf-8'))
    s = json.dumps({"assetPwd": md5.hexdigest()})
    r=urllib.quote(s, safe='')
    print r
    return r

if __name__ == '__main__':
    _auth_data()