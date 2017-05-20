CREATE TABLE [dbo].[Client]
(
	[Id]									UNIQUEIDENTIFIER	NOT NULL,
	[OwnerUserId]							UNIQUEIDENTIFIER	NOT NULL,
	[DisplayName]							NVARCHAR(200)		NOT NULL,
	[Name]									NVARCHAR(200)		NOT NULL DEFAULT(''),
	[ClientUri]								NVARCHAR(2000)		NOT NULL DEFAULT(''),
	[IsLocalAccountEnabled]					BIT					NOT NULL DEFAULT(0),
	[IsRememberLogInEnabled]				BIT					NOT NULL DEFAULT(0),
	[IsSecurityQuestionsEnabled]			BIT					NOT NULL DEFAULT(0),
	[IsEnabled]								BIT					NOT NULL DEFAULT(0),
	[UsersCount]							INT					NOT NULL DEFAULT(0),
	[CreatedAt]								DATETIME2(7)		NOT NULL DEFAULT(GETDATE()),

	[Secret]								NVARCHAR(2000)		NOT NULL DEFAULT(''),
	[DefaultRedirectUrl]					NVARCHAR(2000)		NOT NULL DEFAULT(''),
	[DefaultLogOutRedirectUrl]				NVARCHAR(2000)		NOT NULL DEFAULT(''),
	[AccessTokenTypeId]						INT					NOT NULL DEFAULT(1),
	[AllowAccessTokensViaBrowser]			BIT					NOT NULL DEFAULT(1),
	[AllowOfflineAccess]					BIT					NOT NULL DEFAULT(1),

	[IsEmailEnabled]						BIT					NOT NULL DEFAULT(0),
	[IsEmailPasswordlessEnabled]			BIT					NOT NULL DEFAULT(0),
	[IsEmailPasswordEnabled]				BIT					NOT NULL DEFAULT(0),
	[IsEmailConfirmationRequired]			BIT					NOT NULL DEFAULT(0),
	[IsEmailSearchRelatedProviderEnabled]	BIT					NOT NULL DEFAULT(0),

	[IsPhoneEnabled]						BIT					NOT NULL DEFAULT(0),
	[IsPhonePasswordlessEnabled]			BIT					NOT NULL DEFAULT(0),
	[IsPhonePasswordEnabled]				BIT					NOT NULL DEFAULT(0),
	[IsPhoneConfirmationRequired]			BIT					NOT NULL DEFAULT(0),

	[AbsoluteRefreshTokenLifetime]			INT					NOT NULL DEFAULT(2592000),
	[SlidingRefreshTokenLifetime]			INT					NOT NULL DEFAULT(1296000),
	[AccessTokenLifetime]					INT					NOT NULL DEFAULT(3600),
	[AuthorizationCodeLifetime]				INT					NOT NULL DEFAULT(300),
	[IdentityTokenLifetime]					INT					NOT NULL DEFAULT(300),
	[RefreshTokenExpirationTypeId]			INT					NOT NULL DEFAULT(1),

	[LogoUri]								NVARCHAR(MAX)		NOT NULL DEFAULT(''),

	[ProtocolType]							NVARCHAR(200)		NOT NULL DEFAULT('oidc'),
	[RefreshTokenUsage]						INT					NOT NULL DEFAULT(1),
	[IsConsentRequired]						BIT					NOT NULL DEFAULT(1),

	CONSTRAINT [PK_Client_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_Client_UserId_User_Id] FOREIGN KEY([OwnerUserId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT [FK_Client_RefreshTokenExpirationTypeId_TokenExpirationType_Id] FOREIGN KEY([RefreshTokenExpirationTypeId]) REFERENCES [dbo].[TokenExpirationType] ([Id]),
	CONSTRAINT [IDX_Client_Name_U_N] UNIQUE NONCLUSTERED ([Name] ASC)
);