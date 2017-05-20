CREATE TABLE [dbo].[TokenExpirationType]
(
	[Id]	[INT]				NOT NULL IDENTITY(1,1),
	[Name]	[NVARCHAR](100)		NOT NULL DEFAULT(''),
	CONSTRAINT [PK_TokenExpirationType_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [IDX_TokenExpirationType_Name_U_N] UNIQUE NONCLUSTERED ([Name] ASC)
)
