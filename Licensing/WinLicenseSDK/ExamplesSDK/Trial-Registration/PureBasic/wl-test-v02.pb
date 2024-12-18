XIncludeFile      "WinLicenseSDKMacros.pbi"
XIncludeFile      "\Program Files\PureBasic\Includes\WinLicenseSDK.pbi"

; PB Winlicense Constants 

#wlIsTrial = 0 

#wlIsRegistered = 1 

#wlInvalidLicense = 2 

#wlInvalidHardwareLicense = 3 

#wlNoMoreHwdChanges = 4 

#wlLicenseExpired = 5 

#wlInvalidCountryLicense = 6 

#wlLicenseStolen = 7 

#wlWrongLicenseExp = 8 

#wlWrongLicenseHardware = 9 

 

#wlTrialOk = 0 

#wlTrialDaysExpired = 1 

#wlTrialExecExpired = 2 

#wlTrialDateExpired = 3 

#wlTrialRuntimExpired = 4 

#wlTrialGlobalExpired = 5 

#wlTrailInvalidCountry = 6 

 

;WLLoadWinlicenseDll_() 

 

name.s = Space(1024)
company.s = Space(1024)
custom.s = Space(1024) 
key.s = Space (2048)

WLRegGetLicenseInfo ( @name,@company,@custom)

LicenseName.s = Trim(name)
LicenseCompany.s = Trim(company)
LicenseData.s = Trim(custom)
 
Debug LicenseName + " " + LicenseCompany + " " + LicenseData

WLRegLicenseName ( @name,@company,@custom)

LicenseName.s = Trim(name)
LicenseCompany.s = Trim(company)
LicenseData.s = Trim(custom)
 
Debug LicenseName + " " + LicenseCompany + " " + LicenseData


;;

a = WLTrialTotalExecutions ()
Debug "a" + Str (a)

WLHardwareGetID ( @custom )
Debug "custom|: " + custom
hwid.s = Trim ( custom )

newdate.l = AddDate ( Date(), #PB_Date_Month, 3 ) ; We make it expire in 3 months time
time.SYSTEMTIME
  
time\wDay   = Day ( newdate )
time\wMonth = Month ( newdate )
time\wYear  = Year ( newdate )

hash.s = "yuhsdf873hJHslk99423jhdsfhGSDKURKCFNB78934hGDMNF5634GfghwyIPahfurH6394hfJfhUYfikfhu8whj3yHYFIOOFhNFAPWRE"
a = WLGenLicenseSmartKey ( @hash, @LicenseName, @LicenseCompany, @LicenseData, @hwid, 30, 100, @time, @key )
Debug "key: " + key

a = WLGenLicenseTextKey  ( @hash, @LicenseName, @LicenseCompany, @LicenseData, @hwid, 30, 100, @time, 0,0,0, @key )
Debug "key: " + key
; Return s 1 if license is present and valid ; ; 1 = Valid Full License ; 2 = Valid Trial License ; 3 = Expired Trial ; 4 - Invalid full license ; ;; Procedure.l CheckLicense() 

 

  ;;

  ; It's invalid until proven otherwise.

  ;;

  ReturnValue.l = 3 

 

  RegStatus.l = WLRegGetStatus_(*extended)

 

  ; If it's a try then validate the trial

  If RegStatus = #wlIsTrial 

 

    TrialStatus.l = WLTrialGetStatus_(*reserved)

 

    Select TrialStatus 

 

      Case #wlTrialOk

        ReturnValue = 2

 

      Default 

        ReturnValue = 3

 

    EndSelect 

 

  Else

 

    If RegStatus = #wlIsRegistered

      ReturnValue = 1

    Else 

      ReturnValue = 4

    EndIf 

 

  EndIf

 

  ;ProcedureReturn ReturnValue 

 

;EndProcedure


; IDE Options = PureBasic v4.02 (Windows - x86)
; CursorPosition = 52
; FirstLine = 44
; Folding = -