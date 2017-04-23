CREATE TABLE [dbo].[EmailTemplate]
(
	[Id]				[UNIQUEIDENTIFIER]	NOT NULL,
	[TemplateId]		[INT]				NOT NULL,
	[EmailBodyFormatId] [INT]				NOT NULL DEFAULT(1),
	[FromNameTemplate]	[NVARCHAR](250)		NOT NULL DEFAULT(''),
	[SubjectTemplate]	[NVARCHAR](100)		NOT NULL DEFAULT(''),
	[BodyTemplate]		[NVARCHAR](MAX)		NOT NULL DEFAULT(''),
	[FromEmail]			[NVARCHAR](100)		NOT NULL DEFAULT(''),
	[IsActive]			[BIT]				NOT NULL DEFAULT(1),
	CONSTRAINT [PK_EmailTemplate_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_EmailTemplate_TemplateId_Template_Id] FOREIGN KEY([TemplateId]) REFERENCES [dbo].[Template] ([Id]),
	CONSTRAINT [FK_EmailTemplate_EmailBodyFormatId_EmailBodyFormat_Id] FOREIGN KEY([EmailBodyFormatId]) REFERENCES [dbo].[EmailBodyFormat] ([Id])
)