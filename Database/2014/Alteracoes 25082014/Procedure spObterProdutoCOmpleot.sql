
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana
	FROM Produto WHERE Codigo = @Codigo;


