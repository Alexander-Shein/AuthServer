CREATE TABLE [dbo].[ExternalProviderSetting]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[ExternalProviderId]	[UNIQUEIDENTIFIER]	NULL,
	[ClientExternalProviderId]	[UNIQUEIDENTIFIER]	NULL,
	[Key]					[VARCHAR](100)		NOT NULL DEFAULT(''),
	[Value]					[VARCHAR](MAX)		NOT NULL DEFAULT(''),
	CONSTRAINT [PK_ExternalProviderSetting_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_ExternalProviderSetting_ExternalProviderId_ExternalProvider_Id] FOREIGN KEY([ExternalProviderId]) REFERENCES [dbo].[ExternalProvider] ([Id]),
	CONSTRAINT [FK_ExternalProviderSetting_ClientExternalProviderId_ClientExternalProvider_Id] FOREIGN KEY([ClientExternalProviderId]) REFERENCES [dbo].[ClientExternalProvider] ([Id]),
	CONSTRAINT [IDX_ExternalProviderSetting_ExternalProviderId_AppExternalProviderId_Key_U_N] UNIQUE NONCLUSTERED ([ExternalProviderId] ASC, [ClientExternalProviderId] ASC, [Key] ASC)
);