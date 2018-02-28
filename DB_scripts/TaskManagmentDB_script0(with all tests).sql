USE [master]
GO
/****** Object:  Database [TaskManagmentDB]    Script Date: 28.02.2018 9:15:06 ******/
CREATE DATABASE [TaskManagmentDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskManagmentDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TaskManagmentDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskManagmentDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TaskManagmentDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[Attributes]    Script Date: 28.02.2018 9:15:06 ******/
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
/****** Object:  Table [dbo].[AttrValues]    Script Date: 28.02.2018 9:15:07 ******/
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
/****** Object:  Table [dbo].[PropDataTypes]    Script Date: 28.02.2018 9:15:07 ******/
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
/****** Object:  Table [dbo].[Properties]    Script Date: 28.02.2018 9:15:07 ******/
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
/****** Object:  Table [dbo].[PropValues]    Script Date: 28.02.2018 9:15:07 ******/
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
/****** Object:  Table [dbo].[Tasks]    Script Date: 28.02.2018 9:15:07 ******/
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
/****** Object:  Table [dbo].[TaskTTypes]    Script Date: 28.02.2018 9:15:07 ******/
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
/****** Object:  Table [dbo].[TTypeProps]    Script Date: 28.02.2018 9:15:07 ******/
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
/****** Object:  Table [dbo].[TTypes]    Script Date: 28.02.2018 9:15:08 ******/
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
/****** Object:  Table [dbo].[Works]    Script Date: 28.02.2018 9:15:08 ******/
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
/****** Object:  Table [dbo].[WorkWTypes]    Script Date: 28.02.2018 9:15:08 ******/
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
/****** Object:  Table [dbo].[WTypeAttrs]    Script Date: 28.02.2018 9:15:08 ******/
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
/****** Object:  Table [dbo].[WTypes]    Script Date: 28.02.2018 9:15:08 ******/
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
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'23b10fa7-5a34-49e9-bc9a-002188efafb3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'512fd389-b406-49fc-be39-00230945f839', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e75e157d-e452-4234-b403-00591b277561', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'310dd097-f1ca-4a36-896b-007a4fefa623', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9a5b86d8-a4eb-4f20-9ec0-00b77e319eec', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3e22227f-2256-4a9c-b481-00c0b835a59e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6f4e37eb-f76f-4ba1-9f39-00fc5283fa8c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7e704930-a22f-4e5a-bc43-012e123c769a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'48c1e258-eb53-4ebe-b973-01468edd39f3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'05056650-76f9-41c2-8269-01645e3e8da5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'60b55846-42e1-4373-bae6-01c79cf6c2eb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a5a90355-73ed-4e36-9a2c-01c837e5843a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'354e157a-11ca-4dfa-b392-01eea887bbdc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dc3b1270-0166-49a6-bebd-0221aa4ab05c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e0aa4288-2463-4533-b7b5-035d677554dd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'740bb012-7c0b-49fa-a92a-036bb90287ea', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7aa4dd2f-3c3b-4f97-b586-043441d2bfbc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'268f5901-f5ec-4d49-b6df-043a6d5d601e', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b7e1feda-3f81-4686-82bb-047139a9870f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'81e706d2-0517-45e6-a41d-04f679920fcf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bba0c392-1eb7-4630-ab03-052f99430f76', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b7d4dc5f-5a2d-4cbf-a82e-053f2f90e656', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'001177f5-bbf2-40cb-9ac8-0542b95a5e33', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'16c76755-9d2a-40e8-a40d-058c176bc7f1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'330c6d91-c8a1-49ff-b005-05ad31e65c36', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'89ab6556-156a-4e6a-b36f-05ce93476eff', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cd9e3728-3eb1-4607-bcbe-05d7e05dc7b0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'68d8e1b0-189e-46ae-a615-05e29dfa73c9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f0ce06f4-3148-439d-9cc2-05fb5f3d77fe', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'834ce134-8e52-459e-b5cb-0716113f2775', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'627b2d1a-5166-461d-9293-0784381df436', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'64922c30-23fe-4274-a35b-08a37d5806b8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6efb2860-301f-4a04-ab2f-08b9765aa12a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ad6584a4-ae6b-45e6-8669-08e76d29f626', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'41bc1c21-5900-4cac-b170-092bcf9d3af9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e7f05b12-be17-4818-ad6e-0a2df62a83d5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4828268e-c813-4a6b-992f-0b6202236742', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7467b5af-1495-4f91-bfef-0ba88c24828f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'16c28b31-3c51-4525-8be8-0bfc8dccf852', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'34e5df46-4ff0-494c-a5d2-0c21bfc7f047', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'118b5a9e-baaf-4b01-b529-0c5ea872518d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd2b30f22-eb98-490a-b434-0c9641ac178e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5d3713d9-8a75-4aba-9573-0cacceea0243', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'17465023-bf04-451e-9299-0cf6a26a5c23', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4b69a8d9-8f17-4a18-9e4b-0d1191f03dd0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'71db33ee-7d9e-4a3f-a5ab-0d4e2bfd15c2', N'Обновление', N'991248ed-8fd3-4756-8b0d-686e1b274d75')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2c4e42fa-5b5e-4262-8c60-0d880e0ff459', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ac15985e-94eb-4798-bdaf-0d8d6a0cbec7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'280d7fab-5080-4adc-bfaa-0daa73b50814', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'482cf363-1bf5-4515-a33f-0dd57cb92ed5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cfbc3cab-35f7-4a3a-b983-0deea0ea54bb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5be20689-dc16-4eb6-901c-0df42abf7808', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ee702fb2-0656-4b90-9fe8-0e9f736292ee', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0ca24ea9-28e9-4573-965d-0efebfdff622', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9e1134a3-8f80-4b57-8873-0f5bbd0ca26a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5190ac2d-617d-41af-9028-0f60b33d9371', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'654ab0cb-9db5-4631-9aaa-0f7b35fc3afd', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a7a58969-27df-4e5e-96a5-0fbd52eac7df', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'37d6e8e7-d400-4aa8-b761-0fbd636181b2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'98c36e67-16b7-4a83-9c99-103dcaf32eb9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f0a63374-af51-4d38-801d-10f0f30037c9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6dbe4db4-97d7-41cc-8fad-10ff69812330', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cd8f3510-5588-42c8-84c3-11fbd83b2f30', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'31690c53-5f70-4a24-a731-121d358b5eae', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7406ac2e-31d6-49ea-be11-128956dba78e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c13eba52-67a5-46c9-8ea2-12d1d1b90e0e', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7cda69e0-4c2f-43f2-8be9-13afb9a31dc8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'225d622a-be97-4500-9c17-13b6259ea033', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8100673a-57e9-4b6b-8619-13fc0549406e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3fff2be5-968b-49e4-85da-1400fbf21e05', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e122cf00-b64c-4783-a08e-1461995525f8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'67c96a37-595d-4c36-97d1-149a0d2e0e2d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1aebd67b-72d5-49f8-9d06-14cffff601e4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c066efba-f529-4e02-be66-155cb025af56', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'12822cbb-6e50-4f73-ae04-158f493e0212', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6f43f146-22c6-4fb4-9968-15d75549e923', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'38cdc15f-ee9e-4839-bd71-165976f4a53a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd881dbd5-fe7e-4c2f-88a3-168ba311089b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'63e3db68-0576-4e46-90e4-16a8873b4ff2', N'Консультации', N'2c0e6520-3071-49bd-a774-6aade88376be')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'834b8f5f-6118-4659-8ac1-16b0982ba150', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c71ff69a-d32e-49fc-97d6-1722a3a75bbd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'24b9fc2c-237a-428d-a50d-182f2a15b773', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bda73ec6-5ef5-4d3b-863c-1845022224c4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ad6dcefb-53a0-475b-8381-18779094fdec', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e1c93a89-5c6f-462b-8bd5-19183b9a0ef7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c269904f-0f3d-4f68-bc18-1943deb7e056', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6190d0dd-925d-401b-97de-19505b4b6a65', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0921448f-4c6a-4be0-87dd-1974db135988', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'647ab55c-3a13-4ef9-97cf-197d8294f7a2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'21e5d322-2a5a-4f76-863c-19a4a32b6963', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'945b943d-529a-4073-b64d-1a06fe1016e3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5e74b6f4-8133-447c-aa41-1a0ee7d25231', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'aeb4f91e-e027-47f7-bdf8-1a1acdf92b68', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ce6f6113-787f-4e34-b91a-1a5b9b79d5e0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'85d7d6c0-f6a4-466a-af6d-1b1ecfe961ea', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a0c64bdc-d6eb-4792-9396-1b431f5ab19d', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'21b0f9ca-eae5-4e81-bd0e-1b6acfa8b31e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a895a943-bc1f-495c-8915-1b8fbf8659e7', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd3a9112c-f20f-4a5a-ac4f-1bcd806e2d67', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ac241b6a-5751-4994-939f-1bd1d55fa906', N'Паспорт', N'4a4b2bb9-eddc-4691-8ce8-984a818b30fc')
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b5248c54-5260-4c25-8b93-1c12bee0d69a', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7a5f41b9-4096-47bf-a277-1c7715b6ee36', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4798869e-1c0f-4a1f-b26f-1d9e076d3c0b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'72efabb7-df6c-4273-b3bf-1e264bfd1f54', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'74243f77-0719-410d-8a59-1e2e9a6b2406', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'39dcf95c-a254-4672-a032-1e3cc20dbb9a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ec1b4454-a423-4a3f-ac97-1e9e14920b03', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e1ccd100-2bae-42c0-b801-1eaff1b0fb63', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3b3e60dc-457e-47ff-862b-1f280e6a2d10', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a6276b66-c43e-4b73-bbc6-1f3532adc212', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b397e59c-923f-48ce-91c2-1f701f2861df', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'19a266e9-8ab1-45a5-bc8d-1ff381f937e2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8850967b-eeb0-4f89-bd56-2017e20686b4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'201dd82b-a50f-4458-84f4-201b20482b84', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f306dcc1-6d6d-41b7-8210-2041244b7757', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'47eb939c-2ad0-4565-a316-204da54cd1a0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'712e7f78-91cb-4d1a-bd09-207d4b080ccf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'34077bce-368b-413b-beb8-20a2f2d8f6f4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd43bdb73-c40a-43dc-b5fc-20dccfab96dd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9122681e-fcc9-4082-8734-20f682c118f0', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f764cc33-22ee-4fa6-b957-2104bee6d7fa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ccd81293-9bed-49cf-a2e5-210724250531', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'731a2602-6a7b-4612-a6b6-216b831eefd6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2273d8e6-7fed-4ea3-9789-21b3d03d91ea', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'07fd9366-243a-4250-b59d-21cb6d5b3dc5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c70c48cc-8e8f-496a-a70d-21d5d6aaf127', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4e5f8d07-4fc9-4135-8f24-221c62be6b99', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'20e0a4f0-9e1b-4f8a-836f-229f12e5b788', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd387b229-4293-4299-b305-22b7db6456c4', N'Справочники', N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'abbb7dc8-759e-47de-abc2-22d8c5e71122', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'01e2c8bd-cf8f-4b19-a43b-230474acfcef', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2d174a34-dda5-4647-a34c-2308ba1a2953', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fd249a36-c730-40c7-8e17-2317b7e5c16b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'690b5e86-fa3f-4453-8064-2348d9f6d9d5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b105a87a-99c2-4f41-9b3d-23bfdd05786b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3564f6c7-e81c-4e51-a20d-23d3c5cf9e39', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'12167dcb-a0a0-4fe1-a84f-2410cb60c87e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'06c38734-0cda-4521-8cc1-2435b7525aec', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8559e79a-5429-4720-9f46-24ea11fc30d5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f79cf554-fd80-4bcc-8c21-251da2632e3a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5aee89d8-5822-43eb-be9a-2544534be072', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'846fb952-f572-48f3-a997-254730e52e00', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'654a86c6-4e6a-4a47-a5f2-25758a3a884c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b01066ce-425b-4549-907b-25c2ab3188b0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bfb31249-a83b-4dac-ae36-264322a8bc85', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a9867d6b-d171-4e73-91a5-266d1c704feb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b4589c22-aa8c-4b3a-8fd8-26a48407118f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6c52a25b-559f-4515-b6e0-26d3538c6207', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3633f3a7-3172-46d0-a098-26fa269beb07', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9fc428cc-6c02-481b-87c4-27001a147a33', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'457c708c-bfd4-4ce5-9ecd-2701b47e0f0e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'38888287-d8a0-4273-abb2-2702a032923f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a3623e04-e0f1-42a3-8bc5-270d7242488d', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dc8f8fa5-85fd-40a4-8559-2720648d9688', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b05f5999-f540-4e5a-ae41-276d8bad4700', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'810c743d-974e-414b-a2d6-27e61624ab59', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8c959d9a-e41b-4203-88ee-283af8897a88', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'243d39b2-7af6-4cd8-b39f-286f41efc5e9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'db7c61ce-fdcf-4f70-a7c4-2875f3739976', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ae93dbff-f4c6-41c4-ae50-28dc504de9d1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8b4bc6a0-275b-496c-b6cf-28ed4da89385', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5f725ff9-ce1e-45e0-954d-29392ad8cfc1', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f27b6f6c-0ffa-42d8-9cef-2948cb483101', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ade25f60-fb30-4666-b977-2976f9d45b8d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'559d9779-ba66-4332-9f32-2ae554524e16', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c06babbb-4425-40a3-a7a3-2afccc90c748', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'03f10604-4b59-4883-88e1-2b0008cf8485', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ff4af16e-a330-4dcd-b93f-2b8d92c6a1eb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'008de3f5-552a-4638-95c0-2bc50e41a47b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'08ef6620-e301-4ffa-8a86-2c0f3a664203', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'75070bea-b9b6-440e-91e8-2c1b7248d205', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'11708f09-f98e-4fde-8b93-2c271b1f2ac5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'adb7b330-9d25-4a18-8f5c-2c880bc7cc08', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'18842561-0623-46e4-950a-2cd46fc57ced', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6c246a3d-f8de-4b39-b517-2cecffb192e9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ab5ace29-f69d-4b31-98d5-2d1c3b6aa73b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e9cd093c-4cf8-40ef-9eac-2d74909a70be', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f2c99fa9-b5b3-43cb-b26b-2d798262f8ac', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'275f298a-7630-476e-a35b-2d86fad94776', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'aed6f5c1-d4d5-4f0c-808b-2e6f590868f4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'de8bd432-97ca-47b7-b033-2ec337bd19f8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'63120df5-6978-483a-bf81-2f0d51a19e85', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'385ba9fa-2c16-4341-b52b-2f40c6245bf6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8364ff8c-b81c-435f-8227-2f44f156169a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c77551fb-db4f-4c9b-8c37-2f6f9be7a5c1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c799d1b7-7876-4abb-badc-2f9bca8c10e1', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'52f39d13-a811-4012-bb70-3050b3be49cf', N'Подподзадача для теста', N'e75e157d-e452-4234-b403-00591b277561')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd2519be2-d901-432f-9320-307618e3ec34', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'312bef13-b871-45dd-99d9-307f3c1336d8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'93fa105a-3b8c-4150-8b29-30c715e97375', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f40024f1-5939-4fd8-bc4d-30fe3c045558', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'26c270f9-bdd3-4439-9da5-3117e5bbe62e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'28ee8755-92a4-4f56-a9d2-313326781f6e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd7d5a78d-768b-4659-94d7-31ad1fbc26e2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9e51acde-a89e-4beb-aa7a-32002c75228f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0227dc13-5a67-4db9-8214-3257987cf6fa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7966827d-8101-475c-bb90-328587b57205', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c316811e-e036-48f4-baf9-3308f72f89ee', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5bb451be-1730-428b-be9f-336bdc3af342', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fc9f7876-d938-42da-bf8b-33d53e2985ab', N'Для большого теста', NULL)
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'09b8a456-8249-44c7-a73f-33db69b632b5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b6e6ebd9-3f9e-4e27-8b82-33f638e8a171', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a85f6033-38c1-492d-8d20-342e9232a59a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8272a88f-c7f0-4199-9712-34af97668b8f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f6bbd020-8d4b-45b5-adac-34d5f68aca19', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4498258b-9220-4d4c-b148-352235871cdd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'00a990c7-d16f-4377-97fe-35896c89ae3e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fea9d0f6-b749-4556-9910-3645fed28c44', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9f2dae33-3c92-4865-972b-3660272b701c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'231f9479-3cc5-48ee-a847-36a131de101d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'be3939c9-4c3c-405e-bc02-36a93e4c8730', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a4933d60-6460-4028-bbfd-36aa5ef06e51', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'79066605-9e56-4093-8947-36d079896756', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'239eb76b-b9fb-44fb-abe8-36e55d557fe9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'72b6fdb6-c54b-4516-99ae-3716929919c4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd2c42838-59d1-401d-9ebd-378ed12c9d36', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'68dce0e7-6844-4d07-9228-37b57e48292a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'21782b00-7709-48e4-a6f9-37d4a8c9aa2a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'29ddfca9-dbce-486a-9e95-387cdad46ea3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1a312198-4b47-4faa-a43f-38cf4a872cb5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cf5626ff-f543-4d75-b664-38d89f461504', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'72563651-9c9a-42e3-9f55-396c8faca74e', N'Тестирование ', N'2c0e6520-3071-49bd-a774-6aade88376be')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5a505fc6-05d3-4f2a-a425-39b5f35d5b0b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'66fe033a-b586-423e-b7e7-39ba4aff2b92', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'65a5b989-7770-4820-8e79-3a28225c7536', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'30d389de-aa55-4b92-bd27-3a9b3989195d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'78a69eb7-ac3f-43b3-97e0-3b14b5bf3b63', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4c4d3cd9-434b-4627-9f6c-3b18cec4c138', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a20d30df-d63e-4bf4-b8de-3b4e2b657519', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'56346eed-04bd-43de-b2b7-3b53304d9244', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'13b6fd82-38c6-487e-b51f-3baae07058b8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'82ec599c-6e03-4590-b6a8-3bc6841101ec', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2c54cd48-a522-459d-9650-3bd27d8a5e71', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8c9a5551-9f76-4f9a-9adf-3c057914cef2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1840ce04-cbf9-4932-82ea-3c1b4d5a7b80', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'11867b50-35f5-48db-af25-3c3f8c32e38b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4b4ab4b3-3413-457a-bc21-3cae2108b6a4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e96b4953-ab11-440e-ba7f-3cbe20f02262', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f625057b-a78c-4637-ace7-3d4c49f57425', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ea2d60c9-a431-444c-9183-3d81fbb1acb9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1d19d405-3a7d-4263-9c90-3dd0725257e7', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'95da4e4f-ba6c-4039-a3f1-3de044a1cc31', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1f6fcd14-3d47-4cff-a4c4-3de24c89ef0e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cd779d20-faa6-4ac6-88a5-3e5bb87d2353', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'146a6e77-9afe-45a0-be45-3e75c019e575', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'db943e7a-6b11-49f0-8575-3e9c42c173d7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'91def4d5-b435-4e27-83be-3ef6f79c4c38', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5a534ad4-4af0-4ad2-8093-3f05ccb0cccb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'952710b8-cd9d-40b4-b021-3fb58a450eaa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cd5f9c2f-9302-422c-a0eb-3fec5b8e334b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c75249cd-1385-41e7-bd6c-404700fe4935', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'46615bd8-e4e8-4af0-b6d0-40558548e1e6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c32b8d41-496c-4dc1-9a60-40b917e6ab38', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b81f7717-b815-47eb-9825-40d7f446529f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'940253fc-505b-4d66-8094-419f8e4ecc55', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'90478568-eee4-477f-9556-41d487ee1f8a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd90c84d8-109c-4e3b-b71e-42045b0420e9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'53604b35-97f0-4dc4-8973-422aaa65d31b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5203db52-e242-4699-869f-42574820d4b6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'321cc004-25f0-4b8c-b7b2-425c4d847424', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b1b88eff-8933-4bd9-807c-42a60fe5b9e7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'42b69ee7-c5a7-42e4-8dad-42ab2f0d0886', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a56ab459-c731-4cca-8148-42dccc66a00d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'009b4816-37cf-458f-94d9-42de553f3f99', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c0b51b19-87b1-403d-9058-43e8a5fe691b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bd55f2e1-4593-408c-94b0-4485949a53a1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f25c5700-86d1-4ede-872a-44b654d010f6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c5148126-118b-44d5-98d9-44cb380183c4', N'Подподзадача для теста', N'e75e157d-e452-4234-b403-00591b277561')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'33190704-cc44-4587-a3f9-45409c2239ba', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7e6c72cb-781e-4425-93ed-4591b75c378b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9f633f1a-8177-4eb1-a794-45ba77d21f92', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'87897e30-b444-4da0-a37b-45e253301c5f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'46075464-8bd4-4ac5-a7db-464f86d65dbe', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a30f8cf2-e73f-48a7-911a-4665979b04e9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cbfafa46-1317-4739-a0ed-471502426e9f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'befd3dab-fbff-4d14-a41f-473722ed5928', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'01f4beed-41ea-4268-a32e-4738d67c44af', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'11c444df-8180-401f-b499-4755fb05cfd6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0c759267-23de-4737-be67-4759688eb9fc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ea54404b-783d-464d-b057-475f4a2a3483', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'301e0f80-f42e-4c6b-b4aa-47ad9b240333', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7ffadefa-5c50-4a14-9987-47dd5b53702e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5ca71b3c-0415-4c67-9a61-47e7ba2fef8e', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'205c4de2-90dc-4e22-87f6-48801afe9314', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'29ee79a7-62e7-475f-976b-488952e207d5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6f349c28-820c-432b-b431-48b261b53cd7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c9182a5d-d614-433c-94e1-48e68aedebc1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'64f7835e-9138-4c81-ae99-49236ae062b7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0259f2d2-9a10-46ec-b601-493fbe076038', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'22a37b63-e303-4d36-899d-495f8630cd7c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a63b069e-94a7-4d84-8467-4a0adefbf02b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'523ae102-8283-4845-9f88-4a21c24310ab', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b839d52a-8616-40d8-b4c8-4a5ddfb372b0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9701d153-5096-4533-ba0d-4b401f3d9035', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6916ba29-8b35-4393-a7a9-4b9259679527', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd93464b3-a687-4459-a3f0-4b9699bd7167', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'698e656e-7281-4613-9215-4ba16c3d21dd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c59199d3-e469-42d6-b00d-4ba6e7f979d3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ae9da393-6ffc-4d6b-b37d-4bd22506a200', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd664cd97-257a-4268-9399-4bd32787a0bc', N'Для большого теста', NULL)
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f8b4b0cc-e1a2-4432-98bc-4bdbed3a298b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'895ee186-688c-4c9a-8774-4c17e8cc56ed', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4e4250ce-9ed8-483a-b931-4c45a8398853', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'69af0dba-e7f2-48cb-b895-4c4e17d7353f', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3bcde202-2e56-49b4-97ac-4c7785734627', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ec8c8094-c792-4b8a-acac-4d18c3511b13', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'265c0054-6efe-47f7-b639-4d1ad83cd470', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'47d97209-3458-41de-8600-4d2d61a9ba5c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'05271674-4be8-4150-932a-4d2e46250a49', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9da9fb38-ea99-4b9c-a003-4d3ac9e54031', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7d5b2ee1-ce3e-4467-b546-4d4da0f62e99', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ea338692-b5fe-438b-8385-4d5279db9f13', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'06c88320-e18c-40a2-b7da-4d8c0af38a8a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f39b79cf-c150-4961-9a24-4de1fc85b451', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e2ebcb61-7842-4b14-b5d5-4e8fdf5c0023', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ad71e9b4-d911-48e7-9a21-4e970d4958af', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a25acd08-5d60-4347-821c-4ea0197eb23d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'13f598f4-60e8-46f0-93f4-4efb4fca3fd4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1a176304-2a7c-4a34-aade-4effda81eb95', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'96691804-dcfe-41c7-83e0-4f4cc8fec3bf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3abc99ad-0491-4da9-8cfa-4f669d393614', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0c77177d-3d04-4e21-b679-4fae0c189eab', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5be52152-9420-49a4-b2bd-4fbc682749f7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'06761277-3114-4ac9-8dfa-500c90a70e57', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c3b5b6cf-5f32-444c-90b2-5072996db59f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6ebcaf44-8221-48c4-b4cb-5103167a6217', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cacc149a-e62c-40c6-9d95-51220b0d51eb', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5caee197-3d82-43df-a23b-51277e53eb73', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ba32efde-5c31-4a74-b8f0-518821752972', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'449ee279-8076-4545-ac35-51ebb05e848b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dd1cc8fd-a996-4d16-9880-51fdb88b36b3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f5def867-022a-4102-ba44-526cac98284e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'30a867d2-f9c8-47f0-90c7-52c5ebce4999', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c449eead-7a3f-48e3-80a7-52e3fe8b4222', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8eb7aa47-d120-4ff3-99c9-53642b4718f8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ade9cb41-bf7b-47fc-9e76-538a2d82867f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4017775e-06e1-4f25-b713-539e07c4826a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b67b44f5-8473-44eb-9603-53a03d3733f7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0cd99815-3338-458b-a62a-53a9556a4fde', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e22e1384-c8fc-4998-8b7c-53cdbea79e8c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2ed1632c-53cd-4586-91ce-53d0636fdd73', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c4848c10-4d93-41b0-beaa-540ced89dc55', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4cfdbd39-6782-43e4-b0e1-54611ec582f0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'43b356cc-0ab7-48f5-8f37-5463a03b7dcc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f0b9bb1e-eaf9-4e01-ab22-54b399ce89e6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a899ff2d-bffb-4114-a3f6-54be9a3dd22f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'04fe391c-71b3-4209-8309-54d6f479ed03', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'df348063-b21b-4513-aef5-54e71865525e', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0e0e2c9d-95b4-425b-8f90-5504b3d93360', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'062a7665-55c2-4dbd-aecd-5519c9dd6adb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3462db80-8376-4230-83c0-556a5bcc9059', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cb6a654f-7d2f-4d9a-9437-560d5d9f680f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9e710ae3-c424-4314-ae78-56196849e998', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'975a55c4-0f11-409f-9df8-563640547a30', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'82540bb7-f469-4fd2-b997-565cd6a62232', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f35c2775-4ed2-4358-ad15-566aedc0e432', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'85475d47-4b17-45c2-b335-567622071a8d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0fd462cf-e750-495f-9aa5-5699ada2105b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'22788681-0aec-4eb1-a9fc-56a644455632', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b405e17a-e7d3-4637-8d59-56dddaa4c28c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5ab18d6f-df97-48ee-885e-571c42f1d744', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ce165881-1610-413c-a87a-574bec10f77e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b6f284ca-29cd-4164-9d42-57a5be908a27', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'02c7b653-2732-40fa-9a83-57c2c3f6b358', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'138e0bd6-00e1-459e-9782-57fb36949755', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'92f241dd-d13b-40be-bdb1-58359998e5e8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd4352087-71e8-4c37-9f97-583909b12c7b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b29d802a-9390-46e5-9bd0-586aba7ff26a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bf50d9ff-2856-42eb-b3c9-58d3b597b8ff', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ef29f7a3-1423-4a44-804c-58dde3bcf5b7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c8462e59-c736-448e-a932-58eb8821b4a6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a9c56851-f7b1-4fbe-9a34-59069b3c6b57', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'787c34cc-8112-4ee7-af69-5917df11d005', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0f09c3e8-2c4b-4a33-af4e-591d2f0bcddd', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8cfb77b8-8cbc-4083-9400-597fa0012b74', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'71fad31c-c826-41ca-bef7-59a0bd4f3b9d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'11f019ee-51ca-440f-bda6-59cac64b24c1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9deee945-7aad-4671-96f8-59faefe361cd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'134bc951-ee4d-42f6-b228-5a4b63606739', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0044cbb0-cca1-4e55-8ce9-5a4be74f0dbd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e932263d-8384-4f9a-8ed3-5ab392a8bddf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'09670974-96db-4d85-9309-5ab6517a919e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1f1addba-0e97-4207-ba59-5ab830bf222c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cffd73bd-e0a3-4c7b-bc3a-5ac8dbd3d1f9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b36c0737-a7a4-4ef7-a86d-5acaefef48a5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f00eec4b-3bd3-4714-9fce-5b04516cee87', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9ff6d751-ff80-422a-8b17-5b1034da0059', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b9a4878e-58fa-4dda-b70c-5b5fc0c11a68', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b06eae7f-64af-4016-bd79-5b703165d9d1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0ba6cad1-1f7e-40f3-a021-5bb20a09ca29', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0a23aa1b-4497-4e9b-879e-5bfb79dabe52', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e18daa62-53fe-4716-9dac-5bfc70d85d82', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7dec8a34-c4e1-4a8c-8250-5c2b49156994', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4541185c-49a1-4c14-9d62-5c31c0d778d2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'238c0ec7-5872-4b86-8fb7-5c8e580be0c4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7bbf3165-3941-4c89-90f3-5d0750a812a7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6c36ec58-a495-408a-acad-5d55e64bce77', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f774cf8d-e41f-4f27-be1f-5d6fe29621ce', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1f6b2f04-ecba-4905-a182-5d742625ae83', N'Инструкции', N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cf9a89d2-40a0-4583-99ff-5d81a1988d1d', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ce831eb5-85c8-4200-84e8-5df276e6baca', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd1384fa1-5caf-4328-81ed-5e11759d5d9c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e2430914-3ef5-4621-afb4-5e5ae257e632', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6270331d-1ae0-43df-b452-5eec4df15688', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fb47eb75-cfcb-4e03-b8eb-5effb383f488', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ad16a8b1-776e-4b9b-9bba-5f0d551cd479', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5b9fe1cd-c4f7-428b-b649-5f5bf0586696', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b4346423-32b1-4260-a1c5-5f6a266e9efc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f466ef66-2a4a-4106-8b02-5f71616c0174', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'773b1691-4c2d-4d76-acf8-5f72a53b1ab1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'50842811-0064-4f37-900c-60054fb75916', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a4df2da3-6fb6-4cbb-b3d6-613fd2fbe879', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1475b3c3-8061-40bd-a741-618261e9fd97', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'581eaf76-459c-41dc-9cbe-61915fc3dfbd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b523f82d-af2c-417f-b676-62a0a4ac42d0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd47fe62b-4101-48ef-9f7f-62bfaf434534', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'46225ad7-1b85-4717-bc11-62cf6e02cbc1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dd2deee5-aacb-40a0-bf74-631c977b8fcb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f4dea933-8c91-4358-a3ce-63462b3a5afc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8d19d6d4-0217-426f-a1ee-63c4cca94d4b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'28d155ec-a98f-4d1b-8580-64574f2bc868', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'16cde14c-e606-49b2-9211-6484d4964508', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'07c62aff-03c2-487c-887f-648686631180', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a5c0d927-8d66-46cc-99cc-64d375c7d477', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3827526c-79fd-4fe1-a249-657eb8aec9f9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'818f59f2-ebdc-4d41-960b-65c1c27351df', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c082415d-cf2f-4117-a496-65e38dbcdf34', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e0b6a0db-11d1-4293-92a5-65fbc392d1b1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f6d3b02d-f4e6-4a46-b215-6656b3614640', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ec130707-a31d-451a-b360-66e1599341df', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dc5fb312-f6dc-47e4-b2b1-6741fdd8a79b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'541937b7-5906-40ab-8eff-67730758b9b0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd4bd742b-500f-425e-b448-67c015633675', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7b2858c4-40ec-4404-b6f5-67d0ad984a07', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'34ec65a2-90bc-42b0-8683-683373fd050f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'991248ed-8fd3-4756-8b0d-686e1b274d75', N'Тех.поддержка ', N'4a52f107-2bcd-44ad-a9cf-a156485e5e7e')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'394448bb-a071-49bb-bf3d-68825b5f00fa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1c436d6f-ff6c-4979-a5be-6922ab588a6d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd6c6fe2f-15e4-4170-ba8a-69358ef5ddb4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3264a18e-baff-44e6-8809-69664a95fd5b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ae76ec1b-3526-4a1b-8552-69830f835156', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'20e25b01-ede3-41c8-a05b-69985c2026d6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd98d8f12-d278-4292-b195-699f72eb7b21', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'81943476-c690-44b5-9ece-6a9f95ad89c7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2c0e6520-3071-49bd-a774-6aade88376be', N'Доработка функционала по интеграции с системами ', N'fff68f02-1998-4412-a674-dd020803e5b3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'de3ba2f5-e82e-409c-9c3d-6ab43aaab217', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4dbb6f02-685f-4810-ba1f-6adaf30b016e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8f557adb-b7f3-4efb-9ba6-6b17495416d4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6a24b2e0-49f7-4e7a-aa0a-6b73640f3e81', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3969e331-c11e-44c8-a65a-6baa5645f608', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'defbbf03-c6be-484a-af3c-6bc126d62542', N'Протокол', N'4a4b2bb9-eddc-4691-8ce8-984a818b30fc')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'12260127-ae7e-4f1c-8645-6bd0ca3588f1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3f9b52f8-2d30-47fe-843c-6c2d0d8e93a0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9cd271e3-d87b-4a8e-b0f7-6c2d922ca383', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'25161dce-cfa2-4462-8574-6cb90aedf297', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e968c371-a0ff-46d4-992d-6ccfe9a70c62', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2a327c43-00c1-4038-b6c1-6deea970d6c4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2ce01573-4d66-415f-b718-6e4a93af7ff9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b4aaf8e7-e281-4563-8c91-6e7f4a8cc58c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a1975dc9-08b7-4b41-a2d1-6f028f465809', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e06d2a34-8cd1-4e50-8b20-6ff65d2b07e1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e9583c0a-c0d5-4513-8c48-6ff92f3cd7a9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b425404e-aca3-4fe7-89aa-6fff5c5d6aba', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b7dfb555-de0d-4764-ad19-70bdaaa710db', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd5faa20a-fc41-45ae-a480-710e84b46e8a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'49b1bffd-24c6-4023-9803-71c4fae2ee98', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bb02e12d-5fe6-4f25-8eaf-71d0055d791e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd5c37ab7-5ffd-4056-beab-71ec5885894d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b72bb362-f15c-465d-9690-7253092fd228', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7ad717a3-af6c-40c5-8a9d-7333c53f39cc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'20112900-141a-407c-960c-73435497b87d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'31f8e201-c8e4-4186-9161-7388b047eb32', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c9cbe2e4-b1ff-44d6-9578-746ab3278c32', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'983e667a-3607-4f14-b85e-74e9a132bd3e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7dfa5464-9c3b-4363-856f-751764b0200d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3cb7fdd9-357b-430b-b87f-751a215472e2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'519011f6-f124-4b46-b2e7-7590502e85d8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2e3802cf-aef7-4e0f-80a2-75d2a5846d1b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'92250e1c-7eaf-495b-b9f3-760d6f511cf6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd286731e-7cd7-453b-a92b-763495cb1da4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'00d60510-bcc5-45c9-b0d0-763d3cd4d481', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5a711950-6c14-430a-9ab2-76dfbba736df', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8cc9c6b6-d2ca-41ab-971c-76e661be64b0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'093ff86c-a66f-4782-9f71-772eebe66da4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cc23d536-badd-4bac-9cf5-776bc15670d3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'833e2e4a-5123-4891-bc76-77785e2aed74', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'be4f8029-c1ad-4583-a852-777caf2642d8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5bcb0fe5-f6af-4023-be9c-77c58de21c9b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0fee224f-0af4-4ee3-bebe-783b3aed240d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fc34eb9e-86e6-4a6e-b2ea-785916e56520', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3bb38434-0620-42d5-83bc-7890bd6ec8ef', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'af2d6e3a-7e60-48c0-8622-78a8562a371e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'de66d201-4296-4008-9624-78bf354b3144', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0cd07560-72d0-4031-afdd-79110da27ece', N'Подподзадача для теста', N'e75e157d-e452-4234-b403-00591b277561')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f446b06b-0577-4417-9009-792208310933', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7be0a987-1aa7-453f-89b3-793f0ca944a4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'10e7cb0f-ae71-4e33-8598-794221de15c7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'68e5052c-e673-4236-947b-79582707064e', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2acd9d9d-3025-4824-8b48-79e888cae451', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ede79bd4-a103-45f0-bef7-7a294d4aa091', N'Для большого теста', NULL)
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1f991dac-4e16-4f9d-b5a0-7a3bdcc03a8b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f6f15b93-7987-4203-921b-7a50f79025f6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e95332ed-2629-4d44-acde-7b3943b3c74c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8327e566-482b-4e89-b97c-7bb88ac56ed5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cf87c3d7-7a41-4851-aea0-7bd556812c8a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9478048b-c926-479e-8f7e-7be4ff847d6d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'17d0230c-5b78-4809-bfa5-7c0a0fdef7ad', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'deccc233-9cfd-4437-b0e9-7c467dc4ea43', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'003bca78-7d5d-4495-a3a2-7c6b2120e7ff', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'262edfcd-98a4-41ae-a844-7cb2e321a99a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd31c1b60-2d7f-415f-8cd5-7d0521826329', N'Подподзадача для теста', N'e75e157d-e452-4234-b403-00591b277561')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1c77e91f-5acf-4c66-a415-7d764339d6c6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0843f152-d4bc-44f6-ac4d-7d7830706903', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2dd7342a-d875-4dc7-b7e5-7d9cf43661a0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3c334f13-2ca7-4f06-91e9-7dd62d999a8b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'90dd7a04-34c2-477d-82fa-7dfe7228ca99', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a3c14da6-0823-4d9a-b3b7-7e1b1204cd8e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'aaa3adb8-e445-45e4-a69a-7e4dc03078a2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd0996df9-579f-4744-a019-7e61c22b4d64', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'292d204d-70f8-4602-b02e-7e973383c30e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'96587d51-3937-40a8-afd3-7eba267970ed', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'52185ef8-fca4-4b43-ac8c-7edbf233f589', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'035fcd52-2f36-486f-a75f-7f2043d02834', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c3d19fb0-cb49-4751-aab0-7faeb0877ad6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'01082ea6-5f2f-4b33-8dc7-7fe686a7b279', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'613086f7-9ae8-42f0-8544-809b9001f5ab', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ec360ae5-fc0a-485a-a47b-80dde37d3efe', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'969c2a3c-b7fa-4a57-a59e-816ee78bffb3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e496c4c1-3c42-40bb-af2f-823f27594907', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a2333150-1b12-483f-bf81-82627fea09ff', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e141d882-180e-401f-a5b3-82dec5dfbd84', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f4d8ac4e-3f13-4dc7-87d3-82e9085874ca', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2a78594e-c32e-44f2-bf81-83490abb8620', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7973ac85-9850-4574-982a-8364178e8e86', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd63863ed-1a46-40f9-a9e5-83859294fc1a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3f3a4005-dc3b-4f50-a41c-846f65249ce6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'811c404c-aa13-4ddb-bba2-84f0493c0b4a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'73d8095f-2969-4f98-be7a-84fb1368ded5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a50531eb-9923-499b-86c8-854732f36265', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9009aba1-4ad6-4574-9da0-8574c97af48c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3e5003e7-2f5b-41e3-a859-85934dca9d09', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0ebd9f96-b9a6-4c30-be26-85a365159edc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'edef6627-7c4e-4dcf-97e8-85be0fde3d7e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1fa3a2e1-a9f5-4091-bafa-863e5954101c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'02dfd1da-9056-450e-aadf-86761ea8a639', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7d524164-7bfe-441e-9482-86c69be858c5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2ff5f2c0-b849-4906-a681-86e9d4075a6a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1918161d-87b4-4f72-a7f8-872909971d43', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a562a869-b14a-4240-9fa4-8758b9ccdcf8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5700d5d6-fea8-4c5e-9900-87db1265c7ca', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b86242ab-0d9d-4a28-a71c-87e2d5e6703a', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'83af1acb-190a-4cde-8276-87e7663922bd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'721529a6-138f-444e-bcb6-88830dce3068', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'64ad323b-8ff3-4432-a5a3-8899c00e58f7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8f399d72-0360-4974-a21b-88a6155afdd1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5732a001-767e-4d57-b475-88ad2de90282', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'659adc41-59df-46ba-b917-891d2b591a5f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'85bbff0f-08b6-4554-9ae4-891fd4e85823', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1bf3f840-2e5d-43c5-a862-89aa90657b48', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'29edbbd8-ce54-4122-933c-89d59d8accc5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8f8123ef-b0da-4d85-944f-8a5aaf32d748', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8e65e4cf-f580-430f-907b-8a84236b4beb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8cd4a5c9-aaee-4fe2-8e82-8ac7cd4f39e0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e728d840-0cb9-42e6-a71b-8affaea71a7e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'de3b49d3-0da1-4922-83ae-8b5ad0e9b989', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'eae8b421-c41b-444c-bcb9-8b86fd68e177', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'224b9244-1766-4664-a9da-8c5da535230c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0635dc41-caea-4fa3-9225-8cb6b1fa8975', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ea19a62a-5daf-4521-85a7-8cc3e5f3a74d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'56033721-9e69-41fa-861f-8d035c668c41', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f629cf2c-2cdb-4387-8f6b-8d087e11693a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3cd5ea2d-9936-4e97-97d3-8d6f393d4f3e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3ab6d683-8ece-40af-981e-8d93b075ba92', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f420868a-f8dd-4ecc-8a88-8e3e0ef28e66', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'64263767-f0f6-4538-9840-8e55523f9d71', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3f34cc6b-45b9-4d53-8a52-8eee33812ae2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c503080e-09fd-49ee-9a18-8ef087aa1427', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fdf9c222-0520-499f-9faa-8f2c02ce968c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'da518936-0286-4d61-9356-8f38174e48f9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c2ec58f7-55a3-40ed-be71-8f3afa226b51', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ee473e47-3a97-49a7-a717-8fbc468f9488', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'397ee5b7-e475-40ef-ae51-8fdca076bcd7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'da348857-163d-4406-af6a-8ffb3d47baff', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'568a4905-e321-4cf2-bca6-905b7274b12e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c2b8c4d4-0e66-4de7-ab60-906aca0ab92f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bcd188df-2a67-402e-9c90-906cdb5eca87', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'88538fdc-4c4f-46dd-9aac-91494abf54d7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'07229ed2-250d-4875-b25e-9158e6758ed4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'88b5d1b9-891d-4b05-be9f-9159c837d34c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b8976bca-10fa-4324-8cb9-916f9b1ba941', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd6012cc1-aa4c-4ed9-88e6-918e9e620055', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1015f5d6-a819-4f26-9206-9209461d6b74', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2e699d80-2fe0-44a0-8826-92a39779383f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f88fc688-1f0a-433e-ae6f-9312ef3baee0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'52088d1f-7b7e-4735-8788-931643462c06', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f2e4019f-3d84-41d4-836e-9348e15708aa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2723646e-4d77-46e5-94d3-93652a35ae9c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'02ff1ca8-59ca-4914-873d-93b2cb123946', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cfa1fed7-ec6e-458e-96fe-93b86cc35cdf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b5b616bb-42a5-415a-8374-93ce2100dfa9', N'Для большого теста', NULL)
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a73b1591-f14c-4ea3-af8a-93fd0a158466', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ad393cd1-613d-404e-b30f-945aad091031', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c3661e3e-11dc-482b-8f05-9470a426f426', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'982a99da-0bfa-473f-8f26-948c2a0ccc70', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd4d7e820-9fa2-40fe-9ceb-94cf9221583e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fa8abf5b-a3f7-48f7-a083-94dd84bc5adc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'30704c63-4bd2-4245-9bb0-94e8b79996f9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f4ab44c1-00cf-46da-b2d9-94ee1fa4178c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'692445da-7a1a-4d7a-8a86-95104ee5efaf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'969e9142-9536-4d28-ac5e-955d36fe02f0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4fd255b2-bf09-4812-a9ef-95744d7b9bd0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5bcd57c9-8d21-40ae-b874-95ed91f68ccd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4b953b0d-6d18-4d2c-9032-961f314327bb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c186e5bc-4f1b-4539-8a0b-9685f3fce4f3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a78635d0-331f-422a-ba65-96d8a2f86d88', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd752b50c-8480-4668-8c02-9818d50e251a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4a4b2bb9-eddc-4691-8ce8-984a818b30fc', N'Документы', N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0bd3e07c-513d-4e22-9f39-98733b045491', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5aeded2e-4429-4672-b5dc-988f39286a0d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd9fa4c8c-a965-4d43-b3a3-98c039884481', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'861636cf-bfd8-472b-a9c3-9944c4c9279f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'89e25e9c-2742-4987-9eb8-997fd4439730', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'57fed8ad-5012-4420-b9da-998cc334ba01', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b00aa829-22ec-4483-8202-99916f56c00f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5484d8ab-bbb8-4f85-82d0-999cf1bc534c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'427f84b6-3a9c-4750-830e-99e1089a5302', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c3d41ad2-1ad7-48c5-a12a-9a0012b9d5cf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5950858d-9622-4d68-8e28-9a04f3750c3e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b2155672-671f-40c7-8e1b-9a2024c642ca', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f6c99845-235f-4673-ad99-9ae7a7260e18', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'456347e2-8a62-475d-848a-9bc6a042f6f4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'84523c99-f149-42d8-afcc-9bc78f11b1fb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'36072585-c18d-4038-8f3a-9bf87da486c1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'24c64029-38fb-4654-8529-9c4d28662f41', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'659ba988-8a3d-4671-b664-9c524894fcb5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd0073f8a-05de-4adf-b833-9ce154a39b78', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'918bf21f-0ee0-46b5-97e3-9d337a1a9bdd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bc47e4f4-8d6b-4200-8d4e-9d384deb86aa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'443df72f-48e7-43ba-b33f-9da93b7f0bb2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'75d0119e-6a93-4c0b-9cc8-9ddf0d3085c2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dc741ba3-b7f6-47e2-9274-9f16b16538ae', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8035f248-d252-4fac-9fda-9f671ee7c08d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f8e3ed87-d502-4449-85ae-9f7f0c5c6b29', N'Подподзадача для теста', N'e75e157d-e452-4234-b403-00591b277561')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4b4315bb-7472-40bd-ae33-9fa4599647cf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c4f1f288-690f-45d7-9992-a07347c252da', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ca376daa-05a7-4644-9d18-a08499eb863b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9f4629bd-c6c1-4fec-be26-a131e68c49c8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4a52f107-2bcd-44ad-a9cf-a156485e5e7e', N'ГПН-Лаборатория', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0dcbc48e-3e71-4631-89b9-a16c00ff3bb3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b9aa5786-9d34-4441-b132-a16f2bf35f9b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'df0c961b-80dd-4283-9e6e-a17ecc28ee5b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2218a1d7-fb0b-421f-996e-a1900cb3d752', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3ec4f885-31d6-433f-b6dd-a1989cd49733', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fa5b38f0-3e9a-4eff-8431-a19cb4f87afc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6d200567-5d6f-4fe2-ad01-a24f78150cf7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c7c74e35-05b5-4f69-b40d-a26daaa67469', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e3afddea-af58-474d-a606-a26e826d533f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e4869262-700e-4850-89c0-a33870bd2443', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'383ea00a-fabb-42d6-8c42-a35441ecec01', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'29037495-69f3-4547-a156-a35c9fe41bad', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2e828b30-4359-4469-bab6-a38002841133', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'61b27e97-f869-4ec4-ae5d-a3ae3b8f9cec', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'18859421-81de-4eea-bf5a-a3dc8e85c21d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a2fe7cf6-d4d3-4772-855b-a3e766ee0330', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'45cebd61-1737-4113-9d36-a431195820e1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f6fa019d-4194-466f-8881-a4315d9250ca', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dacce5f8-529d-4cc3-9bf0-a4519c12fbb2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'99c08d26-4b69-4f76-ba4f-a47221157065', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'50e1d01d-b6c0-47f3-993c-a4b225c01115', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd0b76e4c-7e18-4feb-9226-a4f306cb855c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3f22d92f-0c4b-4e8d-8134-a503f8b678f4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a9f4f96e-99be-4d17-a5f9-a5387428a980', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b326dbdf-d27b-4464-b363-a5962c1aef17', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cdd41d45-5234-4a85-8823-a59a6f471ec9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ea8c667b-e981-4c55-9790-a59acf06e06a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'aa72d941-e4df-4b60-8801-a5d8d6adfccc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ee017adb-6f08-4c7c-ba83-a60a27cd4087', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b6846751-33fa-4f49-964c-a61fbacb2bfd', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a3839633-ae00-46a7-8565-a6831d7a361a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0a4b3b6a-7e45-44f8-9312-a70abc7b0970', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd962aebb-d717-4444-b987-a743e6bbf9ed', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a059eff4-0dbc-4d71-834d-a7606eb0bf82', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4f9878c7-4ccd-49d6-9469-a771f1f44437', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4644cd33-4ee7-4932-bdac-a7771842e949', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0e88a111-5f18-4d3a-bf1f-a7a398ed1b44', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c08f3e0f-b76b-47e0-8b6c-a7acf8cda3dd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'65f7a1a1-e137-4e4d-80a6-a826b4a055e5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7ed07e84-c786-48ab-bb97-a868a02a7d83', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b4432c28-2fed-4c5e-b566-a87f9ea568f0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4844a33b-53ca-4b36-bbdf-a907c0420673', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'13c785cd-00c8-492b-95ed-a942fdfe8d59', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b4f5544f-8eb9-4143-985e-a94a81615428', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'62dd9dd9-228c-45a2-8af0-a9c4aea954c1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6f73d60e-ae60-45c1-a2f3-aa9969a3fc2d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2436b68b-11d7-49af-b9e4-aac60d6ec5c6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'693bde69-4198-4fb1-a7fa-aac7d1d3831d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4118b8c2-8817-431a-9a4f-ab24dae11f79', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'984e7d60-8127-407e-a61d-ab3204ffee7f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f5dffbdf-7da1-4840-a91b-ab68432ed715', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e833e691-67b5-4ebe-bd0e-ab7086b31135', N'Для большого теста', NULL)
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fbbde395-11c7-44ed-a62a-abc8ce27325d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd7296504-e703-464d-ad9a-abd306491013', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cc6cf9b7-71bc-4ef2-b37c-abd8f7348b92', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fa618a85-3beb-4bca-9400-abe6ec69a450', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5b68b97f-0eef-412a-a7d5-abf14607350d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'828d0595-2a81-494c-b512-abfe64b9a7a0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd76a00ad-946c-4335-9d7a-ac064695144d', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6daf4c72-08e1-44d7-a3f7-ac4a11def032', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'eeccca81-d1d9-4184-90e6-ac8c67ca46ed', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c280d3d4-604d-48a9-b5f3-acc203496857', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'13488a22-4c5e-4a60-b31d-acd08706084b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bcd81c6b-1771-4c87-851f-acd39c351623', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'60bb695e-b62f-4cac-b176-ad16c70012b2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd6c80b47-3500-4f47-82b1-ad5635861fdb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9bbf7514-8f3a-452f-a982-ad5d36a174a8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd437158b-94fc-485a-bdde-adb01a2f1a59', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fa822b08-4baa-4bf1-bdfb-adb70fed394f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b3ed2a26-5d8f-4b42-af5c-ae15c8fcbddf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1433cf12-3915-4944-b7bd-ae8c0598e5a0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'17895ce1-a9d1-4abd-9c2e-af2309c0d017', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4feed8d1-5db5-4c2f-80dc-af45b5d720af', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'48eec85c-8502-459c-a414-afbbb8edef77', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7d370483-f130-4551-98f4-b02281fd0b67', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'13241c50-5155-4f5d-8536-b0420ccac04f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'987ad179-c25a-4d33-9358-b087fa89b5b9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd2f2a872-8af7-44f5-a778-b095698c405d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e0c686b1-b4ae-4bf7-8553-b0ddf94bcffc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cc6d56cf-40a2-4d27-959b-b0f1416c5f03', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'16b6c907-2797-495f-91c1-b10a12ab4640', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b2658d48-06d0-4fa7-a116-b13fbbceed7d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c16c6ff0-adf4-4985-b215-b156a586042d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'15b00811-275b-4734-aec5-b15e8d273dba', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'eb87b5b8-c1e3-4057-b504-b1c146d9cb42', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c6845479-7b89-4ba6-bccc-b22557e97a43', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'88e551ce-b8ac-45b1-8d4e-b2daafc78299', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b3e1431f-15e6-409a-a36b-b2ff89784d55', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'58cb207f-31a8-4166-aa83-b31376fbbd77', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'596a9aa1-a7ac-4929-9e29-b31b11904c26', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'95a73db8-46ed-41dc-97e2-b36f1c282fb9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'da7f5b25-8737-45df-a1bb-b40c2d9a87c1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd3749ea2-7939-4a6f-807c-b4452d2eda11', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f30b9aba-22a8-435a-aa4a-b460dabb1fa7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e7e9b891-dc61-4d04-b6c5-b4ef1321568a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'93abffff-61f7-48d6-bbb2-b59c8814ec96', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'24f5b02d-8528-4c97-9e34-b5d0b26ed565', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a78d008f-6ea5-412e-a55b-b5ec811e9208', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'47a3ba70-6ee8-4731-a81b-b640c2193bd0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fe511fc7-9005-489c-bec4-b6a0d5b263ef', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'413505d4-cd2e-44b8-9dec-b6bbdc26ec2c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6a324359-7123-40ea-bd7a-b6c71532220f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cf02c17f-74b6-4771-9ebc-b74e3af17316', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'772bec25-e35b-4b31-97f7-b7543c7121f7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b3817048-fccb-4de4-bc33-b7a723b614cb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7a1b782a-6019-4146-93f5-b7e66cb9a061', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'72729ab8-cf4f-4703-a78e-b7ef88b867a5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'762c7821-97ae-4cca-8549-b82c65105dbe', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cf5a6fde-dfc7-4191-8da9-b880ed1990d0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'32c0dc0a-49eb-4195-a762-b8a7a8a42397', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0774ca15-7433-4193-96a6-b8c2f9d4a92e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dd6a888d-d0bb-4398-93e3-b95301c66f67', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'58135e4d-af26-4981-9360-b9706873fa4a', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cab9f288-aad1-47c1-a3ce-b9a1424b03cd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8c50fea4-a9f4-428b-80c7-b9b3590ef8dc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd2f989dd-ff95-4dc4-8e69-ba01aea7f5d8', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4cb63e12-ecca-455b-95e0-ba333af0ed62', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'df7b4690-72eb-4639-b2e6-ba5c9bd2f34a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1e96c47d-a782-4f5b-ad08-bac862e22f56', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e03ed355-5dec-4c23-bec4-bae0813736db', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd8b0b3d6-af52-490f-bc17-bb1c8ff4cd8d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a8734a5c-be1f-4f34-88fd-bbb9fbd8f72b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4b36006e-d9b8-44ac-877a-bbbffbf3c17c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'86903e26-ddad-4ddd-b1ea-bbca608ea1a7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'13fc756b-1a1f-49fc-9661-bc2af3e01df1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd744b98b-07b3-406b-87da-bc38abdc3309', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f4105a6e-d1aa-4924-a91e-bc46a7f1f857', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f86d6347-4556-4176-9834-bc57ef767939', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'aad2b205-0f07-4107-a759-bc9b1aeb78d2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cf5fc623-67e6-455f-8546-bc9edbd6339b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'eee7cc94-6dcb-4a92-8791-bca2bb879333', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c9ebc359-bbc2-452d-a170-bcb968bc61b1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1ce04c4b-fb41-4fcb-9ac9-bcfe361f746a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'26d89584-e8ed-42a6-b5bf-bd15815b2cf9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5b1719b5-31d8-4e71-bbba-bd1b9f2331fb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'304511ed-296d-4cac-9fbd-bd4b707ced16', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a57d190e-ceb3-4593-a5e7-bd81c01941f7', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f7fbad5d-c0b1-4e10-a38e-be25cc090451', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'792d38d0-7344-4b05-8582-be6deac60584', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7b9124d1-6932-4783-b58b-be7c27877f0c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'84adc9ea-a439-425c-86bc-be8a1b826d79', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'04e7818d-a968-4d5f-9276-bebe7f0db68f', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a455b3ba-4f78-4cab-aa08-becac72307aa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0fa39572-ada4-4159-844e-bee61d54227c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'41ab41a7-d390-4b1e-aea8-bf3cde3b0edd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ab212099-f4e6-4c15-8087-bf91b7a6b3c1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'99f7901c-a45b-49ea-a962-bfd14988ed5b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7dd061f2-aa13-4da2-9653-bfebdd7b34e1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'19a8c1cc-b6f5-4cf7-9fa5-c0046d979616', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'27966840-20d9-4b7a-9e67-c03ec423b920', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1a722317-5a40-4fd9-b761-c04ac4f8cf4a', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'59ce7354-d2e7-4634-87e8-c058a57799da', N'Для большого теста', NULL)
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5aab15b0-4af0-455b-9a3c-c0a1622c06a4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'30647d28-f2c6-4900-9e9e-c0a4179bdd83', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6b7fd880-944c-4afe-a963-c139160d9743', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0dab7aab-a3a7-4b46-a13b-c172fe56d7a2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6eb88f46-8a19-4a40-8dac-c17d8d129df4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ca0d0bd3-2f41-4153-8256-c18ce8564135', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dc6179fb-fb43-45b5-9359-c1cb36c526d5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'822d43ab-f9c1-4271-a08d-c20d21c34159', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'96fb847e-250d-4e32-9359-c20de37d1d99', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4cbd8a5e-e0b2-4e4a-92c1-c24b4c275085', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'90224d64-d4fb-4de5-a7e1-c262ab0831e5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e1ccc1de-1196-4b96-8d6b-c27b83a0547d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2e8a58df-ad40-40e0-9724-c2ec6b7d2c5e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b0be7e37-267b-4dd4-a903-c36477d87407', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'67ce8a82-338f-4725-8532-c36cea139222', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3e9af2eb-afd6-4b5d-a219-c3703b0502e5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7b38e4f3-008d-46fa-8881-c373de91f26a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'515a7aff-0290-4689-8fc6-c37bd499029c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a9167cc5-4fef-4c83-a4a7-c383af3ceb68', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'672309dd-0117-41e7-8202-c3b71e097be1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b45aa93f-f589-40b7-a4a7-c40021976f4a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'047dc5c5-fcf3-4977-b2ea-c40fe2d59cb6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cfe86bc8-7caa-4f28-b809-c42fa339e24b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5e167989-5646-4979-9c85-c45937ffe5f7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3e890b2a-7535-4d5f-8a1a-c4676128d099', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0a34bef7-afe2-4263-8a98-c4a29e6384a9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e74842da-20c0-4e10-99b0-c513ea521f91', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'19a5352b-4eb9-475f-98d3-c56e8e9bc0e4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ced43aa8-a5ee-4ca1-a558-c56ea629b253', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e2667c78-8725-49b9-a5cf-c624ea7d2dfc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4d30cf8d-a343-492e-8bed-c62a54710dc6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ed701cb9-fa76-4f35-b9e3-c68967148f9f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'34bd5ea8-6151-438c-b234-c77312fb1568', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a0911db6-0715-44e2-ad41-c7bc09182381', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'580eab32-7408-4ddc-b583-c7c63f464cfe', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'953cb7d5-1c7f-4c87-9077-c8069e1567a4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'46a73261-8bdb-482b-8721-c830c243f4f5', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'232f6c75-6e38-443d-bbf0-c8590db14797', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f35a83e0-9bd9-484c-9377-c88de8df8f3e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cb4e77e9-f0b7-4a0a-aede-c8de53feaf49', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a7b3c487-c85a-4aea-9427-c8f3f7b9d276', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'93169a8b-07e0-4d51-ad4f-c918c7159d98', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'720f72d5-eebd-47dc-ac2e-c9618ac03f16', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2b6e2e95-8ae6-45bc-9293-c9c15ce00ed3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'147dafca-8ab5-44dc-b4d6-c9c77abb568d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'aedcf04b-d3e3-409f-8bf7-c9f6677d6e12', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9cb07015-ae7b-41a2-84eb-ca14b98acf57', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fed336a5-c8c6-4acd-8b2c-ca1d192fe5d4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0a90d77f-0aef-44f3-b446-ca1da427318e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'44d3c9ce-763c-43ec-848b-ca3527abacf9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'99c3816d-17a9-47b5-9c2c-ca57b0762574', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'07c0504b-46e4-4a1b-8198-cadc78dcdcc7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd89acb3c-8a69-4ba0-8d42-cb03efdc0a0d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'80b95a6b-d734-4c91-a316-cbae0043c3cb', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2fb44701-d33a-46a2-97c2-cc6027f64d11', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bf592afd-b269-455b-8a11-cc8842fc4527', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'42724643-a020-49e7-b9d1-cca3beaeb633', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'86f5d306-6d16-4d4c-b249-cce723159d1c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'586badb7-4df7-404a-9829-cd4c3b35f759', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'538f5bde-5b32-42ed-ba87-cdd017fc2584', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'641afd80-c054-42a7-bf86-cdf34395b475', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5daf2362-7c3a-4a9b-9bbc-ce4867aed264', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'bef7e530-0c23-462d-aa6a-ce6c90454232', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0403b06d-ee58-4e9d-9eff-ce93a0e4d1d4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6e8c0fe5-c679-40c6-8f6e-cebbb60ca2ef', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'756f8548-2938-4cd3-88d4-cf1c6897e56b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'277f7fa7-8eba-4425-bb89-cf3b3afc665a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e6d7df5e-8dcf-4b6a-bf1f-cfe6e68f13f0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b1979ab0-b5ac-421e-941e-d01a2487ba4c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd6fb23a8-250d-4e9d-a6ad-d01dc1fec504', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7a2fc27b-ab1e-4731-896d-d066bebec985', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'37b8ef66-cd55-42cc-90e6-d072b1e967ad', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f919afc6-1ffe-4615-8af8-d0c94ac6da4f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd8c7713c-e57b-4510-ae24-d0f2d11d824f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd279b6f5-a3b8-450c-9387-d10acd3c560d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a37c5f09-57ca-441f-9046-d1855d343487', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7f8b5ec8-6fbc-40f8-9365-d1d42fccfbe4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'654b357f-9c9c-4966-9eb9-d29b5d6a459c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dcfec742-d03b-49d5-80ac-d2ba4e247658', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8124d16f-5d06-4669-b5af-d2d90f0bf80d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'db78cead-dade-4d82-b1a0-d2d9cdc46e15', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9edaaeeb-4551-48e1-a0ae-d2f8bc853b62', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8c44c4c9-5345-443c-b9de-d303bb5cde12', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0f204e00-9252-49f0-ae7e-d326ebbcabcc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'87d3aaa3-03e1-4615-b39b-d33acea1e18f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'597ca174-d116-4eea-853e-d368f453133b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b6250ac6-1bbc-49ad-9529-d36a94578086', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a6f0ec56-f7d9-4c96-b21d-d3c64fefc708', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5c5320ec-61be-4e1c-86ef-d41c6b226532', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'782dc9bf-af09-467b-91d7-d41fabd0b2bf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'976c4feb-1cf3-43cf-93e1-d540b5a4e90b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'122cd9ab-d946-4fcf-b3e4-d58b64e5de76', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'34bfc6fe-cb95-4d14-ae09-d59d3a281e1c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'57e39330-2e01-472b-bac0-d6260ca6f539', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ac9012b4-4a81-4ea1-a2b3-d63d47580268', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6a43ae7f-8ced-44ff-af96-d647b3643e12', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7f953567-8a0c-4d1f-ad0c-d64a38d74887', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e9ca9bf5-27a8-499b-b4b1-d64e95308b3a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2baad308-a7da-4193-b2d4-d6b014ac4778', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e0a4921c-6ce1-4afc-af1f-d706971a78b9', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6d386d15-bc31-44b0-b472-d7bcf9b37eca', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9af07fef-cb09-4325-b315-d7d4128a751a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'04929cbe-9b24-49fd-9d3c-d7d5d36b7d35', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'eaf98dd0-a301-463e-8801-d7ea1487971e', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c0407d61-0226-4076-b265-d7ea48a52a8a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5f82e600-2fac-411a-ad78-d7ee75cd153a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1863b5b5-1fd3-46a4-88e3-d80065898aa6', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'34ba0ab6-0135-46b5-945c-d81f76cebda5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'047c46bd-b324-4c46-9b96-d82346232d3a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e5839f12-94e3-4aee-a464-d83ce32a7b25', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5782f8aa-7bd8-4035-860a-d851c2de4123', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5a6da16a-c205-4d50-9593-d8ac0083816c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ab701330-6364-4a2e-a36e-d8c0d6fef51a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'49eae3ad-44f1-4c91-89c0-d91b1fdc9243', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1358a105-a2d4-4fc4-b36c-d9769dd85684', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c70d91f7-e281-45c5-afbe-d98bcc72e14f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0b2f3578-7e06-4ca7-a8f8-d9abb47f0df6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9a347f82-7c4e-4952-bf24-d9f9f466fb64', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'917bbee3-c73c-41f9-8426-dabf0c265bcd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'88050f7d-7411-4cdb-97bd-dac17287f669', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3724b5ad-f6d2-4a38-998b-dacf12f1ff4d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9d54f894-8909-40c8-acf7-dad8cad0c904', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'06f41fc5-6268-43b7-ac9c-db18e186915e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'af5b5e61-59a2-4bf7-86d8-db51d137ab4a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4b5785e5-3aa8-48a9-9f18-db6050e10b77', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8bca52c3-8452-4016-850a-db84af8e6ec3', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd78635a9-cfa9-4c8e-8531-dc504467a09e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c4fabb1d-36c5-4184-8c22-dc726e67e4f2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'85b89213-44dc-43c8-b34e-dc8dd3c509aa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'054e6afb-be26-4104-9aa0-dcf7f55d8a7f', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fff68f02-1998-4412-a674-dd020803e5b3', N'ДС-6 ', N'4a52f107-2bcd-44ad-a9cf-a156485e5e7e')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f57bf479-25b4-409a-90a7-dd5d8915d11c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5bd1c6db-2e01-499c-b75f-dd7752809e14', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'daec7f44-5e64-4b9e-9bde-dd9141ae2d34', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4dfb6fb5-b526-4f36-aded-deec77932c75', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'32880cba-b68e-482c-8b76-df4d6d77953e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'013cbfd0-103f-476f-b0cf-df93fe63434d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8b39648e-a34b-43f1-90b0-dfa23360d992', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1486d9c4-f315-490b-a998-dfc46c903847', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5b01f8e8-efa1-4d77-86ae-dfff37c97704', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c79241af-be51-4383-8a8f-e014231ea436', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3e324acc-d9d3-46fd-a11b-e01e1e187dc0', N'Консультации', N'991248ed-8fd3-4756-8b0d-686e1b274d75')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'baa5fc9a-177d-427a-ab9b-e0c58baf0942', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'882d086c-4c68-4adb-8f67-e0ee94af80dc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca', N'Дополнительные работы', N'991248ed-8fd3-4756-8b0d-686e1b274d75')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'21c07d54-ebdc-46d4-b84a-e16ffde8fd27', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'632b5ed4-8d51-404e-960a-e1b555bf2218', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8599f3da-e576-497e-8f92-e243bef3c907', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'70df2b5b-f312-4b19-bf08-e261aa8f942a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e1c34916-336a-4e80-bdb8-e2ae7608b4f0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'530341d2-86e2-4735-b700-e2d222f3aae5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e07b39d1-e36f-4dde-8546-e2d3735478f4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6938e4c2-7d80-43e7-a330-e2d6029cf011', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'54d5aba7-b9b1-4d0a-a061-e2dc8fdba86d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9b6a5c98-6c30-42c8-9119-e419df13529c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a96999ba-1374-41a8-be92-e4554f8f899e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0af7e6e7-04a8-44da-8609-e56795b0449a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'88ca9b2b-0cf9-4f73-8e7e-e65afaff3b1e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f675604e-d544-48a9-bc1a-e6ce2d342dce', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3ce6f38c-28d3-4fdd-9d13-e6f0a7f4a49c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8b2947ca-5a82-4155-b1ee-e6f0b81ba4c6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c069a7a4-d6eb-425a-84b6-e75a2ddce979', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e50ebd59-9139-46b9-ae3b-e7a0add03a9d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b71d59ba-6ddd-4087-9e3a-e7b0de93d8ab', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'64a5aa1b-3bbf-4c74-b61b-e7c9a5425d0e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd9f0d4bf-7537-4dc9-a9b2-e7f4efad7ad8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'11e25a25-7a8c-4fcb-82ec-e8097fef11cf', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0587bb5d-a2a2-49c1-bec4-e834ee4f3c54', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'63962cc9-10ef-4adc-8dcd-e8454a5c2328', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e49d5315-35fc-4d5a-9745-e92ff10e088f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6c5584cd-593a-4625-af69-e99462d39da0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ea52db4d-7b35-41c2-9daf-e9f383d549a8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5451109b-8a94-4bde-a77b-ea55d592b86f', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f5b232a0-2607-4dbe-a373-ea623ca39c8c', N'Подподзадача для теста', N'e75e157d-e452-4234-b403-00591b277561')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6e7029da-970b-4b6a-9a09-ea8f93430a2d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'214ab635-b340-49e5-bd0a-ead98e5d5cb5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8a309652-2294-4259-bd98-eaff4f5d5201', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e95b60a5-3143-4df5-b431-eb10632a1b61', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a5233ffc-d3f6-48ec-82f8-eb2241b0ad63', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7d475c9a-ea55-4e02-8d35-ebad6b101465', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'75e13f32-9f1e-4bc3-9023-ec1bd10b0863', N'Инструкции/описание', N'2c0e6520-3071-49bd-a774-6aade88376be')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3f619ae9-2a66-4299-a96e-ec6b0638585e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'af87e32d-28a1-44ba-9b0d-ec7bb2082477', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'40e4211d-1ac4-4eb3-85df-ecd6c48e9853', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'28184c7d-2e79-4089-831c-ed1202019937', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fbbc8c8e-2eb7-4a8e-aec3-ed339dbbc603', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4cd44459-abe1-4e46-9914-ed4833912bb6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a1fc67f8-bdc3-4693-928c-ed4b48152102', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b3257bc3-b998-4d1c-9391-eda5dfc13133', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd5021496-8889-4399-8ead-edacf72fb69b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c9ba2d04-9074-441d-b428-ede406bf3370', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ee02debb-0f3c-407e-8e65-ee5df33388bc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'01ff10bb-9134-48ee-8423-ee7f18598990', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9ce134ab-2400-4579-917c-eea20033f541', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2410cb69-1798-4418-b0e1-eeace7626674', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd3a50f6f-2718-4edb-8db3-eead8828e85b', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'9968a49e-b537-4e06-a05c-eed129c8dd0c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5a2de9b2-55cf-45fc-a8b4-eeda7be99c55', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2a9ea815-bf30-47c2-8e57-eef5036287aa', N'Подподзадача для теста', N'e75e157d-e452-4234-b403-00591b277561')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'5ea8da63-b66a-42f7-bbe6-ef030fcba37a', N'Для большого теста', NULL)
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'321f7c5d-e1d9-490b-9978-ef0573219fc9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'63a49536-7396-4992-abfd-ef3dc87133cc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e8085eeb-1909-4942-904e-ef8409aa464c', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ad2bfa42-e869-4ce4-86c4-f093c5c46b94', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2126cbef-4733-45f8-8d50-f0a401003dfe', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a43546fd-db8b-49e8-be03-f18713341e13', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'faeb4bbb-9af9-4c20-b59f-f1b6ca4857b9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd166ed38-398b-48f2-b466-f28c8021b4f0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ea75a53f-5734-4d88-82ac-f2930fb0d6f9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'07fedbc0-482d-4c5b-83c2-f2eaa68072be', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a8cdcf8f-2ed8-4a32-a06c-f2efd64b93a9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'49e07980-2293-4453-b305-f3398106dd16', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'390c3be6-8b32-44f5-a3c4-f33aa9bf2399', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'67d49374-22bc-4813-beb7-f367a8af5476', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'55c37de1-1c97-47fb-bbf5-f36b3116adc6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c7a41af4-a64c-4e65-845b-f38f042649b8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'64b2ad8d-b872-452e-abb8-f3a1d1978e42', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e11198b5-2b76-4935-8446-f3ee234611f0', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'13681b90-b18e-48af-a76e-f4074fe68056', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd6d933cf-3a03-4110-84be-f40e276d62ac', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c88e26c1-2cd4-4dc1-b61f-f41db60b3b07', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'66fb4dcc-5128-4fb9-b4c5-f43ef19a1f6d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b67bcabc-3b9d-427d-9c7f-f54a68cf9543', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c96c07d9-55d0-44bb-8b28-f6a2743e127e', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'eb26ac41-cee3-4e29-aaf1-f6aa10013f79', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'369dbe02-e5ed-4f9a-9f6a-f6acc797acd6', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7d4b7da1-2f92-468c-8e60-f6f6a75eb589', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'cf4bfea0-1eeb-4c50-8ba1-f746d14979ea', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a15fd0cd-92c8-4c10-b6eb-f77b59d59bb5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'15173512-2ca2-4a55-a440-f7df20d498e8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'25e9b530-c2e1-44c4-a2eb-f8013f3eead4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f152bf38-20a8-4118-9537-f82345460c75', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'38d162e9-8e93-4ad5-ad25-f82cbac2b674', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0ce00562-e807-4197-984e-f862c28569e1', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'6a4ed611-ce07-42e4-9487-f88d3db93151', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c7482605-c84e-47bb-a9c9-f8c0fffd6e58', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'e4f54b06-514d-47cd-bb0f-f8e538e1eb0d', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4695421b-fa2c-4d3a-acbd-f8ec41a5b47a', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd22232f3-7bf2-45d8-87f8-f9165c9d22ae', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'601601e2-f637-4adb-8f73-f9657f16aef2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'904648ac-3506-4bb9-abdd-f96a864ddb41', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b6eb0dbe-4eae-49fb-b16d-f999047679b3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'85ee9ac4-008b-4983-8af4-fa220f0b8fc3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ee5912c3-c951-46fa-98ae-fa3f8e8478f8', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1401b461-186c-4f8c-a6ce-fa693aacb7aa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'99af20cb-37f0-4e05-b057-fab2facd4f75', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'01d8e4f9-9dda-49e5-9121-fb41c437b024', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ae45ed44-c4b9-4adc-b1d6-fb612cf4d052', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'7fd59409-e053-42df-9885-fb771d593c14', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8a354c1f-f3a4-471d-8c84-fb7b38c889bd', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f18f44be-b3c0-4af7-a1b3-fbe918f67cb9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'f67afa01-32cd-40d0-86e3-fc9a417a5cd4', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'65547ed7-55d3-4107-9fcc-fcc9dc90e8fa', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'dd4bfe32-7cde-48c2-81e1-fd0bda17ef92', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'159b2b17-4cf6-4ac4-82c1-fd2f2181f536', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'eabc1770-9bea-49d6-9b17-fd30aa0e1b46', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'b5a9ffc0-a2e9-4bba-91bb-fd91dbb43029', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0f426841-7b37-4b73-a561-fdb715eb0220', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0e9112b6-57cc-4fc3-a80d-fe27c2d10ad2', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'c1369746-feb7-4768-b655-fe310eccd539', N'Подзадача для теста', N'23b10fa7-5a34-49e9-bc9a-002188efafb3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'674825ab-bdbd-4122-a451-fe56446fdbd5', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'09c0158c-7328-4cd3-b433-fe8fd9d902a3', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'0f1255ef-ef5f-4c4f-8c9a-fecc1fcf8cbc', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ada3d8c5-1287-42e8-91a0-feef8e8431b7', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'a585171c-2e0a-4556-92d8-ff1715964d22', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'8f73d139-3f6e-40a6-a318-ff4ecb164297', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4dbf3e64-f2cd-44ca-99c0-ff9e64cff1ef', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'82bc298d-88b6-4680-adcd-ffcc218d0fd9', N'Для большого теста', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4c579698-1d84-498a-9f9c-ffda415eda50', N'Для большого теста', NULL)
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
