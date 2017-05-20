CREATE TABLE [dbo].[UserRole]
(
	[UserId]		UNIQUEIDENTIFIER NOT NULL,
	[RoleId]		UNIQUEIDENTIFIER NOT NULL,
	[ClientId]		UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_UserRole_Id] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC, [ClientId] ASC),
	CONSTRAINT [FK_UserRole_UserId_User_Id] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT [FK_UserRole_RoleId_Role_Id] FOREIGN KEY([RoleId]) REFERENCES [dbo].[Role] ([Id]),
	CONSTRAINT [FK_UserRole_ClientId_Client_Id] FOREIGN KEY([ClientId]) REFERENCES [dbo].[Client] ([Id])
)
