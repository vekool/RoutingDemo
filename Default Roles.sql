USE [SampleContext]
GO

IF NOT EXISTS (SELECT * FROM [AspNetRoles] WHERE [Name] = 'Admin')
BEGIN
    INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (NEWID(), 'Admin', 'ADMIN', NEWID());
END
GO

IF NOT EXISTS (SELECT * FROM [AspNetRoles] WHERE [Name] = 'StandardUser')
BEGIN
    INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (NEWID(), 'StandardUser', 'STANDARDUSER', NEWID());
END
GO