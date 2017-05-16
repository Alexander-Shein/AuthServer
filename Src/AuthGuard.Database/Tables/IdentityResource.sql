CREATE TABLE [dbo].[IdentityResource](
	[Id]						[UNIQUEIDENTIFIER]	NOT NULL,
	[Description]				[NVARCHAR](1000)	NOT NULL DEFAULT(''),
	[DisplayName]				[NVARCHAR](200)		NOT NULL DEFAULT(''),
	[Emphasize]					[BIT]				NOT NULL DEFAULT(0),
	[IsEnabled]					[BIT]				NOT NULL DEFAULT(1),
	[Name]						[VARCHAR](200)		NOT NULL DEFAULT(''),
	[IsRequired]				[BIT]				NOT NULL DEFAULT(0),
	[ShowInDiscoveryDocument]	[BIT]				NOT NULL DEFAULT(1),
	[IsReadOnly]				[BIT]				NOT NULL DEFAULT(0),
	[Ts]						[ROWVERSION]		NOT NULL,
	CONSTRAINT [PK_IdentityResource_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [IDX_IdentityResource_Name_U_N] UNIQUE NONCLUSTERED ([Name] ASC)
);