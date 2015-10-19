
ALTER PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int,
	@AtivoSN bit
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana
	
	FROM Produto WHERE AtivoSN= @AtivoSN and Codigo = @Codigo ;


