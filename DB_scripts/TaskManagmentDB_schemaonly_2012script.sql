USE [master]
GO
/****** Object:  Database [TaskManagmentDB]    Script Date: 11.04.2018 21:58:29 ******/
CREATE DATABASE [TaskManagmentDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskManagmentDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TaskManagmentDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskManagmentDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TaskManagmentDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskManagmentDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaskManagmentDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaskManagmentDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaskManagmentDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TaskManagmentDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaskManagmentDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TaskManagmentDB] SET  MULTI_USER 
GO
ALTER DATABASE [TaskManagmentDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaskManagmentDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaskManagmentDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaskManagmentDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TaskManagmentDB', N'ON'
GO
USE [TaskManagmentDB]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 11.04.2018 21:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attributes](
	[Name] [nvarchar](50) NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Attributes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttrValues]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttrValues](
	[Value] [bit] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttrID] [int] NOT NULL,
	[WorkID] [int] NOT NULL,
 CONSTRAINT [PK_AttrValues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropDataTypes]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropDataTypes](
	[Data] [varchar](max) NOT NULL,
	[PropID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_PropDataTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[DataType] [int] NOT NULL,
	[PropName] [varchar](50) NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropValues]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropValues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DataType] [int] NOT NULL,
	[PropID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
	[ValueText] [nvarchar](max) NULL,
	[ValueInt] [int] NULL,
	[ValueDate] [date] NULL,
	[ValueTime] [time](7) NULL,
 CONSTRAINT [PK_PropValues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskName] [varchar](50) NOT NULL,
	[TaskTypeID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentTaskID] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskTypeProps]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskTypeProps](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaskTypeID] [int] NOT NULL,
	[PropID] [int] NOT NULL,
 CONSTRAINT [PK_TTypeProps] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskTypes]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskTypes](
	[TypeName] [varchar](50) NOT NULL,
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_TTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](10) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTasks]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
 CONSTRAINT [PK_UserTasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Works]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Works](
	[WorkName] [varchar](50) NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[WorkTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Works] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkTypeAttrs]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkTypeAttrs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WorkTypeID] [int] NOT NULL,
	[AttrID] [int] NOT NULL,
 CONSTRAINT [PK_WTypeAttrs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkTypes]    Script Date: 11.04.2018 21:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkTypes](
	[WTypeName] [varchar](50) NOT NULL,
	[ID] [int] NOT NULL,
 CONSTRAINT [PK_WTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Tasks_TaskTypeID]  DEFAULT ((0)) FOR [TaskTypeID]
GO
ALTER TABLE [dbo].[Works] ADD  CONSTRAINT [DF_Works_WorkTypeID]  DEFAULT ((0)) FOR [WorkTypeID]
GO
ALTER TABLE [dbo].[AttrValues]  WITH CHECK ADD  CONSTRAINT [FK_AttrValues_Attributes1] FOREIGN KEY([AttrID])
REFERENCES [dbo].[Attributes] ([ID])
GO
ALTER TABLE [dbo].[AttrValues] CHECK CONSTRAINT [FK_AttrValues_Attributes1]
GO
ALTER TABLE [dbo].[AttrValues]  WITH CHECK ADD  CONSTRAINT [FK_AttrValues_Works] FOREIGN KEY([WorkID])
REFERENCES [dbo].[Works] ([ID])
GO
ALTER TABLE [dbo].[AttrValues] CHECK CONSTRAINT [FK_AttrValues_Works]
GO
ALTER TABLE [dbo].[PropDataTypes]  WITH CHECK ADD  CONSTRAINT [FK_PropDataTypes_Properties] FOREIGN KEY([PropID])
REFERENCES [dbo].[Properties] ([ID])
GO
ALTER TABLE [dbo].[PropDataTypes] CHECK CONSTRAINT [FK_PropDataTypes_Properties]
GO
ALTER TABLE [dbo].[PropValues]  WITH CHECK ADD  CONSTRAINT [FK_PropValues_Properties] FOREIGN KEY([PropID])
REFERENCES [dbo].[Properties] ([ID])
GO
ALTER TABLE [dbo].[PropValues] CHECK CONSTRAINT [FK_PropValues_Properties]
GO
ALTER TABLE [dbo].[PropValues]  WITH CHECK ADD  CONSTRAINT [FK_PropValues_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([ID])
GO
ALTER TABLE [dbo].[PropValues] CHECK CONSTRAINT [FK_PropValues_Tasks]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_TaskTypes1] FOREIGN KEY([TaskTypeID])
REFERENCES [dbo].[TaskTypes] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_TaskTypes1]
GO
ALTER TABLE [dbo].[TaskTypeProps]  WITH CHECK ADD  CONSTRAINT [FK_TaskTypeProps_Properties] FOREIGN KEY([PropID])
REFERENCES [dbo].[Properties] ([ID])
GO
ALTER TABLE [dbo].[TaskTypeProps] CHECK CONSTRAINT [FK_TaskTypeProps_Properties]
GO
ALTER TABLE [dbo].[TaskTypeProps]  WITH CHECK ADD  CONSTRAINT [FK_TaskTypeProps_TaskTypes] FOREIGN KEY([TaskTypeID])
REFERENCES [dbo].[TaskTypes] ([ID])
GO
ALTER TABLE [dbo].[TaskTypeProps] CHECK CONSTRAINT [FK_TaskTypeProps_TaskTypes]
GO
ALTER TABLE [dbo].[UserTasks]  WITH CHECK ADD  CONSTRAINT [FK_UserTasks_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([ID])
GO
ALTER TABLE [dbo].[UserTasks] CHECK CONSTRAINT [FK_UserTasks_Tasks]
GO
ALTER TABLE [dbo].[UserTasks]  WITH CHECK ADD  CONSTRAINT [FK_UserTasks_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserTasks] CHECK CONSTRAINT [FK_UserTasks_Users]
GO
ALTER TABLE [dbo].[Works]  WITH CHECK ADD  CONSTRAINT [FK_Works_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([ID])
GO
ALTER TABLE [dbo].[Works] CHECK CONSTRAINT [FK_Works_Tasks]
GO
ALTER TABLE [dbo].[Works]  WITH CHECK ADD  CONSTRAINT [FK_Works_WorkTypes] FOREIGN KEY([WorkTypeID])
REFERENCES [dbo].[WorkTypes] ([ID])
GO
ALTER TABLE [dbo].[Works] CHECK CONSTRAINT [FK_Works_WorkTypes]
GO
ALTER TABLE [dbo].[WorkTypeAttrs]  WITH CHECK ADD  CONSTRAINT [FK_WorkTypeAttrs_Attributes] FOREIGN KEY([AttrID])
REFERENCES [dbo].[Attributes] ([ID])
GO
ALTER TABLE [dbo].[WorkTypeAttrs] CHECK CONSTRAINT [FK_WorkTypeAttrs_Attributes]
GO
ALTER TABLE [dbo].[WorkTypeAttrs]  WITH CHECK ADD  CONSTRAINT [FK_WorkTypeAttrs_WorkTypes] FOREIGN KEY([WorkTypeID])
REFERENCES [dbo].[WorkTypes] ([ID])
GO
ALTER TABLE [dbo].[WorkTypeAttrs] CHECK CONSTRAINT [FK_WorkTypeAttrs_WorkTypes]
GO
USE [master]
GO
ALTER DATABASE [TaskManagmentDB] SET  READ_WRITE 
GO
