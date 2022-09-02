USE MASTER
GO

DECLARE @DatabaseName nvarchar(50) = N'SmartDev'
	
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @DatabaseName OR name = @DatabaseName)))
BEGIN
	DECLARE @SQL varchar(max)
	SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
	FROM MASTER..SysProcesses
	WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId

	EXEC(@SQL)
	DROP DATABASE IF EXISTS SmartDev
END

GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'SmartDev')
BEGIN
	CREATE DATABASE [SmartDev]
END
GO

USE [SmartDev]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/30/2022 3:18:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 8/30/2022 3:18:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserID] [bigint] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[UserEmail] [nvarchar](50) NOT NULL,
	[UserPass] [varchar](50) NOT NULL,
)
GO

/****** Object:  Table [dbo].[Books]    Script Date: 8/30/2022 3:18:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Books]') AND type in (N'U'))
DROP TABLE [dbo].[Books]
GO

/****** Object:  Table [dbo].[Books]    Script Date: 8/30/2022 3:18:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books](
	[BookID] [bigint] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[BookName] [nvarchar](255) NOT NULL,
	[BookDescription] [nvarchar](max) NULL,
	[UserID] [bigint] NOT NULL FOREIGN KEY REFERENCES Users(UserID),
	[Status] [bit] NULL DEFAULT 0
) 
GO
SET IDENTITY_INSERT [Users] ON
INSERT INTO [Users] (UserID, UserName, UserEmail, UserPass) VALUES 
(1, 'Demo1', 'demo1@mail.test', 'aaa'), 
(2, 'Demo2', 'demo2@mail.test', 'bbb')
SET IDENTITY_INSERT [Users] OFF

SET IDENTITY_INSERT [Books] ON
INSERT INTO [Books] (BookID, BookName, BookDescription, UserID, Status) VALUES 
(1, 'Book1', 'Lorem Ipsum is simply dummy text of the printing and typesetting industry.', 1, 0),
(2, 'Book2', 'Lorem Ipsum is simply dummy text of the printing and typesetting industry.', 1, 1),
(3, 'Book3', 'Lorem Ipsum is simply dummy text of the printing and typesetting industry.', 2, 1)
SET IDENTITY_INSERT [Books] OFF