--
--This script contains the SQL scripts to be run to instatiate the project specific details
--for the application
--
--USE [CatalystTemplateDb]
--GO


IF OBJECT_ID('dbo.ExampleEntity', 'U') IS NOT NULL
	DROP TABLE ExampleEntity; 

--
--ExampleEntity
--
CREATE TABLE [dbo].[ExampleEntity](
	[ExampleEntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Description] [varchar](255) NOT NULL,
	[Active] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyUser] [varchar](100) NULL,
	CONSTRAINT [PK_ExampleEntity] PRIMARY KEY CLUSTERED 
	(
		[ExampleEntityId] ASC
	)
);

CREATE UNIQUE INDEX [IX_ExampleEntity_Name] ON [dbo].[ExampleEntity]
(
	[Name] ASC
);


-------------------------------------------------------------------------------------------------
--
-- Default Data
-- Add any default data / insert statements to be run below
-------------------------------------------------------------------------------------------------
--
--ExampleEntity
--
INSERT INTO [dbo].[ExampleEntity]
           ([Name]
           ,[Description]
           ,[Active]
           ,[CreateDate]
           ,[CreateUser])
     VALUES
           ('Test Entity' --Name
           ,'Just a test record' -- description
           ,'Standard User'
           ,1 --<Active, bit,>
           ,GETDATE() --<CreateDate, datetime,>
           ,'Admin' --<CreateUser, varchar(100),>
		   );
GO


-------------------------------------------------------------------------------------------------
--
-- Reseed sequences/identity columns
-- Only needed to do this if you want to set the starting identity value for tables to 
-- be a specific number
--
-------------------------------------------------------------------------------------------------
--DBCC CHECKIDENT('ExampleEntity', RESEED, 10000);
