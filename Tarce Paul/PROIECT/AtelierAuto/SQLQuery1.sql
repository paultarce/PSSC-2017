CREATE TABLE [dbo].[Comanda]
(
	[Id] INT NOT NULL IDENTITY ,
    [TipEveniment] VARCHAR(MAX) NULL, 
    [DetaliiEveniment] VARCHAR(MAX) NULL, 
    [IdRadacina] VARCHAR(MAX) NULL,
	PRIMARY KEY ([Id])
);