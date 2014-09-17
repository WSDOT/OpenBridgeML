#pragma once

#if !defined (BUILDOPENBRIDGEMLSDK)

#if defined _DEBUG
#define SDK_AUTOLIBNAME "OpenBridgeMLD.lib"
#else
#define SDK_AUTOLIBNAME "OpenBridgeML.lib"
#endif

#pragma comment(lib,SDK_AUTOLIBNAME)
#if defined AUTOLIB
#pragma message("Linking with " SDK_AUTOLIBNAME )
#endif

#endif // BUILDOPENBRIDGEMLSDK
