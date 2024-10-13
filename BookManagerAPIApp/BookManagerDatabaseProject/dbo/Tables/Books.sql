CREATE TABLE [dbo].[Books] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50)  NOT NULL,
    [PurchasedDate] DATE           NULL,
    [Price]         REAL           NULL,
    [ImageBlobURL]  NVARCHAR (MAX) NULL,
    [CategoryId]    INT            NOT NULL,
    [LanguageId] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Books_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]), 
    CONSTRAINT [FK_Books_Language] FOREIGN KEY (LanguageId) REFERENCES [dbo].[Language] ([Id])
);

