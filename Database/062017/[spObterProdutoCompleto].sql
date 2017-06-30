
ALTER PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int,
	@AtivoSN bit
as
begin
	SELECT Codigo,NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana,CodGrupo,CodigoPersonalizado,Markup ,
	PrecoSugerido ,PrecoCusto ,PontoFidelidadeVenda,PontoFidelidadeTroca,ControlaEstoque,EstoqueMinimo,PalavrasChaves
	
	FROM Produto WHERE AtivoSN= @AtivoSN and Codigo = @Codigo 
end

