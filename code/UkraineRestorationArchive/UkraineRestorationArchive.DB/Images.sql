CREATE TABLE [dbo].[Images] (
    [Id]       INT        IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR(900) NOT NULL,
    [Image]    IMAGE        NOT NULL,
    [Address] NVARCHAR(MAX) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Scores_Users] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username])
);
