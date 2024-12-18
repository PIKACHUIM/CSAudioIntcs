<?php

/******************************************************************************
 * DataAccess_ActivationDevice_GetNumberActivated
 *****************************************************************************/
function DataAccess_ActivationDevice_GetNumberActivated($conn, $activation_key) 
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $result = mysqli_query($conn, "SELECT ID_ActivationDevice 
                                   FROM   ActivationDevice 
                                   WHERE (
                                          (ID_Activation = '$activation_id') AND
                                          (ActivationDevice_IsRevoked = false)
                                         )");       
    return mysqli_num_rows($result);
}

/******************************************************************************
 * DataAccess_Activation_GetNumberDevices
 *****************************************************************************/
function DataAccess_ActivationDevice_GetNumber($conn, $activation_key) 
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $result = mysqli_query($conn, "SELECT ID_ActivationDevice 
                                   FROM   ActivationDevice 
                                   WHERE  ID_Activation = '$activation_id'");       
    return mysqli_num_rows($result);
}

/******************************************************************************
 * DataAccess_ActivationDevice_SetRevoked
 *****************************************************************************/
function DataAccess_ActivationDevice_SetRevoked($conn, $activation_key, $hardware_id)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $is_matching_hardware_id = DataAccess_GetMySqlHardwareIdComparison($conn, 
                                                                    $activation_key, 
                                                                    $hardware_id);
    $result = mysqli_query($conn, "UPDATE ActivationDevice 
                                    SET   ActivationDevice_IsRevoked = true
                                    WHERE (
                                           (ID_Activation = '$activation_id') AND
                                           $is_matching_hardware_id
                                          )");       
}

/******************************************************************************
 * DataAccess_ActivationDevice_UnsetRevoked
 *****************************************************************************/
function DataAccess_ActivationDevice_UnsetRevoked($conn, $activation_key, $hardware_id)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $is_matching_hardware_id = DataAccess_GetMySqlHardwareIdComparison($conn, 
                                                                $activation_key, 
                                                                $hardware_id);
    $result = mysqli_query($conn, "UPDATE ActivationDevice 
                                   SET    ActivationDevice_IsRevoked = false
                                   WHERE (
                                          (ID_Activation = '$activation_id') AND
                                          $is_matching_hardware_id
                                         )");     
}

/******************************************************************************
 * DataAccess_ActivationDevice_IsRevoked
 *****************************************************************************/
function DataAccess_ActivationDevice_IsRevoked($conn, $activation_key, $hardware_id)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $is_matching_hardware_id = DataAccess_GetMySqlHardwareIdComparison($conn, 
                                                            $activation_key, 
                                                            $hardware_id);
    $result = mysqli_query($conn, "SELECT ActivationDevice_IsRevoked 
                                   FROM   ActivationDevice
                                   WHERE (
                                          $is_matching_hardware_id AND 
                                          (ID_Activation = '$activation_id')
                                         )");      

    $row = $result->fetch_assoc();  
    return $row["ActivationDevice_IsRevoked"];  
}

/******************************************************************************
 * DataAccess_ActivationDevice_GetReactivated
 *****************************************************************************/
function DataAccess_ActivationDevice_GetReactivated($conn, $activation_key, $hardware_id)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $is_matching_hardware_id = DataAccess_GetMySqlHardwareIdComparison($conn, 
                                                                      $activation_key, 
                                                                      $hardware_id);    
    $result = mysqli_query($conn, "SELECT ActivationDevice_NumReactivated 
                                   FROM   ActivationDevice
                                   WHERE  (
                                           $is_matching_hardware_id AND 
                                           (ID_Activation = '$activation_id')
                                          )");      
    $row = $result->fetch_assoc();  
    return $row["ActivationDevice_NumReactivated"];  
}

/******************************************************************************
 * DataAccess_ActivationDevice_Add
 *****************************************************************************/
function DataAccess_ActivationDevice_Add($conn, $activation_key, $hardware_id)
{
    if (!DataAccess_ActivationDevice_IsDeviceActivated($conn, $activation_key, $hardware_id))
    {
        $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
        $result = mysqli_query($conn, "INSERT INTO ActivationDevice (ID_Activation, 
                                                                     ActivationDevice_HardwareId, 
                                                                     ActivationDevice_IsRevoked,
                                                                     ActivationDevice_Date)
                                                                     
                                                            VALUES  ('$activation_id', 
                                                                     '$hardware_id', 
                                                                     false, 
                                                                     NOW())");     
    }  
    else
    {
        DataAccess_ActivationDevice_UnsetRevoked($conn, $activation_key, $hardware_id);
    }
}

/******************************************************************************
 * DataAccess_ActivationDevice_IsDeviceActivated
 *****************************************************************************/
function DataAccess_ActivationDevice_IsDeviceActivated($conn, $activation_key, $hardware_id)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $is_matching_hardware_id = DataAccess_GetMySqlHardwareIdComparison($conn, 
                                                                       $activation_key, 
                                                                       $hardware_id);
    $result = mysqli_query($conn, "SELECT ID_ActivationDevice 
                                   FROM   ActivationDevice 
                                   WHERE  (
                                           $is_matching_hardware_id AND 
                                           (ID_Activation = '$activation_id')
                                          )");  

    if (mysqli_num_rows($result) == 0) 
    {
        return false;
    }
    else 
    {
        return true;
    }
}

/******************************************************************************
 * DataAccess_GetMySqlHardwareIdComparison
 *****************************************************************************/
function DataAccess_GetMySqlHardwareIdComparison($conn, $activation_key, $hardware_id) 
{
    $hardware_id_masked = GetMaskForString($hardware_id, 
                                DataAccess_Product_GetHardwareIdMask($conn, 
                                                                     $activation_key, 
                                                                     $hardware_id));
    if (empty($hardware_id_masked))
    {
        return "(ActivationDevice_HardwareId = '$hardware_id')";
    }
    else
    {
        return "(ActivationDevice_HardwareId LIKE '$hardware_id_masked')";
    }
}

/******************************************************************************
 * DataAccess_ActivationDevice_GetNumberRevoked
 *****************************************************************************/
function DataAccess_ActivationDevice_GetNumberRevoked($conn, $activation_key) 
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $result = mysqli_query($conn, "SELECT ID_ActivationDevice 
                                   FROM   ActivationDevice 
                                   WHERE (
                                          (ID_Activation = '$activation_id') AND
                                          (ActivationDevice_IsRevoked = false)
                                         )");       
    return mysqli_num_rows($result);
}


?>

