USE [master]
GO
/****** Object:  Database [TaskManagmentDB]    Script Date: 27.02.2018 18:20:18 ******/
CREATE DATABASE [TaskManagmentDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskManagmentDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TaskManagmentDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskManagmentDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TaskManagmentDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TaskManagmentDB] SET COMPATIBILITY_LEVEL = 140
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
ALTER DATABASE [TaskManagmentDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TaskManagmentDB', N'ON'
GO
ALTER DATABASE [TaskManagmentDB] SET QUERY_STORE = OFF
GO
USE [TaskManagmentDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [TaskManagmentDB]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attributes](
	[AttrID] [uniqueidentifier] NOT NULL,
	[Name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Attributes] PRIMARY KEY CLUSTERED 
(
	[AttrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttrValues]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttrValues](
	[AttrValuesID] [uniqueidentifier] NOT NULL,
	[AttrID] [uniqueidentifier] NOT NULL,
	[WorkID] [uniqueidentifier] NOT NULL,
	[Value] [bit] NULL,
 CONSTRAINT [PK_AttrValues] PRIMARY KEY CLUSTERED 
(
	[AttrValuesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropDataTypes]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropDataTypes](
	[PropDataTypeID] [uniqueidentifier] NOT NULL,
	[PropID] [uniqueidentifier] NOT NULL,
	[DataValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PropDataTypes] PRIMARY KEY CLUSTERED 
(
	[PropDataTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[PropID] [uniqueidentifier] NOT NULL,
	[DataType] [varchar](max) NOT NULL,
	[PropName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[PropID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropValues]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropValues](
	[PropValueID] [uniqueidentifier] NOT NULL,
	[TaskID] [uniqueidentifier] NOT NULL,
	[PropID] [uniqueidentifier] NOT NULL,
	[Value] [varchar](max) NULL,
 CONSTRAINT [PK_PropValues] PRIMARY KEY CLUSTERED 
(
	[PropValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskID] [uniqueidentifier] NOT NULL,
	[TaskName] [varchar](50) NOT NULL,
	[ParentTaskID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskTTypes]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskTTypes](
	[TaskTTypeID] [uniqueidentifier] NOT NULL,
	[TaskID] [uniqueidentifier] NOT NULL,
	[TTypeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TaskTypes] PRIMARY KEY CLUSTERED 
(
	[TaskTTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTypeProps]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTypeProps](
	[TTypePropID] [uniqueidentifier] NOT NULL,
	[TTypeID] [uniqueidentifier] NOT NULL,
	[PropID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TTypeProps] PRIMARY KEY CLUSTERED 
(
	[TTypePropID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTypes]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTypes](
	[TTypeID] [uniqueidentifier] NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TTypes] PRIMARY KEY CLUSTERED 
(
	[TTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Works]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Works](
	[WorkID] [uniqueidentifier] NOT NULL,
	[WorkName] [varchar](50) NOT NULL,
	[TaskID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Works] PRIMARY KEY CLUSTERED 
(
	[WorkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkWTypes]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkWTypes](
	[WorkWTypesID] [uniqueidentifier] NOT NULL,
	[WorkID] [uniqueidentifier] NOT NULL,
	[WTypeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WorkWTypes] PRIMARY KEY CLUSTERED 
(
	[WorkWTypesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WTypeAttrs]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WTypeAttrs](
	[WTypeAttrID] [uniqueidentifier] NOT NULL,
	[WTypeID] [uniqueidentifier] NOT NULL,
	[AttrID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WTypeAttrs] PRIMARY KEY CLUSTERED 
(
	[WTypeAttrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WTypes]    Script Date: 27.02.2018 18:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WTypes](
	[WTypeID] [uniqueidentifier] NOT NULL,
	[WTypeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_WTypes] PRIMARY KEY CLUSTERED 
(
	[WTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attributes] ADD  CONSTRAINT [DF_Attributes_AttrID]  DEFAULT (newid()) FOR [AttrID]
GO
ALTER TABLE [dbo].[AttrValues] ADD  CONSTRAINT [DF_AttrValues_AttrValuesID]  DEFAULT (newid()) FOR [AttrValuesID]
GO
ALTER TABLE [dbo].[PropDataTypes] ADD  CONSTRAINT [DF_PropDataTypes_PropDataTypeID]  DEFAULT (newid()) FOR [PropDataTypeID]
GO
ALTER TABLE [dbo].[Properties] ADD  CONSTRAINT [DF_Property_PropID]  DEFAULT (newid()) FOR [PropID]
GO
ALTER TABLE [dbo].[PropValues] ADD  CONSTRAINT [DF_PropValues_PropValueID]  DEFAULT (newid()) FOR [PropValueID]
GO
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [DF_Task_TaskID]  DEFAULT (newid()) FOR [TaskID]
GO
ALTER TABLE [dbo].[TaskTTypes] ADD  CONSTRAINT [DF_TaskTypes_TaskTypeID]  DEFAULT (newid()) FOR [TaskTTypeID]
GO
ALTER TABLE [dbo].[TTypeProps] ADD  CONSTRAINT [DF_TypeProps_TypePropID]  DEFAULT (newid()) FOR [TTypePropID]
GO
ALTER TABLE [dbo].[TTypes] ADD  CONSTRAINT [DF_Types_TypeID]  DEFAULT (newid()) FOR [TTypeID]
GO
ALTER TABLE [dbo].[Works] ADD  CONSTRAINT [DF_Works_WorksID]  DEFAULT (newid()) FOR [WorkID]
GO
ALTER TABLE [dbo].[WTypeAttrs] ADD  CONSTRAINT [DF_WTypeAttrs_WTypeAttrID]  DEFAULT (newid()) FOR [WTypeAttrID]
GO
ALTER TABLE [dbo].[WTypes] ADD  CONSTRAINT [DF_WTypes_WTypeID]  DEFAULT (newid()) FOR [WTypeID]
GO
ALTER TABLE [dbo].[AttrValues]  WITH CHECK ADD  CONSTRAINT [FK_AttrValues_Attributes] FOREIGN KEY([AttrID])
REFERENCES [dbo].[Attributes] ([AttrID])
GO
ALTER TABLE [dbo].[AttrValues] CHECK CONSTRAINT [FK_AttrValues_Attributes]
GO
ALTER TABLE [dbo].[AttrValues]  WITH CHECK ADD  CONSTRAINT [FK_AttrValues_Works] FOREIGN KEY([WorkID])
REFERENCES [dbo].[Works] ([WorkID])
GO
ALTER TABLE [dbo].[AttrValues] CHECK CONSTRAINT [FK_AttrValues_Works]
GO
ALTER TABLE [dbo].[PropDataTypes]  WITH CHECK ADD  CONSTRAINT [FK_PropDataTypes_Properties] FOREIGN KEY([PropID])
REFERENCES [dbo].[Properties] ([PropID])
GO
ALTER TABLE [dbo].[PropDataTypes] CHECK CONSTRAINT [FK_PropDataTypes_Properties]
GO
ALTER TABLE [dbo].[PropValues]  WITH CHECK ADD  CONSTRAINT [FK_PropValues_Properties] FOREIGN KEY([PropID])
REFERENCES [dbo].[Properties] ([PropID])
GO
ALTER TABLE [dbo].[PropValues] CHECK CONSTRAINT [FK_PropValues_Properties]
GO
ALTER TABLE [dbo].[PropValues]  WITH CHECK ADD  CONSTRAINT [FK_PropValues_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
GO
ALTER TABLE [dbo].[PropValues] CHECK CONSTRAINT [FK_PropValues_Tasks]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Tasks] FOREIGN KEY([ParentTaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Tasks]
GO
ALTER TABLE [dbo].[TaskTTypes]  WITH CHECK ADD  CONSTRAINT [FK_TaskTTypes_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
GO
ALTER TABLE [dbo].[TaskTTypes] CHECK CONSTRAINT [FK_TaskTTypes_Tasks]
GO
ALTER TABLE [dbo].[TaskTTypes]  WITH CHECK ADD  CONSTRAINT [FK_TaskTTypes_TTypes] FOREIGN KEY([TTypeID])
REFERENCES [dbo].[TTypes] ([TTypeID])
GO
ALTER TABLE [dbo].[TaskTTypes] CHECK CONSTRAINT [FK_TaskTTypes_TTypes]
GO
ALTER TABLE [dbo].[TTypeProps]  WITH CHECK ADD  CONSTRAINT [FK_TTypeProps_Properties] FOREIGN KEY([PropID])
REFERENCES [dbo].[Properties] ([PropID])
GO
ALTER TABLE [dbo].[TTypeProps] CHECK CONSTRAINT [FK_TTypeProps_Properties]
GO
ALTER TABLE [dbo].[TTypeProps]  WITH CHECK ADD  CONSTRAINT [FK_TTypeProps_TTypes] FOREIGN KEY([TTypeID])
REFERENCES [dbo].[TTypes] ([TTypeID])
GO
ALTER TABLE [dbo].[TTypeProps] CHECK CONSTRAINT [FK_TTypeProps_TTypes]
GO
ALTER TABLE [dbo].[Works]  WITH CHECK ADD  CONSTRAINT [FK_Works_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
GO
ALTER TABLE [dbo].[Works] CHECK CONSTRAINT [FK_Works_Tasks]
GO
ALTER TABLE [dbo].[WorkWTypes]  WITH CHECK ADD  CONSTRAINT [FK_WorkWTypes_Works] FOREIGN KEY([WorkID])
REFERENCES [dbo].[Works] ([WorkID])
GO
ALTER TABLE [dbo].[WorkWTypes] CHECK CONSTRAINT [FK_WorkWTypes_Works]
GO
ALTER TABLE [dbo].[WorkWTypes]  WITH CHECK ADD  CONSTRAINT [FK_WorkWTypes_WTypes] FOREIGN KEY([WTypeID])
REFERENCES [dbo].[WTypes] ([WTypeID])
GO
ALTER TABLE [dbo].[WorkWTypes] CHECK CONSTRAINT [FK_WorkWTypes_WTypes]
GO
ALTER TABLE [dbo].[WTypeAttrs]  WITH CHECK ADD  CONSTRAINT [FK_WTypeAttrs_Attributes] FOREIGN KEY([AttrID])
REFERENCES [dbo].[Attributes] ([AttrID])
GO
ALTER TABLE [dbo].[WTypeAttrs] CHECK CONSTRAINT [FK_WTypeAttrs_Attributes]
GO
ALTER TABLE [dbo].[WTypeAttrs]  WITH CHECK ADD  CONSTRAINT [FK_WTypeAttrs_WTypes] FOREIGN KEY([WTypeID])
REFERENCES [dbo].[WTypes] ([WTypeID])
GO
ALTER TABLE [dbo].[WTypeAttrs] CHECK CONSTRAINT [FK_WTypeAttrs_WTypes]
GO
USE [master]
GO
ALTER DATABASE [TaskManagmentDB] SET  READ_WRITE 
GO
