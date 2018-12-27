--USE [CatalystTemplateDb]
--GO

--GRANT EXEC TO CatalystDBUser;

IF OBJECT_ID('vw_SecurityUser', 'V') IS NOT NULL
	DROP VIEW [vw_SecurityUser];
IF OBJECT_ID('usp_Report_SecurityUserLoginHistory', 'P') IS NOT NULL
	DROP PROC [usp_Report_SecurityUserLoginHistory];
IF OBJECT_ID('dbo.AppSQLLog', 'U') IS NOT NULL
	DROP TABLE AppSQLLog; 
IF OBJECT_ID('dbo.SecurityUserActivation', 'U') IS NOT NULL
	DROP TABLE SecurityUserActivation; 
IF OBJECT_ID('dbo.SecuritySingleSignOnToken', 'U') IS NOT NULL
	DROP TABLE SecuritySingleSignOnToken; 
IF OBJECT_ID('dbo.SecurityPasswordResetRequest', 'U') IS NOT NULL
	DROP TABLE SecurityPasswordResetRequest; 
IF OBJECT_ID('dbo.SecurityUserActivityHistory', 'U') IS NOT NULL
	DROP TABLE SecurityUserActivityHistory; 
IF OBJECT_ID('dbo.SecurityUserPasswordHistory', 'U') IS NOT NULL
	DROP TABLE SecurityUserPasswordHistory; 
IF OBJECT_ID('dbo.SecurityUserLoginHistory', 'U') IS NOT NULL 
	DROP TABLE SecurityUserLoginHistory; 
IF OBJECT_ID('dbo.SecurityUserQuestionAnswer', 'U') IS NOT NULL 
	DROP TABLE SecurityUserQuestionAnswer; 
IF OBJECT_ID('dbo.SecurityQuestion', 'U') IS NOT NULL 
	DROP TABLE SecurityQuestion; 
IF OBJECT_ID('dbo.SecurityUserRoleMembership', 'U') IS NOT NULL 
	DROP TABLE SecurityUserRoleMembership; 
IF OBJECT_ID('dbo.SecurityUser', 'U') IS NOT NULL 
	DROP TABLE SecurityUser; 
IF OBJECT_ID('dbo.SecuritySecurableAction', 'U') IS NOT NULL 
	DROP TABLE SecuritySecurableAction; 
IF OBJECT_ID('dbo.SecurityAccess', 'U') IS NOT NULL 
	DROP TABLE SecurityAccess; 
IF OBJECT_ID('dbo.SecuritySecurable', 'U') IS NOT NULL 
	DROP TABLE SecuritySecurable; 
IF OBJECT_ID('dbo.SecurityRole', 'U') IS NOT NULL 
	DROP TABLE SecurityRole; 
IF OBJECT_ID('dbo.SecurityAction', 'U') IS NOT NULL 
	DROP TABLE SecurityAction; 
IF OBJECT_ID('dbo.AppAnnouncement', 'U') IS NOT NULL 
	DROP TABLE AppAnnouncement; 
IF OBJECT_ID('dbo.AppCodeDetail', 'U') IS NOT NULL 
	DROP TABLE AppCodeDetail; 
IF OBJECT_ID('dbo.AppMenuItem', 'U') IS NOT NULL 
	DROP TABLE AppMenuItem; 
IF OBJECT_ID('dbo.AppMenu', 'U') IS NOT NULL 
	DROP TABLE AppMenu; 
IF OBJECT_ID('dbo.AppEmailTemplate', 'U') IS NOT NULL 
	DROP TABLE AppEmailTemplate; 
IF OBJECT_ID('dbo.AppContent', 'U') IS NOT NULL 
	DROP TABLE AppContent; 
IF OBJECT_ID('dbo.AppVariable', 'U') IS NOT NULL 
	DROP TABLE AppVariable; 
IF OBJECT_ID('dbo.AppEventLog', 'U') IS NOT NULL 
	DROP TABLE AppEventLog; 

--
--AppEventLog
--
CREATE TABLE [dbo].[AppEventLog](
	[AppEventLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[MachineName] [varchar](150) NULL,
	[ApplicationCode] [varchar](50) NULL,
	[UserName] [varchar](100) NULL,
	[SessionId] [varchar](100) NULL,
	[TransactionId] [varchar](100) NULL,
	[EventDate] [datetime2](7) DEFAULT GETDATE() NOT NULL,
	[LogLevel] [varchar](10) NULL,
	[Message] [varchar](max) NULL,
	[StackTrace] [varchar](max) NULL,
	[EventCode] [varchar](50) NULL,
	[SourceClass] [varchar](150) NULL,
	[SourceMethod] [varchar](150) NULL,
	[SourceObject] [varchar](150) NULL,
	[SourceId] [varchar](150) NULL,
	CONSTRAINT [PK_AppEventLog] PRIMARY KEY CLUSTERED 
	(
		[AppEventLogId] ASC
	)
);

CREATE INDEX [IX_AppEventLog_EventDate] ON [dbo].[AppEventLog]
(
	[EventDate] ASC
);

--
--AppVariable
--
CREATE TABLE [dbo].[AppVariable](
	[AppVariableId] [int] IDENTITY(1,1) NOT NULL,
	[VariableGroup] [varchar](255) NULL,
	[VariableName] [varchar](255) NOT NULL,
	[VariableValue] [varchar](max) NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppVariable] PRIMARY KEY CLUSTERED 
	(
		[AppVariableId] ASC
	)
);

CREATE UNIQUE INDEX [IX_AppVariable_VariableName] ON [dbo].[AppVariable]
(
	[VariableName] ASC
);

--
--AppContent
--
CREATE TABLE [dbo].[AppContent](
	[AppContentId] [int] IDENTITY(1,1) NOT NULL,
	[ContentGroup] [varchar](255) NULL,
	[ContentName] [varchar](255) NOT NULL,
	[ContentValue] [varchar](max) NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppContent] PRIMARY KEY CLUSTERED 
	(
		[AppContentId] ASC
	)
);

CREATE UNIQUE INDEX [IX_AppContent_ContentName] ON [dbo].[AppContent]
(
	[ContentName] ASC
);


--
--AppEmailTemplate
--
CREATE TABLE [dbo].[AppEmailTemplate](
	[AppEmailTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateCode] [varchar](255) NOT NULL,
	[Name] [varchar](255) NULL,
	[Description] [varchar](2000) NULL,
	[EmailTo] [varchar](2000) NULL,
	[EmailCC] [varchar](2000) NULL,
	[EmailBCC] [varchar](2000) NULL,
	[EmailFrom] [varchar](2000) NULL,
	[EmailFromDisplayName] [varchar](2000) NULL,
	[EmailSubject] [varchar](2000) NULL,
	[EmailBody] [varchar](max) NULL,
	[HTML] [bit] NOT NULL DEFAULT 0,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppEmailTemplate] PRIMARY KEY CLUSTERED 
	(
		[AppEmailTemplateId] ASC
	)
);

CREATE UNIQUE INDEX [IX_AppEmailTemplate_TemplateCode] ON [dbo].[AppEmailTemplate]
(
	[TemplateCode] ASC
);


--
--App Menu
--
CREATE TABLE [dbo].[AppMenu](
	[AppMenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuCode] [varchar](255) NOT NULL,
	[Name] [varchar](255) NULL,
	[Description] [varchar](2000) NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppMenu] PRIMARY KEY CLUSTERED 
	(
		[AppMenuId] ASC
	)
);

