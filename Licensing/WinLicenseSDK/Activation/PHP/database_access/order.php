<?php


/******************************************************************************
 * DataAccess_Order_Add
 *****************************************************************************/
function DataAccess_Order_Add($conn, $customer_id, $product_id, $product_quantity,
                              $payment_method, $order_notes, $license)                               
{
    $registration_name = DataAccess_Customer_GetFirstName($conn, $customer_id) . 
                         " " . DataAccess_Customer_GetLastName($conn, $customer_id);
    $shipping_address = DataAccess_Customer_GetAddress($conn, $customer_id);
    $shipping_city = DataAccess_Customer_GetCity($conn, $customer_id);
    $shipping_zip = DataAccess_Customer_GetZip($conn, $customer_id);
    $shipping_state = DataAccess_Customer_GetState($conn, $customer_id);
    $shipping_country = DataAccess_Customer_GetCountry($conn, $customer_id);
    
    $shipping_fee = DataAccess_Product_GetShippingFee($conn, $product_id);
    $service_fee = DataAccess_Product_GetServiceFee($conn, $product_id);
    $unit_price = DataAccess_Product_GetPrice($conn, $product_id);
    $total = $unit_price * $product_quantity;

    $registration_info = DataAccess_Order_CreateRegistrationInfo($conn, $customer_id, 
                                                            $product_id, $license);

    $registration_info = mb_convert_encoding($registration_info, "UTF-16LE", "UTF-8");

    $generated_license = "";

    $generated_license = GetValueBetweenMarkersUTF16($registration_info, 
                                           REGINFO_LICENSE_DATA_MARKER_START,
                                           REGINFO_LICENSE_DATA_MARKER_END);

    // normalize order subscription expire date
    $subscription_days = intval($license->order_subscription_days);

    if ($subscription_days == 0)
    {
        $subscription_days = SUBCRIPTION_100_YEARS;  
    }

    $registration_info =  mb_convert_encoding($registration_info, "UTF-8", "UTF-16LE");
    
    $result = mysqli_query($conn, "INSERT INTO Orders (Order_Date, Order_ExpireDate, 
                                             Order_MarkedAsExpired,  
                                             Order_RegistrationName, 
                                             Order_ShippingAddress,
                                             Order_ShippingCity, 
                                             Order_ShippingZip, 
                                             Order_ShippingState, 
                                             Order_ShippingCountry,
                                             ID_Product, 
                                             Order_UnitPrice, 
                                             Order_ShippingFee,
                                             Order_ServiceFee, 
                                             Order_Quantity,
                                             Order_Total, 
                                             Order_PaymentMethod,
                                             Order_RegistrationContent,
                                             ID_Customer, 
                                             Order_Notes
                                             )
                                     VALUES (NOW(), 
                                             DATE_ADD(NOW(), INTERVAL '$subscription_days' DAY), 
                                             false, 
                                             '$registration_name', 
                                             '$shipping_address', 
                                             '$shipping_city',
                                             '$shipping_zip',
                                             '$shipping_state',
                                             '$shipping_country',
                                             '$product_id',
                                             '$unit_price',
                                             '$shipping_fee',
                                             '$service_fee',
                                             '$product_quantity',
                                             '$total',
                                             '$payment_method',
                                             '$registration_info',
                                             '$customer_id',
                                             '$order_notes'
                                             )");
/*
    // We have to change to "latin1" so the "$registration_info" (TEXT field) preserves
    // the BOM at start and can be read by "WinLicense Orders Manager"
    $last_inserted_order = mysqli_insert_id($conn);
    mysqli_set_charset($conn, "latin1");
    $result = mysqli_query($conn, "UPDATE Orders 
                                   SET    Order_RegistrationContent = '$registration_info'                                   
                                   WHERE  ID_Order = '$last_inserted_order'");
    mysqli_set_charset($conn, "utf8");
*/
    if ($license->license_type == LICENSE_TYPE_ACTIVATION)
    {
        DataAccess_Activation_AddFromRegistrationInfo($conn, $generated_license,    
                                                      mysqli_insert_id($conn));
    }
    return $generated_license;
}

/******************************************************************************
 * DataAccess_Order_CreateRegistrationInfo
 *****************************************************************************/
function DataAccess_Order_CreateRegistrationInfo($conn, $customer_id, 
                                                 $product_id, $license)

{
    $registration_info = RegistrationInfo_CreateUserDetails($conn,
                                                            $customer_id, 
                                                            $license);
    $registration_info .= RegistrationInfo_CreateLicenseRestrictions($license);
    $registration_info .= RegistrationInfo_CreateMiscellaneous($license);
    
    // Activation specific entries
    if ($license->license_type == LICENSE_TYPE_ACTIVATION)
    {
        $registration_info .= RegistrationInfo_CreateActivation($license);
    }
    else
    {
        $registration_info .= RegistrationInfo_CreateGeneratedLicenseInfo($license);

        $registration_info .= RegistrationInfo_CreateLicenseContent($conn, 
                                                                    $customer_id, 
                                                                    $product_id, 
                                                                    $license);
    }
    return $registration_info;
}

