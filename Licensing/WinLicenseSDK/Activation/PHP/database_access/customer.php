<?php

/******************************************************************************
 * DataAccess_Customer_Add
 *****************************************************************************/
function DataAccess_Customer_Add($conn, $customer)                               
{
    mysqli_query($conn, "INSERT INTO Customers (Customer_Salutation, Customer_FirstName, 
                                                Customer_LastName, Customer_JobTitle,  
                                                Customer_Organization, Customer_Email,
                                                Customer_Phone, Customer_Fax, 
                                                Customer_Address1, Customer_Address2,
                                                Customer_City, Customer_Zip, Customer_State,
                                                Customer_Country, Customer_Notes,
                                                Customer_IsAcceptingMail, Customer_IsRegistered,
                                                Customer_IsBlacklisted, Customer_DateEdit)
                                                
                                        VALUES ('$customer->salutation', '$customer->first_name', 
                                                '$customer->last_name', 
                                                '$customer->job_title', '$customer->organization', 
                                                '$customer->email', '$customer->phone',
                                                '$customer->fax', '$customer->address1', 
                                                '$customer->address2', '$customer->city',
                                                '$customer->zip_code', '$customer->state', 
                                                '$customer->country', '$customer->notes',
                                                 true, true, false, NOW())");
}

/******************************************************************************
 * DataAccess_Customer_IsExisting
 *****************************************************************************/
function DataAccess_Customer_IsExisting($conn, $customer)
{
    $result = mysqli_query($conn, "SELECT ID_Customer 
                                   FROM   Customers 
                                   WHERE (
                                          (Customer_FirstName = '$customer->first_name') AND 
                                          (Customer_LastName = '$customer->last_name') AND
                                          (Customer_Email = '$customer->email') 
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
 * DataAccess_Customer_GetId
 *****************************************************************************/
function DataAccess_Customer_GetId($conn, $customer)
{
    $result = mysqli_query($conn, "SELECT ID_Customer 
                                   FROM   Customers 
                                   WHERE (
                                          (Customer_FirstName = '$customer->first_name') AND 
                                          (Customer_LastName = '$customer->last_name') AND
                                          (Customer_Email = '$customer->email') 
                                         )");  
    $row = $result->fetch_assoc();  
    return $row["ID_Customer"];  
}

/******************************************************************************
 * DataAccess_Customer_GetField
 *****************************************************************************/
function DataAccess_Customer_GetField($conn, $customer_id, $field_name)
{
    $result = mysqli_query($conn, "SELECT * 
                                   FROM   Customers 
                                   WHERE  ID_Customer = '$customer_id'");
    $row = $result->fetch_assoc();  
    return $row[$field_name];  
}

/******************************************************************************
 * DataAccess_Customer_GetFirstName
 *****************************************************************************/
function DataAccess_Customer_GetFirstName($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_FirstName");
}

/******************************************************************************
 * DataAccess_Customer_GetLastName
 *****************************************************************************/
function DataAccess_Customer_GetLastName($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_LastName");
}

/******************************************************************************
 * DataAccess_Customer_GetEmail
 *****************************************************************************/
function DataAccess_Customer_GetEmail($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_Email");
}

/******************************************************************************
 * DataAccess_Customer_GetAddress
 *****************************************************************************/
function DataAccess_Customer_GetAddress($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_Address1") . " " .
           DataAccess_Customer_GetField($conn, $customer_id, "Customer_Address2");
}

/******************************************************************************
 * DataAccess_Customer_GetCity
 *****************************************************************************/
function DataAccess_Customer_GetCity($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_City");
}

/******************************************************************************
 * DataAccess_Customer_GetZip
 *****************************************************************************/
function DataAccess_Customer_GetZip($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_Zip");
}

/******************************************************************************
 * DataAccess_Customer_GetState
 *****************************************************************************/
function DataAccess_Customer_GetState($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_State");
}

/******************************************************************************
 * DataAccess_Customer_GetCountry
 *****************************************************************************/
function DataAccess_Customer_GetCountry($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_Country");
}

/******************************************************************************
 * DataAccess_Customer_GetOrganization
 *****************************************************************************/
function DataAccess_Customer_GetOrganization($conn, $customer_id)
{
    return DataAccess_Customer_GetField($conn, $customer_id, "Customer_Organization");
}


?>
