<?php

/******************************************************************************
 * ParseUrl_CheckRequiredVariables
 *****************************************************************************/
function ParseUrl_CheckRequiredVariables()
{
    if (!isset($_GET[CUSTOMER_FIRST_NAME]))
    {
        die("Error: " . CUSTOMER_FIRST_NAME . " variable not found in URL");
    }

    if (!isset($_GET[CUSTOMER_EMAIL]))
    {
        die("Error: " . CUSTOMER_EMAIL . " variable not found in URL");
    }

    if (!isset($_GET[VENDOR_PRODUCT_ID]))
    {
        die("Error: " . VENDOR_PRODUCT_ID . " variable not found in URL");
    }
}

/******************************************************************************
 * ParseUrl_CustomerInfo
 *****************************************************************************/
function ParseUrl_CustomerInfo($customer)
{
    if (isset($_GET[CUSTOMER_FIRST_NAME]))
        $customer->first_name = $_GET[CUSTOMER_FIRST_NAME];

    if (isset($_GET[CUSTOMER_LAST_NAME]))
        $customer->last_name = $_GET[CUSTOMER_LAST_NAME];

    if (isset($_GET[CUSTOMER_EMAIL]))
        $customer->email = $_GET[CUSTOMER_EMAIL];

    if (isset($_GET[CUSTOMER_COMPANY]))
        $customer->organization = $_GET[CUSTOMER_COMPANY];

    if (isset($_GET[CUSTOMER_PHONE]))
        $customer->phone = $_GET[CUSTOMER_PHONE];

    if (isset($_GET[CUSTOMER_FAX]))
        $customer->fax = $_GET[CUSTOMER_FAX];

    if (isset($_GET[CUSTOMER_ADDRESS]))
        $customer->address1 = $_GET[CUSTOMER_ADDRESS];

    if (isset($_GET[CUSTOMER_CITY]))
        $customer->city = $_GET[CUSTOMER_CITY];

    if (isset($_GET[CUSTOMER_STATE]))
        $customer->state = $_GET[CUSTOMER_STATE];

    if (isset($_GET[CUSTOMER_ZIP]))
        $customer->zip = $_GET[CUSTOMER_ZIP];

    if (isset($_GET[CUSTOMER_COUNTRY]))
        $customer->country = $_GET[CUSTOMER_COUNTRY];
}

/******************************************************************************
 * ParseUrl_GetVariableValue
 *****************************************************************************/
function ParseUrl_GetVariableValue($variable_name)
{
    if (isset($_GET[$variable_name]))
    {
        return $_GET[$variable_name];
    }
    else
    {
        return "";
    }
}

?>

