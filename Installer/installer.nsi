Unicode true
;--------------------------------
;Include Modern UI

!include "MUI2.nsh"

;--------------------------------
;General
Name "Microsoft SSIS Telegram Bot Task"
OutFile "MUI2_EXAMPLE.EXE"

;--------------------------------
;Defines
!define COPYRIGHT_TEXT "Copyright ${U+00A9} 2025 Rostislav Uralskyi"
!define MSSQL_KEY "SOFTWARE\Microsoft\Microsoft SQL Server"
!define SSIS_STATE_VAL_NAME "SQL_DTS_Full"

;Default installation folder
InstallDir "$LOCALAPPDATA\Modern UI Test"

;Request application privileges for Windows Vista
RequestExecutionLevel user

!define MUI_ICON "..\TelegramBotTask\TelegramBotTask.ico"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "InstallerLogo.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "UninstallerLogo.bmp"
!define MUI_BGCOLOR 051338
!define MUI_TEXTCOLOR FFFFFF

;--------------------------------
;Languages
!insertmacro MUI_LANGUAGE "English"

;--------------------------------
!define MUI_PAGE_HEADER_TEXT "Microsoft SSIS Telegram Bot Task"
!define MUI_PAGE_HEADER_SUBTEXT "${COPYRIGHT_TEXT}"

;!define MUI_WELCOMEPAGE_TITLE "Monkeys!!!"
;!define MUI_WELCOMEPAGE_TEXT "Welcome to the Monkey Chooser Installer!"
;Extra space for the title area
;!insertmacro MUI_WELCOMEPAGE_TITLE_3LINES

!define MUI_LICENSEPAGE_TEXT_TOP "License agreement:"
!define MUI_LICENSEPAGE_BUTTON "Continue"
!define MUI_LICENSEPAGE_CHECKBOX
!define MUI_LICENSEPAGE_CHECKBOX_TEXT "I accept the terms of this license agreement."

;!define MUI_COMPONENTSPAGE_TEXT_TOP "Select some Monkeys"
;!define MUI_COMPONENTSPAGE_TEXT_COMPLIST "Choose your Monkeys:"
;!define MUI_COMPONENTSPAGE_TEXT_INSTTYPE "Monkey List:"
!define MUI_COMPONENTSPAGE_NODESC
;!define MUI_COMPONENTSPAGE_TEXT_DESCRIPTION_TITLE "MUI_COMPONENTSPAGE_TEXT_DESCRIPTION_TITLE"
;!define MUI_COMPONENTSPAGE_TEXT_DESCRIPTION_INFO "MUI_COMPONENTSPAGE_TEXT_DESCRIPTION_INFO"

;!define MUI_DIRECTORYPAGE_TEXT_TOP "MUI_DIRECTORYPAGE_TEXT_TOP"
;!define MUI_DIRECTORYPAGE_TEXT_DESTINATION "MUI_DIRECTORYPAGE_TEXT_DESTINATION"
;!define MUI_DIRECTORYPAGE_VARIABLE $INSTDIR

!define MUI_INSTFILESPAGE_FINISHHEADER_TEXT "Everything is done."
!define MUI_INSTFILESPAGE_FINISHHEADER_SUBTEXT "Now there are monkeys loose. Sick sick monkeys."
!define MUI_INSTFILESPAGE_ABORTHEADER_TEXT "You didn't let it finish."
!define MUI_INSTFILESPAGE_ABORTHEADER_SUBTEXT "Patience is a virtue you know. I guess you aren't terribly virtuous."

!define MUI_FINISHPAGE_TITLE "Finished!"
;!define MUI_FINISHPAGE_TITLE_3LINES
!define MUI_FINISHPAGE_TEXT "Monkey Time!"
;Extra space for the text area (if using checkboxes).
;!define MUI_FINISHPAGE_TEXT_LARGE
!define MUI_FINISHPAGE_BUTTON "Booyah."
;!define MUI_FINISHPAGE_CANCEL_ENABLED
;!define MUI_FINISHPAGE_TEXT_REBOOT "MUI_FINISHPAGE_TEXT_REBOOT"
;!define MUI_FINISHPAGE_TEXT_REBOOTNOW "MUI_FINISHPAGE_TEXT_REBOOTNOW"
;!define MUI_FINISHPAGE_TEXT_REBOOTLATER "MUI_FINISHPAGE_TEXT_REBOOTLATER"
;!define MUI_FINISHPAGE_TEXT_REBOOTLATER_DEFAULT

;!define MUI_FINISHPAGE_RUN "some_exe_file"
;!define MUI_FINISHPAGE_RUN_TEXT "MUI_FINISHPAGE_RUN_TEXT"
;Parameters for the application to run. Don't forget to escape double quotes in the value (use $\").
;!define MUI_FINISHPAGE_RUN_PARAMETERS
;!define MUI_FINISHPAGE_RUN_NOTCHECKED
;!define MUI_FINISHPAGE_RUN_FUNCTION

