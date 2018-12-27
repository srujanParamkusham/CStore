
DROP TABLE IF EXISTS AppSQLLog;
DROP TABLE IF EXISTS SecurityUserActivation;
DROP TABLE IF EXISTS SecuritySingleSignOnToken;
DROP TABLE IF EXISTS SecurityPasswordResetRequest;
DROP TABLE IF EXISTS SecurityUserActivityHistory;
DROP TABLE IF EXISTS SecurityUserPasswordHistory;
DROP TABLE IF EXISTS SecurityUserLoginHistory;
DROP TABLE IF EXISTS SecurityUserQuestionAnswer;
DROP TABLE IF EXISTS SecurityQuestion;
DROP TABLE IF EXISTS SecurityUserRoleMembership;
DROP TABLE IF EXISTS SecurityUser;
DROP TABLE IF EXISTS SecuritySecurableAction;
DROP TABLE IF EXISTS SecurityAccess;
DROP TABLE IF EXISTS SecuritySecurable;
DROP TABLE IF EXISTS SecurityRole;
DROP TABLE IF EXISTS SecurityAction;
DROP TABLE IF EXISTS AppAnnouncement;
DROP TABLE IF EXISTS AppCodeDetail;
DROP TABLE IF EXISTS AppMenuItem;
DROP TABLE IF EXISTS AppMenu;
DROP TABLE IF EXISTS AppEmailTemplate;
DROP TABLE IF EXISTS AppContent;
DROP TABLE IF EXISTS AppVariable;
DROP TABLE IF EXISTS AppEventLog;

--
--AppEventLog
--
CREATE TABLE AppEventLog(
	AppEventLogId serial NOT NULL,
	MachineName varchar(150) NULL,
	ApplicationCode varchar(50) NULL,
	UserName varchar(100) NULL,
	SessionId varchar(100) NULL,
	TransactionId varchar(100) NULL,
	EventDate TIMESTAMP DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
	LogLevel varchar(10) NULL,
	"message" text NULL,
	StackTrace text NULL,
	EventCode varchar(50) NULL,
	SourceClass varchar(150) NULL,
	SourceMethod varchar(150) NULL,
	SourceObject varchar(150) NULL,
	SourceId varchar(150) NULL,
	CONSTRAINT PK_AppEventLog PRIMARY KEY 
	(
		AppEventLogId
	)
);

CREATE INDEX IX_AppEventLog_EventDate ON AppEventLog
(
	EventDate
);

--
--AppVariable
--
CREATE TABLE AppVariable(
	AppVariableId serial NOT NULL,
	VariableGroup varchar(255) NULL,
	VariableName varchar(255) NOT NULL,
	VariableValue text NULL,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppVariable PRIMARY KEY 
	(
		AppVariableId
	)
);

CREATE UNIQUE INDEX IX_AppVariable_VariableName ON AppVariable
(
	VariableName
);

--
--AppContent
--
CREATE TABLE AppContent(
	AppContentId serial NOT NULL,
	ContentGroup varchar(255) NULL,
	ContentName varchar(255) NOT NULL,
	ContentValue text NULL,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppContent PRIMARY KEY 
	(
		AppContentId
	)
);

CREATE UNIQUE INDEX IX_AppContent_ContentName ON AppContent
(
	ContentName
);


--
--AppEmailTemplate
--
CREATE TABLE AppEmailTemplate(
	AppEmailTemplateId serial NOT NULL,
	TemplateCode varchar(255) NOT NULL,
	Name varchar(255) NULL,
	"description" varchar(2000) NULL,
	EmailTo varchar(2000) NULL,
	EmailCC varchar(2000) NULL,
	EmailBCC varchar(2000) NULL,
	EmailFrom varchar(2000) NULL,
	EmailFromDisplayName varchar(2000) NULL,
	EmailSubject varchar(2000) NULL,
	EmailBody text NULL,
	HTML boolean NOT NULL DEFAULT false,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppEmailTemplate PRIMARY KEY 
	(
		AppEmailTemplateId
	)
);

CREATE UNIQUE INDEX IX_AppEmailTemplate_TemplateCode ON AppEmailTemplate
(
	TemplateCode
);


--
--App Menu
--
CREATE TABLE AppMenu(
	AppMenuId serial NOT NULL,
	MenuCode varchar(255) NOT NULL,
	Name varchar(255) NULL,
	"description" varchar(2000) NULL,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppMenu PRIMARY KEY 
	(
		AppMenuId
	)
);

CREATE UNIQUE INDEX IX_AppMenu_MenuCode ON AppMenu
(
	MenuCode
);


--
--App Menu Item
--
CREATE TABLE AppMenuItem(
	AppMenuItemId serial NOT NULL,
	AppMenuId int NOT NULL,
	ParentAppMenuItemId int NULL,
	Name varchar(255) NOT NULL,
	Handler varchar(1000) NULL,
	"image" varchar(1000) NULL,
	"text" varchar(1000) NULL,
	Style varchar(1000) NULL,
	ToolTip varchar(1000) NULL,
	Sort int NULL,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppMenuItem PRIMARY KEY 
	(
		AppMenuItemId
	),
	FOREIGN KEY (AppMenuId) REFERENCES AppMenu(AppMenuId),
	FOREIGN KEY (ParentAppMenuItemId) REFERENCES AppMenuItem(AppMenuItemId)
);

CREATE INDEX IX_AppMenuItem_AppMenuId ON AppMenuItem
(
	AppMenuId,
	AppMenuItemId
);

