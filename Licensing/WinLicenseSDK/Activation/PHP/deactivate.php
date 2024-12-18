<html>
<head>
<title>Deactivation page</title>
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
$check_revoke_status = CheckRevokeAllowed($conn, $activationkey, $hardwareid);

if ($check_revoke_status == ACTIVATION_OK)
{
    DoRevokeDevice($conn, $activationkey, $hardwareid);
}

print("<br>__ERROR_CODE_START__" . $check_revoke_status . "__ERROR_CODE_END__" );

CloseDatabase($conn);

?>

</body>
</html> 
