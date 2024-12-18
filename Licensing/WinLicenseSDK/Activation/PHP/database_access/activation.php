<?php

/******************************************************************************
 * DataAccess_Activation_IsKeyInDatabase
 *****************************************************************************/
function DataAccess_Activation_IsKeyInDatabase($conn, $activation_key) 
{
    $result = mysqli_query($conn, "SELECT ID_Activation 
                                   FROM   Activations 
                                   WHERE  Activation_String = '$activation_key'");       
    if(mysqli_num_rows($result) == 0) 
    {
        return false;
    }
    else 
    {
        return true;
    }
}

/******************************************************************************
 * DataAccess_Activation_IsDisabled
 *****************************************************************************/
function DataAccess_Activation_IsDisabled($conn, $activation_key) 
{
    $result = mysqli_query($conn, "SELECT Activation_IsDisabled 
                                   FROM   Activations 
                                   WHERE  Activation_String = '$activation_key'");   
    $row = $result->fetch_assoc();  
    return $row["Activation_IsDisabled"];   
}

/******************************************************************************
 * DataAccess_Activation_GetIdFromKey
 *****************************************************************************/
function DataAccess_Activation_GetIdFromKey($conn, $activation_key) 
{
    $result = mysqli_query($conn, "SELECT ID_Activation 
                                   FROM   Activations 
                                   WHERE  Activation_String = '$activation_key'");   
    $row = $result->fetch_assoc();  
    return $row["ID_Activation"];
}

/******************************************************************************
 * DataAccess_Activation_GetCurrentActivatedTimes
 *****************************************************************************/
function DataAccess_Activation_GetCurrentActivatedTimes($conn, $activation_key) 
{
    $result = mysqli_query($conn, "SELECT Activation_CurrentActivations 
                                   FROM   Activations 
                                   WHERE  Activation_String = '$activation_key'");   
    $row = $result->fetch_assoc();  
    return $row["Activation_CurrentActivations"];
}

/******************************************************************************
 * DataAccess_Activation_GetCurrentDeactivatedTimes
 *****************************************************************************/
function DataAccess_Activation_GetCurrentDeactivatedTimes($conn, $activation_key) 
{
    $result = mysqli_query($conn, "SELECT Activation_CurrentDeactivations 
                                   FROM   Activations 
                                   WHERE  Activation_String = '$activation_key'");   
    $row = $result->fetch_assoc();  
    return $row["Activation_CurrentDeactivations"];
}

/******************************************************************************
 * DataAccess_Activation_IncreaseActivatedTimes
 *****************************************************************************/
function DataAccess_Activation_IncreaseActivatedTimes($conn, $activation_key)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);

    $result = mysqli_query($conn, "UPDATE Activations 
                                   SET    Activation_CurrentActivations = Activation_CurrentActivations + 1
                                   WHERE  ID_Activation = '$activation_id'");
}

/******************************************************************************
 * DataAccess_Activation_IncreaseDeactivatedTimes
 *****************************************************************************/
function DataAccess_Activation_IncreaseDeactivatedTimes($conn, $activation_key)
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $result = mysqli_query($conn, "UPDATE Activations 
                                   SET    Activation_CurrentDeactivations = Activation_CurrentDeactivations + 1
                                   WHERE  ID_Activation = '$activation_id'");
}

/******************************************************************************
 * DataAccess_Activation_GetRegistrationInfo
 *****************************************************************************/
function DataAccess_Activation_GetRegistrationInfo($conn, $activation_key) 
{
    $activation_id = DataAccess_Activation_GetIdFromKey($conn, $activation_key);
    $result = mysqli_query($conn, "SELECT Activation_ID_Order 
                                   FROM   Activations 
                                   WHERE  ID_Activation = '$activation_id'");   
    $row = $result->fetch_assoc();  
    $order_id = $row["Activation_ID_Order"];

    $result = mysqli_query($conn, "SELECT Order_RegistrationContent 
                                   FROM   Orders 
                                   WHERE  ID_Order  = '$order_id'");
    $row = $result->fetch_assoc();  
    return $row["Order_RegistrationContent"];
}


/******************************************************************************
 * DataAccess_Activation_AddFromRegistrationInfo
 *****************************************************************************/
function DataAccess_Activation_AddFromRegistrationInfo($conn, $activation_code,
                                                       $order_id)
{
    mysqli_query($conn, "INSERT INTO Activations (Activation_String, 
                                             Activation_ID_Order,  
                                             Activation_IsDisabled, 
                                             Activation_CreationDate,
                                             Activation_CurrentDeactivations, 
                                             Activation_CurrentActivations
                                             )
                                     VALUES ('$activation_code', 
                                             '$order_id', 
                                             false, 
                                             NOW(), 
                                             0,
                                             0
                                             )");
}




?>

