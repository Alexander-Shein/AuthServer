CREATE TABLE [dbo].[ExternalProviderSetting]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[ExternalProviderId]	[UNIQUEIDENTIFIER]	NULL,
	[AppExternalProviderId]	[UNIQUEIDENTIFIER]	NULL,
	[Key]					[VARCHAR](100)		NOT NULL DEFAULT(''),
	[Value]					[VARCHAR](MAX)		NOT NULL DEFAULT(''),
	CONSTRAINT [PK_ExternalProviderSetting_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_ExternalProviderSetting_ExternalProviderId_ExternalProvider_Id] FOREIGN KEY([ExternalProviderId]) REFERENCES [dbo].[ExternalProvider] ([Id]),
	CONSTRAINT [FK_ExternalProviderSetting_AppExternalProviderId_AppExternalProvider_Id] FOREIGN KEY([AppExternalProviderId]) REFERENCES [dbo].[AppExternalProvider] ([Id])
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_ExternalProviderSetting_ExternalProviderId_AppExternalProviderId_Key_U_N] ON [dbo].[ExternalProviderSetting]([ExternalProviderId] ASC, [AppExternalProviderId] ASC, [Key] ASC);