<?php

// Set here the Paypal IP, so only Paypal can call your license generator

define("PAYMENT_SERVER_IP", "127.0.0.1");


// Associate your Paypal product IDs with the names 
// that you have given in WinLicense Orders Manager

$vendor_product["1111111"] = "Activation";
$vendor_product["2222222"] = "My Product Name in WL Orders Manager";


// Paypal URL variable names

define("CUSTOMER_FIRST_NAME", "first_name");
define("CUSTOMER_LAST_NAME", "last_name");
define("CUSTOMER_EMAIL", "payer_email");
define("CUSTOMER_COMPANY", "payer_business_name");
define("CUSTOMER_PHONE", "contact_phone");
define("CUSTOMER_FAX", "");
define("CUSTOMER_ADDRESS", "address_street");
define("CUSTOMER_CITY", "address_city");
define("CUSTOMER_STATE", "address_state");
define("CUSTOMER_ZIP", "address_zip");
define("CUSTOMER_COUNTRY", "address_country");

define("VENDOR_PRODUCT_ID", "product_name");
define("PRODUCT_QUANTITY", "quantity");

define("PAYMENT_METHOD", "");
define("PAYMENT_NOTES", "");


?>

