USE [master]
GO
/****** Object:  Database [ControlStudy]    Script Date: 20.01.2022 23:02:45 ******/
CREATE DATABASE [ControlStudy]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ControlStudy', FILENAME = N'D:\SQL\ControlStudy\ControlStudy.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1048576KB )
 LOG ON 
( NAME = N'ControlStudy_log', FILENAME = N'D:\SQL\ControlStudy\ControlStudy_log.ldf' , SIZE = 1024KB , MAXSIZE = 2097152KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ControlStudy] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ControlStudy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ControlStudy] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ControlStudy] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ControlStudy] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ControlStudy] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ControlStudy] SET ARITHABORT OFF 
GO
ALTER DATABASE [ControlStudy] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ControlStudy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ControlStudy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ControlStudy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ControlStudy] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ControlStudy] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ControlStudy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ControlStudy] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ControlStudy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ControlStudy] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ControlStudy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ControlStudy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ControlStudy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ControlStudy] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ControlStudy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ControlStudy] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ControlStudy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ControlStudy] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ControlStudy] SET  MULTI_USER 
GO
ALTER DATABASE [ControlStudy] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ControlStudy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ControlStudy] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ControlStudy] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ControlStudy] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ControlStudy] SET QUERY_STORE = OFF
GO
USE [ControlStudy]
GO
/****** Object:  Table [dbo].[Discipline]    Script Date: 20.01.2022 23:02:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discipline](
	[IdDiscipline] [int] IDENTITY(1,1) NOT NULL,
	[Discipline] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Discipline] PRIMARY KEY CLUSTERED 
(
	[IdDiscipline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 20.01.2022 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[IdGroup] [int] IDENTITY(1,1) NOT NULL,
	[Group] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[IdGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 20.01.2022 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[IdPerson] [int] IDENTITY(1,1) NOT NULL,
	[Family] [varchar](100) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Patronimic] [varchar](100) NOT NULL,
	[IdGroup] [int] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[IdPerson] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Progress]    Script Date: 20.01.2022 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Progress](
	[IdProgress] [int] IDENTITY(1,1) NOT NULL,
	[IdPerson] [int] NOT NULL,
	[IdDiscipline] [int] NOT NULL,
	[Grade] [int] NOT NULL,
	[DateGrade] [date] NOT NULL,
 CONSTRAINT [PK_Progress] PRIMARY KEY CLUSTERED 
(
	[IdProgress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 20.01.2022 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 20.01.2022 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[IdSession] [int] IDENTITY(1,1) NOT NULL,
	[IdPerson] [int] NOT NULL,
	[DateSession] [datetime] NOT NULL,
	[Time] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[IdSession] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 20.01.2022 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[LoginUser] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[IdRole] [int] NOT NULL,
	[IdPerson] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Discipline] ON 

INSERT [dbo].[Discipline] ([IdDiscipline], [Discipline]) VALUES (1, N'Математика')
INSERT [dbo].[Discipline] ([IdDiscipline], [Discipline]) VALUES (2, N'Английский язык')
INSERT [dbo].[Discipline] ([IdDiscipline], [Discipline]) VALUES (3, N'Французский язык')
INSERT [dbo].[Discipline] ([IdDiscipline], [Discipline]) VALUES (4, N'Экономика')
INSERT [dbo].[Discipline] ([IdDiscipline], [Discipline]) VALUES (5, N'Философия')
SET IDENTITY_INSERT [dbo].[Discipline] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([IdGroup], [Group]) VALUES (6, N' ')
INSERT [dbo].[Group] ([IdGroup], [Group]) VALUES (1, N'115')
INSERT [dbo].[Group] ([IdGroup], [Group]) VALUES (2, N'215')
INSERT [dbo].[Group] ([IdGroup], [Group]) VALUES (3, N'315')
INSERT [dbo].[Group] ([IdGroup], [Group]) VALUES (4, N'415')
INSERT [dbo].[Group] ([IdGroup], [Group]) VALUES (5, N'515')
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([IdPerson], [Family], [Name], [Patronimic], [IdGroup]) VALUES (40, N'Артемов', N'Матвей', N'Романович', 6)
INSERT [dbo].[Person] ([IdPerson], [Family], [Name], [Patronimic], [IdGroup]) VALUES (41, N'Николаева', N'Анна', N'Викторовна', 6)
INSERT [dbo].[Person] ([IdPerson], [Family], [Name], [Patronimic], [IdGroup]) VALUES (42, N'Савин', N'Александр', N'Николаевич', 1)
INSERT [dbo].[Person] ([IdPerson], [Family], [Name], [Patronimic], [IdGroup]) VALUES (43, N'Данилова', N'Дарья', N'Алексеевна', 5)
INSERT [dbo].[Person] ([IdPerson], [Family], [Name], [Patronimic], [IdGroup]) VALUES (44, N'Петрова', N'Екатерина', N'Андреевна', 1)
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
SET IDENTITY_INSERT [dbo].[Progress] ON 

INSERT [dbo].[Progress] ([IdProgress], [IdPerson], [IdDiscipline], [Grade], [DateGrade]) VALUES (13, 44, 1, 5, CAST(N'2022-01-20' AS Date))
INSERT [dbo].[Progress] ([IdProgress], [IdPerson], [IdDiscipline], [Grade], [DateGrade]) VALUES (14, 44, 4, 5, CAST(N'2022-01-20' AS Date))
INSERT [dbo].[Progress] ([IdProgress], [IdPerson], [IdDiscipline], [Grade], [DateGrade]) VALUES (17, 44, 2, 5, CAST(N'2022-01-20' AS Date))
INSERT [dbo].[Progress] ([IdProgress], [IdPerson], [IdDiscipline], [Grade], [DateGrade]) VALUES (18, 43, 5, 4, CAST(N'2022-01-20' AS Date))
SET IDENTITY_INSERT [dbo].[Progress] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([IdRole], [Role]) VALUES (1, N'Студент')
INSERT [dbo].[Roles] ([IdRole], [Role]) VALUES (2, N'Преподаватель')
INSERT [dbo].[Roles] ([IdRole], [Role]) VALUES (3, N'Администратор')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Session] ON 

INSERT [dbo].[Session] ([IdSession], [IdPerson], [DateSession], [Time]) VALUES (7, 40, CAST(N'2022-01-20T20:48:18.377' AS DateTime), N'0:16:13')
INSERT [dbo].[Session] ([IdSession], [IdPerson], [DateSession], [Time]) VALUES (8, 41, CAST(N'2022-01-20T20:48:18.480' AS DateTime), N'0:11:5')
INSERT [dbo].[Session] ([IdSession], [IdPerson], [DateSession], [Time]) VALUES (9, 42, CAST(N'2022-01-20T19:29:12.810' AS DateTime), N'0:1:41')
INSERT [dbo].[Session] ([IdSession], [IdPerson], [DateSession], [Time]) VALUES (10, 44, CAST(N'2022-01-20T20:48:18.500' AS DateTime), N'0:0:30')
SET IDENTITY_INSERT [dbo].[Session] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [LoginUser], [Password], [IdRole], [IdPerson]) VALUES (41, N'Admin', N'Admin5++', 3, 40)
INSERT [dbo].[User] ([IdUser], [LoginUser], [Password], [IdRole], [IdPerson]) VALUES (42, N'Teacher', N'Teacher5++', 2, 41)
INSERT [dbo].[User] ([IdUser], [LoginUser], [Password], [IdRole], [IdPerson]) VALUES (43, N'Student115', N'Student115/', 1, 42)
INSERT [dbo].[User] ([IdUser], [LoginUser], [Password], [IdRole], [IdPerson]) VALUES (44, N'Student515', N'Student515/', 1, 43)
INSERT [dbo].[User] ([IdUser], [LoginUser], [Password], [IdRole], [IdPerson]) VALUES (45, N'Student2115', N'Student2115/', 1, 44)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Group__D38B8659968F684B]    Script Date: 20.01.2022 23:02:46 ******/
ALTER TABLE [dbo].[Group] ADD UNIQUE NONCLUSTERED 
(
	[Group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Group] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[Group] ([IdGroup])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Group]
GO
ALTER TABLE [dbo].[Progress]  WITH CHECK ADD  CONSTRAINT [FK_Discipline_Progress] FOREIGN KEY([IdDiscipline])
REFERENCES [dbo].[Discipline] ([IdDiscipline])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Progress] CHECK CONSTRAINT [FK_Discipline_Progress]
GO
ALTER TABLE [dbo].[Progress]  WITH CHECK ADD  CONSTRAINT [FK_Person_Progress] FOREIGN KEY([IdPerson])
REFERENCES [dbo].[Person] ([IdPerson])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Progress] CHECK CONSTRAINT [FK_Person_Progress]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Person] FOREIGN KEY([IdPerson])
REFERENCES [dbo].[Person] ([IdPerson])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Person]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Roles]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Users] FOREIGN KEY([IdPerson])
REFERENCES [dbo].[Person] ([IdPerson])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Users]
GO
USE [master]
GO
ALTER DATABASE [ControlStudy] SET  READ_WRITE 
GO