CREATE INDEX IX_AppMenuItem_ParentMenuItemId ON AppMenuItem
(
	ParentAppMenuItemId,
	AppMenuItemId
);


--
--AppCodeDetail
--
CREATE TABLE AppCodeDetail (
	AppCodeDetailId serial NOT NULL,
	CodeGroup varchar(255) NOT NULL,
	CodeValue varchar(255) NOT NULL,
	"description" varchar(255) NULL,
	Sort int NULL,
	"default" boolean NOT NULL DEFAULT false,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppCodeDetail PRIMARY KEY 
	(
		AppCodeDetailId
	)
);

CREATE UNIQUE INDEX IX_AppCodeDetail_Field_Code ON AppCodeDetail
(
	CodeGroup, 
	CodeValue
);


--
--AppAnnouncement
--
CREATE TABLE AppAnnouncement (
	AppAnnouncementId serial NOT NULL,
	EffectiveDate TIMESTAMP,
	ExpirationDate TIMESTAMP,
	"subject" varchar(2000),
	AnnouncementText text,
	ForceToTopOfList boolean NOT NULL DEFAULT false,
	Sort int NULL,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppAnnouncement PRIMARY KEY 
	(
		AppAnnouncementId
	)
);

CREATE INDEX IX_AppAnnouncement_EffectiveDate_ExpirationDate ON AppAnnouncement
(
	EffectiveDate, 
	ExpirationDate
);


--
--SecurityAction
--
CREATE TABLE SecurityAction (
	SecurityActionId serial NOT NULL,
	Name varchar(255) NOT NULL,
	"description" varchar(2000) NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityAction PRIMARY KEY 
	(
		SecurityActionId
	)
);

CREATE UNIQUE INDEX IX_SecurityAction_Name ON SecurityAction
(
	Name
);

--
--SecurityRole
--
CREATE TABLE SecurityRole (
	SecurityRoleId serial NOT NULL,
	Name varchar(255) NOT NULL,
	"description" varchar(2000),
	ADGroupName varchar(255),
	"default" boolean NOT NULL DEFAULT false,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityRole PRIMARY KEY 
	(
		SecurityRoleId
	)
);

CREATE UNIQUE INDEX IX_SecurityRole_Name ON SecurityRole
(
	Name
);

--
--SecuritySecurable
--
CREATE TABLE SecuritySecurable(
	SecuritySecurableId serial NOT NULL,
	Name varchar(255) NOT NULL,
	ParentSecuritySecurableId int NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecuritySecurable PRIMARY KEY 
	(
		SecuritySecurableId
	),
	FOREIGN KEY (ParentSecuritySecurableId) REFERENCES SecuritySecurable(SecuritySecurableId)
);

CREATE UNIQUE INDEX IX_SecuritySecurable_Name ON SecuritySecurable
(
	Name
);

CREATE UNIQUE INDEX IX_SecuritySecurable_ParentSecuritySecurableId ON SecuritySecurable
(
	ParentSecuritySecurableId,
	SecuritySecurableId
);

--
--SecurityAccess
--
CREATE TABLE SecurityAccess(
	SecurityAccessId serial NOT NULL,
	SecurityRoleId int NOT NULL,
	SecuritySecurableId int NOT NULL,
	SecurityActionId int NOT NULL,
	Allowed boolean NOT NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityAccess PRIMARY KEY 
	(
		SecurityAccessId
	),
	FOREIGN KEY (SecurityRoleId) REFERENCES SecurityRole(SecurityRoleId),
	FOREIGN KEY (SecuritySecurableId) REFERENCES SecuritySecurable(SecuritySecurableId),
	FOREIGN KEY (SecurityActionId) REFERENCES SecurityAction(SecurityActionId)
);

CREATE UNIQUE INDEX IX_SecurityAccess_Role_Securable_Action ON SecurityAccess
(
	SecurityRoleId,
	SecuritySecurableId,
	SecurityActionId
);

--
--SecuritySecurableAction
--
CREATE TABLE SecuritySecurableAction(
	SecuritySecurableActionId serial NOT NULL,
	SecuritySecurableId int NOT NULL,
	SecurityActionId int NOT NULL,
	LoggedEvent boolean NOT NULL DEFAULT false,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecuritySecurableAction PRIMARY KEY 
	(
		SecuritySecurableActionId
	),
	FOREIGN KEY (SecuritySecurableId) REFERENCES SecuritySecurable(SecuritySecurableId),
	FOREIGN KEY (SecurityActionId) REFERENCES SecurityAction(SecurityActionId)
);

CREATE UNIQUE INDEX IX_SecuritySecurableAction_SercurableId_ActionId ON SecuritySecurableAction
(
	SecuritySecurableId,
	SecurityActionId
);

