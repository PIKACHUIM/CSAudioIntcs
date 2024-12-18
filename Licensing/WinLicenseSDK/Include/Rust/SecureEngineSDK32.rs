#[allow(dead_code)]
#[link(name="SecureEngineSDK32", kind="dylib")]
extern {
	#[link_name = "VMStart"]
    fn VM_START();

    #[link_name = "VMEnd"]
    fn VM_END();

    #[link_name = "StrEncryptStart"]
    fn STR_ENCRYPT_START();

    #[link_name = "StrEncryptEnd"]
    fn STR_ENCRYPT_END();

    #[link_name = "StrEncryptWStart"]
    fn STR_ENCRYPTW_START();

    #[link_name = "StrEncryptWEnd"]
    fn STR_ENCRYPTW_END();

    #[link_name = "UnprotectedStart"]
    fn UNPROTECTED_START();

    #[link_name = "UnprotectedEnd"]
    fn UNPROTECTED_END();

    #[link_name = "SECheckDebugger"]
    fn CHECK_DEBUGGER(var: &mut i32, val: i32);    

    #[link_name = "SECheckProtection"]
    fn CHECK_PROTECTION(var: &mut i32, val: i32);    

    #[link_name = "SECheckCodeIntegrity"]
    fn CHECK_CODE_INTEGRITY(var: &mut i32, val: i32);    

    #[link_name = "SECheckRegistration"]
    fn CHECK_REGISTRATION(var: &mut i32, val: i32);    

    #[link_name = "SECheckVirtualPC"]
    fn CHECK_VIRTUAL_PC(var: &mut i32, val: i32);    
}
