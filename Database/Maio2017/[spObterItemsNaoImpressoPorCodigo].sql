

ALTER PROCEDURE [dbo].[spObterItemsNaoImpressoPorCodigo]
	@Codigo int
as
	BEGIN
		SELECT  IT.* , 
		(select NumeroMesa from Pedido where Codigo=@Codigo) as NumeroMesa, 
		G.NomeImpressora
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.NomeGrupo =P.GrupoProduto 
		WHERE 
		IT.CodPedido =@Codigo
		and IT.ImpressoSN = 0	
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
	END