--
--Security User
--
CREATE TABLE SecurityUser(
	SecurityUserId serial NOT NULL,
	UserName varchar(100) NOT NULL,
	EmailAddress varchar(255) NULL,
	ExternalId varchar(255) NULL,
	FirstName varchar(500) NULL,
	LastName varchar(500) NULL,
	AuthenticationMethod varchar(50) NULL,
	PasswordHash varchar(500) NULL,
	PasswordSalt varchar(50) NULL,
	PasswordLastChangedDate TIMESTAMP NULL,
	PasswordExpirationDate TIMESTAMP NULL,
	PasswordNeverExpires boolean NOT NULL DEFAULT false,
	UserCannotChangePassword boolean NOT NULL DEFAULT false,
	LastLoginDate TIMESTAMP NULL,
	AccountActivated boolean NOT NULL DEFAULT true,
	AccountLocked boolean NOT NULL DEFAULT false,
	AccountLockedDate TIMESTAMP NULL,
	AccountExpirationDate TIMESTAMP NULL,
	NumConsecutiveFailedLogins int NOT NULL DEFAULT 0,
	ActivationRequestDate TIMESTAMP NULL,
	ActivationConfirmedDate TIMESTAMP NULL,
	ActivationCookie varchar(50) NULL,
	TermsAndConditionsVersion varchar(50) NULL,
	ActiveDirectoryGuid varchar(500) NULL,
	ActiveDirectoryDn varchar(500) NULL,
	UserType varchar(100) NULL,
	TimeZone varchar(100) NULL,
	Locale varchar(100) NULL,
	SystemAdmin boolean NOT NULL DEFAULT false,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityUser PRIMARY KEY 
	(
		SecurityUserId
	)
);

CREATE UNIQUE INDEX IX_SecurityUser_UserName ON SecurityUser
(
	UserName
);

--
--SecurityUserRoleMembership
--
CREATE TABLE SecurityUserRoleMembership(
	SecurityUserRoleMembershipId serial NOT NULL,
	SecurityRoleId int NOT NULL,
	SecurityUserId int NOT NULL,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityUserRoleMembership PRIMARY KEY 
	(
		SecurityUserRoleMembershipId
	),
	FOREIGN KEY (SecurityRoleId) REFERENCES SecurityRole(SecurityRoleId),
	FOREIGN KEY (SecurityUserId) REFERENCES SecurityUser(SecurityUserId)
);

CREATE UNIQUE INDEX IX_SecurityUserRoleMembership_Role_User ON SecurityUserRoleMembership
(
	SecurityRoleId,
	SecurityUserId
);

--
--SecurityQuestion
--
CREATE TABLE SecurityQuestion (
	SecurityQuestionId serial NOT NULL,
	Question varchar(500) NOT NULL,
	Active boolean NOT NULL DEFAULT true,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityQuestion PRIMARY KEY 
	(
		SecurityQuestionId
	)
);

CREATE UNIQUE INDEX IX_SecurityRole_Question ON SecurityQuestion
(
	Question
);

--
--SecurityUserQuestionAnswer
--
CREATE TABLE SecurityUserQuestionAnswer(
	SecurityUserQuestionAnswerId serial NOT NULL,
	SecurityQuestionId int NOT NULL,
	SecurityUserId int NOT NULL,
	QuestionAnswerHash varchar(2000) NOT NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityUserQuestionAnswer PRIMARY KEY 
	(
		SecurityUserQuestionAnswerId
	),
	FOREIGN KEY (SecurityQuestionId) REFERENCES SecurityQuestion(SecurityQuestionId),
	FOREIGN KEY (SecurityUserId) REFERENCES SecurityUser(SecurityUserId)
);

CREATE UNIQUE INDEX IX_SecurityUserQuestionAnswer_Question_User ON SecurityUserQuestionAnswer
(
	SecurityQuestionId,
	SecurityUserId
);

--
--Security User Login History
--
CREATE TABLE SecurityUserLoginHistory (
	SecurityUserLoginHistoryId serial NOT NULL,
	SecurityUserId int NULL,
	UserName varchar(100) NULL,
	MachineName varchar(150) NULL,
	ApplicationCode varchar(50) NULL,
	SuccessfulLogin boolean DEFAULT false NOT NULL,
	AccountWasLocked boolean DEFAULT false NOT NULL,
	IPAddress varchar(100) NULL,
	Browser varchar(255) NULL,
	ScreenResolution varchar(255) NULL,
	"message" varchar(255) NULL,
	SessionId varchar(255) NULL,
	SessionEndDate TIMESTAMP NULL,
	LastRequestDate TIMESTAMP NULL,
	SessionTimeoutDate TIMESTAMP NULL,
	LastRequestUrl varchar(2000) NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityUserLog PRIMARY KEY 
	(
		SecurityUserLoginHistoryId
	),
	FOREIGN KEY (SecurityUserId) REFERENCES SecurityUser(SecurityUserId)
);

CREATE INDEX IX_SecurityUserLoginHistory_UserName_CreateDate ON SecurityUserLoginHistory
(
	UserName,
	CreateDate
);


CREATE INDEX IX_SecurityUserLoginHistory_SecurityUserId_CreateDate ON SecurityUserLoginHistory
(
	SecurityUserId,
	CreateDate
);

CREATE INDEX IX_SecurityUserLoginHistory_CreateDate ON SecurityUserLoginHistory
(
	CreateDate
);


--
--Security User Password History
--
CREATE TABLE SecurityUserPasswordHistory (
	SecurityUserPasswordHistoryId serial NOT NULL,
	SecurityUserId int NOT NULL,
	PasswordHash varchar(500) NULL,
	PasswordSalt varchar(50) NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityUserPasswordHistory PRIMARY KEY 
	(
		SecurityUserPasswordHistoryId
	),
	FOREIGN KEY (SecurityUserId) REFERENCES SecurityUser(SecurityUserId)
);

CREATE INDEX IX_SecurityUserLoginHistory_SecurityUserId ON SecurityUserPasswordHistory
(
	SecurityUserId
);

