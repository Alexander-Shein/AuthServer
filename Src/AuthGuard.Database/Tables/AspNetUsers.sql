CREATE TABLE [dbo].[AspNetUsers](
	[Id]					[NVARCHAR](450)		NOT NULL,
	[AccessFailedCount]		[INT]				NOT NULL,
	[ConcurrencyStamp]		[NVARCHAR](max)		NULL,
	[Email]					[NVARCHAR](256)		NULL,
	[EmailConfirmed]		[BIT]				NOT NULL,
	[LockoutEnabled]		[BIT]				NOT NULL,
	[LockoutEnd]			[datetimeoffset](7) NULL,
	[NormalizedEmail]		[NVARCHAR](256)		NULL,
	[NormalizedUserName]	[NVARCHAR](256)		NULL,
	[PasswordHash]			[NVARCHAR](max)		NULL,
	[PhoneNumber]			[NVARCHAR](max)		NULL,
	[PhoneNumberConfirmed]	[BIT]				NOT NULL,
	[SecurityStamp]			[NVARCHAR](max)		NULL,
	[TwoFactorEnabled]		[BIT]				NOT NULL,
	[UserName]				[NVARCHAR](256)		NULL,
	CONSTRAINT [PK_AspNetUser_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
)