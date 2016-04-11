/****** Object:  Table [dbo].[LogApplications]    Script Date: 3/27/2016 7:59:23 PM ******/
CREATE TABLE [dbo].[LogApplications](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[LogId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.LogApplications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[LogMessageDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ContentKey] [nvarchar](128) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_LogMessageDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[LogMessages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ElmahId] [nvarchar](1000) NULL,
	[DateTime] [datetime] NOT NULL,
	[Detail] [nvarchar](max) NULL,
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
 CONSTRAINT [PK_dbo.LogMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


CREATE TABLE [dbo].[LogMessageSources](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
 CONSTRAINT [PK_dbo.LogMessageSources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[LogMessageTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Icon] [varchar](20) NULL CONSTRAINT [DF_LogMessageTypes_Icon]  DEFAULT ('cloud'),
	[Color] [varchar](10),
 CONSTRAINT [PK_dbo.LogMessageTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[Logs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[LogId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[LogUsers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
 CONSTRAINT [PK_dbo.LogUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];



/****** Object:  Index [IX_LogId]    Script Date: 3/27/2016 7:59:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_LogId] ON [dbo].[LogApplications]
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_ApplicationId] ON [dbo].[LogMessages]
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_LogId] ON [dbo].[LogMessages]
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IX_MessageTypeId] ON [dbo].[LogMessages]([MessageTypeId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
CREATE NONCLUSTERED INDEX [IX_SourceId] ON [dbo].[LogMessages] ([SourceId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[LogMessages] ([UserId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];

ALTER TABLE [dbo].[LogApplications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LogApplications_dbo.Logs_LogId] FOREIGN KEY([LogId]) REFERENCES [dbo].[Logs] ([Id]);
ALTER TABLE [dbo].[LogMessages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogApplications_ApplicationId] FOREIGN KEY([ApplicationId]) REFERENCES [dbo].[LogApplications] ([Id]);
ALTER TABLE [dbo].[LogMessages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageSources_SourceId] FOREIGN KEY([SourceId]) REFERENCES [dbo].[LogMessageSources] ([Id]);
ALTER TABLE [dbo].[LogMessages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageTypes_MessageTypeId] FOREIGN KEY([MessageTypeId]) REFERENCES [dbo].[LogMessageTypes] ([Id]);
ALTER TABLE [dbo].[LogMessages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.Logs_LogId] FOREIGN KEY([LogId]) REFERENCES [dbo].[Logs] ([Id]);
ALTER TABLE [dbo].[LogMessages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LogMessages_dbo.LogUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[LogUsers] ([Id]);
ALTER TABLE [dbo].[LogMessages]  WITH CHECK ADD  CONSTRAINT [FK_LogMessages_LogMessageDetail] FOREIGN KEY([DetailId]) REFERENCES [dbo].[LogMessageDetail] ([Id]);

--ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogApplications_ApplicationId];
--ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageSources_SourceId];
--ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogMessageTypes_MessageTypeId];
--ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.Logs_LogId];
--ALTER TABLE [dbo].[LogApplications] CHECK CONSTRAINT [FK_dbo.LogApplications_dbo.Logs_LogId];
--ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_dbo.LogMessages_dbo.LogUsers_UserId];
--ALTER TABLE [dbo].[LogMessages] CHECK CONSTRAINT [FK_LogMessages_LogMessageDetail];