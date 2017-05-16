CREATE TABLE [dbo].[IdentityResourceClaim] (
	[Id]					[UNIQUEIDENTIFIER]	NOT NULL,
	[IdentityResourceId]	[UNIQUEIDENTIFIER]	NOT NULL,
	[IdentityClaimId]		[UNIQUEIDENTIFIER]	NOT NULL,
	CONSTRAINT [PK_IdentityResourceClaim_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_IdentityResourceClaim_IdentityResourceId_IdentityResource_Id] FOREIGN KEY([IdentityResourceId]) REFERENCES [dbo].[IdentityResource] ([Id]),
	CONSTRAINT [FK_IdentityResourceClaim_IdentityClaimId_IdentityClaim_Id] FOREIGN KEY([IdentityClaimId]) REFERENCES [dbo].[IdentityClaim] ([Id])
);