USE [master]
GO

IF DB_ID (N'EmpresaXYZ') IS NOT NULL
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'EmpresaXYZ'
	ALTER DATABASE [EmpresaXYZ] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE EmpresaXYZ;
END

/****** Object:  Database [EmpresaXYZ]    Script Date: 11/14/2012 5:56:15 AM ******/
CREATE DATABASE [EmpresaXYZ]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmpresaXYZ', FILENAME = N'$(input)\EmpresaXYZ.mdf' , SIZE = 5500KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmpresaXYZ_log', FILENAME = N'$(input)\EmpresaXYZ_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EmpresaXYZ] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmpresaXYZ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmpresaXYZ] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [EmpresaXYZ] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [EmpresaXYZ] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmpresaXYZ] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmpresaXYZ] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmpresaXYZ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmpresaXYZ] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EmpresaXYZ] SET  MULTI_USER 
GO
ALTER DATABASE [EmpresaXYZ] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmpresaXYZ] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmpresaXYZ] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmpresaXYZ] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [EmpresaXYZ]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 11/14/2012 5:56:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Branches](
	[BranchID] [int] NOT NULL,
	[BranchName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/14/2012 5:56:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Branch] [int] NULL,
	[JobTitle] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobTitles]    Script Date: 11/14/2012 5:56:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobTitles](
	[JobTitleId] [int] NOT NULL,
	[Job] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobTitleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (1, N'Head Office')
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (2, N'New York')
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (3, N'Washington DC')
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (4, N'Seattle')
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (5, N'San Francisco')
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (6, N'Los Angeles')
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (7, N'San Diego')
GO
INSERT [dbo].[Branches] ([BranchID], [BranchName]) VALUES (8, N'Boston')
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1001, N'Terry', N'Adams', CAST(0x7EFA0A00 AS Date), 2, 1)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1002, N'Toni', N'Poe', CAST(0x50FC0A00 AS Date), 5, 2)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1003, N'Charlie', N'Herb', CAST(0xDDFB0A00 AS Date), 4, 3)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1004, N'Diane', N'Prescott', CAST(0x70F70A00 AS Date), 1, 3)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1005, N'Glen', N'John', CAST(0x06F60A00 AS Date), 3, 1)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1006, N'Sean', N'Bentley', CAST(0xE0F50A00 AS Date), 2, 2)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1007, N'Will', N'Kennedy', CAST(0xD7F70A00 AS Date), 4, 3)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [DateOfBirth], [Branch], [JobTitle]) VALUES (1008, N'Dennis', N'Saylor', CAST(0x310B0B00 AS Date), 5, 1)
GO
INSERT [dbo].[JobTitles] ([JobTitleId], [Job]) VALUES (1, N'Branch Manager')
GO
INSERT [dbo].[JobTitles] ([JobTitleId], [Job]) VALUES (2, N'Barista')
GO
INSERT [dbo].[JobTitles] ([JobTitleId], [Job]) VALUES (3, N'Trainee Barista')
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Branches] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branches] ([BranchID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Branches]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_JobTitles] FOREIGN KEY([JobTitle])
REFERENCES [dbo].[JobTitles] ([JobTitleId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_JobTitles]
GO
USE [master]
GO
ALTER DATABASE [EmpresaXYZ] SET  READ_WRITE 
GO
