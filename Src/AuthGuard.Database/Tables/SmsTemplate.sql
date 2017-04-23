CREATE TABLE [dbo].[SmsTemplate]
(
	[Id]				[UNIQUEIDENTIFIER]	NOT NULL,
	[TemplateId]		[INT]				NOT NULL,
	[MessageTemplate]	[NVARCHAR](MAX)		NOT NULL DEFAULT(''),
	[FromPhoneNumber]	[VARCHAR](15)		NOT NULL DEFAULT(''),
	[IsActive]			[BIT]				NOT NULL DEFAULT(1),
	CONSTRAINT [PK_SmsTemplate_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_SmsTemplate_TemplateId_Template_Id] FOREIGN KEY([TemplateId]) REFERENCES [dbo].[Template] ([Id])
)