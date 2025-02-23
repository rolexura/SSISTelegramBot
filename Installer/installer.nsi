Unicode true
;--------------------------------
;Includes
!include "x64.nsh"
!include "MUI2.nsh"

;--------------------------------
;General
Name "Microsoft SSIS Telegram Bot Task"
OutFile "MUI2_EXAMPLE.EXE"

;--------------------------------
;Defines
!define COPYRIGHT_TEXT "Copyright ${U+00A9} 2025 Rostislav Uralskyi"
!define MSSQL_KEY "SOFTWARE\Microsoft\Microsoft SQL Server"
!define MSSQL_ROOT_DIR_VAL_NAME "VerSpecificRootDir"
!define SSIS_STATE_VAL_NAME "SQL_DTS_Full"
!define DTS_CONN_PATH "DTS\Connections"
!define DTS_TASK_PATH "DTS\Tasks"

;Default installation folder
InstallDir "$PROGRAMFILES64\SSIS Telegram Bot Task"

;Request application privileges for Windows Vista
RequestExecutionLevel admin

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

!define MUI_LICENSEPAGE_TEXT_TOP "License agreement:"
!define MUI_LICENSEPAGE_BUTTON "Continue"
!define MUI_LICENSEPAGE_CHECKBOX
!define MUI_LICENSEPAGE_CHECKBOX_TEXT "I accept the terms of this license agreement."

!define MUI_COMPONENTSPAGE_NODESC

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

;!define MUI_FINISHPAGE_NOREBOOTSUPPORT

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
!undef MUI_PAGE_HEADER_TEXT
!define MUI_PAGE_HEADER_TEXT "Select destination folder"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

;!insertmacro MUI_UNPAGE_WELCOME
;!insertmacro MUI_UNPAGE_CONFIRM
;!insertmacro MUI_UNPAGE_INSTFILES
;!insertmacro MUI_UNPAGE_FINISH

; Install paths for all supported SQL Server versions
Var SQL2017InstallPath32
Var SQL2017InstallPath64
Var SQL2019InstallPath32
Var SQL2019InstallPath64
Var SQL2022InstallPath32
Var SQL2022InstallPath64

;---------------------------------------------------------------------------
; Macro to generate designtime section contents for given SQL server version
!macro InstallDesignTimeFilesForVersion VERSION
  ; Install files for 64-bit SQL Server
  SetOutPath "$SQL${VERSION}InstallPath64${DTS_CONN_PATH}"
  File "..\TelegramBotConnectionManager\bin\Release-${VERSION}\XBase.TelegramBotConnectionManager.dll"
  File "..\TelegramBotConnectionManagerUI\bin\Release-${VERSION}\XBase.TelegramBotConnectionManager.UI.dll"
  SetOutPath "$SQL${VERSION}InstallPath64${DTS_TASK_PATH}"
  File "..\TelegramBotTask\bin\Release-${VERSION}\XBase.TelegramBotTask.dll"
  File "..\TelegramBotTaskUI\bin\Release-${VERSION}\XBase.TelegramBotTask.UI.dll"

  ; Install files for 32-bit SQL Server
  SetOutPath "$SQL${VERSION}InstallPath32${DTS_CONN_PATH}"
  File "..\TelegramBotConnectionManager\bin\Release-${VERSION}\XBase.TelegramBotConnectionManager.dll"
  File "..\TelegramBotConnectionManagerUI\bin\Release-${VERSION}\XBase.TelegramBotConnectionManager.UI.dll"
  SetOutPath "$SQL${VERSION}InstallPath32${DTS_TASK_PATH}"
  File "..\TelegramBotTask\bin\Release-${VERSION}\XBase.TelegramBotTask.dll"
  File "..\TelegramBotTaskUI\bin\Release-${VERSION}\XBase.TelegramBotTask.UI.dll"
!macroend

;--------------------------------
; Installer Sections
SectionGroup /e "Microsoft SQL Server 2017" SSISTask2017
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2017
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2017
        !insertmacro InstallDesignTimeFilesForVersion "2017"
    SectionEnd
