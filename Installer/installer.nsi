Unicode true
;--------------------------------
;Includes
!include "x64.nsh"
!include "MUI2.nsh"

;--------------------------------
;Defines
!define PRODUCT_NAME "Microsoft SSIS Telegram Bot Task"
!define PRODUCT_VERSION "1.0.0"
!define PRODUCT_AUTHOR "Rostislav Uralskyi"
!define COPYRIGHT_TEXT "Copyright ${U+00A9} 2025 ${PRODUCT_AUTHOR}"
!define MSSQL_KEY "SOFTWARE\Microsoft\Microsoft SQL Server"
!define MSSQL_ROOT_DIR_VAL_NAME "VerSpecificRootDir"
!define SSIS_STATE_VAL_NAME "SQL_DTS_Full"
!define DTS_CONN_PATH "DTS\Connections"
!define DTS_TASK_PATH "DTS\Tasks"
!define PUBLIC_KEY_TOKEN "54ddf9908a8304bd"
!define BIN_PATH "Release"

;--------------------------------
;General
Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "SSIS_Telegram_Bot_Task_1_0_Setup.exe"
SetCompressor /FINAL /SOLID lzma
;Request application privileges for Windows Vista
RequestExecutionLevel admin

;Default installation folder
InstallDir "$PROGRAMFILES64\SSIS Telegram Bot Task"


!define MUI_ICON "..\TelegramBotTask\TelegramBotTask.ico"
!define MUI_UNICON "..\TelegramBotTask\TelegramBotTask.ico"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "InstallerLogo.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "UninstallerLogo.bmp"
!define MUI_BGCOLOR 051338
!define MUI_TEXTCOLOR FFFFFF

;--------------------------------
!define MUI_PAGE_HEADER_TEXT "Microsoft SSIS Telegram Bot Task"
!define MUI_PAGE_HEADER_SUBTEXT "${COPYRIGHT_TEXT}"

!define MUI_LICENSEPAGE_TEXT_TOP "License agreement:"
!define MUI_LICENSEPAGE_BUTTON "Continue"
!define MUI_LICENSEPAGE_CHECKBOX
!define MUI_LICENSEPAGE_CHECKBOX_TEXT "I accept the terms of this license agreement."

!define MUI_COMPONENTSPAGE_NODESC

!define MUI_INSTFILESPAGE_FINISHHEADER_TEXT "Installation completed successfully."
!define MUI_INSTFILESPAGE_FINISHHEADER_SUBTEXT "${COPYRIGHT_TEXT}"
!define MUI_INSTFILESPAGE_ABORTHEADER_TEXT "Installation not finished."
!define MUI_INSTFILESPAGE_ABORTHEADER_SUBTEXT "${COPYRIGHT_TEXT}"

;--------------------------------
;Pages
;!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "license.txt"
!define MUI_PAGE_HEADER_TEXT "Select components to install"
!insertmacro MUI_PAGE_COMPONENTS
!define MUI_PAGE_HEADER_TEXT "Select destination folder"
!insertmacro MUI_PAGE_DIRECTORY
!define MUI_PAGE_HEADER_TEXT "Installation in progress${U+2026}"
!insertmacro MUI_PAGE_INSTFILES
;!insertmacro MUI_PAGE_FINISH

;!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
;Languages
!insertmacro MUI_LANGUAGE "English"

; Install paths for all supported SQL Server versions
Var SQL2017InstallPath32
Var SQL2017InstallPath64
Var SQL2019InstallPath32
Var SQL2019InstallPath64
Var SQL2022InstallPath32
Var SQL2022InstallPath64

;---------------------------------------------------------------------------
; Macro to generate runtime section contents for given SQL server version
!macro InstallRuntimeFilesForVersion VERSION
    SetOutPath "$INSTDIR\${VERSION}"
    File "..\TelegramBotConnectionManager\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotConnectionManager.dll"
    File "..\TelegramBotTask\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotTask.dll"
    ExecWait '"$INSTDIR\GacInstaller.exe" "$INSTDIR\${VERSION}\XBase.TelegramBotConnectionManager.dll"'
    ExecWait '"$INSTDIR\GacInstaller.exe" "$INSTDIR\${VERSION}\XBase.TelegramBotTask.dll"'
    Delete "$INSTDIR\${VERSION}\*.*" ; This files are installed in GAC and not needed more
    SetOutPath "$INSTDIR"  ; We cannot remove current working directory, so we must change it
    RMDir "$INSTDIR\${VERSION}"
!macroend   

