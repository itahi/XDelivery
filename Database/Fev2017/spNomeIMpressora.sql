
create PROCEDURE [dbo].[spObterNomeImpressoraPorCodigoPedido]
	@Codigo int
as
	BEGIN
		SELECT  G.NomeImpressora,G.Codigo
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.Codigo =P.CodGrupo 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		CodPedido =@Codigo
		and ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
		
	END