/******************************************************************************
 * RegistrationInfo_CreateUserDetails
 *****************************************************************************/
function RegistrationInfo_CreateUserDetails($conn, $customer_id, $license)
{
    $registration_info = "License User Details\n";
    $registration_info .= "--------------------\n";

    $registration_info .= REGINFO_USER_NAME . '=' . 
                          DataAccess_Customer_GetFirstName($conn, $customer_id) . ' ' .
                          DataAccess_Customer_GetLastName ($conn, $customer_id) . "\n";

    $customer_organization = DataAccess_Customer_GetOrganization($conn, $customer_id);
    if (!empty($customer_organization))
    {
        $registration_info .= REGINFO_COMPANY . '=' .
                              DataAccess_Customer_GetOrganization($conn, $customer_id) . "\n";
    }

    if (!empty($license->hardware_id))
    {
        $registration_info .= REGINFO_HARDWARE_ID . '=' . $license->hardware_id . "\n";
    }

    if (!empty($license->custom_data))
    {
        $registration_info .= REGINFO_CUSTOM_DATA . '=<custom_start>' . 
                              $license->custom_data . "<custom_end>\n";
    }
    return $registration_info;
}

/******************************************************************************
 * RegistrationInfo_CreateLicenseRestrictions
 *****************************************************************************/
function RegistrationInfo_CreateLicenseRestrictions($license)
{
    $restriction_days = $license->expiration_days;
    $restriction_executions = $license->expiration_executions;
    $restriction_date = $license->expiration_date;
    $restriction_runtime = $license->expiration_runtime;
    $restriction_globaltime = $license->expiration_global_time;
    $restriction_installbefore = $license->install_before_date;

    // check if no restrictions are set
    if (empty($restriction_days) && empty($restriction_executions) && 
        empty($restriction_date) && empty($restriction_runtime) && 
        empty($restriction_globaltime) && empty($restriction_installbefore))
    {
        return "";
    }

    $registration_info = "\nLicense Restrictions\n";
    $registration_info .= "--------------------\n";

    if (!empty($restriction_days))
    {
        $registration_info .= REGINFO_DAYS_EXPIRATION . '=' . 
                              $restriction_days . "\n";
    }

    if (!empty($restriction_executions))
    {
        $registration_info .= REGINFO_EXECUTIONS . '=' . 
                              $restriction_executions . "\n";
    }

    if (!empty($restriction_date))
    {
        $registration_info .= REGINFO_DATE_EXPIRATION . '=' . 
                              $restriction_date . "\n";
    }

    if (!empty($restriction_runtime))
    {
        $registration_info .= REGINFO_RUNTIME_EXECUTION . '=' . 
                              $restriction_runtime . "\n";
    }

    if (!empty($restriction_globaltime))
    {
        $registration_info .= REGINFO_GLOBAL_TIME . '=' . 
                              $restriction_globaltime . "\n";
    }

    if (!empty($restriction_installbefore))
    {
        $registration_info .= REGINFO_INSTALL_BEFORE_DATE . '=' . 
                              $restriction_installbefore . "\n";
    }
    return $registration_info;
}


/******************************************************************************
 * RegistrationInfo_CreateMiscellaneous
 *****************************************************************************/
function RegistrationInfo_CreateMiscellaneous($license)
{
    $restriction_countrylock = $license->country_lock;
    $restriction_netinstances = $license->network_instances;  
    $store_creation_date = $license->store_creation_date;
    $embed_user_info = $license->embed_user_info; 
    $is_unicode = $license->is_unicode;

    // check if no miscellaneous information
    if (empty($restriction_netinstances) && empty($store_creation_date) && 
        empty($embed_user_info) && empty($is_unicode) &&
        empty($restriction_countrylock))
    {
        return "";
    }

    $registration_info = "\nMiscellaneous\n";
    $registration_info .= "-------------\n";

    if (!empty($restriction_netinstances))
    {
        $registration_info .= REGINFO_NETWORK_INSTANCES . '=' . 
                              $restriction_netinstances . "\n";
    }

    if (!empty($store_creation_date))
    {
        $registration_info .= REGINFO_STORE_CREATION_DATE . '=' . 
                              $store_creation_date . "\n";
    }

    if (!empty($embed_user_info))
    {
        $registration_info .= REGINFO_EMBED_USER_INFO . '=' . 
                              $embed_user_info . "\n";
    }

    if (!empty($is_unicode))
    {
        $registration_info .= REGINFO_IS_UNICODE . '=' . $is_unicode . "\n";
    }

    if (!empty($restriction_countrylock))
    {
        $registration_info .= REGINFO_LOCKED_COUNTRY . '=' . 
                              $restriction_countrylock . "\n";
    }
    return $registration_info;
}

