CREATE TABLE [dbo].[User](
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[AccessFailedCount]		[INT]				NOT NULL DEFAULT(0),
	[ConcurrencyStamp]		[NVARCHAR](MAX)		NULL,
	[Email]					[NVARCHAR](256)		NULL,
	[EmailConfirmed]		[BIT]				NOT NULL DEFAULT(0),
	[LockoutEnabled]		[BIT]				NOT NULL DEFAULT(1),
	[LockoutEnd]			[DATETIMEOFFSET](7) NULL,
	[NormalizedEmail]		[NVARCHAR](256)		NULL,
	[NormalizedUserName]	[NVARCHAR](256)		NULL,
	[PasswordHash]			[NVARCHAR](MAX)		NULL,
	[PhoneNumber]			[NVARCHAR](MAX)		NULL,
	[PhoneNumberConfirmed]	[BIT]				NOT NULL DEFAULT(0),
	[SecurityStamp]			[NVARCHAR](MAX)		NULL,
	[TwoFactorEnabled]		[BIT]				NOT NULL DEFAULT(0),
	[UserName]				[NVARCHAR](256)		NOT NULL DEFAULT(''),
	CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_User_Email_U_N] ON [dbo].[User]([Email] ASC) WHERE [Email] IS NOT NULL;

GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_User_PhoneNumber_U_N] ON [dbo].[User]([PhoneNumber] ASC) WHERE [PhoneNumber] IS NOT NULL;