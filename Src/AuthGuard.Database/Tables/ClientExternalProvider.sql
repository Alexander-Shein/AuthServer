CREATE TABLE [dbo].[ClientExternalProvider]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[ClientId]				[UNIQUEIDENTIFIER]	NOT NULL,
	[ExternalProviderId]	[UNIQUEIDENTIFIER]	NOT NULL,
	CONSTRAINT [PK_ClientExternalProvider_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_ClientExternalProvider_ClientId_Client_Id] FOREIGN KEY([ClientId]) REFERENCES [dbo].[Client] ([Id]),
	CONSTRAINT [FK_ClientExternalProvider_ExternalProviderId_ExternalProvider_Id] FOREIGN KEY([ExternalProviderId]) REFERENCES [dbo].[ExternalProvider] ([Id]),
	CONSTRAINT [IDX_ClientExternalProvider_ClientId_ExternalProviderId_U_N] UNIQUE NONCLUSTERED ([ClientId] ASC, [ExternalProviderId] ASC)
);