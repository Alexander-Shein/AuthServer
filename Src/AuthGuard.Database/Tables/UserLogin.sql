CREATE TABLE [dbo].[UserLogin]
(
	[UserId]				UNIQUEIDENTIFIER NOT NULL,
	[LoginProvider]			NVARCHAR(450) NOT NULL,
	[ProviderKey]			NVARCHAR(450) NOT NULL,
	[ProviderDisplayName]	NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_UserLogin_Id] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
	CONSTRAINT [FK_UserLogin_UserId_User_Id] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])
)