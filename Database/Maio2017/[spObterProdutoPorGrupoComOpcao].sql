create PROCEDURE [dbo].[spObterProdutoPorGrupoComOpcao]
	@Codigo int
as
	SELECT 
	P.Codigo,
	P.NomeProduto +' ' +O.Nome as Nome,
	P.PrecoProduto
	FROM Produto P
	left join Produto_Opcao PO on PO.CodProduto = P.Codigo
	left join Opcao O on O.Codigo = PO.CodOpcao
	left join Produto_OpcaoTipo POT on POT.Codigo = PO.CodTipo
	 WHERE P.AtivoSN= 1  and POT.Tipo=1 and CODGRUPO = @Codigo




