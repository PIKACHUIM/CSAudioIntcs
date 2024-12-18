<?php

include_once "database_access/constants.php";
include_once "database_access/aux_functions.php";
include_once "database_access/registration_info.php";
include_once "database_access/activation.php";
include_once "database_access/activationdevice.php";
include_once "database_access/activationdevice.php";
include_once "database_access/product.php";
include_once "database_access/customer.php";
include_once "database_access/order.php";

/******************************************************************************
 * OpenDatabase 
 *****************************************************************************/
function OpenDatabase($servername, $username, $password, $dbname)
{
    $conn = new mysqli($servername, $username, $password, $dbname);
    mysqli_set_charset($conn, "utf8");
    
    if ($conn->connect_error) 
    {
        die("Connection failed: " . $conn->connect_error);
    } 
    return $conn;
}

/******************************************************************************
 * CloseDatabase
 *****************************************************************************/
function CloseDatabase($conn)
{
    $conn->close();
}

?>
