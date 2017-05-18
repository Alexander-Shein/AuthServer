CREATE TABLE [dbo].[ApiScope](
	[Id]						[UNIQUEIDENTIFIER]	NOT NULL,
	[ApiResourceId]				[UNIQUEIDENTIFIER]	NOT NULL,
	[Description]				[NVARCHAR](1000)	NOT NULL DEFAULT(''),
	[DisplayName]				[NVARCHAR](200)		NOT NULL DEFAULT(''),
	[Emphasize]					[BIT]				NOT NULL DEFAULT(0),
	[IsEnabled]					[BIT]				NOT NULL DEFAULT(1),
	[Name]						[NVARCHAR](200)		NOT NULL DEFAULT(''),
	[IsRequired]				[BIT]				NOT NULL DEFAULT(0),
	[ShowInDiscoveryDocument]	[BIT]				NOT NULL DEFAULT(1),
	[Ts]						[ROWVERSION]		NOT NULL,
	CONSTRAINT [PK_ApiScope_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [IDX_ApiScope_Name_U_N] UNIQUE NONCLUSTERED ([Name] ASC),
	CONSTRAINT [FK_ApiScope_ApiResourceId_ApiResource_Id] FOREIGN KEY([ApiResourceId]) REFERENCES [dbo].[ApiResource] ([Id])
);