create PROCEDURE [dbo].[spAlteraPedidoBalcaoApp]
	@Codigo int output,
	@NomeCliente nvarchar(max)	
as
	BEGIN			
		update Pedido set Observacao=@NomeCliente where Codigo = @Codigo
		exec spAlteraTotalPedido @Codigo
	END


