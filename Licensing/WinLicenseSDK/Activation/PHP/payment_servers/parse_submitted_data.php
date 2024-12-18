<?php

/******************************************************************************
 * SubmittedData_CheckRequiredVariables
 *****************************************************************************/
function SubmittedData_CheckRequiredVariables()
{
    if (!SubmittedData_IsVariableDefined(CUSTOMER_FIRST_NAME))
    {
        die("Error: " . CUSTOMER_FIRST_NAME . " variable not found in URL");
    }

    if (!SubmittedData_IsVariableDefined(CUSTOMER_EMAIL))
    {
        die("Error: " . CUSTOMER_EMAIL . " variable not found in URL");
    }

    if (!SubmittedData_IsVariableDefined(VENDOR_PRODUCT_ID))
    {
        die("Error: " . VENDOR_PRODUCT_ID . " variable not found in URL");
    }
}

/******************************************************************************
 * SubmittedData_GetCustomerInfo
 *****************************************************************************/
function SubmittedData_GetCustomerInfo($customer)
{
    if (SubmittedData_IsVariableDefined(CUSTOMER_FIRST_NAME))
        $customer->first_name = SubmittedData_GetVariableValue(CUSTOMER_FIRST_NAME);

    if (SubmittedData_IsVariableDefined(CUSTOMER_LAST_NAME))
        $customer->last_name = SubmittedData_GetVariableValue(CUSTOMER_LAST_NAME);

    if (SubmittedData_IsVariableDefined(CUSTOMER_EMAIL))
        $customer->email = SubmittedData_GetVariableValue(CUSTOMER_EMAIL);

    if (SubmittedData_IsVariableDefined(CUSTOMER_COMPANY))
        $customer->organization = SubmittedData_GetVariableValue(CUSTOMER_COMPANY);

    if (SubmittedData_IsVariableDefined(CUSTOMER_PHONE))
        $customer->phone = SubmittedData_GetVariableValue(CUSTOMER_PHONE);

    if (SubmittedData_IsVariableDefined(CUSTOMER_FAX))
        $customer->fax = SubmittedData_GetVariableValue(CUSTOMER_FAX);

    if (SubmittedData_IsVariableDefined(CUSTOMER_ADDRESS))
        $customer->address1 = SubmittedData_GetVariableValue(CUSTOMER_ADDRESS);

    if (SubmittedData_IsVariableDefined(CUSTOMER_CITY))
        $customer->city = SubmittedData_GetVariableValue(CUSTOMER_CITY);

    if (SubmittedData_IsVariableDefined(CUSTOMER_STATE))
        $customer->state = SubmittedData_GetVariableValue(CUSTOMER_STATE);

    if (SubmittedData_IsVariableDefined(CUSTOMER_ZIP))
        $customer->zip = SubmittedData_GetVariableValue(CUSTOMER_ZIP);

    if (SubmittedData_IsVariableDefined(CUSTOMER_COUNTRY))
        $customer->country = SubmittedData_GetVariableValue(CUSTOMER_COUNTRY);
}

/******************************************************************************
 * SubmittedData_GetVariableValue
 *****************************************************************************/
function SubmittedData_GetVariableValue($variable_name)
{
    if (isset($_GET[$variable_name]))
    {
        return $_GET[$variable_name];
    }
    else if (isset($_POST[$variable_name]))
    {
        return $_POST[$variable_name];
    }
    else
    {
        return "";
    }
}

/******************************************************************************
 * SubmittedData_IsVariableDefined
 *****************************************************************************/
function SubmittedData_IsVariableDefined($variable_name)
{
    if (isset($_GET[$variable_name]) || 
        isset($_POST[$variable_name]))
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}

?>

