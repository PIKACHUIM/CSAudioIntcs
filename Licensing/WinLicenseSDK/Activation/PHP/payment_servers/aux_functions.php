<?php

/******************************************************************************
 * IsPaymentServerIP
 *****************************************************************************/
function IsPaymentServerIP($caller_ip)
{
    $ip_caller = sprintf("%u", ip2long($caller_ip));
    $ip_payment_server = sprintf("%u", ip2long(PAYMENT_SERVER_IP));

    if ($ip_caller == $ip_payment_server)
    {
        return true;
    }
    else
    {   
        return false;
    }
}

/******************************************************************************
 * GetInternalProductNameFromVendorId
 *****************************************************************************/
function GetInternalProductNameFromVendorId($vendor_product_id)
{
    global $vendor_product;

    if (!isset($vendor_product[$vendor_product_id]))
    {
        die("ERROR: The product_name = " . $vendor_product_id . 
            " is not associate to a product name in the 'vendor_product' array in the 'settings.php' file.<br>");
    }
    return $vendor_product[$vendor_product_id];
}


?>

