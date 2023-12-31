
ALTER PROCEDURE [dbo].[spObterItemsPedidoApp]
	@Codigo int	
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			isnull(i.CodUsuario,1)CodUsuario,
			i.NomeProduto,
			i.PrecoItem,
			i.PrecoTotalItem,
			isnull(i.Quantidade,1) Quantidade,
			i.Item,
			p.CodigoMesa
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo 
		ORDER BY Codigo
	END
