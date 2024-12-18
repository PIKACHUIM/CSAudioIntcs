<?php

/******************************************************************************
 * GenerateLicense_ForType
 *****************************************************************************/
function GenerateLicense_ForType($license_hash, $user_name, 
                                 $company, $license_settings)
{
    $args = GenerateLicense_GenerateArguments($license_hash, $user_name, 
                                              $company, $license_settings);                                              
    $return_value = "";            
    $generated_key = GenerateLicense_ExecuteKeyGenerator($args, $return_value);        
    return $generated_key; 
}

/******************************************************************************
 * GenerateLicense_GenerateArguments
 *****************************************************************************/
function GenerateLicense_GenerateArguments($license_hash, $user_name, 
                                            $company, $license_settings)
{
    $custom_data = $license_settings->custom_data;
    $hardware_id = $license_settings->hardware_id;
    $days_expiration = $license_settings->expiration_days;
    $date_expiration = $license_settings->expiration_date;
    $executions = $license_settings->expiration_executions;
    $runtime_execution = $license_settings->expiration_runtime;
    $global_time = $license_settings->expiration_global_time;
    $install_before_date = $license_settings->install_before_date;
    $network_instances = $license_settings->network_instances;
    $is_unicode = $license_settings->is_unicode;
    $locked_country = $license_settings->country_lock;
    $append_begin_end = $license_settings->insert_start_end_markers;
    $license_type = $license_settings->license_type;

    $args = "";
    $args.= "-l \"" . $license_hash . "\"";

    // convert UTF8 as Windows unicode
    if (IsSettingTrue($is_unicode))
    {
        if ($user_name != "")
            $args.= " -a \"" . bin2hex(mb_convert_encoding($user_name, "UTF-16LE", "UTF-8")) . "\"";

        if ($company != "")
            $args.= " -b \"" . bin2hex(mb_convert_encoding($company, "UTF-16LE", "UTF-8")) . "\"";

         if ($custom_data != "")
         {
            $custom_data_no_new_line = str_replace("\r\n", "*-[NL]-*", $custom_data);
            $args.= " -c \"" . bin2hex(mb_convert_encoding($custom_data_no_new_line, "UTF-16LE", "UTF-8")) . "\"";
         }

        $args.= " -u 1";
    }
    else
    {
        if ($user_name != "")
            $args.= " -a \"" . $user_name . "\"";  

        if ($company != "")
            $args.= " -b \"" . $company . "\"";      

         if ($custom_data != "")
         {
            $custom_data_no_new_line = str_replace("\r\n", "*-[NL]-*", $custom_data);
            $args.= " -c \"" . $custom_data_no_new_line . "\"";
        }
    }

    if ($hardware_id != "")
        $args.= " -h \"" . $hardware_id . "\"";

    if ($executions != "")
        $args.= " -e \"" . $executions . "\"";

    if ($days_expiration != "")
    {
        $args.= " -d \"" . $days_expiration . "\"";
    }

    if ($date_expiration != "")
        $args.= " -n \"" . $date_expiration . "\"";

    if ($runtime_execution != "")
        $args.= " -i \"" . $runtime_execution . "\"";

    if ($global_time != "")
        $args.= " -g \"" . $global_time . "\"";

    if ($install_before_date != "")
        $args.= " -j \"" . $install_before_date . "\"";

    if ($network_instances != "")
        $args.= " -k \"" . $network_instances . "\"";

    if ($locked_country != "")
        $args.= " -m \"" . $locked_country . "\"";

    $args.= " -t " . $license_type;

    if (IsSettingTrue($append_begin_end))
    {
        $args.= " -p 1";
    }

    return $args;
}

/******************************************************************************
 * GenerateLicense_ExecuteKeyGenerator
 *****************************************************************************/
function GenerateLicense_ExecuteKeyGenerator($args, $return_value)
{
    if (!file_exists(ACTIVATION_APP))
    {
        echo "<br>Cannot find your activation application (ACTIVATION_APP = " . ACTIVATION_APP . ")<br>";
        return "";
    }
    
    // Call external license generator application    
    exec(ACTIVATION_APP . " " . $args, $output, $return_value);

    if (empty($output[0]))
    {
        echo "<br>An error occurred when calling " . ACTIVATION_APP . "<br>";
        echo "<br>return_arg = " . $return_value . "<br>";
        return "";
    }
    else
    {
        return $output[0];
    }
}

/******************************************************************************
 * IsSettingTrue
 *****************************************************************************/
function IsSettingTrue($current_setting)
{
    if (!empty($current_setting) && 
            (($current_setting[0] == 'y') || 
            ($current_setting[0] == 'Y') ||
            ($current_setting[0] == 'T') ||
            ($current_setting[0] == 't')))
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}

/******************************************************************************
 * GenerateActivationKey 
 *****************************************************************************/
function GenerateActivationKey($activation_key_format)
{
    $result = "";
    
    for ($i = 0; $i < strlen($activation_key_format); $i++)
    {
        if ($activation_key_format[$i] == '#')
        {
            $result .= GenerateActivationKeyGroup(1);
        }
        else
        {
            $result .= $activation_key_format[$i];
        }
    }
    return $result;
}

/******************************************************************************
 * GenerateActivationKeyGroup
 *****************************************************************************/
function GenerateActivationKeyGroup($num_chars_in_group)
{
    $letters = "ABCDEFGHJKMNPQRSTUVWXYZ";
    $numbers = "23456789";
    $result = "";

    for ($i = 0; $i < $num_chars_in_group; $i++)
    {
        if (rand(0, 1) == 0)
        {
            $result .= $letters[rand(0, strlen($letters) - 1)];
        }
        else
        {
            $result .= $numbers[rand(0, strlen($numbers) - 1)];
        }
    }
    return $result;
}


?>

