ALTER PROCEDURE [dbo].[spObterItemsPedidoApp]
	@Codigo int	
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			i.CodUsuario,
			i.NomeProduto,
			i.PrecoItem,
			i.PrecoTotalItem,
			cast(Quantidade as integer),
			i.Item,
			p.CodigoMesa
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo and CodigoMesa!=null and P.Finalizado=0
		ORDER BY Codigo
	END

