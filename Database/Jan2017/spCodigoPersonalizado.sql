
create PROCEDURE [dbo].[spObterProdutoCompletoPorCodPersonalizado]
	@Codigo int,
	@AtivoSN bit
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana,CodGrupo,CodigoPersonalizado
	
	FROM Produto WHERE AtivoSN= @AtivoSN and CodigoPersonalizado = @Codigo ;
