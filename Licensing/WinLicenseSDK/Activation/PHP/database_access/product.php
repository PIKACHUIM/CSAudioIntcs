<?php

/******************************************************************************
 * DataAccess_Product_GetCustomKeyValueFromActivation
 *****************************************************************************/
function DataAccess_Product_GetCustomKeyValueFromActivation($conn, $activation_key, $custom_key)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $result = mysqli_query($conn, "SELECT Activation_ID_Order 
                                   FROM   Activations 
                                   WHERE  ID_Activation = '$activation_id'");   
    $row = $result->fetch_assoc();  
    $order_id = $row["Activation_ID_Order"];

    $result = mysqli_query($conn, "SELECT ID_Product 
                                   FROM   Orders 
                                   WHERE  ID_Order = '$order_id'");
    $row = $result->fetch_assoc();  
    $product_id = $row["ID_Product"];

    $result = mysqli_query($conn, "SELECT Product_CustomInfo 
                                   FROM   Products 
                                   WHERE  ID_Product = '$product_id'");
    $row = $result->fetch_assoc();  
    $product_info = $row["Product_CustomInfo"];
    return GetKeyValueInUTF8($product_info, $custom_key);
}

/******************************************************************************
 * DataAccess_Product_GetCustomKeyValue
 *****************************************************************************/
function DataAccess_Product_GetCustomKeyValue($conn, $product_id, $custom_key)
{
    $result = mysqli_query($conn, "SELECT Product_CustomInfo 
                                   FROM   Products 
                                   WHERE  ID_Product = '$product_id'");
    $row = $result->fetch_assoc();  
    $product_info = $row["Product_CustomInfo"];
    return GetKeyValueInUTF8($product_info, $custom_key);
}


/******************************************************************************
 * DataAccess_Product_GetLicenseUniqueKeyFromActivation
 *****************************************************************************/
function DataAccess_Product_GetLicenseUniqueKeyFromActivation($conn, $activation_key) 
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $result = mysqli_query($conn, "SELECT Activation_ID_Order 
                                   FROM   Activations 
                                   WHERE  ID_Activation = '$activation_id'");   
    $row = $result->fetch_assoc();  
    $order_id = $row["Activation_ID_Order"];

    $result = mysqli_query($conn, "SELECT ID_Product 
                                   FROM   Orders 
                                   WHERE  ID_Order = '$order_id'");
    $row = $result->fetch_assoc();  
    $product_id = $row["ID_Product"];

    $result = mysqli_query($conn, "SELECT Product_LicenseHash 
                                   FROM   Products 
                                   WHERE  ID_Product = '$product_id'");
    $row = $result->fetch_assoc();  
    $product_license_hash = $row["Product_LicenseHash"];

    return $product_license_hash;
}

/******************************************************************************
 * DataAccess_Product_GetLicenseUniqueKey
 *****************************************************************************/
function DataAccess_Product_GetLicenseUniqueKey($conn, $product_id)
{
    $result = mysqli_query($conn, "SELECT Product_LicenseHash 
                                   FROM   Products 
                                   WHERE  ID_Product = '$product_id'");
    $row = $result->fetch_assoc();  
    $product_license_hash = $row["Product_LicenseHash"];

    return $product_license_hash;
}

/******************************************************************************
 * DataAccess_Product_GetHardwareIdMask
 *****************************************************************************/
function DataAccess_Product_GetHardwareIdMask($conn, $activation_key) 
{
    return DataAccess_Product_GetCustomKeyValueFromActivation($conn, $activation_key, 
                                                              PRODUCTINFO_HARDWARE_ID_MASK);
}

/******************************************************************************
 * DataAccess_Product_IsMatchingCustomKeyValueWithValue
 *****************************************************************************/
function DataAccess_Product_IsMatchingCustomKeyValueWithValue($conn, $activation_key, 
                                                      $custom_key, $value_to_compare) 
{
    if (DataAccess_Product_GetCustomKeyValueFromActivation($conn, $activation_key, $custom_key) == $value_to_compare)
    {
        return true;
    }
    else
    {
        return false;
    }
}

/******************************************************************************
 * DataAccess_Product_GetId
 *****************************************************************************/
function DataAccess_Product_GetId($conn, $product_name)
{
    $result = mysqli_query($conn, "SELECT ID_Product 
                                   FROM   Products 
                                   WHERE  Product_Name  = '$product_name'");
    $row = $result->fetch_assoc();  
    return $row["ID_Product"];  
}

/******************************************************************************
 * DataAccess_Product_GetField
 *****************************************************************************/
function DataAccess_Product_GetField($conn, $product_id, $field_name)
{
    $result = mysqli_query($conn, "SELECT *
                                   FROM   Products 
                                   WHERE  ID_Product = '$product_id'");
    $row = $result->fetch_assoc();  
    return $row[$field_name];  
}

/******************************************************************************
 * DataAccess_Product_GetName
 *****************************************************************************/
function DataAccess_Product_GetName($conn, $product_id)
{
    return DataAccess_Product_GetField($conn, $product_id, "Product_Name");
}

/******************************************************************************
 * DataAccess_Product_GetPrice
 *****************************************************************************/
function DataAccess_Product_GetPrice($conn, $product_id)
{
    return DataAccess_Product_GetField($conn, $product_id, "Product_UnitPrice");
}

/******************************************************************************
 * DataAccess_Product_GetShippingFee
 *****************************************************************************/
function DataAccess_Product_GetShippingFee($conn, $product_id)
{
    return DataAccess_Product_GetField($conn, $product_id, "Product_ShippingFee");
}

/******************************************************************************
 * DataAccess_Product_GetServiceFee
 *****************************************************************************/
function DataAccess_Product_GetServiceFee($conn, $product_id)
{
    return DataAccess_Product_GetField($conn, $product_id, "Product_ServiceFee");
}

/******************************************************************************
 * DataAccess_Product_InitializeLicenseFromProductValues
 *****************************************************************************/
function DataAccess_Product_InitializeLicenseFromProductValues($conn, $product_id, License $license)
{
    $custom_data = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_CUSTOM_DATA)); 
    $expiration_days = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_DAYS));
    $expiration_executions = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_EXEC));
    $expiration_date = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_DATE));
    $expiration_runtime = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_RUNTIME));
    $expiration_global_time = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_GLOBAL_TIME));
    $country_lock = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_COUNTRY_LOCKING));
    $store_creation = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_STORE_CREATION));
    $network_instances = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_NETWORK_INSTANCES));
    $embed_user_info = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_EMBED_USER));
    $is_unicode = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_IS_UNICODE));
    $insert_start_end_markers = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_LICENSE_INSERT_START_END_MARKERS));
    $activation_simultaneous_devices = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_ACTIVATION_SIMULTANEOUS_DEVICES));
    $activation_deactivation_limit = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_ACTIVATION_DEACTIVATION_LIMIT));
    $activation_activation_limit = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_ACTIVATION_ACTIVATION_LIMIT));
    $activation_max_different_devices = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_ACTIVATION_MAX_DIFFERENT_DEVICES));
    $activation_expiration_date = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_ACTIVATION_EXPIRATION_DATE));
    $activation_key_format = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_ACTIVATION_FORMAT));

    $order_subscription_days = trim(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                        PRODUCTINFO_DEFAULT_ORDER_SUBSCRIPTION_DAYS));
    $license_type = intval(DataAccess_Product_GetCustomKeyValue($conn, $product_id,
                                                                  PRODUCTINFO_DEFAULT_LICENSE_TYPE));

    // assign values to license if they have been defined
    if (!empty($custom_data))
    {
        $license->custom_data = $custom_data;
    }

    if (!empty($expiration_days))
    {
        $license->expiration_days = $expiration_days;
    }

    if (!empty($expiration_executions))
    {
        $license->expiration_executions = $expiration_executions;
    }

    if (!empty($expiration_date))
    {
        $license->expiration_date = $expiration_date;
    }

    if (!empty($expiration_runtime))
    {
        $license->expiration_runtime = $expiration_runtime;
    }

    if (!empty($expiration_global_time))
    {
        $license->expiration_global_time = $expiration_global_time;
    }

    if (!empty($country_lock))
    {
        $license->country_lock = $country_lock;
    }

    if (!empty($store_creation))
    {
        $license->store_creation = $store_creation;
    }

    if (!empty($network_instances))
    {
        $license->network_instances = $network_instances;
    }

    if (!empty($embed_user_info))
    {
        $license->embed_user_info = $embed_user_info;
    }

    if (!empty($is_unicode))
    {
        $license->is_unicode = $is_unicode;
    }

    if (!empty($activation_simultaneous_devices))
    {
        $license->activation_simultaneous_devices = $activation_simultaneous_devices;
    }

    if (!empty($activation_deactivation_limit))
    {
        $license->activation_deactivation_limit = $activation_deactivation_limit;
    }

    if (!empty($activation_activation_limit))
    {
        $license->activation_activation_limit = $activation_activation_limit;
    }

    if (!empty($activation_max_different_devices))
    {
        $license->activation_max_different_devices = $activation_max_different_devices;
    }

    if (!empty($activation_expiration_date))
    {
        $license->activation_expiration_date = $activation_expiration_date;
    }

    if (!empty($activation_key_format))
    {
        $license->activation_key_format = $activation_key_format;
    }
    
    if (!empty($order_subscription_days))
    {
        $license->order_subscription_days = $order_subscription_days;
    }

    if (!empty($license_type))
    {
        $license->license_type = $license_type;
    }
}

?>