--
--Security User Activity History
--
CREATE TABLE SecurityUserActivityHistory (
	SecurityUserActivityHistoryId serial NOT NULL,
	SecurityUserId int NULL,
	SecurityUserLoginHistoryId int NULL,
	UserName varchar(100) NULL,
	MachineName varchar(150) NULL,
	ApplicationCode varchar(50) NULL,			
	IPAddress varchar(100) NULL,
	SessionId varchar(255) NULL,
	TransactionId varchar(100) NULL,
	RequestUrl varchar(2000) NULL,
	Method varchar(100) NULL,
	StatusCode int NULL,
	LoadTimeMS int NULL,
	Referrer varchar(2000) NULL,
	SecuritySecurableActionId int NULL,
	AddtlInfo text NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityUserActivityHistory PRIMARY KEY 
	(
		SecurityUserActivityHistoryId
	)
);

CREATE INDEX IX_SecurityUserActivityHistory_SecurityUserId ON SecurityUserActivityHistory
(
	SecurityUserId,
	CreateDate
);

CREATE INDEX IX_SecurityUserActivityHistory_CreateDate_SecurityUserId ON SecurityUserActivityHistory
(
	CreateDate,
	SecurityUserId
);

CREATE INDEX IX_SecurityUserActivityHistory_SecurityUserLoginHistoryId ON SecurityUserActivityHistory
(
	SecurityUserLoginHistoryId,
	CreateDate
);

--
--Password Reset Request
--
CREATE TABLE SecurityPasswordResetRequest (
	SecurityPasswordResetRequestId serial NOT NULL,
	SecurityUserId int NULL,
	UserName varchar(100) NULL,
	Token varchar(255) NOT NULL,
	EmailAddress varchar(255) NULL,
	IPAddress varchar(100) NULL,
	RequestDate TIMESTAMP NOT NULL,
	ExpirationDate TIMESTAMP NULL,
	Processed boolean NOT NULL DEFAULT false,
	ProcessDate TIMESTAMP NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityPasswordResetRequest PRIMARY KEY 
	(
		SecurityPasswordResetRequestId
	),
	FOREIGN KEY (SecurityUserId) REFERENCES SecurityUser(SecurityUserId)
);

CREATE INDEX IX_SecurityPasswordResetRequest_SecurityUserId_Token ON SecurityPasswordResetRequest
(
	SecurityUserId,
	Token
);

CREATE INDEX IX_SecurityPasswordResetRequest_UserName_Token ON SecurityPasswordResetRequest
(
	UserName,
	Token
);


--
--Single Sign On Token
--
CREATE TABLE SecuritySingleSignOnToken (
	SecuritySingleSignOnTokenId serial NOT NULL,
	SecurityUserId int NOT NULL,
	UserName varchar(100) NULL,
	Token varchar(255) NOT NULL,
	IPAddress varchar(100) NULL,
	RequestDate TIMESTAMP NOT NULL,
	ExpirationDate TIMESTAMP NULL,
	Processed boolean NOT NULL DEFAULT false,
	ProcessDate TIMESTAMP NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecuritySingleSignOnToken PRIMARY KEY 
	(
		SecuritySingleSignOnTokenId
	),
	FOREIGN KEY (SecurityUserId) REFERENCES SecurityUser(SecurityUserId)
);

CREATE INDEX IX_SecuritySingleSignOnToken_SecurityUserId_Token ON SecuritySingleSignOnToken
(
	SecurityUserId,
	Token
);

CREATE INDEX IX_SecuritySingleSignOnToken_UserName_Token ON SecuritySingleSignOnToken
(
	UserName,
	Token
);


--
--Account Activation
--
CREATE TABLE SecurityUserActivation (
	SecurityUserActivationId serial NOT NULL,
	SecurityUserId int NOT NULL,
	UserName varchar(100) NULL,
	Token varchar(255) NOT NULL,
	EmailAddress varchar(255) NULL,
    IPAddress varchar(100) NULL,
	RequestDate TIMESTAMP NOT NULL,
	ExpirationDate TIMESTAMP NULL,
	Processed boolean NOT NULL DEFAULT false,
	ProcessDate TIMESTAMP NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_SecurityUserActivation PRIMARY KEY 
	(
		SecurityUserActivationId
	),
	FOREIGN KEY (SecurityUserId) REFERENCES SecurityUser(SecurityUserId)
);

CREATE INDEX IX_SecurityUserActivation_SecurityUserId_Token ON SecurityUserActivation
(
	SecurityUserId,
	Token
);

CREATE INDEX IX_SecurityUserActivation_UserName_Token ON SecurityUserActivation
(
	UserName,
	Token
);


--
--AppSQLLog
--
CREATE TABLE AppSQLLog (
	AppSQLLogId serial NOT NULL,
	SecurityUserId int NULL,
	SecurityUserLoginHistoryId int NULL,
	UserName varchar(100) NULL,
	MachineName varchar(150) NULL,
	ApplicationCode varchar(50) NULL,
	SessionId varchar(100) NULL,
	TransactionId varchar(100) NULL,
	EventDate TIMESTAMP DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
	Connection varchar(255) NULL,
	SQLStatement text NULL,
	SQLParameters text NULL,
	Successful boolean DEFAULT false NOT NULL,
	ExecuteTimeMS int NULL,
	ExceptionDetail text NULL,
	AddtlInfo text NULL,
	CreateDate TIMESTAMP NOT NULL,
	CreateUser varchar(100) NOT NULL,
	ModifyDate TIMESTAMP NULL,
	ModifyUser varchar(100) NULL,
	CONSTRAINT PK_AppSQLLog PRIMARY KEY  
	(
		AppSQLLogId
	)
);

