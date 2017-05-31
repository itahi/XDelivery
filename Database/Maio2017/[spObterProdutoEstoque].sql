drop procedure [spObterProdutoPorGrupoComOpcao]
go
create PROCEDURE [dbo].[spObterProdutoEstoque]
	@Codigo int
as
	SELECT 
	P.Codigo,
	P.NomeProduto,
	P.PrecoProduto
	FROM Produto P
	 WHERE P.AtivoSN= 1  and P.ControlaEstoque=1 and CODGRUPO = @Codigo




