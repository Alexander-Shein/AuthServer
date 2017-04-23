CREATE TABLE [dbo].[Sms]
(
	[Id]				[UNIQUEIDENTIFIER]	NOT NULL,
	[SmsTemplateId]		[UNIQUEIDENTIFIER]	NOT NULL,
	[ToPhoneNumber]		[NVARCHAR](15)		NOT NULL DEFAULT(''),
	[FromPhoneNumber]	[NVARCHAR](15)		NOT NULL DEFAULT(''),
	[Message]			[NVARCHAR](MAX)		NOT NULL DEFAULT(''),
	[IsSent]			[BIT]				NOT NULL DEFAULT(0),
	[CreatedAt]			[datetime2](7)		NOT NULL DEFAULT(GETDATE()),
	CONSTRAINT [PK_Sms_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_Sms_SmsTemplateId_SmsTemplate_Id] FOREIGN KEY([SmsTemplateId]) REFERENCES [dbo].[SmsTemplate] ([Id])
)