!define MUI_FINISHPAGE_SHOWREADME "somefile.txt"
;Don't make this label too long or it'll cut on top and bottom.
!define MUI_FINISHPAGE_SHOWREADME_TEXT "This would open a README if there was one."
!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
;MUI_FINISHPAGE_SHOWREADME_FUNCTION Function

!define MUI_FINISHPAGE_LINK "This goes to reddit just because."
!define MUI_FINISHPAGE_LINK_LOCATION "http://www.reddit.com/"
;!define MUI_FINISHPAGE_LINK_COLOR RRGGBB

!define MUI_FINISHPAGE_NOREBOOTSUPPORT

;!define MUI_UNCONFIRMPAGE_TEXT_TOP "MUI_UNCONFIRMPAGE_TEXT_TOP"
;!define MUI_UNCONFIRMPAGE_TEXT_LOCATION "MUI_UNCONFIRMPAGE_TEXT_LOCATION"

;hide descriptions on hover
;!define MUI_COMPONENTSPAGE_NODESC

;--------------------------------
;Pages
;!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "license.txt"
!undef MUI_PAGE_HEADER_TEXT
!define MUI_PAGE_HEADER_TEXT "Select components to install"
!insertmacro MUI_PAGE_COMPONENTS
;!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

;!insertmacro MUI_UNPAGE_WELCOME
;!insertmacro MUI_UNPAGE_CONFIRM
;!insertmacro MUI_UNPAGE_INSTFILES
;!insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Installer Sections

SectionGroup /e "Microsoft SQL Server 2017" SSISTask2017
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2017
        ClearErrors
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2017
        ClearErrors
    SectionEnd
SectionGroupEnd

SectionGroup /e "Microsoft SQL Server 2019" SSISTask2019
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2019
        ClearErrors
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2019
        ClearErrors
    SectionEnd
SectionGroupEnd

SectionGroup /e "Microsoft SQL Server 2022" SSISTask2022
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2022
        ClearErrors
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2022
        ClearErrors
    SectionEnd
SectionGroupEnd

Function .onInit
    SetRegView 64

    ; In $R0 we create bitmask of installed SSIS versions:
	;   bit 0 - 2017
	;   bit 1 - 2019
	;   bit 2 - 2022
	StrCpy $R0 0
	
    ; Check for SSIS 2017
    ReadRegStr $R1 HKLM "${MSSQL_KEY}\140\ConfigurationState" ${SSIS_STATE_VAL_NAME}
    ${If} $R1 == "1"
		IntOp $R0 $R0 | 1
	${EndIf}

    ; Check for SSIS 2019
    ReadRegStr $R1 HKLM "${MSSQL_KEY}\150\ConfigurationState" ${SSIS_STATE_VAL_NAME}
	${If} $R1 == "1"
		IntOp $R0 $R0 | 2
	${EndIf}
	
    ; Check for SSIS 2022
    ReadRegStr $R1 HKLM "${MSSQL_KEY}\160\ConfigurationState" ${SSIS_STATE_VAL_NAME}
	${If} $R1 == "1"
		IntOp $R0 $R0 | 4
	${EndIf}

	; If none of supported versions installed
	${If} $R0 == "0"
        MessageBox MB_OK "Microsoft SQL Server 2017/2019/2022 Integration services is not installed. Installation aborted."
        Abort
    ${EndIf}
	
	; R2 = selected and readonly flags
    IntOp $R2 ${SF_SELECTED} | ${SF_RO}
	
	; Hide SQL Server 2017 group if it is not installed
	IntOp $R1 $R0 & 1
	${If} $R1 == "0"
		Push ${SSISTask2017}
		Call HideSectionGroup
	${Else}
		SectionSetFlags ${SSISTaskRuntime2017} $R2
	${EndIf}
	
	; Hide SQL Server 2019 group if it is not installed
	IntOp $R1 $R0 & 2
	${If} $R1 == "0"
		Push ${SSISTask2019}
		Call HideSectionGroup
	${Else}
		SectionSetFlags ${SSISTaskRuntime2019} $R2
	${EndIf}

	; Hide SQL Server 2022 group if it is not installed
	IntOp $R1 $R0 & 4
	${If} $R1 == "0"
		Push ${SSISTask2022}
		Call HideSectionGroup
	${Else}
		SectionSetFlags ${SSISTaskRuntime2022} $R2
	${EndIf}
FunctionEnd

