CREATE TABLE [dbo].[Client]
(
	[Id]									[UNIQUEIDENTIFIER]	NOT NULL,
	[OwnerUserId]							[UNIQUEIDENTIFIER]	NOT NULL,
	[DisplayName]							[NVARCHAR](100)		NOT NULL,
	[Key]									[NVARCHAR](100)		NOT NULL DEFAULT(''),
	[WebsiteUrl]							[NVARCHAR](2000)	NOT NULL DEFAULT(''),
	[IsLocalAccountEnabled]					[BIT]				NOT NULL DEFAULT(0),
	[IsRememberLogInEnabled]				[BIT]				NOT NULL DEFAULT(0),
	[IsSecurityQuestionsEnabled]			[BIT]				NOT NULL DEFAULT(0),
	[IsActive]								[BIT]				NOT NULL DEFAULT(0),
	[UsersCount]							[INT]				NOT NULL DEFAULT(0),
	[CreatedAt]								[datetime2](7)		NOT NULL DEFAULT(GETDATE()),

	[Secret]								[NVARCHAR](2000)	NOT NULL DEFAULT(''),
	[DefaultRedirectUrl]					[NVARCHAR](2000)	NOT NULL DEFAULT(''),
	[DefaultLogOutRedirectUrl]				[NVARCHAR](2000)	NOT NULL DEFAULT(''),

	[IsEmailEnabled]						[BIT]				NOT NULL DEFAULT(0),
	[IsEmailPasswordlessEnabled]			[BIT]				NOT NULL DEFAULT(0),
	[IsEmailPasswordEnabled]				[BIT]				NOT NULL DEFAULT(0),
	[IsEmailConfirmationRequired]			[BIT]				NOT NULL DEFAULT(0),
	[IsEmailSearchRelatedProviderEnabled]	[BIT]				NOT NULL DEFAULT(0),

	[IsPhoneEnabled]						[BIT]				NOT NULL DEFAULT(0),
	[IsPhonePasswordlessEnabled]			[BIT]				NOT NULL DEFAULT(0),
	[IsPhonePasswordEnabled]				[BIT]				NOT NULL DEFAULT(0),
	[IsPhoneConfirmationRequired]			[BIT]				NOT NULL DEFAULT(0),

	CONSTRAINT [PK_Client_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_Client_UserId_User_Id] FOREIGN KEY([OwnerUserId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT [IDX_Client_Key_U_N] UNIQUE NONCLUSTERED ([Key] ASC)
);