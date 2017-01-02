/****** Object:  Table [dbo].[Filters]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Filters](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [varchar](max) NULL,
	[Distribution] [varchar](max) NULL,
	[Query] [varchar](max) NULL,
 CONSTRAINT [PK_Filters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LogApplications]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogApplications](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[LogId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.LogApplications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogMessageDetail]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogMessageDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ContentKey] [nvarchar](128) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_LogMessageDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogMessages]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogMessages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ElmahId] [nvarchar](1000) NULL,
	[DateTime] [datetime] NOT NULL,
	[Hostname] [nvarchar](1000) NULL,
	[Severity] [int] NOT NULL,
	[StatusCode] [int] NULL,
	[Title] [varchar](max) NULL,
	[Url] [nvarchar](1000) NULL,
	[Blob] [nvarchar](max) NULL,
	[LogId] [bigint] NOT NULL,
	[ApplicationId] [bigint] NULL,
	[UserId] [bigint] NULL,
	[MessageTypeId] [bigint] NULL,
	[SourceId] [bigint] NULL,
	[IpAddressId] [bigint] NULL,
	[DetailId] [bigint] NULL,
	[LogCount] [int] NOT NULL DEFAULT ((1)),
	ContentKey nvarchar(128) NULL, -- content key hash used to match log messages
	Copies varchar(MAX) NULL, -- count # of copies
 CONSTRAINT [PK_dbo.LogMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LogMessageSources]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogMessageSources](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
 CONSTRAINT [PK_dbo.LogMessageSources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogMessageTypes]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogMessageTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Icon] [varchar](20) NULL CONSTRAINT [DF_LogMessageTypes_Icon]  DEFAULT ('cloud'),
	[Color] [varchar](10) NULL CONSTRAINT [DF_LogMessageTypes_Color]  DEFAULT ('#000000'),
 CONSTRAINT [PK_dbo.LogMessageTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[LogId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogUsers]    Script Date: 5/21/2016 9:34:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogUsers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
 CONSTRAINT [PK_dbo.LogUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_LogId]    Script Date: 5/21/2016 9:34:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_LogId] ON [dbo].[LogApplications]
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationId]    Script Date: 5/21/2016 9:34:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationId] ON [dbo].[LogMessages]
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LogId]    Script Date: 5/21/2016 9:34:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_LogId] ON [dbo].[LogMessages]
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageTypeId]    Script Date: 5/21/2016 9:34:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_MessageTypeId] ON [dbo].[LogMessages]
(
	[MessageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceId]    Script Date: 5/21/2016 9:34:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_SourceId] ON [dbo].[LogMessages]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 5/21/2016 9:34:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[LogMessages]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LogApplications]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LogApplications_dbo.Logs_LogId] FOREIGN KEY([LogId])
REFERENCES [dbo].[Logs] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LogApplications] CHECK CONSTRAINT [FK_dbo.LogApplications_dbo.Logs_LogId]
GO
ALTER TABLE [dbo].[LogMessages]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogApplications_ApplicationId] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[LogApplications] ([Id])
GO
ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogApplications_ApplicationId]
GO
ALTER TABLE [dbo].[LogMessages]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageSources_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[LogMessageSources] ([Id])
GO
ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageSources_SourceId]
GO
ALTER TABLE [dbo].[LogMessages]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageTypes_MessageTypeId] FOREIGN KEY([MessageTypeId])
REFERENCES [dbo].[LogMessageTypes] ([Id])
GO
ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageTypes_MessageTypeId]
GO
ALTER TABLE [dbo].[LogMessages]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.Logs_LogId] FOREIGN KEY([LogId])
REFERENCES [dbo].[Logs] ([Id])
GO
ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.Logs_LogId]
GO
ALTER TABLE [dbo].[LogMessages]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[LogUsers] ([Id])
GO
ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogUsers_UserId]
GO
ALTER TABLE [dbo].[LogMessages]  WITH NOCHECK ADD  CONSTRAINT [FK_LogMessages_LogMessageDetail] FOREIGN KEY([DetailId])
REFERENCES [dbo].[LogMessageDetail] ([Id])
GO
ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_LogMessages_LogMessageDetail]
GO
