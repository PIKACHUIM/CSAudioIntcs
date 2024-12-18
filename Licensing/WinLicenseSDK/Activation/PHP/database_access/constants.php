<?php

/******************************************************************************
 *                               Constants
 *****************************************************************************/

// Key-pair values for Registration Content (in Orders Table)
define("REGINFO_USER_NAME", "User Name");              
define("REGINFO_COMPANY", "Company");
define("REGINFO_HARDWARE_ID", "Hardware ID");
define("REGINFO_CUSTOM_DATA", "Custom Data");
define("REGINFO_DAYS_EXPIRATION", "Days Expiration");
define("REGINFO_DATE_EXPIRATION", "Date Expiration");
define("REGINFO_EXECUTIONS", "Executions");
define("REGINFO_RUNTIME_EXECUTION", "Runtime execution");
define("REGINFO_GLOBAL_TIME", "Global Time");
define("REGINFO_INSTALL_BEFORE_DATE", "Install Before Date");
define("REGINFO_NETWORK_INSTANCES", "Network Instances");
define("REGINFO_STORE_CREATION_DATE", "Store Creation Date");
define("REGINFO_EMBED_USER_INFO", "Embed User Info in Key");
define("REGINFO_IS_UNICODE", "Unicode License");
define("REGINFO_LOCKED_COUNTRY", "Locked to Country");
define("REGINFO_LICENSE_FORMAT", "License Format");
define("REGINFO_LICENSE_DATA", "License Data");
define("REGINFO_LICENSE_DATA_MARKER_START", "<license_start>");
define("REGINFO_LICENSE_DATA_MARKER_END", "<license_end>");
define("REGINFO_ACTIVATION_DIFFERENT_DEVICES_ALLOWED", "Maximum Different Devices");
define("REGINFO_ACTIVATION_SIMULTANEOUS", "Simultaneous Devices");
define("REGINFO_ACTIVATION_MAX_DEACTIVATIONS", "Deactivation Limit");
define("REGINFO_ACTIVATION_MAX_ACTIVATIONS", "Activation Limit");
define("REGINFO_ACTIVATION_MAX_DIFFERENT_DEVICES", "Maximum Different Devices");
define("REGINFO_ACTIVATION_EXPIRATION_DATE", "Expiration Date");
define("REGINFO_ACTIVATION_FORMAT", "Activation Format");
define("REGINFO_ACTIVATION_KEY", "Activation Code");

define("REGINFO_CUSTOM_DATA_MARKER_START", "<custom_start>");
define("REGINFO_CUSTOM_DATA_MARKER_END", "<custom_end>");

// Key-pair values for Custom Product Values (in Products Table)
define("PRODUCTINFO_LICENSE_HASH", "Product_LicenseHash");
define("PRODUCTINFO_HARDWARE_ID_MASK", "HardwareIdMask");
define("PRODUCTINFO_DEFAULT_CUSTOM_DATA", "DefaultLicenseCustomData");
define("PRODUCTINFO_DEFAULT_LICENSE_DAYS", "DefaultLicenseDaysExp");
define("PRODUCTINFO_DEFAULT_LICENSE_EXEC", "DefaultLicenseExecutionsExp");
define("PRODUCTINFO_DEFAULT_LICENSE_DATE", "DefaultLicenseDateExp");
define("PRODUCTINFO_DEFAULT_LICENSE_RUNTIME", "DefaultLicenseRuntimeExp");
define("PRODUCTINFO_DEFAULT_LICENSE_GLOBAL_TIME", "DefaultLicenseGlobalTimeExp");
define("PRODUCTINFO_DEFAULT_LICENSE_NETWORK_INSTANCES", "DefaultLicenseNetworkInstances");
define("PRODUCTINFO_DEFAULT_LICENSE_STORE_CREATION", "DefaultLicenseStoreCreation");
define("PRODUCTINFO_DEFAULT_LICENSE_EMBED_USER", "DefaultLicenseEmbedUser");
define("PRODUCTINFO_DEFAULT_LICENSE_COUNTRY_LOCKING", "DefaultLicenseCountryLocking");
define("PRODUCTINFO_DEFAULT_LICENSE_IS_UNICODE", "DefaultLicenseIsUnicode");
define("PRODUCTINFO_DEFAULT_LICENSE_INSERT_START_END_MARKERS", "DefaultLicenseInsertStartEndMarkers");
define("PRODUCTINFO_DEFAULT_ORDER_SUBSCRIPTION_DAYS", "DefaultOrderSubscriptionDays");
define("PRODUCTINFO_DEFAULT_LICENSE_TYPE", "DefaultLicenseType");
define("PRODUCTINFO_DEFAULT_ACTIVATION_FORMAT", "DefaultActivationFormat");
define("PRODUCTINFO_DEFAULT_ACTIVATION_SIMULTANEOUS_DEVICES", "DefaultActivationSimultaneousDevices");
define("PRODUCTINFO_DEFAULT_ACTIVATION_EXPIRATION_DATE", "DefaultActivationExpirationDate");
define("PRODUCTINFO_DEFAULT_ACTIVATION_DEACTIVATION_LIMIT", "DefaultActivationLimitDeactivation");
define("PRODUCTINFO_DEFAULT_ACTIVATION_ACTIVATION_LIMIT", "DefaultActivationLimitActivation");
define("PRODUCTINFO_DEFAULT_ACTIVATION_MAX_DIFFERENT_DEVICES", "DefaultActivationMaxDifferentDevices");

// Type of keys
define("LICENSE_TYPE_FILE", 0);
define("LICENSE_TYPE_REGISTRY", 1);
define("LICENSE_TYPE_TEXT", 2);
define("LICENSE_TYPE_STATIC_SMARTKEY", 3);
define("LICENSE_TYPE_DYNAMIC_SMARTKEY", 4);
define("LICENSE_TYPE_ACTIVATION", 5);

// Subscription expiration constants (for orders)
define("SUBCRIPTION_ONE_MONTH", 30);
define("SUBCRIPTION_THREE_MONTHS", 91);
define("SUBCRIPTION_SIX_MONTHS", 183);
define("SUBCRIPTION_ONE_YEAR", 365);
define("SUBCRIPTION_TWO_YEARS", 730);
define("SUBCRIPTION_100_YEARS", 36500);
define("SUBCRIPTION_200_YEARS", 36500 * 2);


?>