;---------------------------------------------------------------------------
; Macro to generate designtime section contents for given SQL server version
!macro InstallDesignTimeFilesForVersion VERSION
    ; Install files for 64-bit SQL Server
    SetOutPath "$SQL${VERSION}InstallPath64${DTS_CONN_PATH}"
    File "..\TelegramBotConnectionManagerUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotConnectionManager.dll"
    File "..\TelegramBotConnectionManagerUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotConnectionManager.UI.dll"
    SetOutPath "$SQL${VERSION}InstallPath64${DTS_TASK_PATH}"
    File "..\TelegramBotTaskUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotTask.dll"
    File "..\TelegramBotTaskUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotTask.UI.dll"

    ; Install files for 32-bit SQL Server
    SetOutPath "$SQL${VERSION}InstallPath32${DTS_CONN_PATH}"
    File "..\TelegramBotConnectionManagerUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotConnectionManager.dll"
    File "..\TelegramBotConnectionManagerUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotConnectionManager.UI.dll"
    
    SetOutPath "$SQL${VERSION}InstallPath32${DTS_TASK_PATH}"
    File "..\TelegramBotTaskUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotTask.dll"
    File "..\TelegramBotTaskUI\bin\${BIN_PATH}-${VERSION}\XBase.TelegramBotTask.UI.dll"

    ExecWait '"$INSTDIR\GacInstaller.exe" "$SQL${VERSION}InstallPath64${DTS_CONN_PATH}\XBase.TelegramBotConnectionManager.UI.dll"'
    ExecWait '"$INSTDIR\GacInstaller.exe" "$SQL${VERSION}InstallPath64${DTS_TASK_PATH}\XBase.TelegramBotTask.UI.dll"'
!macroend

!macro DeleteFilesForVersion VERSION
    Delete "$SQL${VERSION}InstallPath32${DTS_CONN_PATH}\XBase.TelegramBotConnectionManager.dll"
    Delete "$SQL${VERSION}InstallPath64${DTS_CONN_PATH}\XBase.TelegramBotConnectionManager.UI.dll"
    
    Delete "$SQL${VERSION}InstallPath32${DTS_CONN_PATH}\XBase.TelegramBotConnectionManager.dll"
    Delete "$SQL${VERSION}InstallPath64${DTS_CONN_PATH}\XBase.TelegramBotConnectionManager.UI.dll"

    Delete "$SQL${VERSION}InstallPath32${DTS_TASK_PATH}\XBase.TelegramBotTask.dll"
    Delete "$SQL${VERSION}InstallPath32${DTS_TASK_PATH}\XBase.TelegramBotTask.UI.dll"

    Delete "$SQL${VERSION}InstallPath64${DTS_TASK_PATH}\XBase.TelegramBotTask.dll"
    Delete "$SQL${VERSION}InstallPath64${DTS_TASK_PATH}\XBase.TelegramBotTask.UI.dll"

    ExecWait "$INSTDIR\GacInstaller.exe /u XBase.TelegramBotConnectionManager,Version=${PRODUCT_VERSION}.${VERSION},PublicKeyToken=${PUBLIC_KEY_TOKEN}"
    ExecWait "$INSTDIR\GacInstaller.exe /u XBase.TelegramBotTask,Version=${PRODUCT_VERSION}.${VERSION},PublicKeyToken=${PUBLIC_KEY_TOKEN}"
    ExecWait "$INSTDIR\GacInstaller.exe /u XBase.TelegramBotConnectionManager.UI,Version=${PRODUCT_VERSION}.${VERSION},PublicKeyToken=${PUBLIC_KEY_TOKEN}"
    ExecWait "$INSTDIR\GacInstaller.exe /u XBase.TelegramBotTask.UI,Version=${PRODUCT_VERSION}.${VERSION},PublicKeyToken=${PUBLIC_KEY_TOKEN}"
!macroend

