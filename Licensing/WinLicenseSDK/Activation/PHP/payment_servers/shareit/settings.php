<?php

// Set here the Shareit IP, so only Shareit can call your license generator

define("PAYMENT_SERVER_IP", "127.0.0.1");


// Associate your Shareit product IDs with the names 
// that you have given in WinLicense Orders Manager

$vendor_product["1111111"] = "Activation";
$vendor_product["2222222"] = "My Product Name in WL Orders Manager";


// Shareit URL variable names

define("CUSTOMER_FIRST_NAME", "FIRSTNAME");
define("CUSTOMER_LAST_NAME", "LASTNAME");
define("CUSTOMER_EMAIL", "EMAIL");
define("CUSTOMER_COMPANY", "COMPANY");
define("CUSTOMER_PHONE", "PHONE");
define("CUSTOMER_FAX", "FAX");
define("CUSTOMER_ADDRESS", "STREET");
define("CUSTOMER_CITY", "CITY");
define("CUSTOMER_STATE", "STATE");
define("CUSTOMER_ZIP", "ZIP");
define("CUSTOMER_COUNTRY", "COUNTRY");

define("VENDOR_PRODUCT_ID", "PRODUCT_ID");
define("PRODUCT_QUANTITY", "QUANTITY");

define("PAYMENT_METHOD", "");
define("PAYMENT_NOTES", "");

?>

