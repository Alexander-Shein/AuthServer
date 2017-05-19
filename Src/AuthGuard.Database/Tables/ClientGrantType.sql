CREATE TABLE [dbo].[ClientGrantType]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[ClientId]				[UNIQUEIDENTIFIER]	NOT NULL,
	[GrantTypeId]			[INT]				NOT NULL,
	CONSTRAINT [PK_ClientGrantType_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_ClientGrantType_ClientId_Client_Id] FOREIGN KEY([ClientId]) REFERENCES [dbo].[Client] ([Id]),
	CONSTRAINT [FK_ClientGrantType_GrantTypeId_GrantType_Id] FOREIGN KEY([GrantTypeId]) REFERENCES [dbo].[GrantType] ([Id]),
	CONSTRAINT [IDX_ClientGrantType_ClientId_GrantTypeId_U_N] UNIQUE NONCLUSTERED ([ClientId] ASC, [GrantTypeId] ASC)
)