SectionGroupEnd

SectionGroup /e "Microsoft SQL Server 2019" SSISTask2019
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2019
        ClearErrors
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2019
        !insertmacro InstallDesignTimeFilesForVersion "2019"
    SectionEnd
SectionGroupEnd

SectionGroup /e "Microsoft SQL Server 2022" SSISTask2022
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2022
        ClearErrors
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2022
        !insertmacro InstallDesignTimeFilesForVersion "2022"
    SectionEnd
SectionGroupEnd

Function .onInit
    ${IfNot} ${RunningX64}
        MessageBox MB_ICONSTOP|MB_OK "This installer is for 64-bit systems only. Installation aborted."
        Abort
    ${EndIf}

    Call CheckForPowerShellInstalled
    Pop $R0
    ${If} $R0 == "0"
        MessageBox MB_OK "PowerShell is not present or not functional."
        Abort
    ${EndIf}    

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
        MessageBox MB_ICONSTOP|MB_OK "Microsoft SQL Server 2017/2019/2022 Integration services is not installed. Installation aborted."
        Abort
    ${EndIf}
    
    ; R2 = selected and readonly flags
    IntOp $R2 ${SF_SELECTED} | ${SF_RO}
    

    ; Hide SQL Server 2017 group if it is not installed and get paths to it (32 and 64 bit) if installed
    ; Todo convert to macro
    IntOp $R1 $R0 & 1
    ${If} $R1 == "0"
        Push ${SSISTask2017}
        Call HideSectionGroup
    ${Else}
        SetRegView 32
        ReadRegStr $SQL2017InstallPath32 HKLM "${MSSQL_KEY}\140" ${MSSQL_ROOT_DIR_VAL_NAME}
        SetRegView 64
        ReadRegStr $SQL2017InstallPath64 HKLM "${MSSQL_KEY}\140" ${MSSQL_ROOT_DIR_VAL_NAME}
        SectionSetFlags ${SSISTaskRuntime2017} $R2
    ${EndIf}
    
    ; Hide SQL Server 2019 group if it is not installed and get paths to it (32 and 64 bit) if installed
    IntOp $R1 $R0 & 2
    ${If} $R1 == "0"
        Push ${SSISTask2019}
        Call HideSectionGroup
    ${Else}
        SetRegView 32
        ReadRegStr $SQL2019InstallPath32 HKLM "${MSSQL_KEY}\150" ${MSSQL_ROOT_DIR_VAL_NAME}
        SetRegView 64
        ReadRegStr $SQL2019InstallPath64 HKLM "${MSSQL_KEY}\150" ${MSSQL_ROOT_DIR_VAL_NAME}
        SectionSetFlags ${SSISTaskRuntime2019} $R2
    ${EndIf}

    ; Hide SQL Server 2022 group if it is not installed and get paths to it (32 and 64 bit) if installed
    IntOp $R1 $R0 & 4
    ${If} $R1 == "0"
        Push ${SSISTask2022}
        Call HideSectionGroup
    ${Else}
        SetRegView 32
        ReadRegStr $SQL2022InstallPath32 HKLM "${MSSQL_KEY}\160" ${MSSQL_ROOT_DIR_VAL_NAME}
        SetRegView 64
        ReadRegStr $SQL2022InstallPath64 HKLM "${MSSQL_KEY}\160" ${MSSQL_ROOT_DIR_VAL_NAME}
        SectionSetFlags ${SSISTaskRuntime2022} $R2
    ${EndIf}
FunctionEnd

