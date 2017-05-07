CREATE TABLE [dbo].[ExternalProvider]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[DisplayName]			[NVARCHAR](100)		NOT NULL DEFAULT(''),
	[AuthenticationScheme]	[VARCHAR](100)		NOT NULL DEFAULT(''),
	[IsSearchable]			[BIT]				NOT NULL DEFAULT(0),
	[Patterns]				[NVARCHAR](MAX)		NOT NULL DEFAULT(''),
	CONSTRAINT [PK_ExternalProvider_Id] PRIMARY KEY CLUSTERED([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_ExternalProvider_DisplayName_U_N] ON [dbo].[ExternalProvider]([DisplayName] ASC);