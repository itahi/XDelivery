ALTER PROCEDURE [dbo].[spObterItemsNaoImpressoPorCodigo]
	@Codigo int,
	@CodGrupo int
as
	BEGIN
		SELECT  IT.* , Pe.NumeroMesa, G.NomeImpressora
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.NomeGrupo =P.GrupoProduto 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		IT.CodPedido =@Codigo
		and P.CodGrupo =@CodGrupo
		and ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
		
	END