CREATE UNIQUE INDEX [IX_AppMenu_MenuCode] ON [dbo].[AppMenu]
(
	[MenuCode] ASC
);


--
--App Menu Item
--
CREATE TABLE [dbo].[AppMenuItem](
	[AppMenuItemId] [int] IDENTITY(1,1) NOT NULL,
	[AppMenuId] [int] NOT NULL,
	[ParentAppMenuItemId] [int] NULL,
	[Name] [varchar](255) NOT NULL,
	[Handler] [varchar](1000) NULL,
	[Image] [varchar](1000) NULL,
	[Text] [varchar](1000) NULL,
	[Style] [varchar](1000) NULL,
	[ToolTip] [varchar](1000) NULL,
	[Sort] [int] NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppMenuItem] PRIMARY KEY CLUSTERED 
	(
		[AppMenuItemId] ASC
	),
	FOREIGN KEY ([AppMenuId]) REFERENCES [AppMenu]([AppMenuId]),
	FOREIGN KEY ([ParentAppMenuItemId]) REFERENCES [AppMenuItem]([AppMenuItemId])
);

CREATE INDEX [IX_AppMenuItem_AppMenuId] ON [dbo].[AppMenuItem]
(
	[AppMenuId] ASC,
	[AppMenuItemId] ASC
);

CREATE INDEX [IX_AppMenuItem_ParentMenuItemId] ON [dbo].[AppMenuItem]
(
	[ParentAppMenuItemId] ASC,
	[AppMenuItemId] ASC
);


--
--AppCodeDetail
--
CREATE TABLE [dbo].[AppCodeDetail] (
	[AppCodeDetailId] [int] IDENTITY(1,1) NOT NULL,
	[CodeGroup] [varchar](255) NOT NULL,
	[CodeValue] [varchar](255) NOT NULL,
	[Description] [varchar](255) NULL,
	[Sort] [int] NULL,
	[Default] [bit] NOT NULL DEFAULT 0,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppCodeDetail] PRIMARY KEY CLUSTERED 
	(
		[AppCodeDetailId] ASC
	)
);

CREATE UNIQUE INDEX [IX_AppCodeDetail_Field_Code] ON [dbo].[AppCodeDetail]
(
	[CodeGroup], 
	[CodeValue]
);


--
--AppAnnouncement
--
CREATE TABLE [dbo].[AppAnnouncement] (
	[AppAnnouncementId] [int] IDENTITY(1,1) NOT NULL,
	[EffectiveDate] [datetime],
	[ExpirationDate] [datetime],
	[Subject] [varchar](2000),
	[AnnouncementText] [varchar](max),
	[ForceToTopOfList] [bit] NOT NULL DEFAULT 0,
	[Sort] [int] NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppAnnouncement] PRIMARY KEY CLUSTERED 
	(
		[AppAnnouncementId] ASC
	)
);

CREATE INDEX [IX_AppAnnouncement_EffectiveDate_ExpirationDate] ON [dbo].[AppAnnouncement]
(
	[EffectiveDate], 
	[ExpirationDate]
);


--
--SecurityAction
--
CREATE TABLE [dbo].[SecurityAction] (
	[SecurityActionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Description] [varchar](2000) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityAction] PRIMARY KEY CLUSTERED 
	(
		[SecurityActionId] ASC
	)
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecurityAction_Name] ON [dbo].[SecurityAction]
(
	[Name] ASC
);

--
--SecurityRole
--
CREATE TABLE [dbo].[SecurityRole] (
	[SecurityRoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Description] [varchar](2000),
	[ADGroupName] [varchar](255),
	[Default] [bit] NOT NULL DEFAULT 0,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityRole] PRIMARY KEY CLUSTERED 
	(
		[SecurityRoleId] ASC
	)
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecurityRole_Name] ON [dbo].[SecurityRole]
(
	[Name] ASC
);

--
--SecuritySecurable
--
CREATE TABLE [dbo].[SecuritySecurable](
	[SecuritySecurableId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[ParentSecuritySecurableId] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecuritySecurable] PRIMARY KEY CLUSTERED 
	(
		[SecuritySecurableId] ASC
	),
	FOREIGN KEY ([ParentSecuritySecurableId]) REFERENCES [SecuritySecurable]([SecuritySecurableId])
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecuritySecurable_Name] ON [dbo].[SecuritySecurable]
(
	[Name] ASC
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecuritySecurable_ParentSecuritySecurableId] ON [dbo].[SecuritySecurable]
(
	[ParentSecuritySecurableId] ASC,
	[SecuritySecurableId] ASC
);

--
--SecurityAccess
--
CREATE TABLE [dbo].[SecurityAccess](
	[SecurityAccessId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityRoleId] [int] NOT NULL,
	[SecuritySecurableId] [int] NOT NULL,
	[SecurityActionId] [int] NOT NULL,
	[Allowed] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityAccess] PRIMARY KEY CLUSTERED 
	(
		[SecurityAccessId] ASC
	),
	FOREIGN KEY ([SecurityRoleId]) REFERENCES [SecurityRole]([SecurityRoleId]),
	FOREIGN KEY ([SecuritySecurableId]) REFERENCES [SecuritySecurable]([SecuritySecurableId]),
	FOREIGN KEY ([SecurityActionId]) REFERENCES [SecurityAction]([SecurityActionId])
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecurityAccess_Role_Securable_Action] ON [dbo].[SecurityAccess]
(
	[SecurityRoleId] ASC,
	[SecuritySecurableId] ASC,
	[SecurityActionId] ASC
);

--
--SecuritySecurableAction
--
CREATE TABLE [dbo].[SecuritySecurableAction](
	[SecuritySecurableActionId] [int] IDENTITY(1,1) NOT NULL,
	[SecuritySecurableId] [int] NOT NULL,
	[SecurityActionId] [int] NOT NULL,
	[LoggedEvent] [bit] NOT NULL DEFAULT 0,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecuritySecurableAction] PRIMARY KEY CLUSTERED 
	(
		[SecuritySecurableActionId] ASC
	),
	FOREIGN KEY ([SecuritySecurableId]) REFERENCES [SecuritySecurable]([SecuritySecurableId]),
	FOREIGN KEY ([SecurityActionId]) REFERENCES [SecurityAction]([SecurityActionId])
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecuritySecurableAction_SecurableId_ActionId] ON [dbo].[SecuritySecurableAction]
(
	[SecuritySecurableId] ASC,
	[SecurityActionId] ASC
);

