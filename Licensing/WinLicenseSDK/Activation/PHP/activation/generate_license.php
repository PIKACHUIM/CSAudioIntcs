<?php

include_once "main_settings.php";
include_once "classes/license.php";
include_once "key_generation/generate_key.php";

/******************************************************************************
 * GenerateLicenseFromActivation
 *****************************************************************************/
function GenerateLicenseFromActivation($conn, $activation_key, $hardware_id)
{
    $args = GetLicenseGenerationArgsForActivation($conn, $activation_key, $hardware_id);
    $args .= " -x 1";   // Winlicense protected applications expect the license in Hex string

    $return_value = "";                                         
    $generated_license = GenerateLicense_ExecuteKeyGenerator($args, $return_value);

    // display generated key to output
    print("<br>" . $generated_license);

    // display exec exit code
    print("<br><br> PHP 'exec' exit code: " . $return_value);
}


/******************************************************************************
 * GetLicenseGenerationArgsForActivation
 *****************************************************************************/
function GetLicenseGenerationArgsForActivation($conn, $activation_key, $hardware_id)
{
    $license_settings = new License;
    $license_settings->license_type = LICENSE_TYPE_FILE;
    $license_settings->insert_start_end_markers = "yes";

    // retrieve license details for activation
    $license_hash = DataAccess_Product_GetLicenseUniqueKeyFromActivation($conn, 
                                                                    $activation_key);
    $user_name = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_USER_NAME);
    $company = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_COMPANY);
    $license_settings->custom_data = DataAccess_Registration_GetValueInMarkersFromActivation($conn, 
                                            $activation_key, 
                                            REGINFO_CUSTOM_DATA_MARKER_START,
                                            REGINFO_CUSTOM_DATA_MARKER_END);
    $license_settings->expiration_days = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_DAYS_EXPIRATION);
    $license_settings->expiration_date = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_DATE_EXPIRATION);
    $license_settings->expiration_executions = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_EXECUTIONS);
    $license_settings->expiration_runtime = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_RUNTIME_EXECUTION);
    $license_settings->expiration_global_time = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_GLOBAL_TIME);
    $license_settings->install_before_date = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_INSTALL_BEFORE_DATE);
    $license_settings->network_instances = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_NETWORK_INSTANCES);
    $license_settings->is_unicode = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_IS_UNICODE);
    $license_settings->country_lock = GetStringBetween(DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                            $activation_key, REGINFO_LOCKED_COUNTRY), '(', ')');
    $license_settings->hardware_id = $hardware_id;
    
    return GenerateLicense_GenerateArguments($license_hash, $user_name, 
                                             $company, $license_settings);
}


?>

