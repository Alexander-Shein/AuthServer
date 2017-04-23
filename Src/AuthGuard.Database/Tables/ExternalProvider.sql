CREATE TABLE [dbo].[ExternalProvider]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[DisplayName]			[NVARCHAR](100)		NOT NULL DEFAULT(''),
	[AuthenticationScheme]	[VARCHAR](100)		NOT NULL DEFAULT(''),
	CONSTRAINT [PK_ExternalProvider_Id] PRIMARY KEY CLUSTERED([Id] ASC)
)