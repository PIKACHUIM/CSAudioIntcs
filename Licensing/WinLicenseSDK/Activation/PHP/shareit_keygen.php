<html>
<head>
<title>Shareit Keygenerator page</title>
</head>
<body>

<!-- 
    *******************************************************************
    If we are returning the license in text format, we have to set the 
    'Content-Type' to 'text/plain'.
    If we are returning the license in binary format, we have to set the 
    'Content-Type' to 'image/gif'.
    ******************************************************************* 
-->

<meta http-equiv="Content-Type" content="text/html;">

<?php 

/******************************************************************************
;                                Includes
;*****************************************************************************/

include_once "main_settings.php";
include_once "database_access/database_access.php";
include_once "key_generation/generate_key.php";
include_once "classes/license.php";
include_once "classes/customer.php";
include_once "payment_servers/shareit/settings.php";
include_once "payment_servers/generate_order.php";
include_once "payment_servers/aux_functions.php";


/******************************************************************************
;                                  Code
;*****************************************************************************/

if (IsPaymentServerIP($_SERVER["REMOTE_ADDR"]))
{
    $generated_license = GenerateOrder();

    // RETURN TEXT KEY 
    echo KeyAsText($generated_license);

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


