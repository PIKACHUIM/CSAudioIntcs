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
include_once "activation/activation.php";

/******************************************************************************
;                                HTML GET
;*****************************************************************************/
$activationkey = $_GET["activationkey"];
$hardwareid = $_GET["hardwareid"];

/******************************************************************************
;                                  Code
;*****************************************************************************/
$conn = OpenDatabase($servername, $username, $password, $dbname);

$activation_allowed = CheckActivationAllowed($conn, $activationkey, $hardwareid);

if ($activation_allowed == ACTIVATION_OK)
{
    DoActivation($conn, $activationkey, $hardwareid);
}

print("<br>__ERROR_CODE_START__" . $activation_allowed . "__ERROR_CODE_END__" );

if ($activation_allowed == ACTIVATION_OK)
{
    GenerateLicenseFromActivation($conn, $activationkey, $hardwareid);
}

CloseDatabase($conn);

?>

</body>
</html> 
