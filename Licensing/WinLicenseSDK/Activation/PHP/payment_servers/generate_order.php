<?php

/******************************************************************************
;                                Includes
;*****************************************************************************/
include_once "payment_servers/default_license_settings.php";
include_once "payment_servers/parse_submitted_data.php";


/******************************************************************************
 * GenerateOrder
 *****************************************************************************/

function GenerateOrder()
{
    SubmittedData_CheckRequiredVariables();
    return GenerateOrderInDatabase();
}

/******************************************************************************
 * GenerateOrderInDatabase
 *****************************************************************************/
function GenerateOrderInDatabase()
{
    global $servername;
    global $password;
    global $dbname;
    global $username;
    
    // --------------------------------------------
    // Get payment information from URL
    // --------------------------------------------
    $customer = new Customer;
    SubmittedData_GetCustomerInfo($customer);

    $product_name = GetInternalProductNameFromVendorId(SubmittedData_GetVariableValue(VENDOR_PRODUCT_ID));
    $product_quantity = SubmittedData_GetVariableValue(PRODUCT_QUANTITY);    
    $payment_method = SubmittedData_GetVariableValue(PAYMENT_METHOD); 
    $order_notes = SubmittedData_GetVariableValue(PAYMENT_NOTES); 

    // --------------------------------------------
    // Create customer in database if not found
    // --------------------------------------------
    $conn = OpenDatabase($servername, $username, $password, $dbname);

    if (!DataAccess_Customer_IsExisting($conn, $customer))
    {
        DataAccess_Customer_Add($conn, $customer);
    }

    // --------------------------------------------
    // Create current order into our database
    // --------------------------------------------
    $customer_id = DataAccess_Customer_GetId($conn, $customer);
    $product_id = DataAccess_Product_GetId($conn, $product_name);

    $license = new License;
    DataAccess_Product_InitializeLicenseFromProductValues($conn, $product_id, $license);
    SetDefaultLicenseSettings($license);
    
    $generated_license = DataAccess_Order_Add($conn, $customer_id, $product_id, 
                         $product_quantity, $payment_method, $order_notes, $license);
    CloseDatabase($conn);

    return $generated_license;
}


?>