CREATE INDEX IX_AppSQLLog_EventDate ON AppSQLLog
(
	CreateDate
);

CREATE INDEX IX_AppSQLLog_SecurityUserId ON AppSQLLog
(
	SecurityUserId,
	CreateDate
);

CREATE INDEX IX_AppSQLLog_SecurityUserLoginHistoryId ON AppSQLLog
(
	SecurityUserLoginHistoryId,
	CreateDate
);


INSERT INTO SecurityUser
           (SecurityUserId
		   ,UserName
           ,EmailAddress
           ,ExternalId
           ,FirstName
           ,LastName
           ,PasswordHash
           ,PasswordSalt
           ,PasswordLastChangedDate
           ,PasswordExpirationDate
		   ,PasswordNeverExpires
		   ,UserCannotChangePassword
           ,LastLoginDate
           ,AccountActivated
           ,AccountLocked
           ,AccountLockedDate
           ,NumConsecutiveFailedLogins
           ,ActivationRequestDate
           ,ActivationConfirmedDate
           ,ActiveDirectoryGuid
           ,ActiveDirectoryDn
           ,UserType
           ,TimeZone
           ,SystemAdmin
           ,Active
           ,ActivationCookie
           ,TermsAndConditionsVersion
           ,CreateDate
           ,CreateUser
           ,ModifyDate
           ,ModifyUser)
     VALUES
           (1,
		   'Admin'
           ,'tea@swc.com'
           ,NULL
           ,'System'
           ,'Admin'
           ,'KXLnyD2noGbAVqQSdVG7YX0hMEnOgFyWtoRPjbt8KpQ=' --Password1
           ,'J1z+P6bT'
           ,NOW()
           ,NOW()
		   ,true --[PasswordNeverExpires]
		   ,false --[UserCannotChangePassword]
           ,NULL
           ,true --[AccountActivated]
           ,false --<AccountLocked, bit,>
           ,NULL --<AccountLockedDate, datetime,>
           ,0 --<NumConsecutiveFailedLogins, int,>
           ,NULL --<ActivationRequestDate, datetime,>
           ,NULL --<ActivationConfirmedDate, datetime,>
           ,NULL --<ActiveDirectoryGuid, uniqueidentifier,>
           ,NULL --<ActiveDirectoryDn, varchar(500),>
           ,NULL --<UserType, varchar(100),>
           ,NULL --<TimeZone, varchar(100),>
           ,true --<Admin, bit,>
           ,true --<Active, bit,>
           ,NULL --<ActivationCookie, varchar(50),>
           ,NULL --<TermsAndConditionsVersion, varchar(50),>
           ,NOW()
           ,'Admin'
           ,NULL --<ModifyDate, datetime,>
           ,NULL --<ModifyUser, varchar(100)
           );

INSERT INTO SecurityUser
           (SecurityUserId
		   ,UserName
           ,EmailAddress
           ,ExternalId
           ,FirstName
           ,LastName
           ,PasswordHash
           ,PasswordSalt
           ,PasswordLastChangedDate
           ,PasswordExpirationDate
		   ,PasswordNeverExpires
		   ,UserCannotChangePassword
           ,LastLoginDate
           ,AccountActivated
           ,AccountLocked
           ,AccountLockedDate
           ,NumConsecutiveFailedLogins
           ,ActivationRequestDate
           ,ActivationConfirmedDate
           ,ActiveDirectoryGuid
           ,ActiveDirectoryDn
           ,UserType
           ,TimeZone
           ,SystemAdmin
           ,Active
           ,ActivationCookie
           ,TermsAndConditionsVersion
           ,CreateDate
           ,CreateUser
           ,ModifyDate
           ,ModifyUser)
     VALUES
           (2,
		   'Anonymous'
           ,NULL
           ,NULL
           ,'Anonymous'
           ,'User'
           ,NULL --Password
           ,NULL
           ,NOW()
           ,NOW()
		   ,false --[PasswordNeverExpires]
		   ,false --[UserCannotChangePassword]
           ,NULL
           ,false --[AccountActivated]
           ,true --<AccountLocked, bit,>
           ,NULL --<AccountLockedDate, datetime,>
           ,0 --<NumConsecutiveFailedLogins, int,>
           ,NULL --<ActivationRequestDate, datetime,>
           ,NULL --<ActivationConfirmedDate, datetime,>
           ,NULL --<ActiveDirectoryGuid, uniqueidentifier,>
           ,NULL --<ActiveDirectoryDn, varchar(500),>
           ,NULL --<UserType, varchar(100),>
           ,NULL --<TimeZone, varchar(100),>
           ,false --<Admin, bit,>
           ,false --<Active, bit,>
           ,NULL --<ActivationCookie, varchar(50),>
           ,NULL --<TermsAndConditionsVersion, varchar(50),>
           ,NOW()
           ,'Admin'
           ,NULL --<ModifyDate, datetime,>
           ,NULL --<ModifyUser, varchar(100)
           );

