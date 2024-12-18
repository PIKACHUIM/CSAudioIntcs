<?php

/******************************************************************************
 * GetKeyValueInUTF16
 *****************************************************************************/
function GetKeyValueInUTF16($registration_info, $key_value)
{
    //$registration_info_utf8 = mb_convert_encoding($registration_info, "UTF-8", "UTF-16LE");
    $registration_info_utf8 = $registration_info;
    $registration_info_utf8 = str_replace("\n", '&', $registration_info_utf8);

    parse_str($registration_info_utf8, $registration_info_array);
    return isset($registration_info_array[$key_value])? $registration_info_array[$key_value] : null;
}

/******************************************************************************
 * GetKeyValueInUTF8
 *****************************************************************************/
function GetKeyValueInUTF8($registration_info_utf8, $key_value)
{
    $registration_info_utf8 = str_replace("\n", '&', $registration_info_utf8);

    parse_str($registration_info_utf8, $registration_info_array);
    return isset($registration_info_array[$key_value])? $registration_info_array[$key_value] : null;
}

/******************************************************************************
 * GetStringBetween
 *****************************************************************************/
function GetStringBetween($str,$from,$to)
{
    $sub = substr($str, strpos($str,$from)+strlen($from),strlen($str));
    return substr($sub,0,strpos($sub,$to));
}

/******************************************************************************
 * GetValueBetweenMarkersUTF16
 *****************************************************************************/
function GetValueBetweenMarkersUTF16($registration_info, $marker_start, $marker_end)
{
    //$registration_info_utf8 = mb_convert_encoding($registration_info, "UTF-8", "UTF-16LE");
    $registration_info_utf8 = $registration_info;
    return GetStringBetween($registration_info_utf8, $marker_start, $marker_end);
}

/******************************************************************************
 * GetValueBetweenMarkersUTF8
 *****************************************************************************/
function GetValueBetweenMarkersUTF8($registration_info, $marker_start, $marker_end)
{
    return GetStringBetween($registration_info, $marker_start, $marker_end);
}

/******************************************************************************
 * GetMaskForString
 *****************************************************************************/
function GetMaskForString($string, $user_mask)
{
    $mask_out = $string;
    $mask_length = strlen($user_mask);

    for ($i = 0; $i < $mask_length; $i++)
    {
        if ($user_mask[$i] == '?')
        {
            $mask_out[$i] = '_';
        }
    }
    return $mask_out;
}

/******************************************************************************
 * HexDump
 *****************************************************************************/
function HexDump ($data, $htmloutput = true, $uppercase = false, $return = false)
{
    // Init
    $hexi   = '';
    $ascii  = '';
    $dump   = ($htmloutput === true) ? '<pre>' : '';
    $offset = 0;
    $len    = strlen($data);
 
    // Upper or lower case hexadecimal
    $x = ($uppercase === false) ? 'x' : 'X';
 
    // Iterate string
    for ($i = $j = 0; $i < $len; $i++)
    {
        // Convert to hexidecimal
        $hexi .= sprintf("%02$x ", ord($data[$i]));
 
        // Replace non-viewable bytes with '.'
        if (ord($data[$i]) >= 32) {
            $ascii .= ($htmloutput === true) ?
                            htmlentities($data[$i]) :
                            $data[$i];
        } else {
            $ascii .= '.';
        }
 
        // Add extra column spacing
        if ($j === 7) {
            $hexi  .= ' ';
            $ascii .= ' ';
        }
 
        // Add row
        if (++$j === 16 || $i === $len - 1) {
            // Join the hexi / ascii output
            $dump .= sprintf("%04$x  %-49s  %s", $offset, $hexi, $ascii);
            
            // Reset vars
            $hexi   = $ascii = '';
            $offset += 16;
            $j      = 0;
            
            // Add newline            
            if ($i !== $len - 1) {
                $dump .= "\n";
            }
        }
    }
 
    // Finish dump
    $dump .= $htmloutput === true ?
                '</pre>' :
                '';
    $dump .= "\n";
 
    // Output method
    if ($return === false) {
        echo $dump;
    } else {
        return $dump;
    }
}

/******************************************************************************
 * KeyAsText
 *****************************************************************************/
function KeyAsText($license_key)
{
    return $license_key;
}

/******************************************************************************
 * KeyAsBinary
 *****************************************************************************/
function KeyAsBinary($license_key)
{
    // We are currently encoding binary key in base64
    return base64_decode($license_key);
}


?>

