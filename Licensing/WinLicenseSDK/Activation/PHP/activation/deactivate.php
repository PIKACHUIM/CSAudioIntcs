<?php

/******************************************************************************
 * DoRevokeDevice
 *****************************************************************************/
function DoRevokeDevice($conn, $activation_key, $hardware_id)
{
    DataAccess_ActivationDevice_SetRevoked($conn, $activation_key, $hardware_id);
    DataAccess_Activation_IncreaseDeactivatedTimes($conn, $activation_key);
}

/******************************************************************************
 * CheckRevokeAllowed
 *****************************************************************************/
function CheckRevokeAllowed($conn, $activation_key, $hardware_id)
{
    // Check if activation key to revoke exists
    $status_activation_key = CheckActivationKey($conn, $activation_key);

    if ($status_activation_key != ACTIVATION_OK)
    {
        return $status_activation_key;
    }

    // check if maximum number of deactivations is reached
    $status_max_deactivations = CheckDeactivationLimit($conn, $activation_key);

    if ($status_max_deactivations != ACTIVATION_OK)
    {
        return $status_max_deactivations;
    }

    // check that device is already activated
    if (!DataAccess_ActivationDevice_IsDeviceActivated($conn, $activation_key, $hardware_id))
    {
        return ACTIVATION_ERROR_DEVICE_NOT_FOUND;
    }
    return ACTIVATION_OK;
}

/******************************************************************************
 * CheckDeactivationLimit
 *****************************************************************************/
function CheckDeactivationLimit($conn, $activation_key)
{   
    // check if we have unlimited deactivations to continue checking limits or not
    $max_deactivations = DataAccess_Registration_GetKeyValueFromActivation($conn, 
                            $activation_key, REGINFO_ACTIVATION_MAX_DEACTIVATIONS);

    if ($max_deactivations == ACTIVATION_UNLIMITED_VALUE)
    {
        return ACTIVATION_OK;
    }

    // final check for deactivated times
    $current_deactivations = DataAccess_Activation_GetCurrentDeactivatedTimes($conn, 
                                                                    $activation_key);
    if ($current_deactivations >= $max_deactivations)
    {
        return ACTIVATION_ERROR_NO_MORE_DEACTIVATIONS_ALLOWED;
    }
    else
    {
        return ACTIVATION_OK;
    }
}

?>
