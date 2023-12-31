
CREATE	 PROCEDURE [dbo].[spObterTodosItemsPedidoBalcaoApp]
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			isnull(i.CodUsuario,1) as CodUsuario,
			i.NomeProduto,
			i.PrecoItem as PrecoUnitario,
			i.PrecoTotalItem as PrecoTotal,
			Quantidade ,
			i.Item,
			P.Senha 
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			Senha is not null and P.Finalizado=0
		ORDER BY Codigo
	END
