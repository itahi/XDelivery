
ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,DiaSemana,PrecoDesconto
	FROM Produto WHERE  AtivoSN= 1 and Codigo = @Codigo;
