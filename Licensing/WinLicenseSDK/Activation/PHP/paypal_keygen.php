<html>
<head>
<title>Paypal Keygenerator page</title>
</head>
<body>

<meta http-equiv="Content-Type" content="text/html;charset=UTF-8">

<?php 

/******************************************************************************
;                                Includes
;*****************************************************************************/

include_once "main_settings.php";
include_once "database_access/database_access.php";
include_once "key_generation/generate_key.php";
include_once "classes/license.php";
include_once "classes/customer.php";
include_once "payment_servers/paypal/settings.php";
include_once "payment_servers/generate_order.php";
include_once "payment_servers/aux_functions.php";


/******************************************************************************
;                                  Code
;*****************************************************************************/

if (IsPaymentServerIP($_SERVER["REMOTE_ADDR"]))
{
    $generated_license = GenerateOrder();

    // RETURN TEXT KEY 
    echo "<br> Paypal key = " . KeyAsText($generated_license) . "<br>";

    // RETURN BINARY KEY 
    //echo KeyAsBinary($generated_license);
}
else
{
    echo "<br>Sorry, you are allowed to generate a license from your IP address <br>";
}

?>

</body>
</html> 


