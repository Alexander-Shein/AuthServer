CREATE TABLE [dbo].[UserToken]
(
	[UserId]			UNIQUEIDENTIFIER	NOT NULL,
	[LoginProvider]		NVARCHAR(450)		NOT NULL DEFAULT(''),
	[Name]				NVARCHAR(450)		NOT NULL DEFAULT(''),
	[Value]				NVARCHAR(MAX)		NULL,
	CONSTRAINT [PK_UserToken_Id] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC),
	CONSTRAINT [FK_UserToken_UserId_User_Id] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])
)