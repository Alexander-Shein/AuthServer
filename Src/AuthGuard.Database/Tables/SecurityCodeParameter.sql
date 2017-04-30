CREATE TABLE [dbo].[SecurityCodeParameter]
(
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[SecurityCodeId]		[UNIQUEIDENTIFIER]	NOT NULL,
	[Name]					[VARCHAR](100)		NOT NULL DEFAULT(''),
	[Value]					[NVARCHAR](MAX)		NOT NULL DEFAULT(''),

	CONSTRAINT [PK_SecurityCodeParameter_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_SecurityCodeParameter_SecurityCodeId_SecurityCode_Id] FOREIGN KEY([SecurityCodeId]) REFERENCES [dbo].[SecurityCode] ([Id])
)
