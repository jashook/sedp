<?php

require_once("HTTP/Request2.php");

$Request = new HttpRequest("ev9.cloudapp.net", HttpRequest::METH_POST);
$Request.addPutData(file_get_contents("php://input"));

try {
   echo $Request->send()->getBody();
 
} catch(HttpException $Exception) {
   echo $Exception;

}

?>