INSERT INTO SecurityUser
           (SecurityUserId
		   ,UserName
           ,EmailAddress
           ,ExternalId
           ,FirstName
           ,LastName
           ,PasswordHash
           ,PasswordSalt
           ,PasswordLastChangedDate
           ,PasswordExpirationDate
		   ,PasswordNeverExpires
		   ,UserCannotChangePassword
           ,LastLoginDate
           ,AccountActivated
           ,AccountLocked
           ,AccountLockedDate
           ,NumConsecutiveFailedLogins
           ,ActivationRequestDate
           ,ActivationConfirmedDate
           ,ActiveDirectoryGuid
           ,ActiveDirectoryDn
           ,UserType
           ,TimeZone
           ,SystemAdmin
           ,Active
           ,ActivationCookie
           ,TermsAndConditionsVersion
           ,CreateDate
           ,CreateUser
           ,ModifyDate
           ,ModifyUser)
     VALUES
           (3,
		   'TestUser'
           ,'tea@swc.com'
           ,NULL
           ,'Test'
           ,'User'
           ,'KXLnyD2noGbAVqQSdVG7YX0hMEnOgFyWtoRPjbt8KpQ=' --Password1
           ,'J1z+P6bT' --salt
           ,NOW() --expiration
           ,NOW() --last changed
		   ,false --[PasswordNeverExpires]
		   ,false --[UserCannotChangePassword]
           ,NULL
           ,true --[AccountActivated]
           ,false --<AccountLocked, bit,>
           ,NULL --<AccountLockedDate, datetime,>
           ,0 --<NumConsecutiveFailedLogins, int,>
           ,NULL --<ActivationRequestDate, datetime,>
           ,NULL --<ActivationConfirmedDate, datetime,>
           ,NULL --<ActiveDirectoryGuid, uniqueidentifier,>
           ,NULL --<ActiveDirectoryDn, varchar(500),>
           ,NULL --<UserType, varchar(100),>
           ,NULL --<TimeZone, varchar(100),>
           ,false --<Admin, bit,>
           ,true --<Active, bit,>
           ,NULL --<ActivationCookie, varchar(50),>
           ,NULL --<TermsAndConditionsVersion, varchar(50),>
           ,NOW()
           ,'Admin'
           ,NULL --<ModifyDate, datetime,>
           ,NULL --<ModifyUser, varchar(100)
           );



