
CREATE TABLE [dbo].[Person](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
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