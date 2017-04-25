CREATE TABLE [dbo].[AppExternalProvider]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[AppId]					[UNIQUEIDENTIFIER]	NOT NULL,
	[ExternalProviderId]	[UNIQUEIDENTIFIER]	NOT NULL,
	CONSTRAINT [PK_AppExternalProvider_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_AppExternalProvider_AppId_App_Id] FOREIGN KEY([AppId]) REFERENCES [dbo].[App] ([Id]),
	CONSTRAINT [FK_AppExternalProvider_ExternalProviderId_ExternalProvider_Id] FOREIGN KEY([ExternalProviderId]) REFERENCES [dbo].[ExternalProvider] ([Id])
)