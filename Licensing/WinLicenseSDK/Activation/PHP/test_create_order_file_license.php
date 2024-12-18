<html>
<head>
<title>Activation page</title>
</head>
<body>

<meta http-equiv="Content-Type" content="text/html;charset=UTF-8">

<?php 

/******************************************************************************
;                                Includes
;*****************************************************************************/
include_once "main_settings.php";
include_once "database_access/database_access.php";
include_once "key_generation/generate_key.php";
include_once "classes/license.php";
include_once "classes/customer.php";

/******************************************************************************
;                                HTML GET
;*****************************************************************************/

$customer = new Customer;

$customer->first_name = $_GET["user_first_name"];
$customer->last_name = $_GET["user_last_name"];
$customer->email = $_GET["user_email"];
$product_name = $_GET["product_name"];


/******************************************************************************
;                                  Code
;*****************************************************************************/

$conn = OpenDatabase($servername, $username, $password, $dbname);

if (!DataAccess_Customer_IsExisting($conn, $customer))
{
    DataAccess_Customer_Add($conn, $customer);
}

$customer_id = DataAccess_Customer_GetId($conn, $customer);
$product_id = DataAccess_Product_GetId($conn, $product_name);

$license = new License;

DataAccess_Product_InitializeLicenseFromProductValues($conn, $product_id, $license);
$generated_license = DataAccess_Order_Add($conn, $customer_id, $product_id, 1, "", "my order notes", $license);

echo "<br> keygen output  = " . $generated_license . "<br>";

CloseDatabase($conn);

?>

</body>
</html> 
