 
; Copyright (c) 2010, Direct Project
; All rights reserved.
;
; Authors:
;    Joseph Shook    JoeShook@Gmail.com
;   
; 
;Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
;
;Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
;Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
;Neither the name of The Direct Project (directproject.org) nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
;THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 


; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#include "InnoScripts\IISUtils.iss"
#include "InnoScripts\VcRuntimeInstalled.iss"


[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
ArchitecturesInstallIn64BitMode=x64 ia64
AppId={{995D337A-5620-4537-9704-4B19EC628A39}
AppName=Direct Project .NET Gateway
AppVerName=Direct Project .NET Gateway 1.0.0.0
AppPublisher=The Direct Project (nhindirect.org)
AppPublisherURL=http://nhindirect.org
AppSupportURL=http://nhindirect.org
AppUpdatesURL=http://nhindirect.org
DefaultDirName={pf}\Direct Project .NET Gateway
DefaultGroupName=Direct Project .NET Gateway
AllowNoIcons=yes
OutputDir=.
OutputBaseFilename=Direct-1.0.0.0-NET35
Compression=lzma
SolidCompression=yes
VersionInfoVersion=1.0.0.0
SetupLogging=yes

WizardImageFile=Direct.bmp
WizardSmallImageFile=DirectSmall.bmp
WizardImageStretch=Yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Components]
Name: dnsresponder; Description: Install the DNS Responder; Types: development dns; 
Name: dnswebservice; Description: DNS Web service; Types: development config; 
Name: configwebservice; Description: Config Web services; Types: development config; 
Name: configui; Description: UI Web Admin; Types: development config;
Name: directgateway; Description: Gateway to SMTP; Types: development gateway; 
Name: developergateway; Description: Developer gateway configuration to SMTP; Types: development; 
Name: database; Description: DirectConfig database; Types: development database;


[Types]
Name: gateway; Description: Gateway               
Name: dns; Description: DNS Responder; 
Name: config; Description: Config services             
Name: database; Description: Database
Name: custom; Description: Custom Install; Flags: iscustom;
Name: development; Description: Developer Install (Single machine and development gateway version)


[Files]
Source: "..\bin\debug\*.dll"; DestDir: "{app}"; Flags: ignoreversion;  Components: dnsresponder dnswebservice configwebservice configui directgateway developergateway; 
Source: "..\bin\debug\Win32\smtpEventHandler.dll"; DestDir: "{app}"; Flags: ignoreversion; Check: IsX86;  Components: dnsresponder dnswebservice configwebservice configui directgateway developergateway; 
Source: "..\bin\debug\x64\smtpEventHandler.dll"; DestDir: "{app}"; Flags: ignoreversion; Check: IsX64 or IsIA64; Components: dnsresponder dnswebservice configwebservice configui directgateway developergateway;                            
Source: "..\bin\debug\*.config"; DestDir: "{app}"; Excludes: "*.vshost.*,*.dll.config"; Flags: ignoreversion; Components: dnsresponder dnswebservice configwebservice configui directgateway developergateway; 
Source: "..\bin\debug\*.exe"; DestDir: "{app}"; Excludes: "*.vshost.*"; Flags: ignoreversion; Components: dnsresponder dnswebservice configwebservice configui directgateway developergateway; 
Source: "..\bin\debug\Certificates\*"; DestDir: "{app}\Certificates"; Flags: ignoreversion recursesubdirs;   Components: developergateway; 
Source: "..\bin\debug\ConfigConsoleSettings.xml"; DestDir: "{app}"; Flags: ignoreversion; Components: developergateway

Source: "..\config\service\*.svc"; DestDir: "{app}\ConfigService"; Flags: ignoreversion; Components: configwebservice; 
Source: "..\config\service\*.aspx"; DestDir: "{app}\ConfigService"; Flags: ignoreversion; Components: configwebservice; 
Source: "..\config\service\*.config"; DestDir: "{app}\ConfigService"; Flags: ignoreversion; Components: configwebservice; 
Source: "..\config\service\bin\*.dll"; DestDir: "{app}\ConfigService\bin"; Flags: ignoreversion recursesubdirs; Components: configwebservice; 

Source: "..\dnsresponder.service\*.svc"; DestDir: "{app}\DnsService"; Flags: ignoreversion; Components: dnswebservice; 
Source: "..\dnsresponder.service\*.aspx"; DestDir: "{app}\DnsService"; Flags: ignoreversion; Components: dnswebservice; 
Source: "..\dnsresponder.service\*.config"; DestDir: "{app}\DnsService"; Flags: ignoreversion; Components: dnswebservice; 
Source: "..\dnsresponder.service\bin\*.dll"; DestDir: "{app}\DnsService\bin"; Flags: ignoreversion recursesubdirs; Components: dnswebservice; 

Source: "configui\*"; DestDir: "{app}\ConfigUI"; Flags: ignoreversion recursesubdirs; Components: configui; 

Source: "..\gateway\install\*.vbs"; DestDir: "{app}"; Flags: ignoreversion; Components: directgateway;
Source: "..\gateway\install\*.bat"; DestDir: "{app}"; Excludes: "backup.bat,copybins.bat"; Flags: ignoreversion; Components: directgateway;
Source: "SmtpAgentConfig.xml"; DestDir: {app}; Flags: ignoreversion; Components: directgateway;

Source: "..\gateway\devInstall\DevAgentWithServiceConfig.xml"; DestDir: "{app}"; DestName: "DevAgentConfig.xml"; Flags: ignoreversion; Components: developergateway;            
Source: "..\gateway\devInstall\setupdomains.txt"; DestDir: "{app}"; Flags: ignoreversion; Components: developergateway;
Source: "..\gateway\devInstall\simple.eml"; DestDir: "{app}\Samples"; Flags: ignoreversion; Components: developergateway;

Source: "..\external\microsoft\vcredist\vcredist_x86.exe"; DestDir: "{app}\Libraries"; DestName: "vcredist.exe"; Flags: ignoreversion recursesubdirs; Check: IsX86; Components: directgateway developergateway;
Source: "..\external\microsoft\vcredist\vcredist_x64.exe"; DestDir: "{app}\Libraries"; DestName: "vcredist.exe"; Flags: ignoreversion recursesubdirs; Check: IsX64 or IsIA64; Components: directgateway developergateway;

Source: "*.bat"; DestDir: "{app}"; Excludes: "build-installer.bat"; Flags: ignoreversion;
Source: "*.ps1"; DestDir: "{app}"; Flags: ignoreversion;
Source: "event-sources.txt"; DestDir: "{app}"; Flags: ignoreversion;
Source: "..\config\store\Schema.sql"; DestDir: "{app}\SQL"; Flags: ignoreversion; Components: database; 
Source: "createuser.sql"; DestDir: "{app}\SQL"; Flags: ignoreversion; Components: database; 
                        
Source: "toolutil\install.tools\bin\debug\Health.Direct.Install.Tools.dll"; DestDir: "{app}\InstallTools"; Flags: ignoreversion;  Components: dnsresponder and not developergateway;  

                                 
[UninstallDelete]
Type: files; Name: "{app}\direct.ini"
Type: files; Name: "{app}\Health.Direct.SmtpAgent.tlb"
Type: files; Name: "{app}\InstallationLogFile.log"
Type: files; Name: "{app}\installdnsresponder.log"
Type: files; Name: "{app}\installgateway.log"
Type: files; Name: "{app}\createeventlogsource.log"
Type: files; Name: "{app}\InstallTools\Health.Direct.Install.Tools.tlb"

[Icons]
Name: "{group}\Admin Console"; Filename: "{app}\AdminConsole.exe"; WorkingDir: "{app}";
Name: "{group}\Config Console"; Filename: "{app}\ConfigConsole.exe"; WorkingDir: "{app}";
Name: "{group}\Config Web UI"; Filename: "http://localhost/ConfigUI/";
Name: "{group}\Test Database"; Filename: "http://localhost/ConfigService/TestService.aspx";
Name: "{group}\{cm:UninstallProgram,Direct Gateway}"; Filename: "{uninstallexe}";


[Run]
Filename: {app}\Libraries\vcredist.exe; Description: "Microsoft Visual C++ 2008 Redistributable Package"; Flags: postinstall runascurrentuser unchecked; Components: directgateway or developergateway; Check: not IsVCRT
Filename: {app}\createdatabase.bat; Parameters: ".\sqlexpress DirectConfig ""{app}\SQL\Schema.sql"" ""{app}\SQL\createuser.sql"""; Description: Install Database; Flags: runascurrentuser postinstall; Components: developergateway and not database;
Filename: {app}\createdatabase.bat; Parameters: ".\sqlexpress DirectConfig ""{app}\SQL\Schema.sql"" ""{app}\SQL\createuser.sql"""; Description: Install Database; Flags: runascurrentuser; Components: database;
Filename: {app}\install-dev.bat; Parameters: """{app}"""; Description: "Install Gateway (DEVELOPMENT VERSION)"; WorkingDir: "{app}"; Flags: postinstall runascurrentuser unchecked; Components: developergateway;
Filename: {app}\installdnsresponder.bat; Parameters: """{app}"" >> ""{app}\installdnsresponder.log"" 2>&1"; Description: Install DNS Responder; Flags: runascurrentuser ; Components: dnsresponder and not developergateway; 
Filename: {dotnet20}\RegAsm.exe; Parameters: Health.Direct.Install.Tools.dll /codebase; WorkingDir:{app}\InstallTools; StatusMsg: Installing installer tools; Description: Register tool com visible; Flags: runascurrentuser; Components: dnsresponder and not developergateway;
Filename: {app}\installgateway.bat; Parameters:  """{app}"" >> ""{app}\installgateway.log"" 2>&1";  Description: Install Gateway; Flags: runascurrentuser ; Components: directgateway and not developergateway; 
Filename: {app}\createadmin.bat; Description:Create Admin.  (Database must exist); Flags: runascurrentuser postinstall unchecked; Components: not developergateway; 
Filename: {app}\createeventlogsource.bat; Parameters: " >> ""{app}\createeventlogsource.log"" 2>&1"; Description:Setup event log; Flags: runascurrentuser; Components: not developergateway; 


[UninstallRun]
Filename: {app}\uninstall.bat; Flags: runascurrentuser; RunOnceId: 'RemoveDeveloperGateway';   Components: developergateway;
Filename: {app}\uninstallDnsResponder.bat; RunOnceId: 'RemoveDnsResponder';  Components: dnsresponder and not developergateway;
Filename: {app}\uninstallGateway.bat; RunOnceId: 'RemoveGateway'; Components: directgateway and not developergateway;
Filename: {dotnet20}\RegAsm.exe; RunOnceId: 'Removeinstall.tools'; Parameters: Health.Direct.Install.Tools.dll /unregister; WorkingDir:{app}\InstallTools; Flags: runascurrentuser; Components: dnsresponder and not developergateway;


[INI]
Filename: {app}\direct.ini; section: InstallSettings; key: "DnsWebService_Vdir"; string: DnsService; Components: dnswebservice
Filename: {app}\direct.ini; section: InstallSettings; key: "ConfigWebService_Vdir"; string: ConfigService; Components: configwebservice
Filename: {app}\direct.ini; section: InstallSettings; key: ConfigUiWebApp_Vdir; string: ConfigUI; Components: configui



[Code]


//Log file maintenance
var
  OkToCopyLog : Boolean;



function IsX64: Boolean;
begin
  Result := Is64BitInstallMode and (ProcessorArchitecture = paX64);
end;

function IsIA64: Boolean;
begin
  Result := Is64BitInstallMode and (ProcessorArchitecture = paIA64);
end;

function IsX86: Boolean;
begin
  Result := (ProcessorArchitecture = paX86);
end;

//included script to test for VC redistributable
function IsVCRT: Boolean;
begin
  Result := VCRT_IsInstalled (VC2008_ANY_x64) = 5;
end;

procedure CheckDnsResponderServiceOnClick(Sender: TObject);
var
  ErrorCode: Integer;
  Button: TButton;
  DnsServiceTestTextBox: TNewEdit;
  DnsResponderPage: TWizardPage;
begin
  Button := TButton(Sender);
  DnsResponderPage := TWizardPage(Button.Owner);
  DnsServiceTestTextBox := TNewEdit(DnsResponderPage.FindComponent('DnsServiceTestTextBox'));
  ShellExecAsOriginalUser('open', DnsServiceTestTextBox.Text, '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
end;

procedure CheckDatabaseConnOnClick(Sender: TObject);
var
  tools: Variant;
  Button: TButton;
  DbConnStrTextBox: TNewEdit;
  DatabaseConnPage: TWizardPage;
  ErrorLabel : TNewStaticText;
  SuccessLabel : TNewStaticText;
  StatusLabel : TNewStaticText;
  success : Boolean;
  message : String;
begin
  Button := TButton(Sender);
  DatabaseConnPage := TWizardPage(Button.Owner);
  DbConnStrTextBox := TNewEdit(DatabaseConnPage.FindComponent('DbConnStrTextBox')); 
  SuccessLabel := TNewStaticText(DatabaseConnPage.FindComponent('SuccessLabel'));
  ErrorLabel := TNewStaticText(DatabaseConnPage.FindComponent('ErrorLabel'));
  StatusLabel := TNewStaticText(DatabaseConnPage.FindComponent('StatusLabel'));
  SuccessLabel.Caption := '';
  ErrorLabel.Caption := '';
  StatusLabel.Caption := 'Checking Db Connection...';
  StatusLabel.Update;
  StatusLabel.BringToFront;
  try
    tools := CreateOleObject('Direct.Installer.SqlDbTools');
  except
    RaiseException('Cannot find Direct.Installer.SqlDbTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;
    success := tools.TestConnection(DbConnStrTextBox.Text, message);
    StatusLabel.Caption := '';
    StatusLabel.Update;
    if(success) then
    begin
      SuccessLabel.Caption := 'Success';
      SuccessLabel.Update;
      SuccessLabel.BringToFront;
    end
    else
    begin
      ErrorLabel.Caption := 'Failed: ' + message;
      ErrorLabel.Update;
      ErrorLabel.BringToFront;
    end;         
end;


procedure SetDnsResponderUrl(url: String);
var
  tools: Variant;
begin
  try                              
    tools := CreateOleObject('Direct.Installer.XPathTools');
  except
    RaiseException('Cannot find Direct.Installer.XPathTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;
    tools.XmlFilePath := ExpandConstant('{app}') + '\DirectDnsResponderSvc.exe.config' ;
    tools.SetSingleAttribute('/configuration/ServiceSettingsGroup/RecordRetrievalServiceSettings/@Url', url);
end;






procedure SetDatabaseConnSting(connStr: String);
var
  tools : Variant;
begin
  try
    tools := CreateOleObject('Direct.Installer.XPathTools');
  except
    RaiseException('Cannot find Direct.Installer.XPathTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;
    if (pos( 'dnswebservice', WizardSelectedComponents( false)) > 0) and (pos( 'development', WizardSetupType( false)) = 0) then  
    begin
        tools.XmlFilePath := ExpandConstant('{app}') + '\DnsService\Web.Config' ;
        tools.SetSingleAttribute('configuration/connectionStrings/add[@name="configStore"]/@connectionString', connStr);
    end
    else
    if (pos( 'configwebservice', WizardSelectedComponents( false)) > 0)  and (pos( 'development', WizardSetupType( false)) = 0) then  
    begin
        tools.XmlFilePath := ExpandConstant('{app}') + '\ConfigService\Web.Config' ;
        tools.SetSingleAttribute('configuration/connectionStrings/add[@name="configStore"]/@connectionString', connStr);
    end;
end;
       

       



function SetDnsResponderUrlOnClick(Sender: TWizardPage): Boolean;
var
  DnsServiceUrlLabel : TNewStaticText;
begin         
    DnsServiceUrlLabel := TNewStaticText(Sender.FindComponent('DnsServiceUrlLabel'));
    SetDnsResponderUrl(DnsServiceUrlLabel.Caption);
    Result := True;
end;


//Save connection string
//Determine which config files to save it to.
function SetDatabaseConnUrlOnClick(Sender: TWizardPage): Boolean;
var
  DbConnStrTextBox : TNewEdit;
begin  
  DbConnStrTextBox := TNewEdit(Sender.FindComponent('DbConnStrTextBox')); 
  SetDatabaseConnSting(DbConnStrTextBox.Text);
  Result := True; 
end;


function GetDnsResponderUrl(): String;
var
  XPathTools: Variant; 
  dnsResponderUrl: String;
begin
  try                              
    XPathTools := CreateOleObject('Direct.Installer.XPathTools');
  except
    RaiseException('Cannot find Direct.Installer.XPathTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;
    XPathTools.XmlFilePath := ExpandConstant('{app}') + '\DirectDnsResponderSvc.exe.config' ;
    dnsResponderUrl := XPathTools.SelectSingleAttribute('/configuration/ServiceSettingsGroup/RecordRetrievalServiceSettings/@Url');
    Result := dnsResponderUrl;
end;


procedure DnsServiceTestOnChange(Sender: TObject);
var
  tools : Variant;
  DnsServiceUrlLabel, ErrorLabel : TNewStaticText;
  DnsServiceTestTextBox : TNewEdit;
  DnsResponderPage : TWizardPage;
  hostport : String;
begin
  DnsServiceTestTextBox := TNewEdit(Sender); 
  DnsResponderPage := TWizardPage(DnsServiceTestTextBox.Owner)
  ErrorLabel := TNewStaticText(DnsResponderPage.FindComponent('ErrorLabel'));
  ErrorLabel.Caption := '';
  ErrorLabel.Update;
     
  //Update DnsService (Will be persisted to config file when finished with wizard) 
  try                              
    tools := CreateOleObject('Direct.Installer.UrlTools');
  except
    RaiseException('Cannot find Direct.Installer.UrlTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;  
  
    
 if(tools.ValidUrl(DnsServiceTestTextBox.Text)) then
 begin  
  DnsServiceUrlLabel := TNewStaticText(DnsResponderPage.FindComponent('DnsServiceUrlLabel'));  
  hostPort :=  tools.HostPort(DnsServiceTestTextBox.Text); 
  DnsServiceUrlLabel.Caption :=  tools.UpdateUrlHost(DnsServiceUrlLabel.Caption, hostport).FullUrl
  //Need to write a line wrap routine here to place a space in the url to allow a wrap at the correct location. 
  WizardForm.AdjustLabelHeight(DnsServiceUrlLabel);
  DnsServiceUrlLabel.Update;
  
 end         
 else
 begin
    ErrorLabel.Caption := 'Invalid Url.';
    ErrorLabel.BringToFront;
    ErrorLabel.Update;
 end;
end;



procedure DnsResponderPageOnActivate(Sender: TWizardPage);
var
  tools : Variant;
  DnsServiceUrlLabel : TNewStaticText;
  DnsServiceTestTextBox : TNewEdit;
begin
  //Set DnsService from config file.
  DnsServiceUrlLabel := TNewStaticText(Sender.FindComponent('DnsServiceUrlLabel'));
  DnsServiceUrlLabel.Caption :=   GetDnsResponderUrl();
  
  //Set TestService.aspx by rebuilding Url 
  try                              
    tools := CreateOleObject('Direct.Installer.UrlTools');
  except
    RaiseException('Cannot find Direct.Installer.UrlTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;  
  DnsServiceTestTextBox := TNewEdit(Sender.FindComponent('DnsServiceTestTextBox')); 
  DnsServiceTestTextBox.Text := tools.UpdateUrlPathAndQuery(DnsServiceUrlLabel.Caption, '/DnsService/TestService.aspx').FullUrl;
  
end;



function GetDbConnStr(): String;
var
  XPathTools: Variant; 
  dnsResponderUrl: String;
begin
  try                              
    XPathTools := CreateOleObject('Direct.Installer.XPathTools');
  except
    RaiseException('Cannot find Direct.Installer.XPathTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;
    XPathTools.XmlFilePath := ExpandConstant('{app}') + '\ConfigService\Web.Config' ;
    dnsResponderUrl := XPathTools.SelectSingleAttribute('configuration/connectionStrings/add[@name="configStore"]/@connectionString');
    Result := dnsResponderUrl;
end;



procedure DatabaseConnPageOnActivate(Sender: TWizardPage);
var
  DbConnStrTextBox : TNewEdit;
begin
  DbConnStrTextBox := TNewEdit(Sender.FindComponent('DbConnStrTextBox')); 
  DbConnStrTextBox.Text := GetDbConnStr();
end;



function DnsResponderPage_ShouldSkip(Page: TwizardPage): Boolean;
begin
  Result := (pos( 'dnsresponder', WizardSelectedComponents( false)) = 0) ;
end;

//Skip this page if not configuring a service that relies on the database.
function DatabaseConnPage_ShouldSkip(Page: TwizardPage): Boolean;
begin
  Result := ((pos( 'dnswebservice', WizardSetupType( false)) > 0)
              or (pos( 'configwebservice', WizardSetupType( false)) > 0))
                and (pos( 'development', WizardSetupType( false)) = 0)
end;

//only call this once.  Notice we register the Health.Direct.Install.Tools.dll via the temp folder once then remove.
//later it is registered when the files have been placed in their deployment location.
function MsSmtpServiceExists(Host: String; Port: Integer): Boolean;
var
    ResultCode: Integer;
    SmtpExists: Boolean;
    SmtpTools: Variant;
begin
  ExtractTemporaryFile('Health.Direct.Install.Tools.dll');
  Exec(ExpandConstant('{dotnet20}\RegAsm.exe'),'Health.Direct.Install.Tools.dll /codebase', ExpandConstant('{tmp}'), SW_SHOW, ewWaitUntilTerminated, ResultCode );

  try                              
    SmtpTools := CreateOleObject('Direct.Installer.SmtpTools');
  except
    RaiseException('Cannot find Direct.SmtpTools.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;
    try
      Log('Checking Smtp connection with host:port of ' + Host + ':' + IntToStr(Port)); 
      SmtpExists := SmtpTools.TestConnection(Host, Port);
    except
      Log('Error: ' + GetExceptionMessage);       
    end
    Result := SmtpExists;
end;

function NextButtonClick(CurPageID: Integer): Boolean;
  
 begin
  if(CurPageID = wpSelectComponents) then 
    begin                
      //check for smtp services
      if (pos( 'directgateway', WizardSelectedComponents( false)) > 0)  or (pos( 'development', WizardSetupType( false)) > 0) then  
      begin
          if not (MsSmtpServiceExists('LocalHost', 25)) then //Because we are always installing locally this should suffice for now.
          begin
            MsgBox('Failed to find smtp running.' #13#13 + 'Cannot install directgateway.', mbInformation, mb_Ok);
            Result := False;
          end 
          else begin
            Result := True;
          end;
      end
      else begin
        Result := True;
      end;      
  end    
  else begin
    Result := True;
  end;    
end;

procedure DnsServiceUrlOnClick(Sender: TObject);
var
  ErrorCode: Integer;
begin
  ShellExecAsOriginalUser('open', TNewStaticText(Sender).Caption, '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
end;

procedure DnsHelpButtonOnClick(Sender: TObject);

begin
  MsgBox('Set database connectivity at the Dns Web Service.'  #13#10 +
    'Show the resulting Dns Web Service endpoint used by the DnsResponder Windows Service.', mbInformation, mb_Ok);
end;

procedure URLLabelOnClick(Sender: TObject);
var
  ErrorCode: Integer;
  URLLabel: TNewStaticText;
  begin
  URLLabel := TNewStaticText(Sender);
  ShellExecAsOriginalUser('open', URLLabel.Caption, '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
end;

function CreateDnsResponderWizardPage(): TWizardPage;
var
  DnsResponderPage: TWizardPage;
  DnsServiceTestTextBox: TNewEdit;
  Button, HelpButton: TNewButton;
  DnsServiceLabel, DnsServiceUrlLabel : TNewStaticText;
  ErrorLabel : TNewStaticText;
begin

  DnsResponderPage := CreateCustomPage(wpInfoAfter, 'Configure DnsResponder', 'DnsService endpoint stored in DirectDnsResponderSvc.exe.config');
    
  HelpButton := TNewButton.Create(DnsResponderPage);      
  HelpButton.Left := DnsResponderPage.Surface.Width - ScaleX(20);
  HelpButton.Width := ScaleX(20);
  HelpButton.Height := ScaleY(20);   
  HelpButton.Caption := '?';
  HelpButton.OnClick := @DnsHelpButtonOnClick;
  HelpButton.Parent := DnsResponderPage.Surface;  

  Button := TNewButton.Create(DnsResponderPage);
  Button.Top := HelpButton.Top + HelpButton.Height + ScaleY(20);
  Button.Height :=  WizardForm.NextButton.Height;
  Button.Width := DnsResponderPage.SurfaceWidth div 4;
  Button.Caption := 'Test: ';
  Button.OnClick := @CheckDnsResponderServiceOnClick;
  Button.Parent := DnsResponderPage.Surface;
  
  DnsServiceTestTextBox := TNewEdit.Create(DnsResponderPage);
  DnsServiceTestTextBox.Name := 'DnsServiceTestTextBox';
  DnsServiceTestTextBox.Top := HelpButton.Top + HelpButton.Height + ScaleY(20);;
  DnsServiceTestTextBox.Left :=  Button.Width + ScaleX(8);
  DnsServiceTestTextBox.Width := DnsResponderPage.SurfaceWidth - (Button.Left + Button.Width);
  DnsServiceTestTextBox.Parent := DnsResponderPage.Surface;
  DnsServiceTestTextBox.OnChange := @DnsServiceTestOnChange;
            
  DnsServiceLabel := TNewStaticText.Create(DnsResponderPage);
  DnsServiceLabel.Top := DnsServiceTestTextBox.Top + DnsServiceTestTextBox.Height + ScaleY(16); 
  DnsServiceLabel.Caption := 'DnsService endpoint: ';
  DnsServiceLabel.Width := Button.Width;
  DnsServiceLabel.Parent := DnsResponderPage.Surface;

  DnsServiceUrlLabel := TNewStaticText.Create(DnsResponderPage);
  DnsServiceUrlLabel.Name := 'DnsServiceUrlLabel';
  DnsServiceUrlLabel.Cursor := crHand;
  DnsServiceUrlLabel.OnClick := @DnsServiceUrlOnClick;
  //DnsServiceUrlLabel.Left := Button.Width + ScaleX(8);
  //DnsServiceUrlLabel.Top := DnsServiceTestTextBox.Top + DnsServiceTestTextBox.Height + ScaleY(16);
  DnsServiceUrlLabel.Top := DnsServiceLabel.Top + DnsServiceLabel.Height + ScaleY(16);
  DnsServiceUrlLabel.Width := DnsResponderPage.SurfaceWidth;
  DnsServiceUrlLabel.AutoSize := False;
  DnsServiceUrlLabel.Font.Style := DnsServiceUrlLabel.Font.Style + [fsUnderline];
  DnsServiceUrlLabel.Font.Color := clBlue;
  DnsServiceUrlLabel.WordWrap := True;
  DnsServiceUrlLabel.Parent := DnsResponderPage.Surface;
           

  ErrorLabel := TNewStaticText.Create(DnsResponderPage);
  ErrorLabel.Name := 'ErrorLabel';
  ErrorLabel.Top := DnsResponderPage.SurfaceHeight - ScaleY(16);
  ErrorLabel.Font.Color := clRed;
  ErrorLabel.Parent := DnsResponderPage.Surface;
                       
  
  DnsResponderPage.OnActivate := @DnsResponderPageOnActivate;
  DnsResponderPage.OnNextButtonClick := @SetDnsResponderUrlOnClick;
  DnsResponderPage.OnShouldSkipPage := @DnsResponderPage_ShouldSkip;
  Result := DnsResponderPage;
end;

function CreateDatabaseConnWizardPage(pageBefore: TWizardPage): TWizardPage;
var
  DatabaseConnPage: TWizardPage;       
  Button, HelpButton: TNewButton;
  DbConnStrTextBox : TNewEdit;
  ErrorLabel, SuccessLabel, StatusLabel: TNewStaticText;
begin

  DatabaseConnPage := CreateCustomPage(pageBefore.ID, 'Configure database connection string', '');
    
  HelpButton := TNewButton.Create(DatabaseConnPage);      
  HelpButton.Left := DatabaseConnPage.Surface.Width - ScaleX(20);
  HelpButton.Width := ScaleX(20);
  HelpButton.Height := ScaleY(20);   
  HelpButton.Caption := '?';
  HelpButton.OnClick := @DnsHelpButtonOnClick;
  HelpButton.Parent := DatabaseConnPage.Surface;  

  Button := TNewButton.Create(DatabaseConnPage);
  Button.Top := HelpButton.Top + HelpButton.Height + ScaleY(20);
  Button.Height :=  WizardForm.NextButton.Height;
  Button.Width := DatabaseConnPage.SurfaceWidth div 4;
  Button.Caption := 'Test: ';
  Button.OnClick := @CheckDatabaseConnOnClick;
  Button.Parent := DatabaseConnPage.Surface;

  DbConnStrTextBox := TNewEdit.Create(DatabaseConnPage);
  DbConnStrTextBox.Name := 'DbConnStrTextBox';
  DbConnStrTextBox.Top := Button.Top + Button.Height + ScaleY(20);
  DbConnStrTextBox.Width := DatabaseConnPage.SurfaceWidth - ScaleX(8);
  DbConnStrTextBox.Parent := DatabaseConnPage.Surface;

  ErrorLabel := TNewStaticText.Create(DatabaseConnPage);
  ErrorLabel.Name := 'ErrorLabel';
  ErrorLabel.Caption := '';
  ErrorLabel.Top := DatabaseConnPage.SurfaceHeight - ScaleY(16);  
  ErrorLabel.Left := ScaleX(8);
  ErrorLabel.Font.Color := clRed;
  ErrorLabel.Parent := DatabaseConnPage.Surface;
   
  SuccessLabel := TNewStaticText.Create(DatabaseConnPage);
  SuccessLabel.Name := 'SuccessLabel';
  SuccessLabel.Caption := '';
  SuccessLabel.Top := DatabaseConnPage.SurfaceHeight - ScaleY(16);
  SuccessLabel.Left := ScaleX(8);
  SuccessLabel.Font.Color := clGreen;
  SuccessLabel.Parent := DatabaseConnPage.Surface;

  StatusLabel := TNewStaticText.Create(DatabaseConnPage);
  StatusLabel.Name := 'StatusLabel';
  StatusLabel.Caption := '';
  StatusLabel.Top := DatabaseConnPage.SurfaceHeight - ScaleY(16);
  StatusLabel.Left := ScaleX(8);
  StatusLabel.Parent := DatabaseConnPage.Surface;

  DatabaseConnPage.OnActivate := @DatabaseConnPageOnActivate;
  DatabaseConnPage.OnNextButtonClick := @SetDatabaseConnUrlOnClick;
  DatabaseConnPage.OnShouldSkipPage := @DatabaseConnPage_ShouldSkip;

end;


procedure CreateAboutButtonAndURLLabel(ParentForm: TSetupForm; CancelButton: TNewButton);
var    
  URLLabel: TNewStaticText;
begin
  

  URLLabel := TNewStaticText.Create(ParentForm);
  URLLabel.Caption := 'http://wiki.directproject.org/';
  URLLabel.Cursor := crHand;
  URLLabel.OnClick := @URLLabelOnClick;
  URLLabel.Parent := ParentForm;
  { Alter Font *after* setting Parent so the correct defaults are inherited first }
  URLLabel.Font.Style := URLLabel.Font.Style + [fsUnderline];
  URLLabel.Font.Color := clBlue;
  URLLabel.Top := CancelButton.Top + CancelButton.Height - CancelButton.Height - 2;
  URLLabel.Left := ScaleX(20);
end;


//Create Virtual directories
procedure CurStepChanged(CurStep: TSetupStep);
          
begin
  
  //Post install step
  if (CurStep = ssPostInstall) then begin
    //Dns web service somponent selected and Not installing development type
    if (pos( 'dnswebservice', WizardSelectedComponents( false)) > 0) and (pos( 'development', WizardSetupType( false)) = 0) then  
      begin
        CreateIISVirtualDir('DnsService', ExpandConstant('{app}') + '\DnsService', 'Direct DNS Responder Service');
      end; 
    //Config web service component selected and Not installing development type
    if (pos( 'configwebservice', WizardSelectedComponents( false)) > 0)  and (pos( 'development', WizardSetupType( false)) = 0) then  
      begin
        CreateIISVirtualDir('ConfigService', ExpandConstant('{app}') + '\ConfigService', 'Direct Config Service');
      end;
    //Config UI web application component selected and Not installing development type
    if (pos( 'configui', WizardSelectedComponents( false)) > 0)  and (pos( 'development', WizardSetupType( false)) = 0) then  
      begin
        CreateIISVirtualDir('ConfigUI', ExpandConstant('{app}') + '\ConfigUI', 'Direct Config Admin');
      end;
   // Exec('IISReset');
  end;
  //Log file maintenance
  if CurStep = ssDone then
    OkToCopyLog := True;


end;



//Remove Virtual directories.
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  DnsWebService_Vdir, ConfigWebService_Vdir, ConfigUiWebApp_Vdir: String;
begin
  
  //Post install step
  if (CurUninstallStep = usUninstall) then begin

    DnsWebService_Vdir := GetIniString('InstallSettings', 'DnsWebService_Vdir', 'unknown', ExpandConstant('{app}') + '\direct.ini');
    ConfigWebService_Vdir := GetIniString('InstallSettings', 'ConfigWebService_Vdir', 'unknown', ExpandConstant('{app}') + '\direct.ini');
    ConfigUiWebApp_Vdir := GetIniString('InstallSettings', 'ConfigUiWebApp_Vdir', 'unknown', ExpandConstant('{app}') + '\direct.ini')


    //Dns web service somponent selected and Not installing development type
    if not (CompareStr('unknown', DnsWebService_Vdir) = 0)  then  
      begin
        DeleteIISVirtualDir(DnsWebService_Vdir);
      end; 
    //Config web service component selected and Not installing development type
    if not (CompareStr('unknown', ConfigWebService_Vdir) = 0)  then  
      begin
        DeleteIISVirtualDir(ConfigWebService_Vdir);
      end;
    //Config UI web application component selected and Not installing development type
    if not (CompareStr('unknown', ConfigUiWebApp_Vdir) = 0)  then  
      begin
        DeleteIISVirtualDir(ConfigUiWebApp_Vdir);
      end;
  end;
end;


procedure DeinitializeSetup();
begin
  if OkToCopyLog then
    FileCopy (ExpandConstant ('{log}'), ExpandConstant ('{app}\InstallationLogFile.log'), FALSE);
    RestartReplace (ExpandConstant ('{log}'), '');   // remove the temp log file during the next system restart.
end;

procedure InitializeWizard;
var
  Page : TWizardPage;
begin

  Page := CreateDnsResponderWizardPage;
  Page := CreateDatabaseConnWizardPage(Page);

  CreateAboutButtonAndURLLabel(WizardForm, WizardForm.CancelButton);
end;



