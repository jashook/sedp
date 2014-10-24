import httplib, urllib
import cgi, cgitb

def SendData(json_data):

   headers = { "Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain" }

   connection = httplib.HTTPConnection("http://ev9.cloudapp.net")

   connection.request("POST", "/", json_data, headers)

   response = connection.getresponse()

   print response.status, response.reason

   data = response.read()

   connection.close()

json_data = cgi.FieldStorage().getvalue("board")

print json_data

SendData(json_data)
