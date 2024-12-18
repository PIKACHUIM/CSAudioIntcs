#[cfg(target_pointer_width = "32")]
include!("SecureEngineSDK32.rs");

#[cfg(target_pointer_width = "32")]
include!("SecureEngineSDK32_CustomVMs.rs");

#[cfg(target_pointer_width = "64")]
include!("SecureEngineSDK64.rs");

#[cfg(target_pointer_width = "64")]
include!("SecureEngineSDK64_CustomVMs.rs");