--
--Security User
--
CREATE TABLE [dbo].[SecurityUser](
	[SecurityUserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[EmailAddress] [varchar](255) NULL,
	[ExternalId] [varchar](255) NULL,
	[FirstName] [varchar](500) NULL,
	[LastName] [varchar](500) NULL,
	[AuthenticationMethod] [varchar](50) NULL,
	[PasswordHash] [varchar](500) NULL,
	[PasswordSalt] [varchar](50) NULL,
	[PasswordLastChangedDate] [datetime] NULL,
	[PasswordExpirationDate] [datetime] NULL,
	[PasswordNeverExpires] [BIT] DEFAULT 0 NOT NULL,
	[UserCannotChangePassword] [BIT] DEFAULT 0 NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[AccountActivated] [bit] NOT NULL DEFAULT 1,
	[AccountLocked] [bit] NOT NULL DEFAULT 0,
	[AccountLockedDate] [datetime] NULL,
	[AccountExpirationDate] [datetime] NULL,
	[NumConsecutiveFailedLogins] [int] NOT NULL DEFAULT 0,
	[ActivationRequestDate] [datetime] NULL,
	[ActivationConfirmedDate] [datetime] NULL,
	[ActivationCookie] [varchar](50) NULL,
	[TermsAndConditionsVersion] [varchar](50) NULL,
	[ActiveDirectoryGuid] [uniqueidentifier] NULL,
	[ActiveDirectoryDn] [varchar](500) NULL,
	[UserType] [varchar](100) NULL,
	[TimeZone] [varchar](100) NULL,
	[Locale] [varchar](100) NULL,
	[SystemAdmin] [bit] NOT NULL DEFAULT 0,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityUser] PRIMARY KEY CLUSTERED 
	(
		[SecurityUserId] ASC
	)
);

CREATE UNIQUE INDEX [IX_SecurityUser_UserName] ON [dbo].[SecurityUser]
(
	[UserName] ASC
);

--
--SecurityUserRoleMembership
--
CREATE TABLE [dbo].[SecurityUserRoleMembership](
	[SecurityUserRoleMembershipId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityRoleId] [int] NOT NULL,
	[SecurityUserId] [int] NOT NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityUserRoleMembership] PRIMARY KEY CLUSTERED 
	(
		[SecurityUserRoleMembershipId] ASC
	),
	FOREIGN KEY ([SecurityRoleId]) REFERENCES [SecurityRole]([SecurityRoleId]),
	FOREIGN KEY ([SecurityUserId]) REFERENCES [SecurityUser]([SecurityUserId])
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecurityUserRoleMembership_Role_User] ON [dbo].[SecurityUserRoleMembership]
(
	[SecurityRoleId] ASC,
	[SecurityUserId] ASC
);

--
--SecurityQuestion
--
CREATE TABLE [dbo].[SecurityQuestion] (
	[SecurityQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [varchar](500) NOT NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityQuestion] PRIMARY KEY CLUSTERED 
	(
		[SecurityQuestionId] ASC
	)
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecurityRole_Question] ON [dbo].[SecurityQuestion]
(
	[Question] ASC
);

--
--SecurityUserQuestionAnswer
--
CREATE TABLE [dbo].[SecurityUserQuestionAnswer](
	[SecurityUserQuestionAnswerId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityQuestionId] [int] NOT NULL,
	[SecurityUserId] [int] NOT NULL,
	[QuestionAnswerHash] [varchar](2000) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityUserQuestionAnswer] PRIMARY KEY CLUSTERED 
	(
		[SecurityUserQuestionAnswerId] ASC
	),
	FOREIGN KEY ([SecurityQuestionId]) REFERENCES [SecurityQuestion]([SecurityQuestionId]),
	FOREIGN KEY ([SecurityUserId]) REFERENCES [SecurityUser]([SecurityUserId])
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_SecurityUserQuestionAnswer_Question_User] ON [dbo].[SecurityUserQuestionAnswer]
(
	[SecurityQuestionId] ASC,
	[SecurityUserId] ASC
);

--
--Security User Login History
--
CREATE TABLE [dbo].[SecurityUserLoginHistory] (
	[SecurityUserLoginHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityUserId] [int] NULL,
	[UserName] [varchar](100) NULL,
	[MachineName] [varchar](150) NULL,
	[ApplicationCode] [varchar](50) NULL,			
	[SuccessfulLogin] [bit] DEFAULT 0 NOT NULL,
	[AccountWasLocked] [bit] DEFAULT 0 NOT NULL,
	[IPAddress] [varchar](100) NULL,
	[Browser] [varchar](255) NULL,
	[ScreenResolution] [varchar](255) NULL,
	[Message] varchar(2000) NULL,
	[SessionId] [varchar](255) NULL,
	[SessionEndDate] [datetime] NULL,
	[LastRequestDate] [datetime] NULL,
	[SessionTimeoutDate] [datetime] NULL,
	[LastRequestUrl] [varchar](2000) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityUserLog] PRIMARY KEY CLUSTERED 
	(
		[SecurityUserLoginHistoryId] ASC
	),
	FOREIGN KEY ([SecurityUserId]) REFERENCES [SecurityUser]([SecurityUserId])
);

CREATE INDEX [IX_SecurityUserLoginHistory_UserName_CreateDate] ON [dbo].[SecurityUserLoginHistory]
(
	[UserName] ASC,
	[CreateDate] ASC
);


CREATE INDEX [IX_SecurityUserLoginHistory_SecurityUserId_CreateDate] ON [dbo].[SecurityUserLoginHistory]
(
	[SecurityUserId] ASC,
	[CreateDate] ASC
);

CREATE INDEX [IX_SecurityUserLoginHistory_CreateDate] ON [dbo].[SecurityUserLoginHistory]
(
	[CreateDate] ASC
);


--
--Security User Password History
--
CREATE TABLE [dbo].[SecurityUserPasswordHistory] (
	[SecurityUserPasswordHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityUserId] [int] NOT NULL,
	[PasswordHash] [varchar](500) NULL,
	[PasswordSalt] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityUserPasswordHistory] PRIMARY KEY CLUSTERED 
	(
		[SecurityUserPasswordHistoryId] ASC
	),
	FOREIGN KEY ([SecurityUserId]) REFERENCES [SecurityUser]([SecurityUserId])
);

CREATE INDEX [IX_SecurityUserLoginHistory_SecurityUserId] ON [dbo].[SecurityUserPasswordHistory]
(
	[SecurityUserId] ASC
);

--
--Security User Activity History
--
CREATE TABLE [dbo].[SecurityUserActivityHistory] (
	[SecurityUserActivityHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityUserId] [int] NULL,
	[SecurityUserLoginHistoryId] [int] NULL,
	[UserName] [varchar](100) NULL,
	[MachineName] [varchar](150) NULL,
	[ApplicationCode] [varchar](50) NULL,			
	[IPAddress] [varchar](100) NULL,
	[SessionId] [varchar](255) NULL,
	[TransactionId] [varchar](100) NULL,
	[RequestUrl] [varchar](2000) NULL,
	[Method] [varchar](100) NULL,
	[StatusCode] [int] NULL,
	[LoadTimeMS] [int] NULL,
	[Referrer] [varchar](2000) NULL,
	[SecuritySecurableActionId] [int] NULL,
	[AddtlInfo] [varchar](MAX) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityUserActivityHistory] PRIMARY KEY CLUSTERED 
	(
		[SecurityUserActivityHistoryId] ASC
	)
);

CREATE INDEX [IX_SecurityUserActivityHistory_SecurityUserId] ON [dbo].[SecurityUserActivityHistory]
(
	[SecurityUserId] ASC,
	[CreateDate] ASC
);

CREATE INDEX [IX_SecurityUserActivityHistory_CreateDate_SecurityUserId] ON [dbo].[SecurityUserActivityHistory]
(
	[CreateDate] ASC,
	[SecurityUserId] ASC
);

CREATE INDEX [IX_SecurityUserActivityHistory_SecurityUserLoginHistoryId] ON [dbo].[SecurityUserActivityHistory]
(
	[SecurityUserLoginHistoryId] ASC,
	[CreateDate] ASC
);