; VERSION_INTERNAL - "Internal" MS SQL version number ("140"/"150/"160")
; VERSION_FLAG - bit mask for version (1 for 2017, 2 for 2019, 4 for 2022)
!macro CheckForSQLVersion VERSION_INTERNAL VERSION_FLAG
    ReadRegStr $R1 HKLM "${MSSQL_KEY}\${VERSION_INTERNAL}\ConfigurationState" ${SSIS_STATE_VAL_NAME}
    ${If} $R1 == "1"
        IntOp $R0 $R0 | ${VERSION_FLAG}
    ${EndIf}
!macroend

; VERSION - "Official" MS SQL version number ("2017"/"2019"/"2022")
; VERSION_INTERNAL - "Internal" MS SQL version number ("140"/"150/"160")
!macro ReadSQLInstallPath VERSION VERSION_INTERNAL
    ; Read 32-bit registry path
    SetRegView 32
    ReadRegStr $0 HKLM "${MSSQL_KEY}\${VERSION_INTERNAL}" ${MSSQL_ROOT_DIR_VAL_NAME}
    StrCpy $SQL${VERSION}InstallPath32 $0  ; Store in version-specific variable

    ; Read 64-bit registry path
    SetRegView 64
    ReadRegStr $0 HKLM "${MSSQL_KEY}\${VERSION_INTERNAL}" ${MSSQL_ROOT_DIR_VAL_NAME}
    StrCpy $SQL${VERSION}InstallPath64 $0
!macroend

; VERSION - "Official" MS SQL version number ("2017"/"2019"/"2022")
; VERSION_INTERNAL - "Internal" MS SQL version number ("140"/"150/"160")
; VERSION_FLAG - bit mask for version (1 for 2017, 2 for 2019, 4 for 2022)
!macro InitForSQLVersion VERSION VERSION_INTERNAL VERSION_FLAG
    IntOp $R1 $R0 & ${VERSION_FLAG}
    ${If} $R1 == "0"
        Push ${SSISTask${VERSION}}
        Call HideSectionGroup
    ${Else}
        !insertmacro ReadSQLInstallPath "${VERSION}" "${VERSION_INTERNAL}"
        SectionSetFlags ${SSISTaskRuntime${VERSION}} $R2
    ${EndIf}
!macroend

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

;--------------------------------
; Installer Sections
Section -HelperFiles
    ;DetailPrint "Installing helper files..."
    SetOutPath "$INSTDIR"
    File "..\GACInstaller\bin\Release\GACInstaller.exe"
SectionEnd

SectionGroup /e "Microsoft SQL Server 2017" SSISTask2017
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2017
        !insertmacro InstallRuntimeFilesForVersion "2017"
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2017
        !insertmacro InstallDesignTimeFilesForVersion "2017"
    SectionEnd
SectionGroupEnd

SectionGroup /e "Microsoft SQL Server 2019" SSISTask2019
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2019
        !insertmacro InstallRuntimeFilesForVersion "2019"
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2019
        !insertmacro InstallDesignTimeFilesForVersion "2019"
    SectionEnd
SectionGroupEnd

SectionGroup /e "Microsoft SQL Server 2022" SSISTask2022
    Section "Telegram Bot SSIS Task" SSISTaskRuntime2022
        !insertmacro InstallRuntimeFilesForVersion "2022"
    SectionEnd

    Section "Telegram Bot SSIS Task (Design-time)" SSISTaskDesignTime2022
        !insertmacro InstallDesignTimeFilesForVersion "2022"
    SectionEnd
SectionGroupEnd

Section -"Install"
    SetOutPath "$INSTDIR"
    WriteUninstaller "$INSTDIR\uninstall.exe"
    
    ; Register in Add/Remove Programs
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask" "DisplayName" "${PRODUCT_NAME}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask" "UninstallString" "$INSTDIR\uninstall.exe"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask" "InstallLocation" "$INSTDIR"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask" "Publisher" "${PRODUCT_AUTHOR}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask" "DisplayVersion" "${PRODUCT_VERSION}"
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask" "NoModify" 1
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask" "NoRepair" 1
SectionEnd

;--------------------------------
; Uninstaller Section
;
Section "Uninstall"
    ${IfNot} ${FileExists} "$INSTDIR\GacInstaller.exe"
        MessageBox MB_ICONSTOP|MB_OK "Installation corrupted. Uninstaller aborted."
        DetailPrint "$INSTDIR\GacInstaller.exe not found."
        Abort
    ${EndIf}

    ; Detect SQL server instances paths
    !insertmacro ReadSQLInstallPath "2017" "140"
    !insertmacro ReadSQLInstallPath "2019" "150"
    !insertmacro ReadSQLInstallPath "2022" "160"

    ; Remove designtime files
    !insertmacro DeleteFilesForVersion "2017"
    !insertmacro DeleteFilesForVersion "2019"
    !insertmacro DeleteFilesForVersion "2022"

    Delete "$INSTDIR\GacInstaller.exe"
    Delete "$INSTDIR\uninstall.exe"

    ; Remove directory
    RMDir "$INSTDIR"
    ; Remove Add/Remove Programs entry
    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SSISTelegramBotTask"
SectionEnd

;--------------------------------
; Functions
;
Function .onInit
    ${IfNot} ${RunningX64}
        MessageBox MB_ICONSTOP|MB_OK "This product is for 64-bit systems only. Installation aborted."
        Abort
    ${EndIf}

    SetRegView 64

    ; In $R0 we create bitmask of installed SSIS versions
    StrCpy $R0 0
    
    !insertmacro CheckForSQLVersion "140" 1
    !insertmacro CheckForSQLVersion "150" 2
    !insertmacro CheckForSQLVersion "160" 4

    ; If none of supported versions installed
    ${If} $R0 == "0"
        MessageBox MB_ICONSTOP|MB_OK "Microsoft SQL Server 2017/2019/2022 Integration services is not installed. Installation aborted."
        Abort
    ${EndIf}
    
    ; R2 = selected and readonly flags
    IntOp $R2 ${SF_SELECTED} | ${SF_RO}
    
    ; Hide product group if it is not installed / get paths to it (32 and 64 bit) if installed
    !insertmacro InitForSQLVersion "2017" "140" 1
    !insertmacro InitForSQLVersion "2019" "150" 2
    !insertmacro InitForSQLVersion "2022" "160" 4
FunctionEnd

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
