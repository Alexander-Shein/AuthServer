CREATE TABLE [dbo].[Email]
(
	[Id]				[UNIQUEIDENTIFIER]	NOT NULL,
	[EmailTemplateId]	[UNIQUEIDENTIFIER]	NOT NULL,
	[ToEmail]			[NVARCHAR](250)		NOT NULL DEFAULT(''),
	[FromEmail]			[NVARCHAR](250)		NOT NULL DEFAULT(''),
	[FromName]			[NVARCHAR](250)		NOT NULL DEFAULT(''),
	[Subject]			[NVARCHAR](100)		NOT NULL DEFAULT(''),
	[Body]				[NVARCHAR](MAX)		NOT NULL DEFAULT(''),
	[IsSent]			[BIT]				NOT NULL DEFAULT(0),
	[CreatedAt]			[datetime2](7)		NOT NULL DEFAULT(GETDATE()),
	CONSTRAINT [PK_Email_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_Email_EmailTemplateId_EmailTemplate_Id] FOREIGN KEY([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id])
)
