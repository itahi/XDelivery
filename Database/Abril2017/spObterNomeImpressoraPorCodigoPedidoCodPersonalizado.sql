

ALTER PROCEDURE [dbo].[spObterNomeImpressoraPorCodigoPedidoCodPersonalizado]
	@Codigo int
as
	BEGIN
		SELECT  G.Codigo as CodGrupo, G.NomeImpressora,IT.CodPedido
		FROM ItemsPedido IT 
		left join Produto P on P.CodigoPersonalizado = IT.CodProduto 
		left join Grupo G   on G.Codigo =P.CODGRUPO 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		IT.CodPedido =@Codigo
		and ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
		
	END