!macro HandleSSISTaskSelection VERSION
  ${If} $0 == ${SSISTask${VERSION}}
    SectionGetFlags $0 $R1
    ${If} $R1 & ${SF_SELECTED}
      SectionSetFlags ${SSISTaskRuntime${VERSION}} $R2
      SectionSetFlags ${SSISTaskDesigntime${VERSION}} ${SF_SELECTED}
    ${Else}
      SectionSetFlags ${SSISTaskRuntime${VERSION}} 0
      SectionSetFlags ${SSISTaskDesigntime${VERSION}} 0
    ${EndIf}
  ${ElseIf} $0 == ${SSISTaskDesigntime${VERSION}}
    SectionGetFlags $0 $R1
    ${If} $R1 & ${SF_SELECTED}
      SectionSetFlags ${SSISTaskRuntime${VERSION}} $R2
    ${Else}
      SectionSetFlags ${SSISTaskRuntime${VERSION}} ${SF_SELECTED}
    ${EndIf}
  ${EndIf}
!macroend

Function .onSelChange
    ; In $0 we have an ID of changed section
    ; $R1 - Temporary store for flags
    ; $R2 - Selected and readonly flags

    IntOp $R2 ${SF_SELECTED} | ${SF_RO}

    ; Manage flags
    !insertmacro HandleSSISTaskSelection "2017"
    !insertmacro HandleSSISTaskSelection "2019"
    !insertmacro HandleSSISTaskSelection "2022"

    ; Disable "Install" button if none selected
    Call GetNumberOfSelectedSections
    Pop $R0
    GetDlgItem $R1 $HWNDPARENT 1  ; Get handle for "Install" button
    ${If} $R0 == 0
        EnableWindow $R1 0  ; Disable button
    ${Else}
        EnableWindow $R1 1  ; Enable button
    ${EndIf}
FunctionEnd

Function HideSectionGroup
    Exch $0  ; $0 = Input: Section Group ID

    ${Do}
        SectionGetFlags $0 $R1
        SectionSetFlags $0 0
        SectionSetText $0 "" 
        ${If} $R1 & ${SF_SECGRPEND}
            ${Break}
        ${EndIf}
        IntOp $0 $0 + 1
    ${Loop}

    Pop $0
FunctionEnd

Function GetNumberOfSelectedSections
    StrCpy $R0 0  ; Initialize the counter of active sections in $0
    StrCpy $R2 0  ; Initialize the counter of sections in $2

    ; Loop through sections until an invalid one is found
    ${Do}
        StrCpy $R1 ""  ; Initialize $1 to a known value (empty string)
        SectionGetFlags $R2 $R1  ; Get the flags of the current section
        ${If} $R1 == ""  ; If $1 is unchanged, the section is invalid
            ${Break}  ; Exit the loop if the section is invalid
        ${EndIf}
        ${If} $R1 & ${SF_SELECTED}
        ${AndIfNot} $R1 & ${SF_SECGRPEND}
            IntOp $R0 $R0 + 1 ; Increment the counter of active sections
        ${EndIf}
        IntOp $R2 $R2 + 1  ; Increment the counter
    ${Loop}

    Push $R0 ; Push the result (number of active sections) onto the stack
FunctionEnd

; Returns the Powershell version or 0 if Powershell is not installed
Function CheckForPowerShellInstalled
    ; Internal variables:
    ; $R0 = Path to powershell.exe
    ; $R1 = High part of the file version
    ; $R2 = Low part of the file version
    ; $R3 = Exit code from PowerShell command
    ; $R4 = Temporary variable for version string

    ; Set the path to powershell.exe
    StrCpy $R0 "$SYSDIR\WindowsPowerShell\v1.0\powershell.exe"

    ; Check if powershell.exe exists
    ${If} ${FileExists} "$R0"
        ; Get the file version of powershell.exe
        GetDLLVersion "$R0" $R1 $R2
        IntOp $R4 $R1 / 0x00010000  ; Extract major version
        IntOp $R5 $R1 & 0x0000FFFF  ; Extract minor version
        IntOp $R6 $R2 / 0x00010000  ; Extract build version
        IntOp $R7 $R2 & 0x0000FFFF  ; Extract revision version
        StrCpy $R0 "$R4.$R5.$R6.$R7"  ; Combine into version string
    ${Else}
        StrCpy $R0 "0"  ; PowerShell not found
    ${EndIf}

    Push $R0  ; Return the result in $R0
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
