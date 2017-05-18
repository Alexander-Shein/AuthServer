CREATE TABLE [dbo].[ApiScopeClaim] (
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[ApiScopeId]			[UNIQUEIDENTIFIER]	NOT NULL,
	[IdentityClaimId]		[UNIQUEIDENTIFIER]	NOT NULL,
	CONSTRAINT [PK_ApiScopeClaim_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_ApiScopeClaim_ApiScopeId_ApiScope_Id] FOREIGN KEY([ApiScopeId]) REFERENCES [dbo].[ApiScope] ([Id]),
	CONSTRAINT [FK_ApiScopeClaim_IdentityClaimId_IdentityClaim_Id] FOREIGN KEY([IdentityClaimId]) REFERENCES [dbo].[IdentityClaim] ([Id])
);