-------------------------------------------------------------------------------------------------
--
-- DEFAULT EMAIL TEMPLATES
--
-------------------------------------------------------------------------------------------------
--
--Forgot Password Email Template
--
INSERT INTO AppEmailTemplate
           (TemplateCode
           ,Name
           ,"description"
           ,EmailTo
           ,EmailCC
           ,EmailBCC
           ,EmailFrom
           ,EmailSubject
           ,EmailBody
           ,Html
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           ('ForgotPassword'
           ,'Forgot Password Email Template'
           ,'Sent to the user when they forget their password and request it to be reset.'
           ,'[[EmailAddress]]' --EmailTo
           ,NULL --EmailCC
           ,NULL --EmailBCC
           ,'catalyst@swc.com' --EmailFrom
           ,'Password Reset Request'
,'Dear [[FirstName]],<br/><br/>
This is an automated message generated because we''ve received a request to reset your password.<br/><br/>
If you did not request a password reset please disregard this email message, your account is still secure.<br/><br/>
To reset your password please click <a href="[[ForgotPasswordURL]]">here<a/>.<br/><br/>
Or copy the following line into your browser:<br/>
[[ForgotPasswordURL]]
<br/>
<br/>
IMPORTANT: Please do not reply to this message to attempt to reset your password.<br/><br/>'
           ,true --HTML
           ,true --Active
           ,NOW()
           ,'Admin');

--
--Password Changed Email Template
--
INSERT INTO AppEmailTemplate
           (TemplateCode
           ,Name
           ,"description"
           ,EmailTo
           ,EmailCC
           ,EmailBCC
           ,EmailFrom
           ,EmailSubject
           ,EmailBody
           ,Html
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           ('PasswordChanged'
           ,'Password Changed Email Template'
           ,'Sent to the user when their password has been changed.'
           ,'[[EmailAddress]]' --EmailTo
           ,NULL --EmailCC
           ,NULL --EmailBCC
           ,'catalyst@swc.com' --EmailFrom
           ,'Password Successfully Changed'
,'Dear [[FirstName]],<br/><br/>
This is an automated message generated to inform you that your password has been changed.<br/><br/>
If you did not change your password, please contact our customer support team. 
<br/>
<br/>
IMPORTANT: This is an automated email, please do not reply to this message.<br/><br/>'
           ,true --HTML
           ,true --Active
           ,NOW()
           ,'Admin');

--
--Account Locked Email Template
--
INSERT INTO AppEmailTemplate
           (TemplateCode
           ,Name
           ,"description"
           ,EmailTo
           ,EmailCC
           ,EmailBCC
           ,EmailFrom
           ,EmailSubject
           ,EmailBody
           ,Html
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           ('AccountLocked'
           ,'Account Locked Email Template'
           ,'Sent to the user when their account gets locked due to too many invalid logins.'
           ,'[[EmailAddress]]' --EmailTo
           ,NULL --EmailCC
           ,NULL --EmailBCC
           ,'catalyst@swc.com' --EmailFrom
           ,'Your Account Has Been Locked'
,'Dear [[FirstName]],<br/><br/>
Your account has been locked due to an excessive number of invalid login attempts. 
Please use the forgot password page on our site to validate your identity, 
change your password, and unlock your account. If you have any questions please contact 
our customer support team.
<br/><br/>
We are sorry for the inconvenience, but your security is our top priority.
<br/><br/>
IMPORTANT: This is an automated email, please do not reply to this message to attempt to resolve the issue.<br/><br/>'
           ,true --HTML
           ,true --Active
           ,NOW()
           ,'Admin');


-------------------------------------------------------------------------------------------------
--
-- DEFAULT MENUS
--
-------------------------------------------------------------------------------------------------
--
--Main Root Menu
--
INSERT INTO AppMenu
           (AppMenuId
		   ,MenuCode
           ,Name
           ,"description"
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           (1
		   ,'ROOT'
           ,'Root Menu'
           ,'The main application menu.'
           ,true
           ,NOW()
           ,'Admin');


--
--Admin Menu
--
INSERT INTO AppMenuItem
           (AppMenuItemId
		   ,AppMenuId
           ,ParentAppMenuItemId
           ,Name
           ,Handler
           ,"image"
           ,"text"
           ,Style
           ,ToolTip
           ,Sort
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           (1000
		   ,1 --The root menu created above
           ,NULL --ParentAppMenuItemId
           ,'Admin' --Name
           ,NULL --Handler
           ,NULL --Image
           ,'Admin' --Text
           ,NULL --Style
           ,NULL --ToolTip
           ,200 --Sort
           ,true --Active
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO AppMenuItem
           (AppMenuItemId
		   ,AppMenuId
           ,ParentAppMenuItemId
           ,Name
           ,Handler
           ,"image"
           ,"text"
           ,Style
           ,ToolTip
           ,Sort
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           (1010
		   ,1 --The root menu created above
           ,1000 --ParentAppMenuItemId
           ,'Users' --Name
           ,NULL --Handler
           ,NULL --Image
           ,'Users' --Text
           ,NULL --Style
           ,NULL --ToolTip
           ,100 --Sort
           ,true --Active
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );



-------------------------------------------------------------------------------------------------
--
-- Security Roles
--
-------------------------------------------------------------------------------------------------

--
--All Users Role
--
INSERT INTO SecurityRole
           (SecurityRoleId
		   ,Name
           ,"description"
           ,ADGroupName
		   ,"default"
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           (1,
		   'All Users'
           ,'All authenticated users should have this role.'
           ,'Domain Users'
		   ,true --Default
           ,true --Active
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );


-------------------------------------------------------------------------------------------------
--
-- Security User Role Memberships
--
-------------------------------------------------------------------------------------------------

INSERT INTO SecurityUserRoleMembership
           (SecurityUserRoleMembershipId
		   ,SecurityRoleId
           ,SecurityUserId
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           (1
		   ,1 --Role Id (All Users)
           ,3 --SecurityUserId (TestUser)
           ,true --Active
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );



-------------------------------------------------------------------------------------------------
--
-- Security Actions
--
-------------------------------------------------------------------------------------------------

INSERT INTO SecurityAction
           (SecurityActionId
		   ,Name
           ,"description"
           ,CreateDate
           ,CreateUser)
     VALUES
           (1
		   ,'get' --name
           ,'HTTP GET Request Action' --description
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAction
           (SecurityActionId
		   ,Name
           ,"description"
           ,CreateDate
           ,CreateUser)
     VALUES
           (2
		   ,'post' --name
           ,'HTTP POST Request Action' --description
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAction
           (SecurityActionId
		   ,Name
           ,"description"
           ,CreateDate
           ,CreateUser)
     VALUES
           (3
		   ,'edit' --name
           ,'Edit HTML Element Action' --description
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );




-------------------------------------------------------------------------------------------------
--
-- Security Securables
--
-------------------------------------------------------------------------------------------------

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (1
		   ,'swccatalysttemplate.web' --[name]
           ,NULL --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (2
		   ,'swccatalysttemplate.web.home' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (3
		   ,'swccatalysttemplate.web.home.index' --[name]
           ,2 --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (20
		   ,'swccatalysttemplate.web.authentication' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (21
		   ,'swccatalysttemplate.web.authentication.password' --[name]
           ,20 --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (22
		   ,'swccatalysttemplate.web.authentication.password.expired' --[name]
           ,21 --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (23
		   ,'swccatalysttemplate.web.authentication.password.change' --[name]
           ,21 --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecuritySecurable
           (SecuritySecurableId
		   ,Name
           ,ParentSecuritySecurableId
           ,CreateDate
           ,CreateUser)
     VALUES
           (30
		   ,'swccatalysttemplate.web.admin' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );


-------------------------------------------------------------------------------------------------
--
-- Security Securable Actions
--
-------------------------------------------------------------------------------------------------
INSERT INTO SecuritySecurableAction
           (SecuritySecurableId
		   ,SecurityActionId
		   ,LoggedEvent
           ,CreateDate
           ,CreateUser)
     VALUES
           (3 --[SecuritySecurableId]
		   ,1 --[SecurityActionId]
           ,false --[LoggedEvent]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

-------------------------------------------------------------------------------------------------
--
-- Security Access
--
-------------------------------------------------------------------------------------------------

--
--Home page
--
INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (1
		   ,1 --[SecurityRoleId]
           ,2 --[SecuritySecurableId] (Home)
		   ,1 --[SecurityActionId] (Get)
		   ,true --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (2
		   ,1 --[SecurityRoleId]
           ,2 --[SecuritySecurableId] (Home)
		   ,2 --[SecurityActionId] (Post)
		   ,true --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (3
		   ,1 --[SecurityRoleId]
           ,2 --[SecuritySecurableId] (Home)
		   ,3 --[SecurityActionId] (Edit)
		   ,true --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Authentication Area
--
INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (20
		   ,1 --[SecurityRoleId]
           ,20 --[SecuritySecurableId] (Authentication)
		   ,1 --[SecurityActionId] (Get)
		   ,true --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (21
		   ,1 --[SecurityRoleId]
           ,20 --[SecuritySecurableId]  (Authentication)
		   ,2 --[SecurityActionId] (Post)
		   ,true --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (22
		   ,1 --[SecurityRoleId]
           ,20 --[SecuritySecurableId] (Authentication)
		   ,3 --[SecurityActionId] (Edit)
		   ,true --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Admin Area
--
INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (30
		   ,1 --[SecurityRoleId]
           ,30 --[SecuritySecurableId] (Admin)
		   ,1 --[SecurityActionId] (Get)
		   ,false --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (31
		   ,1 --[SecurityRoleId]
           ,30 --[SecuritySecurableId] (Admin)
		   ,2 --[SecurityActionId] (Post)
		   ,false --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );

INSERT INTO SecurityAccess
           (SecurityAccessId
		   ,SecurityRoleId
		   ,SecuritySecurableId
		   ,SecurityActionId
		   ,Allowed
           ,CreateDate
           ,CreateUser)
     VALUES
           (32
		   ,1 --[SecurityRoleId]
           ,30 --[SecuritySecurableId] (Admin)
		   ,3 --[SecurityActionId] (Edit)
		   ,false --[Allowed]
           ,NOW() --CreateDate
           ,'Admin' --CreateUser
		   );



-------------------------------------------------------------------------------------------------
--
-- App Code Details
--
-------------------------------------------------------------------------------------------------
--
--User Types
--
INSERT INTO AppCodeDetail
           (CodeGroup
           ,CodeValue
           ,"description"
           ,Sort
		   ,"default"
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           ('SecurityUser.UserType' --CodeGroup
           ,'Standard'
           ,'Standard User'
           ,100
		   ,false --Default
           ,true --<Active, bit,>
           ,NOW() --<CreateDate, datetime,>
           ,'Admin' --<CreateUser, varchar(100),>
		   );

--
--Authentication Methods
--
INSERT INTO AppCodeDetail
           (CodeGroup
           ,CodeValue
           ,"description"
           ,Sort
		   ,"default"
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           ('SecurityUser.AuthenticationMethod' --CodeGroup
           ,'SecurityUser'
           ,'SQL Security User Table'
           ,100
		   ,true --Default
           ,true --<Active, bit,>
           ,NOW() --<CreateDate, datetime,>
           ,'Admin' --<CreateUser, varchar(100),>
		   );

INSERT INTO AppCodeDetail
           (CodeGroup
           ,CodeValue
           ,"description"
           ,Sort
		   ,"default"
           ,Active
           ,CreateDate
           ,CreateUser)
     VALUES
           ('SecurityUser.AuthenticationMethod' --CodeGroup
           ,'ActiveDirectory'
           ,'Active Directory'
           ,200
		   ,false --Default
           ,true --<Active, bit,>
           ,NOW() --<CreateDate, datetime,>
           ,'Admin' --<CreateUser, varchar(100),>
		   );

SELECT setval('appcodedetail_appcodedetailid_seq', 10000);
SELECT setval('appemailtemplate_appemailtemplateid_seq', 10000);
SELECT setval('appmenu_appmenuid_seq', 10000);
SELECT setval('appmenuitem_appmenuitemid_seq', 10000);
SELECT setval('securityuser_securityuserid_seq', 10000);
SELECT setval('securityrole_securityroleid_seq', 10000);
SELECT setval('securityuserrolemembership_securityuserrolemembershipid_seq', 10000);
SELECT setval('securityaction_securityactionid_seq', 10000);
SELECT setval('securitysecurable_securitysecurableid_seq', 10000);
SELECT setval('securitysecurableaction_securitysecurableactionid_seq', 10000);
SELECT setval('securityaccess_securityaccessid_seq', 10000);


GRANT ALL ON AppEventLog TO PUBLIC;
GRANT ALL ON AppVariable TO PUBLIC;
GRANT ALL ON AppContent TO PUBLIC;
GRANT ALL ON AppEmailTemplate TO PUBLIC;
GRANT ALL ON AppMenu TO PUBLIC;
GRANT ALL ON AppMenuItem TO PUBLIC;
GRANT ALL ON AppCodeDetail TO PUBLIC;
GRANT ALL ON AppAnnouncement TO PUBLIC;
GRANT ALL ON SecurityAction TO PUBLIC;
GRANT ALL ON SecurityRole TO PUBLIC;
GRANT ALL ON SecuritySecurable TO PUBLIC;
GRANT ALL ON SecurityAccess TO PUBLIC;
GRANT ALL ON SecuritySecurableAction TO PUBLIC;
GRANT ALL ON SecurityUser TO PUBLIC;
GRANT ALL ON SecurityUserRoleMembership TO PUBLIC;
GRANT ALL ON SecurityQuestion TO PUBLIC;
GRANT ALL ON SecurityUserQuestionAnswer TO PUBLIC;
GRANT ALL ON SecurityUserLoginHistory TO PUBLIC;
GRANT ALL ON SecurityUserPasswordHistory TO PUBLIC;
GRANT ALL ON SecurityUserActivityHistory TO PUBLIC;
GRANT ALL ON SecurityPasswordResetRequest TO PUBLIC;
GRANT ALL ON SecuritySingleSignOnToken TO PUBLIC;
GRANT ALL ON SecurityUserActivation TO PUBLIC;
GRANT ALL ON AppSQLLog TO PUBLIC;
