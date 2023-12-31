
ALTER PROCEDURE [dbo].[spObterItemsPedidoApp]
	@Codigo int	
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			isnull(i.CodUsuario,1) as CodUsuario,
			i.NomeProduto,
			i.PrecoItem,
			i.PrecoTotalItem,
			Quantidade ,
			i.Item,
			p.CodigoMesa
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo and CodigoMesa is not null and P.Finalizado=0
		ORDER BY Codigo
	END

