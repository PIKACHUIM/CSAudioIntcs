<?php

/******************************************************************************
 * DataAccess_Registration_GetKeyValueFromActivation
 *****************************************************************************/
function DataAccess_Registration_GetKeyValueFromActivation($conn, $activation_key, $key_name) 
{
    $registration_info = DataAccess_Activation_GetRegistrationInfo($conn, $activation_key);
    $key_value = GetKeyValueInUTF16($registration_info, str_replace(' ', '_', $key_name));
  
    return preg_replace("/\r\n|\r|\n/", '', $key_value);
}

/******************************************************************************
 * DataAccess_Registration_GetValueInMarkersFromActivation
 *****************************************************************************/
function DataAccess_Registration_GetValueInMarkersFromActivation($conn, $activation_key, 
                                                             $marker_start, $marker_end) 
{
    $registration_info = DataAccess_Activation_GetRegistrationInfo($conn, $activation_key);
    $value = GetValueBetweenMarkersUTF16($registration_info, $marker_start, $marker_end);

    return $value;
}

?>