--
--Password Reset Request
--
CREATE TABLE [dbo].[SecurityPasswordResetRequest] (
	[SecurityPasswordResetRequestId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityUserId] [int] NULL,
	[UserName] [varchar](100) NULL,
	[Token] [varchar](255) NOT NULL,
	[EmailAddress] [varchar](255) NULL,
	[IPAddress] [varchar](100) NULL,
	[RequestDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[Processed] [bit] NOT NULL DEFAULT 0,
	[ProcessDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityPasswordResetRequest] PRIMARY KEY CLUSTERED 
	(
		[SecurityPasswordResetRequestId] ASC
	),
	FOREIGN KEY ([SecurityUserId]) REFERENCES [SecurityUser]([SecurityUserId])
);

CREATE INDEX [IX_SecurityPasswordResetRequest_SecurityUserId_Token] ON [dbo].[SecurityPasswordResetRequest]
(
	[SecurityUserId] ASC,
	[Token] ASC
);

CREATE INDEX [IX_SecurityPasswordResetRequest_UserName_Token] ON [dbo].[SecurityPasswordResetRequest]
(
	[UserName] ASC,
	[Token] ASC
);


--
--Single Sign On Token
--
CREATE TABLE [dbo].[SecuritySingleSignOnToken] (
	[SecuritySingleSignOnTokenId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityUserId] [int] NULL,
	[UserName] [varchar](100) NULL,
	[Token] [varchar](255) NOT NULL,
	[IPAddress] [varchar](100) NULL,
	[RequestDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[Processed] [bit] NOT NULL DEFAULT 0,
	[ProcessDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecuritySingleSignOnToken] PRIMARY KEY CLUSTERED 
	(
		[SecuritySingleSignOnTokenId] ASC
	),
	FOREIGN KEY ([SecurityUserId]) REFERENCES [SecurityUser]([SecurityUserId])
);

CREATE INDEX [IX_SecuritySingleSignOnToken_SecurityUserId_Token] ON [dbo].[SecuritySingleSignOnToken]
(
	[SecurityUserId] ASC,
	[Token] ASC
);

CREATE INDEX [IX_SecuritySingleSignOnToken_UserName_Token] ON [dbo].[SecuritySingleSignOnToken]
(
	[UserName] ASC,
	[Token] ASC
);


--
--Account Activation
--
CREATE TABLE [dbo].[SecurityUserActivation] (
	[SecurityUserActivationId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityUserId] [int] NOT NULL,
	[UserName] [varchar](100) NULL,
	[Token] [varchar](255) NOT NULL,
	[EmailAddress] [varchar](255) NULL,
    [IPAddress] [varchar](100) NULL,
	[RequestDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[Processed] [bit] NOT NULL DEFAULT 0,
	[ProcessDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_SecurityUserActivation] PRIMARY KEY CLUSTERED 
	(
		[SecurityUserActivationId] ASC
	),
	FOREIGN KEY ([SecurityUserId]) REFERENCES [SecurityUser]([SecurityUserId])
);

CREATE INDEX [IX_SecurityUserActivation_SecurityUserId_Token] ON [dbo].[SecurityUserActivation]
(
	[SecurityUserId] ASC,
	[Token] ASC
);

CREATE INDEX [IX_SecurityUserActivation_UserName_Token] ON [dbo].[SecurityUserActivation]
(
	[UserName] ASC,
	[Token] ASC
);


--
--AppSQLLog
--
CREATE TABLE [dbo].[AppSQLLog](
	[AppSQLLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[SecurityUserId] [int] NULL,
	[SecurityUserLoginHistoryId] [int] NULL,
	[UserName] [varchar](100) NULL,
	[MachineName] [varchar](150) NULL,
	[ApplicationCode] [varchar](50) NULL,
	[SessionId] [varchar](100) NULL,
	[TransactionId] [varchar](100) NULL,
	[EventDate] [datetime2](7) DEFAULT GETDATE() NOT NULL,
	[Connection] [varchar](255) NULL,
	[SQLStatement] [varchar](max) NULL,
	[SQLParameters] [varchar](max) NULL,
	[Successful] [bit] default 0 NOT NULL,
	[ExecuteTimeMS] [int] NULL,
	[ExceptionDetail] [varchar](max) NULL,
	[AddtlInfo] [varchar](MAX) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_AppSQLLog] PRIMARY KEY CLUSTERED 
	(
		[AppSQLLogId] ASC
	)
);

CREATE INDEX [IX_AppSQLLog_EventDate] ON [dbo].[AppSQLLog]
(
	[CreateDate] ASC
);

CREATE INDEX [IX_AppSQLLog_SecurityUserId] ON [dbo].[AppSQLLog]
(
	[SecurityUserId] ASC,
	[CreateDate] ASC
);

CREATE INDEX [IX_AppSQLLog_SecurityUserLoginHistoryId] ON [dbo].[AppSQLLog]
(
	[SecurityUserLoginHistoryId] ASC,
	[CreateDate] ASC
);

GO

-------------------------------------------------------------------------------------------------
--
-- VIEWS
--
-------------------------------------------------------------------------------------------------

--
--[vw_SecurityUser]
--
CREATE VIEW [dbo].[vw_SecurityUser]
AS
SELECT [SecurityUserId]
      ,[UserName]
      ,[EmailAddress]
      ,[ExternalId]
      ,[FirstName]
      ,[LastName]
      ,[AuthenticationMethod]
      ,[PasswordHash]
      ,[PasswordSalt]
      ,[PasswordLastChangedDate]
      ,[PasswordExpirationDate]
      ,[PasswordNeverExpires]
      ,[UserCannotChangePassword]
      ,[LastLoginDate]
      ,[AccountActivated]
      ,[AccountLocked]
      ,[AccountLockedDate]
      ,[AccountExpirationDate]
      ,[NumConsecutiveFailedLogins]
      ,[ActivationRequestDate]
      ,[ActivationConfirmedDate]
      ,[ActivationCookie]
      ,[TermsAndConditionsVersion]
      ,[ActiveDirectoryGuid]
      ,[ActiveDirectoryDn]
      ,[UserType]
      ,[TimeZone]
      ,[Locale]
      ,[SystemAdmin]
      ,[Active]
      ,[CreateDate]
      ,[CreateUser]
      ,[ModifyDate]
      ,[ModifyUser]
	  ,ISNULL((SELECT 
			Stuff((SELECT ', ' + sr.Name AS [text()]
			  FROM SecurityUserRoleMembership surm
			  JOIN SecurityRole sr ON surm.SecurityRoleId = sr.SecurityRoleId 
			  WHERE surm.SecurityUserId = su.SecurityUserId
			  For XML PATH ('')),1,1,'')), 'None') as AssignedRoles
FROM [dbo].[SecurityUser] su;

GO

-------------------------------------------------------------------------------------------------
--
-- STORED PROCS
--
-------------------------------------------------------------------------------------------------

--
--[usp_Report_SecurityUserLoginHistory]
--
CREATE PROCEDURE [dbo].[usp_Report_SecurityUserLoginHistory]
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @SecurityUserId INT = NULL
AS
BEGIN
SELECT [SecurityUserLoginHistoryId]
      ,[SecurityUserId]
      ,[UserName]
      ,[MachineName]
      ,[ApplicationCode]
      ,[SuccessfulLogin]
      ,[AccountWasLocked]
      ,[IPAddress]
      ,[Browser]
      ,[ScreenResolution]
      ,[Message]
      ,[SessionId]
      ,[SessionEndDate]
      ,[LastRequestDate]
      ,[SessionTimeoutDate]
      ,[LastRequestUrl]
      ,[CreateDate]
      ,[CreateUser]
      ,[ModifyDate]
      ,[ModifyUser]
FROM [dbo].[SecurityUserLoginHistory] his
WHERE (@StartDate IS NULL OR his.CreateDate >= @StartDate)
AND (@EndDate IS NULL OR his.CreateDate <= @EndDate)
AND (@SecurityUserId IS NULL OR his.SecurityUserId = @SecurityUserId) 
ORDER BY
	his.CreateDate DESC,
	his.UserName ASC
END;
GO

-------------------------------------------------------------------------------------------------
--
-- DEFAULT USERS
--
-------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT SecurityUser ON

INSERT INTO [dbo].[SecurityUser]
           ([SecurityUserId]
		   ,[UserName]
           ,[EmailAddress]
           ,[ExternalId]
           ,[FirstName]
           ,[LastName]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[PasswordLastChangedDate]
           ,[PasswordExpirationDate]
		   ,[PasswordNeverExpires]
		   ,[UserCannotChangePassword]
           ,[LastLoginDate]
           ,[AccountActivated]
           ,[AccountLocked]
           ,[AccountLockedDate]
           ,[NumConsecutiveFailedLogins]
           ,[ActivationRequestDate]
           ,[ActivationConfirmedDate]
           ,[ActiveDirectoryGuid]
           ,[ActiveDirectoryDn]
           ,[UserType]
           ,[TimeZone]
           ,[SystemAdmin]
           ,[Active]
           ,[ActivationCookie]
           ,[TermsAndConditionsVersion]
           ,[CreateDate]
           ,[CreateUser]
           ,[ModifyDate]
           ,[ModifyUser])
     VALUES
           (1,
		   'Admin'
           ,'tea@swc.com'
           ,NULL
           ,'System'
           ,'Admin'
           ,'KXLnyD2noGbAVqQSdVG7YX0hMEnOgFyWtoRPjbt8KpQ=' --Password1
           ,'J1z+P6bT'
           ,GETDATE()
           ,GETDATE()
		   ,1 --[PasswordNeverExpires]
		   ,0 --[UserCannotChangePassword]
           ,NULL
           ,1 --[AccountActivated]
           ,0 --<AccountLocked, bit,>
           ,NULL --<AccountLockedDate, datetime,>
           ,0 --<NumConsecutiveFailedLogins, int,>
           ,NULL --<ActivationRequestDate, datetime,>
           ,NULL --<ActivationConfirmedDate, datetime,>
           ,NULL --<ActiveDirectoryGuid, uniqueidentifier,>
           ,NULL --<ActiveDirectoryDn, varchar(500),>
           ,NULL --<UserType, varchar(100),>
           ,NULL --<TimeZone, varchar(100),>
           ,1 --<Admin, bit,>
           ,1 --<Active, bit,>
           ,NULL --<ActivationCookie, varchar(50),>
           ,NULL --<TermsAndConditionsVersion, varchar(50),>
           ,GETDATE()
           ,'Admin'
           ,NULL --<ModifyDate, datetime,>
           ,NULL --<ModifyUser, varchar(100)
           );
GO

INSERT INTO [dbo].[SecurityUser]
           ([SecurityUserId]
		   ,[UserName]
           ,[EmailAddress]
           ,[ExternalId]
           ,[FirstName]
           ,[LastName]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[PasswordLastChangedDate]
           ,[PasswordExpirationDate]
		   ,[PasswordNeverExpires]
		   ,[UserCannotChangePassword]
           ,[LastLoginDate]
           ,[AccountActivated]
           ,[AccountLocked]
           ,[AccountLockedDate]
           ,[NumConsecutiveFailedLogins]
           ,[ActivationRequestDate]
           ,[ActivationConfirmedDate]
           ,[ActiveDirectoryGuid]
           ,[ActiveDirectoryDn]
           ,[UserType]
           ,[TimeZone]
           ,[SystemAdmin]
           ,[Active]
           ,[ActivationCookie]
           ,[TermsAndConditionsVersion]
           ,[CreateDate]
           ,[CreateUser]
           ,[ModifyDate]
           ,[ModifyUser])
     VALUES
           (2,
		   'Anonymous'
           ,NULL
           ,NULL
           ,'Anonymous'
           ,'User'
           ,NULL --Password
           ,NULL
           ,GETDATE()
           ,GETDATE()
		   ,0 --[PasswordNeverExpires]
		   ,0 --[UserCannotChangePassword]
           ,NULL
           ,0 --[AccountActivated]
           ,1 --<AccountLocked, bit,>
           ,NULL --<AccountLockedDate, datetime,>
           ,0 --<NumConsecutiveFailedLogins, int,>
           ,NULL --<ActivationRequestDate, datetime,>
           ,NULL --<ActivationConfirmedDate, datetime,>
           ,NULL --<ActiveDirectoryGuid, uniqueidentifier,>
           ,NULL --<ActiveDirectoryDn, varchar(500),>
           ,NULL --<UserType, varchar(100),>
           ,NULL --<TimeZone, varchar(100),>
           ,0 --<Admin, bit,>
           ,0 --<Active, bit,>
           ,NULL --<ActivationCookie, varchar(50),>
           ,NULL --<TermsAndConditionsVersion, varchar(50),>
           ,GETDATE()
           ,'Admin'
           ,NULL --<ModifyDate, datetime,>
           ,NULL --<ModifyUser, varchar(100)
           );
GO

INSERT INTO [dbo].[SecurityUser]
           ([SecurityUserId]
		   ,[UserName]
           ,[EmailAddress]
           ,[ExternalId]
           ,[FirstName]
           ,[LastName]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[PasswordLastChangedDate]
           ,[PasswordExpirationDate]
		   ,[PasswordNeverExpires]
		   ,[UserCannotChangePassword]
           ,[LastLoginDate]
           ,[AccountActivated]
           ,[AccountLocked]
           ,[AccountLockedDate]
           ,[NumConsecutiveFailedLogins]
           ,[ActivationRequestDate]
           ,[ActivationConfirmedDate]
           ,[ActiveDirectoryGuid]
           ,[ActiveDirectoryDn]
           ,[UserType]
           ,[TimeZone]
           ,[SystemAdmin]
           ,[Active]
           ,[ActivationCookie]
           ,[TermsAndConditionsVersion]
           ,[CreateDate]
           ,[CreateUser]
           ,[ModifyDate]
           ,[ModifyUser])
     VALUES
           (3,
		   'TestUser'
           ,'tea@swc.com'
           ,NULL
           ,'Test'
           ,'User'
           ,'KXLnyD2noGbAVqQSdVG7YX0hMEnOgFyWtoRPjbt8KpQ=' --Password1
           ,'J1z+P6bT' --salt
           ,GETDATE() --expiration
           ,GETDATE() --last changed
		   ,0 --[PasswordNeverExpires]
		   ,0 --[UserCannotChangePassword]
           ,NULL
           ,1 --[AccountActivated]
           ,0 --<AccountLocked, bit,>
           ,NULL --<AccountLockedDate, datetime,>
           ,0 --<NumConsecutiveFailedLogins, int,>
           ,NULL --<ActivationRequestDate, datetime,>
           ,NULL --<ActivationConfirmedDate, datetime,>
           ,NULL --<ActiveDirectoryGuid, uniqueidentifier,>
           ,NULL --<ActiveDirectoryDn, varchar(500),>
           ,NULL --<UserType, varchar(100),>
           ,NULL --<TimeZone, varchar(100),>
           ,0 --<Admin, bit,>
           ,1 --<Active, bit,>
           ,NULL --<ActivationCookie, varchar(50),>
           ,NULL --<TermsAndConditionsVersion, varchar(50),>
           ,GETDATE()
           ,'Admin'
           ,NULL --<ModifyDate, datetime,>
           ,NULL --<ModifyUser, varchar(100)
           );
GO

SET IDENTITY_INSERT SecurityUser OFF


-------------------------------------------------------------------------------------------------
--
-- DEFAULT EMAIL TEMPLATES
--
-------------------------------------------------------------------------------------------------
--
--Forgot Password Email Template
--
INSERT INTO [dbo].[AppEmailTemplate]
           ([TemplateCode]
           ,[Name]
           ,[Description]
           ,[EmailTo]
           ,[EmailCC]
           ,[EmailBCC]
           ,[EmailFrom]
           ,[EmailSubject]
           ,[EmailBody]
           ,[Html]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
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
           ,1 --HTML
           ,1 --Active
           ,GETDATE()
           ,'Admin');

--
--Password Changed Email Template
--
INSERT INTO [dbo].[AppEmailTemplate]
           ([TemplateCode]
           ,[Name]
           ,[Description]
           ,[EmailTo]
           ,[EmailCC]
           ,[EmailBCC]
           ,[EmailFrom]
           ,[EmailSubject]
           ,[EmailBody]
           ,[Html]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
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
           ,1 --HTML
           ,1 --Active
           ,GETDATE()
           ,'Admin');

--
--Account Locked Email Template
--
INSERT INTO [dbo].[AppEmailTemplate]
           ([TemplateCode]
           ,[Name]
           ,[Description]
           ,[EmailTo]
           ,[EmailCC]
           ,[EmailBCC]
           ,[EmailFrom]
           ,[EmailSubject]
           ,[EmailBody]
           ,[Html]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
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
           ,1 --HTML
           ,1 --Active
           ,GETDATE()
           ,'Admin');


-------------------------------------------------------------------------------------------------
--
-- DEFAULT MENUS
--
-------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT AppMenu ON

--
--Main Root Menu
--
INSERT INTO [dbo].[AppMenu]
           ([AppMenuId]
		   ,[MenuCode]
           ,[Name]
           ,[Description]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (1
		   ,'ROOT'
           ,'Root Menu'
           ,'The main application menu.'
           ,1
           ,GETDATE()
           ,'Admin');


--
--Main Top Menu
--
INSERT INTO [dbo].[AppMenu]
           ([AppMenuId]
		   ,[MenuCode]
           ,[Name]
           ,[Description]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (2
		   ,'TOPMENU'
           ,'Top Menu'
           ,'The top navigation menu in the application.'
           ,1
           ,GETDATE()
           ,'Admin');
SET IDENTITY_INSERT AppMenu OFF

--
--Root Menu Node
--
SET IDENTITY_INSERT AppMenuItem ON

--
--Home Menu
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (1000
		   ,1 --The root menu created above
           ,NULL --ParentAppMenuItemId
           ,'Home' --Name
           ,'/' --Handler
           ,'fa-th-large' --Image
           ,'Home' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Site Functions Menu
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (2000
		   ,1 --The root menu created above
           ,NULL --ParentAppMenuItemId
           ,'SiteFunctions' --Name
           ,NULL --Handler
           ,'fa-table' --Image
           ,'Site Functions' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,200 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Administration Menu
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4000
		   ,1 --The root menu created above
           ,NULL --ParentAppMenuItemId
           ,'Administration' --Name
           ,NULL --Handler
           ,'fa-database' --Image
           ,'Administration' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,300 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Administration->Application Menu
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4200
		   ,1 --The root menu created above
           ,4000 --ParentAppMenuItemId
           ,'AdminTools' --Name
           ,NULL --Handler
           ,'fa-wrench' --Image
           ,'Tools' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--AES Key Generator under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4210
		   ,1 --The root menu created above
           ,4200 --ParentAppMenuItemId
           ,'AESKeyGenerator' --Name
           ,'/Admin/AESKeyGenerator' --Handler
           ,NULL --Image
           ,'AES Key Generator' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Password Hash Generator under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4220
		   ,1 --The root menu created above
           ,4200 --ParentAppMenuItemId
           ,'PasswordHashGenerator' --Name
           ,'/Admin/PasswordHashGenerator' --Handler
           ,NULL --Image
           ,'Password Hash Generator' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );



--
--Administration->Application Menu
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4400
		   ,1 --The root menu created above
           ,4000 --ParentAppMenuItemId
           ,'AdminApplication' --Name
           ,NULL --Handler
           ,'fa-life-buoy' --Image
           ,'Application' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--App Variable maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4410
		   ,1 --The root menu created above
           ,4400 --ParentAppMenuItemId
           ,'AppVariableMaintenance' --Name
           ,'/Admin/AppVariableMaintenance' --Handler
           ,NULL --Image
           ,'Variables' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--App Content maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4420
		   ,1 --The root menu created above
           ,4400 --ParentAppMenuItemId
           ,'AppContentMaintenance' --Name
           ,'/Admin/AppContentMaintenance' --Handler
           ,NULL --Image
           ,'Content' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--App Code Detail maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4430
		   ,1 --The root menu created above
           ,4400 --ParentAppMenuItemId
           ,'AppCodeDetailMaintenance' --Name
           ,'/Admin/AppCodeDetailMaintenance' --Handler
           ,NULL --Image
           ,'Codes' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--App Menu maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4440
		   ,1 --The root menu created above
           ,4400 --ParentAppMenuItemId
           ,'AppMenuMaintenance' --Name
           ,'/Admin/AppMenuMaintenance' --Handler
           ,NULL --Image
           ,'Menus' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );


--
--Email Template maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4450
		   ,1 --The root menu created above
           ,4400 --ParentAppMenuItemId
           ,'EmailTemplateMaintenance' --Name
           ,'/Admin/AppEmailTemplateMaintenance' --Handler
           ,NULL --Image
           ,'Email Templates' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );


--
--Announcement maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4460
		   ,1 --The root menu created above
           ,4400 --ParentAppMenuItemId
           ,'AnnouncementMaintenance' --Name
           ,'/Admin/AppAnnouncementMaintenance' --Handler
           ,NULL --Image
           ,'Announcements' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );


--
--Administration->Security Menu
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4600
		   ,1 --The root menu created above
           ,4000 --ParentAppMenuItemId
           ,'AdminSecurity' --Name
           ,NULL --Handler
           ,'fa-lock' --Image
           ,'Security' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Security Question maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4610
		   ,1 --The root menu created above
           ,4600 --ParentAppMenuItemId
           ,'SecurityQuestionMaintenance' --Name
           ,'/Admin/SecurityQuestionMaintenance' --Handler
           ,NULL --Image
           ,'Security Questions' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );


--
--Security Role maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4620
		   ,1 --The root menu created above
           ,4600 --ParentAppMenuItemId
           ,'SecurityRoleMaintenance' --Name
           ,'/Admin/SecurityRoleMaintenance' --Handler
           ,NULL --Image
           ,'Roles' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
		   
--
--Security Action maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4630
		   ,1 --The root menu created above
           ,4600 --ParentAppMenuItemId
           ,'SecurityActionMaintenance' --Name
           ,'/Admin/SecurityActionMaintenance' --Handler
           ,NULL --Image
           ,'Actions' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Security Securable maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4640
		   ,1 --The root menu created above
           ,4600 --ParentAppMenuItemId
           ,'SecuritySecurableMaintenance' --Name
           ,'/Admin/SecuritySecurableMaintenance' --Handler
           ,NULL --Image
           ,'Securables' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );


--
--User maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4650
		   ,1 --The root menu created above
           ,4600 --ParentAppMenuItemId
           ,'UserMaintenance' --Name
           ,'/Admin/SecurityUserMaintenance' --Handler
           ,NULL --Image
           ,'Users' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Security Access maintenance under admin menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4660
		   ,1 --The root menu created above
           ,4600 --ParentAppMenuItemId
           ,'SecurityAccessMaintenance' --Name
           ,'/Admin/SecurityAccessMaintenance' --Handler
           ,NULL --Image
           ,'Permissions' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );


--
--Reports Menu
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (5000
		   ,1 --The root menu created above
           ,NULL --ParentAppMenuItemId
           ,'Reports' --Name
           ,NULL --Handler
           ,'fa-file' --Image
           ,'Reports' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,400 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--User Login History under Reports menu
--
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (5101
		   ,1 --The root menu created above
           ,5000 --ParentAppMenuItemId
           ,'Login History' --Name
           ,'/Reports/SecurityUserLoginHistoryReport' --Handler
           ,NULL --Image
           ,'Login History' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

--
--Top Menu Item
--
SET IDENTITY_INSERT AppMenuItem ON
INSERT INTO [dbo].[AppMenuItem]
           ([AppMenuItemId]
		   ,[AppMenuId]
           ,[ParentAppMenuItemId]
           ,[Name]
           ,[Handler]
           ,[Image]
           ,[Text]
           ,[Style]
           ,[ToolTip]
           ,[Sort]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (8000
		   ,2 --The root menu created above
           ,NULL --ParentAppMenuItemId
           ,'Home' --Name
           ,'/' --Handler
           ,'fa-th-large' --Image
           ,'Home' --Text
           ,NULL --Style
           ,'' --ToolTip
           ,100 --Sort
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );

SET IDENTITY_INSERT AppMenuItem OFF


-------------------------------------------------------------------------------------------------
--
-- Security Roles
--
-------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT SecurityRole ON

--
--All Users Role
--
INSERT INTO [dbo].[SecurityRole]
           ([SecurityRoleId]
		   ,[Name]
           ,[Description]
           ,[ADGroupName]
		   ,[Default] 
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (1,
		   'All Users'
           ,'All authenticated users should have this role.'
           ,'Domain Users'
		   ,1 --Default
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO
SET IDENTITY_INSERT SecurityRole OFF

-------------------------------------------------------------------------------------------------
--
-- Security User Role Memberships
--
-------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT SecurityUserRoleMembership ON

INSERT INTO [dbo].[SecurityUserRoleMembership]
           ([SecurityUserRoleMembershipId]
		   ,[SecurityRoleId]
           ,[SecurityUserId]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (1
		   ,1 --Role Id (All Users)
           ,3 --SecurityUserId (TestUser)
           ,1 --Active
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

SET IDENTITY_INSERT SecurityUserRoleMembership OFF


-------------------------------------------------------------------------------------------------
--
-- Security Actions
--
-------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT SecurityAction ON

INSERT INTO [dbo].[SecurityAction]
           ([SecurityActionId]
		   ,[Name]
           ,[Description]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (1
		   ,'View' --name
           ,'If a page is viewable or not to a user. Only applies to pages, not controls.' --description
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAction]
           ([SecurityActionId]
		   ,[Name]
           ,[Description]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (2
		   ,'Visible' --name
           ,'If a control is visible on the screen to the user.' --description
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAction]
           ([SecurityActionId]
		   ,[Name]
           ,[Description]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (3
		   ,'Enabled' --name
           ,'If a control is enabled/editable on the screen to the user.' --description
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAction]
           ([SecurityActionId]
		   ,[Name]
           ,[Description]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (4
		   ,'Get' --name
           ,'Used for GET Requests on Web Services.' --description
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAction]
           ([SecurityActionId]
		   ,[Name]
           ,[Description]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (5
		   ,'Post' --name
           ,'Used for POST Requests on Web Services.' --description
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAction]
           ([SecurityActionId]
		   ,[Name]
           ,[Description]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (6
		   ,'Put' --name
           ,'Used for PUT Requests on Web Services.' --description
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAction]
           ([SecurityActionId]
		   ,[Name]
           ,[Description]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (7
		   ,'Delete' --name
           ,'Used for DELETE Requests on Web Services.' --description
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO


SET IDENTITY_INSERT SecurityAction OFF


-------------------------------------------------------------------------------------------------
--
-- Security Securables
--
-------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT SecuritySecurable ON

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (1
		   ,'swccatalysttemplate.web' --[name]
           ,NULL --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (2
		   ,'swccatalysttemplate.web.home' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (3
		   ,'swccatalysttemplate.web.home.index' --[name]
           ,2 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (20
		   ,'swccatalysttemplate.web.authentication' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (21
		   ,'swccatalysttemplate.web.authentication.password' --[name]
           ,20 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (22
		   ,'swccatalysttemplate.web.authentication.password.expired' --[name]
           ,21 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (23
		   ,'swccatalysttemplate.web.authentication.password.change' --[name]
           ,21 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (30
		   ,'swccatalysttemplate.web.admin' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (100
		   ,'swccatalysttemplate.web.leftnavigation' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (101
		   ,'swccatalysttemplate.web.leftnavigation.administration' --[name]
           ,100 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (110
		   ,'swccatalysttemplate.web.topnavigation' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (120
		   ,'swccatalysttemplate.web.toastmessage' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecuritySecurable]
           ([SecuritySecurableId]
		   ,[Name]
           ,[ParentSecuritySecurableId]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (130
		   ,'swccatalysttemplate.web.selectlist' --[name]
           ,1 --[ParentSecuritySecurableId]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

SET IDENTITY_INSERT SecuritySecurable OFF

-------------------------------------------------------------------------------------------------
--
-- Security Securable Actions
-- Setup default VISIBLE, VIEW and EDIT actions for each securable.
--
-------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[SecuritySecurableAction]
           ([SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[LoggedEvent]
           ,[CreateDate]
           ,[CreateUser])
SELECT sec.SecuritySecurableId
        ,act.SecurityActionId
        ,0 --[LoggedEvent]
        ,GETDATE() --CreateDate
        ,'Admin' --CreateUser
FROM SecuritySecurable sec,
     SecurityAction act
WHERE act.Name IN ('View', 'Visible', 'Enabled')

GO

-------------------------------------------------------------------------------------------------
--
-- Security Access
--
-------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT SecurityAccess ON

--
--Home page
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (1
		   ,1 --[SecurityRoleId]
           ,2 --[SecuritySecurableId] (Home)
		   ,1 --[SecurityActionId] (View)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (2
		   ,1 --[SecurityRoleId]
           ,2 --[SecuritySecurableId] (Home)
		   ,2 --[SecurityActionId] (Visible)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (3
		   ,1 --[SecurityRoleId]
           ,2 --[SecuritySecurableId] (Home)
		   ,3 --[SecurityActionId] (Enabled)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

--
--Authentication Area
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (20
		   ,1 --[SecurityRoleId]
           ,20 --[SecuritySecurableId] (Authentication)
		   ,1 --[SecurityActionId] (View)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (21
		   ,1 --[SecurityRoleId]
           ,20 --[SecuritySecurableId]  (Authentication)
		   ,2 --[SecurityActionId] (Visible)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (22
		   ,1 --[SecurityRoleId]
           ,20 --[SecuritySecurableId] (Authentication)
		   ,3 --[SecurityActionId] (Enabled)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

--
--Admin Area
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (30
		   ,1 --[SecurityRoleId]
           ,30 --[SecuritySecurableId] (Admin)
		   ,1 --[SecurityActionId] (View)
		   ,0 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (31
		   ,1 --[SecurityRoleId]
           ,30 --[SecuritySecurableId] (Admin)
		   ,2 --[SecurityActionId] (Visible)
		   ,0 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (32
		   ,1 --[SecurityRoleId]
           ,30 --[SecuritySecurableId] (Admin)
		   ,3 --[SecurityActionId] (Enabled)
		   ,0 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO


--
--Left Navigation Area
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (41
		   ,1 --[SecurityRoleId]
           ,100 --[SecuritySecurableId] 
		   ,2 --[SecurityActionId] (Visible)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (42
		   ,1 --[SecurityRoleId]
           ,100 --[SecuritySecurableId] 
		   ,3 --[SecurityActionId] (Enabled)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (43
		   ,1 --[SecurityRoleId]
           ,100 --[SecuritySecurableId] 
		   ,1 --[SecurityActionId] (View)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

--
--Left Navigation Administration Area
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (45
		   ,1 --[SecurityRoleId]
           ,101 --[SecuritySecurableId] (left nav)
		   ,2 --[SecurityActionId] (Visible)
		   ,0 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (46
		   ,1 --[SecurityRoleId]
           ,101 --[SecuritySecurableId] (left nav)
		   ,3 --[SecurityActionId] (Enabled)
		   ,0 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (47
		   ,1 --[SecurityRoleId]
           ,101 --[SecuritySecurableId] (left nav)
		   ,1 --[SecurityActionId] (View)
		   ,0 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

--
--Top Navigation Area
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (61
		   ,1 --[SecurityRoleId]
           ,110 --[SecuritySecurableId] (Top Nav)
		   ,2 --[SecurityActionId] (Visible)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (62
		   ,1 --[SecurityRoleId]
           ,110 --[SecuritySecurableId] (Top Nav)
		   ,3 --[SecurityActionId] (Enabled)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (63
		   ,1 --[SecurityRoleId]
           ,110 --[SecuritySecurableId] (Top Nav)
		   ,1 --[SecurityActionId] (View)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

--
--Toast Messages Area
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (70
		   ,1 --[SecurityRoleId]
           ,120 --[SecuritySecurableId] (toast messages)
		   ,1 --[SecurityActionId] (View)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (71
		   ,1 --[SecurityRoleId]
           ,120 --[SecuritySecurableId] (toast messages)
		   ,2 --[SecurityActionId] (Visible)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (72
		   ,1 --[SecurityRoleId]
           ,120 --[SecuritySecurableId] (toast messages)
		   ,3 --[SecurityActionId] (Enabled)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

--
--Select List Area
--
INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (80
		   ,1 --[SecurityRoleId]
           ,130 --[SecuritySecurableId] (select lists)
		   ,1 --[SecurityActionId] (View)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (81
		   ,1 --[SecurityRoleId]
           ,130 --[SecuritySecurableId] (select lists)
		   ,2 --[SecurityActionId] (Visible)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO

INSERT INTO [dbo].[SecurityAccess]
           ([SecurityAccessId]
		   ,[SecurityRoleId]
		   ,[SecuritySecurableId]
		   ,[SecurityActionId]
		   ,[Allowed]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           (82
		   ,1 --[SecurityRoleId]
           ,130 --[SecuritySecurableId] (select lists)
		   ,3 --[SecurityActionId] (Enabled)
		   ,1 --[Allowed]
           ,GETDATE() --CreateDate
           ,'Admin' --CreateUser
		   );
GO


SET IDENTITY_INSERT SecurityAccess OFF


-------------------------------------------------------------------------------------------------
--
-- App Code Details
--
-------------------------------------------------------------------------------------------------
--
--User Types
--
INSERT INTO [dbo].[AppCodeDetail]
           ([CodeGroup]
           ,[CodeValue]
           ,[Description]
           ,[Sort]
		   ,[Default]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           ('SecurityUser.UserType' --CodeGroup
           ,'Standard'
           ,'Standard User'
           ,100
		   ,0 --Default
           ,1 --<Active, bit,>
           ,GETDATE() --<CreateDate, datetime,>
           ,'Admin' --<CreateUser, varchar(100),>
		   );
GO

--
--Authentication Methods
--
INSERT INTO [dbo].[AppCodeDetail]
           ([CodeGroup]
           ,[CodeValue]
           ,[Description]
           ,[Sort]
		   ,[Default]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           ('SecurityUser.AuthenticationMethod' --CodeGroup
           ,'SecurityUser'
           ,'SQL Security User Table'
           ,100
		   ,1 --Default
           ,1 --<Active, bit,>
           ,GETDATE() --<CreateDate, datetime,>
           ,'Admin' --<CreateUser, varchar(100),>
		   );
GO

INSERT INTO [dbo].[AppCodeDetail]
           ([CodeGroup]
           ,[CodeValue]
           ,[Description]
           ,[Sort]
		   ,[Default]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           ('SecurityUser.AuthenticationMethod' --CodeGroup
           ,'ActiveDirectory'
           ,'Active Directory'
           ,200
		   ,0 --Default
           ,1 --<Active, bit,>
           ,GETDATE() --<CreateDate, datetime,>
           ,'Admin' --<CreateUser, varchar(100),>
		   );
GO

DBCC CHECKIDENT('AppCodeDetail', RESEED, 10000);
DBCC CHECKIDENT('AppEmailTemplate', RESEED, 10000);
DBCC CHECKIDENT('AppMenu', RESEED, 10000);
DBCC CHECKIDENT('AppMenuItem', RESEED, 10000);
DBCC CHECKIDENT('SecurityUser', RESEED, 10000);
DBCC CHECKIDENT('SecurityRole', RESEED, 10000);
DBCC CHECKIDENT('SecurityUserRoleMembership', RESEED, 10000);
DBCC CHECKIDENT('SecurityAction', RESEED, 10000);
DBCC CHECKIDENT('SecuritySecurable', RESEED, 10000);
DBCC CHECKIDENT('SecuritySecurableAction', RESEED, 10000);
DBCC CHECKIDENT('SecurityAccess', RESEED, 10000);

--
--Run this to replace the default catalyst template namespace with your custom app namespace
--This allows security to work then.
--
--UPDATE [SecuritySecurable]
--SET Name = REPLACE(Name, 'swccatalysttemplate', 'yourprojectnamespace');
--GO

