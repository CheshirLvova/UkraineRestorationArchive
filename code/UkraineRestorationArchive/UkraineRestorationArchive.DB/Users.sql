CREATE TABLE [dbo].[Users] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [Username]    NVARCHAR(900)  NOT NULL,
    [EmailAddres] NVARCHAR(MAX) NOT NULL,
    [Password]    NVARCHAR(MAX)  NOT NULL,
    [Type]        NCHAR (5)   NOT NULL DEFAULT 'User',
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username]),
    CHECK ([Type]='User' OR [Type]='Admin')
);