Function .onSelChange
	Push $1 ; Temporary store for flags
	Push $2 ; Selected items counter
	Push $3 ; Selected and readonly flags
	
	; $3 = selected and readonly flags
    IntOp $3 ${SF_SELECTED} | ${SF_RO}

	; Manage flags
	${If} $0 == ${SSISTask2017}
	    SectionGetFlags $0 $1
        ${If} $1 & ${SF_SELECTED}
			SectionSetFlags ${SSISTaskRuntime2017} $3
			SectionSetFlags ${SSISTaskDesigntime2017} ${SF_SELECTED}
		${Else}
			SectionSetFlags ${SSISTaskRuntime2017} 0
			SectionSetFlags ${SSISTaskDesigntime2017} 0
		${EndIf}
	${ElseIf} $0 == ${SSISTaskDesigntime2017}
	    SectionGetFlags $0 $1
        ${If} $1 & ${SF_SELECTED}
			SectionSetFlags ${SSISTaskRuntime2017} $3
		${Else}
			SectionSetFlags ${SSISTaskRuntime2017} ${SF_SELECTED}
        ${EndIf}
	${ElseIf} $0 == ${SSISTask2019}
	    SectionGetFlags $0 $1
        ${If} $1 & ${SF_SELECTED}
			SectionSetFlags ${SSISTaskRuntime2019} $3
			SectionSetFlags ${SSISTaskDesigntime2019} ${SF_SELECTED}
		${Else}
			SectionSetFlags ${SSISTaskRuntime2019} 0
			SectionSetFlags ${SSISTaskDesigntime2019} 0
		${EndIf}
	${ElseIf} $0 == ${SSISTaskDesigntime2019}
	    SectionGetFlags $0 $1
        ${If} $1 & ${SF_SELECTED}
			SectionSetFlags ${SSISTaskRuntime2019} $3
		${Else}
			SectionSetFlags ${SSISTaskRuntime2019} ${SF_SELECTED}
        ${EndIf}
	${ElseIf} $0 == ${SSISTask2022}
	    SectionGetFlags $0 $1
        ${If} $1 & ${SF_SELECTED}
			SectionSetFlags ${SSISTaskRuntime2022} $3
			SectionSetFlags ${SSISTaskDesigntime2022} ${SF_SELECTED}
		${Else}
			SectionSetFlags ${SSISTaskRuntime2022} 0
			SectionSetFlags ${SSISTaskDesigntime2022} 0
		${EndIf}
	${ElseIf} $0 == ${SSISTaskDesigntime2022}
	    SectionGetFlags $0 $1
        ${If} $1 & ${SF_SELECTED}
			SectionSetFlags ${SSISTaskRuntime2022} $3
		${Else}
			SectionSetFlags ${SSISTaskRuntime2022} ${SF_SELECTED}
        ${EndIf}
	${EndIf}

	; Count selected items (dumb, but working method)
	StrCpy $2 0
	SectionGetFlags ${SSISTaskRuntime2017} $1
    ${If} $1 & ${SF_SELECTED}
		IntOp $2 $2 + 1
	${EndIf}
	SectionGetFlags ${SSISTaskDesignTime2017} $1
    ${If} $1 & ${SF_SELECTED}
		IntOp $2 $2 + 1
	${EndIf}
	SectionGetFlags ${SSISTaskRuntime2019} $1
    ${If} $1 & ${SF_SELECTED}
		IntOp $2 $2 + 1
	${EndIf}
	SectionGetFlags ${SSISTaskDesignTime2019} $1
    ${If} $1 & ${SF_SELECTED}
		IntOp $2 $2 + 1
	${EndIf}
	SectionGetFlags ${SSISTaskRuntime2022} $1
    ${If} $1 & ${SF_SELECTED}
		IntOp $2 $2 + 1
	${EndIf}
	SectionGetFlags ${SSISTaskDesignTime2022} $1
    ${If} $1 & ${SF_SELECTED}
		IntOp $2 $2 + 1
	${EndIf}

	; Disable "Install" button if none selected
	${If} $2 == 0
        GetDlgItem $0 $HWNDPARENT 1  ; Get handle for "Install" button
        EnableWindow $0 0  ; Disable button
    ${Else}
        GetDlgItem $0 $HWNDPARENT 1
        EnableWindow $0 1  ; Enable button
    ${EndIf}

	Pop $3
	Pop $2
	Pop $1
FunctionEnd

Function HideSectionGroup
    Exch $0  ; $0 = Input: Section Group ID
    Push $1  ; Stores section flags

    ${While} 1 == 1
        SectionGetFlags $0 $1
	    SectionSetFlags $0 0
		SectionSetText $0 "" 
        ${If} $1 & ${SF_SECGRPEND}
            ${ExitWhile}
        ${EndIf}
        IntOp $0 $0 + 1
    ${EndWhile}

    Pop $1
    Pop $0
FunctionEnd

;--------------------------------
;Uninstaller Section
;
;Section "Uninstall"
;
;  ;ADD YOUR OWN STUFF HERE...
;
;  ;Delete "$INSTDIR\Uninstall.exe"
;
;  ;RMDir "$INSTDIR"
;
;  ;DeleteRegKey /ifempty HKCU "Software\Modern UI Test"
;
;SectionEnd
