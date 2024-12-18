<?php

/******************************************************************************
 *                              Constants
 *****************************************************************************/

 // Activation exit codes
define("ACTIVATION_OK", "0");
define("ACTIVATION_ERROR_KEY_NOT_FOUND", "1");
define("ACTIVATION_ERROR_MAX_DEVICES_REACHED", "2");
define("ACTIVATION_ERROR_NO_MORE_ACTIVATIONS_ALLOWED", "3");
define("ACTIVATION_ERROR_NO_MORE_DEACTIVATIONS_ALLOWED", "4");
define("ACTIVATION_ERROR_DEVICE_NOT_FOUND", "5");
define("ACTIVATION_ERROR_WRONG_DATA_RECEIVED", "6");
define("ACTIVATION_ERROR_KEY_DISABLED_BY_SELLER", "7");
define("ACTIVATION_ERROR_KEY_EXPIRED", "8");
define("ACTIVATION_ERROR_NO_MORE_DIFFERENT_DEVICES_ALLOWED", "9");

// Activation miscellaneous constants
define("ACTIVATION_UNLIMITED_VALUE", "65535");

// Key-pair values for license restrictions when creating a license
define("LICENSE_CUSTOM_DATA", "license_custom_data");
define("LICENSE_HARDWARE_ID", "hardware_id");
define("LICENSE_INSERT_START_END_MARKERS", "license_insert_start_end_markers");
define("LICENSE_RESTRICTION_DAYS", "license_restriction_days");
define("LICENSE_RESTRICTION_EXECUTIONS", "license_restriction_executions");
define("LICENSE_RESTRICTION_DATE", "license_restriction_date");
define("LICENSE_RESTRICTION_RUNTIME", "license_restriction_runtime");
define("LICENSE_RESTRICTION_GLOBALTIME", "license_restriction_globaltime");
define("LICENSE_RESTRICTION_INSTALL_BEFORE_DATE", "license_restriction_install_before_date");
define("LICENSE_RESTRICTION_COUNTRY_LOCK", "license_restriction_country_lock");
define("LICENSE_RESTRICTION_NETWORK_INSTANCES", "license_restriction_network_instances");
define("LICENSE_STORE_CREATION_DATE", "license_store_creation_date");
define("LICENSE_EMBED_USER_INFO", "license_embed_user_info");
define("LICENSE_IS_UNICODE", "license_is_unicode");
define("LICENSE_ACTIVATION_SIMULTANEOUS_DEVICES", "license_activation_simultaneous_devices");
define("LICENSE_ACTIVATION_DEACTIVATION_LIMIT", "license_activation_deactivation_limit");
define("LICENSE_ACTIVATION_ACTIVATION_LIMIT", "license_activation_activation_limit");
define("LICENSE_ACTIVATION_MAX_DIFFERENT_DEVICES", "license_activation_max_different_devices");
define("LICENSE_ACTIVATION_EXPIRATION_DATE", "license_activation_expiration_date");
define("LICENSE_ACTIVATION_KEY_FORMAT", "license_activation_key_format");

?>
