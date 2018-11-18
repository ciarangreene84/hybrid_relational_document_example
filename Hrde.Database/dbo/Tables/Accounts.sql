CREATE TABLE [dbo].[Accounts] (
    [AccountId]      INT           IDENTITY (1, 1) NOT NULL,
    [Type]    VARCHAR (128) NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([AccountId] ASC),
    CONSTRAINT [CK_Accounts_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Accounts_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [IX_Accounts_AccountId] UNIQUE NONCLUSTERED ([AccountId] ASC),
    CONSTRAINT [UQ_Accounts_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

