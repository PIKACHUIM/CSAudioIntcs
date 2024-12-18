// ActivationTestEXE.cpp: define el punto de entrada de la aplicación de consola.
//

#include "stdafx.h"
#include <Windows.h>
#include "..\ActivationSDK.h"
#include <WinSock.h>

extern "C" __declspec(dllexport) bool ActivationHandler(int  EventId, int  ActivationErrorCode,
										int  WinsockErrorCode, char *ServerResponseString, char *pActivationKey);

int _tmain(int argc, _TCHAR* argv[])
{
	char server_response[1024];
	char activation_key[256];

	ActivationHandler(EVENT_SHOW_ACTIVATION, 0, 0, server_response, activation_key);
	MessageBoxA(NULL, activation_key, "Activation Key", MB_OK);

	ActivationHandler(EVENT_CHECK_RESPONSE, RESPONSE_ERROR_KEY_NOT_FOUND, 0, server_response, activation_key);
	return 0;
}