/******************************************************************************
 * RegistrationInfo_CreateActivation
 *****************************************************************************/
function RegistrationInfo_CreateActivation($license)
{

    $simulateneous_devices = $license->activation_simultaneous_devices;
    $deactivation_limit = $license->activation_deactivation_limit;
    $activation_limit = $license->activation_activation_limit;
    $max_different_devices = $license->activation_max_different_devices;
    $expiration_date = $license->activation_expiration_date;
    $key_format = $license->activation_key_format;

    $registration_info = "\nActivation Information\n";

    if (!empty($simulateneous_devices))
    {
        $registration_info .= REGINFO_ACTIVATION_SIMULTANEOUS . '=' . 
                              $simulateneous_devices . "\n";
    }

    if (!empty($deactivation_limit))
    {
        $registration_info .= REGINFO_ACTIVATION_MAX_DEACTIVATIONS . '=' . 
                              $deactivation_limit . "\n";
    }

    if (!empty($activation_limit))
    {
        $registration_info .= REGINFO_ACTIVATION_MAX_ACTIVATIONS . '=' . 
                              $activation_limit . "\n";
    }

    if (!empty($max_different_devices))
    {
        $registration_info .= REGINFO_ACTIVATION_MAX_DIFFERENT_DEVICES . '=' . 
                              $max_different_devices . "\n";
    }

    if (!empty($expiration_date))
    {
        $registration_info .= REGINFO_ACTIVATION_EXPIRATION_DATE . '=' .    
                              $expiration_date . "\n";
    }

    if (!empty($key_format))
    {
        $registration_info .= REGINFO_ACTIVATION_FORMAT . '=' . 
                              $key_format . "\n\n";
    }

    $registration_info .= REGINFO_ACTIVATION_KEY . '=' . REGINFO_LICENSE_DATA_MARKER_START .
                          GenerateActivationKey($key_format) . REGINFO_LICENSE_DATA_MARKER_END . "\n";

    return $registration_info;
}

/******************************************************************************
 * RegistrationInfo_RegistrationTypeToString
 *****************************************************************************/
function RegistrationInfo_RegistrationTypeToString($registration_type)
{
    $license_type_str = "";
    
    if ($registration_type == LICENSE_TYPE_FILE)
    {
        $license_type_str = 'File Key';
    }
    else if ($registration_type == LICENSE_TYPE_REGISTRY)
    {
        $license_type_str = 'Registry Key';
    }
    else if ($registration_type == LICENSE_TYPE_TEXT)
    {
        $license_type_str = 'Text Key';
    }
    else if ($registration_type == LICENSE_TYPE_STATIC_SMARTKEY)
    {
        $license_type_str = 'Static SmartKey';
    }
    else if ($registration_type == LICENSE_TYPE_DYNAMIC_SMARTKEY)
    {
        $license_type_str = 'Dynamic SmartKey';
    }
    else if ($registration_type == LICENSE_TYPE_ACTIVATION)
    {
        $license_type_str = 'Activation';
    }
    return $license_type_str;
}

/******************************************************************************
 * RegistrationInfo_CreateGeneratedLicenseInfo
 *****************************************************************************/
function RegistrationInfo_CreateGeneratedLicenseInfo($license)
{
    $registration_type = $license->license_type;
    
    $registration_info = "";
    $license_type_str = RegistrationInfo_RegistrationTypeToString($registration_type);
        
    if ($registration_type != LICENSE_TYPE_ACTIVATION)
    {
        $registration_info .= "\nGenerated License (" . strtoupper($license_type_str) . ")\n";
        $registration_info .= "-----------------\n";
    }

    if ($registration_type == LICENSE_TYPE_FILE)
    {
        $registration_info .= REGINFO_LICENSE_FORMAT . "=Binary\n";
    }
    else
    {
        $registration_info .= REGINFO_LICENSE_FORMAT . "=Text\n";
    }
    return $registration_info;
}

/******************************************************************************
 * RegistrationInfo_CreateLicenseContent
 *****************************************************************************/
function RegistrationInfo_CreateLicenseContent($conn, $customer_id, 
                                               $product_id, $license)
{
    $user_name = DataAccess_Customer_GetFirstName($conn, $customer_id) 
                        . ' ' .  DataAccess_Customer_GetLastName ($conn, $customer_id);
    $user_company = DataAccess_Customer_GetOrganization($conn, $customer_id);
    $license_hash = DataAccess_Product_GetLicenseUniqueKey($conn, $product_id);
    $license_data = GenerateLicense_ForType($license_hash, $user_name, 
                                            $user_company, $license);

    return REGINFO_LICENSE_DATA . "=" . REGINFO_LICENSE_DATA_MARKER_START . 
           $license_data . REGINFO_LICENSE_DATA_MARKER_END;            
}


?>

