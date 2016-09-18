drop table [NetworkIpAddress]
drop table [UserAccess]
drop table [session];
drop table [Role];
drop table [Site];
drop table [LoginProfiles]
drop table loginKeys;
drop table person;

CREATE TABLE [dbo].[Person](
	[PersonId] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](256) NULL,
	[Username] [varchar](256) NULL,
	[Password] [nvarchar](128) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[PasswordSalt] [nvarchar](128) NULL,
	[AccountStatus] [char](1) NOT NULL,
	[Authentication] [char](1) NOT NULL,	
	[PasswordFormat] [int] NOT NULL,
	[PasswordDate] [datetime2] NULL,
	[PasswordReset] [bit] NOT NULL,
	[PasswordCount] [tinyint] NOT NULL,
	[LockoutCount] [tinyint] NOT NULL,
	[LoginDate] [datetime2] NULL,
	[CreationDate] [datetime2] NOT NULL,
	[ModificationDate] [datetime2] NOT NULL,
	[Source] [varchar](50) NULL,
	[SourceKey] [varchar](50) NULL
 CONSTRAINT [PK_Person_PersonId] PRIMARY KEY CLUSTERED ([PersonId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_AccountStatus]  DEFAULT ('N') FOR [AccountStatus];
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_Authentication]  DEFAULT ('D') FOR [Authentication];
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_PasswordFormat]  DEFAULT ((0)) FOR [PasswordFormat];
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_ResetPassword]  DEFAULT ((0)) FOR [PasswordReset];
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_PasswordCount]  DEFAULT ((0)) FOR [PasswordCount];
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_LockoutCount]  DEFAULT ((0)) FOR [LockoutCount];
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_CreationDate]  DEFAULT (getdate()) FOR [CreationDate];
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_ModificationDate]  DEFAULT (getdate()) FOR [ModificationDate];

GO

CREATE TABLE [dbo].[LoginKeys](
	[PersonId] [bigint] NOT NULL,
	[ProviderName] [varchar](10) NOT NULL,
	[ProfileKey] [nvarchar](128) NOT NULL,
	[SessionId] [bigint] NULL,
	[Token] [nvarchar](1000) NULL,
 CONSTRAINT [PK_LoginKeys] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC,
	[ProviderName] ASC,
	[ProfileKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[LoginKeys]  WITH CHECK ADD  CONSTRAINT [FK_LoginKeys_Person] FOREIGN KEY([PersonId]) REFERENCES [dbo].[Person] ([PersonId]) ON DELETE CASCADE ;


CREATE TABLE [dbo].[LoginProfiles](
	[PersonId] [bigint] NOT NULL,
	[ProviderName] [varchar](10) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Created] [datetime] NOT NULL CONSTRAINT [DF_LoginProfiles_Created]  DEFAULT (getdate()),
	[LoginDate] [datetime] NULL,
	[LoginCount] [int] NOT NULL CONSTRAINT [DF_LoginProfiles_LoginCount]  DEFAULT ((0)),
	[Url] [varchar](128) NULL,
	[Picture] [varchar](128) NULL,
 CONSTRAINT [PK_LoginProfiles] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC,
	[ProviderName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[LoginProfiles]  WITH CHECK ADD  CONSTRAINT [FK_LoginProfiles_Person] FOREIGN KEY([PersonId]) REFERENCES [dbo].[Person] ([PersonId]) ON DELETE CASCADE;



CREATE TABLE [dbo].[Site](
	[SiteId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Description] [varchar](200) NULL,
	[Url] [varchar](50) NULL
 CONSTRAINT [PK_Site] PRIMARY KEY NONCLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];
GO


CREATE TABLE [dbo].[Session](
	[SessionId] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [uniqueidentifier] NOT NULL,
	[Start] [datetime2] NOT NULL,
	[End] [datetime2] NOT NULL,
	[SiteId] [int] NULL,
	[UserId] [bigint] NULL,
	[Referrer] [varchar](260) NULL,
	[Querystring] [varchar](256) NULL,
	[Domain] [varchar](100) NULL,
	[ProviderName] [varchar](10) NULL,
	[IpAddressId] [bigint] NULL,
	[Data] [varchar](max) NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_Key]  DEFAULT (newid()) FOR [Key];
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_Start]  DEFAULT (getdate()) FOR [Start];
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_End]  DEFAULT (getdate()) FOR [End];
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Site] FOREIGN KEY([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Person] ([PersonId]);


CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Title] [varchar](250) NULL,
	[Description] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


CREATE TABLE [dbo].[UserAccess](
	[UserId] [bigint] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED (	[UserId] ASC,	[RoleId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];

ALTER TABLE [dbo].[UserAccess]  WITH CHECK ADD  CONSTRAINT [FK_User_Access_Role] FOREIGN KEY([RoleId]) REFERENCES [dbo].[Role] ([RoleId]);
ALTER TABLE [dbo].[UserAccess]  WITH CHECK ADD  CONSTRAINT [FK_User_Access_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[Person] ([PersonId]) ON DELETE CASCADE;

CREATE TABLE [dbo].[NetworkIpAddress](
	[IpAddressId] [bigint] NOT NULL,
	[IpAddress] [varchar](15) NOT NULL,
	[CountryCode] [char](2) NULL,
	[RegionCode] [char](10) NULL,
	[City] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[Latitude] [decimal](7, 5) NOT NULL,
	[Longitude] [decimal](8, 5) NOT NULL,
	[FraudCount] [int] NOT NULL,
 CONSTRAINT [PK_NetworkIpAddress] PRIMARY KEY CLUSTERED 
(	[IpAddressId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];
	
ALTER TABLE [dbo].[NetworkIpAddress] ADD  CONSTRAINT [DF_NetworkIpAddress_Latitude]  DEFAULT ((0)) FOR [Latitude];
ALTER TABLE [dbo].[NetworkIpAddress] ADD  CONSTRAINT [DF_NetworkIpAddress_Longitude]  DEFAULT ((0)) FOR [Longitude];
ALTER TABLE [dbo].[NetworkIpAddress] ADD  CONSTRAINT [DF_NetworkIpAddress_FraudCount]  DEFAULT ((0)) FOR [FraudCount];