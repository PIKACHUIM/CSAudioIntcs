<?php

/******************************************************************************
 * DoActivation
 *****************************************************************************/
function DoActivation($conn, $activation_key, $hardware_id)
{
    DataAccess_ActivationDevice_Add($conn, $activation_key, $hardware_id);
    DataAccess_Activation_IncreaseActivatedTimes($conn, $activation_key);
}

/******************************************************************************
 * CheckActivationAllowed
 *****************************************************************************/
function CheckActivationAllowed($conn, $activation_key, $hardware_id)
{
    // Check activation key is found and active
    $status_activation_key = CheckActivationKey($conn, $activation_key);

    if ($status_activation_key != ACTIVATION_OK)
    {
        return $status_activation_key;
    }

    // Check that we have not reached the maximum number of activations for 
    // an "Activation key"
    $status_max_activations = CheckActivationLimit($conn, $activation_key);

    if ($status_max_activations != ACTIVATION_OK)
    {
        return $status_max_activations;
    }

    // check that we have not reached the maximun number of simultaneous devices
    $status_simulatenous_devices = CheckSimultaneousDevicesLimit($conn, 
                                                $activation_key, $hardware_id);

    if ($status_simulatenous_devices != ACTIVATION_OK)
    {
        return $status_simulatenous_devices;
    }

    // check for different devices allowed
    $status_different_devices = CheckDifferentDevicesReached($conn, $activation_key, 
                                                                $hardware_id);
    if ($status_different_devices != ACTIVATION_OK)
    {
        return $status_different_devices;
    }

    // check that we are activating within the given "installation date"
    $status_installation_date = CheckInstallationDate($conn, $activation_key);

    if ($status_installation_date != ACTIVATION_OK)
    {
        return $status_installation_date;
    }

    // return OK if all checks are passed
    return ACTIVATION_OK;
}


/******************************************************************************
 * CheckActivationLimit
 *****************************************************************************/
function CheckActivationLimit($conn, $activation_key)
{   
    $max_activations = DataAccess_Registration_GetKeyValueFromActivation($conn, $activation_key, 
                                                   REGINFO_ACTIVATION_MAX_ACTIVATIONS);

    if ($max_activations == ACTIVATION_UNLIMITED_VALUE)
    {
        return ACTIVATION_OK;
    }

    $current_activations = DataAccess_Activation_GetCurrentActivatedTimes($conn, 
                                                                        $activation_key);

    if ($current_activations >= $max_activations)
    {
        return ACTIVATION_ERROR_NO_MORE_ACTIVATIONS_ALLOWED;
    }
    else
    {
        return ACTIVATION_OK;
    }
}

/******************************************************************************
 * CheckSimultaneousDevicesLimit
 *****************************************************************************/
function CheckSimultaneousDevicesLimit($conn, $activation_key,  $hardware_id)
{
    // check if we are unlimited
    $max_simultaneous = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                                    $activation_key, REGINFO_ACTIVATION_SIMULTANEOUS);
    if ($max_simultaneous == ACTIVATION_UNLIMITED_VALUE)
    {
        return ACTIVATION_OK;
    }

    // if device is already activated and not revoked, then we just allow it
    if (DataAccess_ActivationDevice_IsDeviceActivated($conn, $activation_key,  $hardware_id) &&
        !DataAccess_ActivationDevice_IsRevoked($conn, $activation_key,  $hardware_id))
    {
        return ACTIVATION_OK;
    }

    // check current number of activated devices are reached or not
    $current_activated = DataAccess_ActivationDevice_GetNumberActivated($conn, $activation_key);

    if ($current_activated >= $max_simultaneous)
    {
        return ACTIVATION_ERROR_MAX_DEVICES_REACHED;
    }
    else
    {
        return ACTIVATION_OK;
    }
}

/******************************************************************************
 * CheckInstallationDate
 *****************************************************************************/
function CheckInstallationDate($conn, $activation_key)
{
    $expiration_date = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                             $activation_key, REGINFO_ACTIVATION_EXPIRATION_DATE);

    // check if "No/no" string is in Date field, so we proceed comparing dates or not
    if (($expiration_date[0] != 'N') && ($expiration_date[0] != 'n'))
    {
        $expiration_date = strtotime($expiration_date);

        if (time() > $expiration_date)
        {
            return ACTIVATION_ERROR_KEY_EXPIRED;
        }
    }
    return ACTIVATION_OK;
}

/******************************************************************************
 * CheckDifferentDevicesReached
 *****************************************************************************/
function CheckDifferentDevicesReached($conn, $activation_key,  $hardware_id)
{
    // if device is already activated and not revoked, then we just allow it
    if (DataAccess_ActivationDevice_IsDeviceActivated($conn, $activation_key,  $hardware_id))
    {
        return ACTIVATION_OK;
    }

    // check if we are unlimited. In that case any different device is accepted
    $max_different = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                        $activation_key, REGINFO_ACTIVATION_DIFFERENT_DEVICES_ALLOWED);
    if ($max_different == ACTIVATION_UNLIMITED_VALUE)
    {
        return ACTIVATION_OK;
    }

    // check number of current activated devices
    $num_devices = DataAccess_ActivationDevice_GetNumber($conn, $activation_key);

    if ($num_devices >= $max_different)
    {
        return ACTIVATION_ERROR_NO_MORE_DIFFERENT_DEVICES_ALLOWED;
    }
    else
    {
        return ACTIVATION_OK;
    }
}

?>

