SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE master
GO

CREATE DATABASE ConfigurationManager2
GO

USE ConfigurationManager2
GO

CREATE TABLE [dbo].[Submodule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SubmoduleParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Value] [nvarchar](50) NOT NULL,
	[SubmoduleID] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [int] NULL
) ON [PRIMARY] 

ALTER TABLE [dbo].[Submodule] ADD  CONSTRAINT [PK_Submodules] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SubmoduleParameter] ADD  CONSTRAINT [PK_SubmoduleParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SubmoduleParameter]  WITH CHECK ADD  CONSTRAINT [FK__Submodule__Submo__3B75D760] FOREIGN KEY([SubmoduleID])
REFERENCES [dbo].[Submodule] ([ID])
GO
ALTER TABLE [dbo].[SubmoduleParameter] CHECK CONSTRAINT [FK__Submodule__Submo__3B75D760]
GO
