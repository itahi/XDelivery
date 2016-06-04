
CREATE TABLE [dbo].[Produto_Estoque](
	[CodProduto] [int] NOT NULL,
	[Quantidade] [decimal](10, 2) NULL,
	[DataAtualizacao] [date] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Produto_Estoque]  WITH CHECK ADD  CONSTRAINT [FK_O1CODPRODUTOESTOQUE] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO

ALTER TABLE [dbo].[Produto_Estoque] CHECK CONSTRAINT [FK_O1CODPRODUTOESTOQUE]
GO


