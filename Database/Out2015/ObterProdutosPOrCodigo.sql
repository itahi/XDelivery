
ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int,
	@AtivoSN bit
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,DiaSemana,PrecoDesconto,OnlineSN,Codigo,MaximoAdicionais
	FROM Produto WHERE  AtivoSN= @AtivoSN and Codigo = @Codigo;
