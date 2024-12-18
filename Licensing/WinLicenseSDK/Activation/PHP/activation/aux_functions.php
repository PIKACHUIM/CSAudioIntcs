<?php

/******************************************************************************
 * CheckActivationKey
 *****************************************************************************/
function CheckActivationKey($conn, $activation_key)
{
    // check if activation code is in database
    if (!DataAccess_Activation_IsKeyInDatabase($conn, $activation_key))
    {
        return ACTIVATION_ERROR_KEY_NOT_FOUND;
    }

    // check if activation key has been disabled by the seller
    if (DataAccess_Activation_IsDisabled($conn, $activation_key))
    {
        return ACTIVATION_ERROR_KEY_DISABLED_BY_SELLER;
    }
    return ACTIVATION_OK;
}

/******************************************************************************
 * ActivationErrorCodeToText
 *****************************************************************************/
function ActivationErrorCodeToText($error_code)
{
    if ($error_code == ACTIVATION_OK)
    {
        return "ACTIVATION_OK";
    }
    else if ($error_code == ACTIVATION_ERROR_KEY_NOT_FOUND)
    {
        return "ACTIVATION_ERROR_KEY_NOT_FOUND";
    }
    else if ($error_code == ACTIVATION_ERROR_DEVICE_REVOKED)
    {
        return "ACTIVATION_ERROR_DEVICE_REVOKED";
    }
    else if ($error_code == ACTIVATION_ERROR_MAX_DEVICES_REACHED)
    {
        return "ACTIVATION_ERROR_MAX_DEVICES_REACHED";
    }
    else if ($error_code == ACTIVATION_ERROR_NO_MORE_ACTIVATIONS_ALLOWED)
    {
        return "ACTIVATION_ERROR_NO_MORE_ACTIVATIONS_ALLOWED";
    }
    else if ($error_code == ACTIVATION_ERROR_NO_MORE_DEACTIVATIONS_ALLOWED)
    {
        return "ACTIVATION_ERROR_NO_MORE_DEACTIVATIONS_ALLOWED";
    }
    else if ($error_code == ACTIVATION_ERROR_DEVICE_NOT_FOUND)
    {
        return "ACTIVATION_ERROR_DEVICE_NOT_FOUND";
    }        
    else if ($error_code == ACTIVATION_ERROR_KEY_DISABLED_BY_SELLER)
    {
        return "ACTIVATION_ERROR_KEY_DISABLED_BY_SELLER";
    }         
    else if ($error_code == ACTIVATION_ERROR_NO_MORE_DIFFERENT_DEVICES_ALLOWED)
    {
        return "ACTIVATION_ERROR_NO_MORE_DIFFERENT_DEVICES_ALLOWED";
    }         
    else if ($error_code == ACTIVATION_ERROR_KEY_EXPIRED)
    {
        return "ACTIVATION_ERROR_KEY_EXPIRED";
    }         
}


?>

