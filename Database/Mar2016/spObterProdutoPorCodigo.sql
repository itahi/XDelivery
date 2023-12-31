
ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int,
	@AtivoSN bit
as
	SELECT 
	NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,
	DiaSemana,PrecoDesconto,OnlineSN,Codigo,MaximoAdicionais,
	UrlImagem,DataInicioPromocao,DataFimPromocao,CodGrupo,AtivoSN
	FROM Produto WHERE  AtivoSN= @AtivoSN and Codigo = @Codigo;
