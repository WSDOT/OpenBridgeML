#pragma once

// Define BUILDOPENBRIDGEMLSDK when building this library
// For static builds, define OPENBRIDGEMLSDKLIB
// For static binding, define OPENBRIDGEMLSDKLIB
// For dynamic binding, nothing is required to be defined

#if defined (BUILDOPENBRIDGEMLSDK) && !defined(OPENBRIDGEMLSDKLIB)
#define OPENBRIDGEMLSDK __declspec(dllexport)
#elif defined(OPENBRIDGEMLSDKLIB)
#define OPENBRIDGEMLSDK
#else
#define OPENBRIDGEMLSDK __declspec(dllimport)
#endif

#include <AutoLib.h>
