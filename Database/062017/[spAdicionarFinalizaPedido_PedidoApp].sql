create procedure [dbo].[spAdicionarFinalizaPedido_PedidoApp]
@CodigoMesa int,
@CodPagamento int,
@ValorPagamento decimal(10,2)
as 
 begin
    declare @CodPedido int;
	set @CodPedido =(select Codigo from Pedido where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto')
    insert into Pedido_Finalizacao values (@CodPedido,@CodPagamento,@ValorPagamento)
 end



