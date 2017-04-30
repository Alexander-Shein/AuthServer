CREATE TABLE [dbo].[SecurityCode]
(
	[Id]									[UNIQUEIDENTIFIER]	NOT NULL,
	[UserId]								[NVARCHAR](450)		NULL,
	[SecurityCodeActionId]					[INT]				NOT NULL,
	[Code]									[INT]				NOT NULL DEFAULT(0),
	[ExpiredAt]								[DATETIME2](7)		NOT NULL DEFAULT(GETDATE()),

	CONSTRAINT [PK_SecurityCode_Id] PRIMARY KEY CLUSTERED([Id] ASC),
	CONSTRAINT [FK_SecurityCode_UserId_User_Id] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_SecurityCode_SecurityCodeActionId_SecurityCodeAction_Id] FOREIGN KEY([SecurityCodeActionId]) REFERENCES [dbo].[SecurityCodeAction] ([Id])
)