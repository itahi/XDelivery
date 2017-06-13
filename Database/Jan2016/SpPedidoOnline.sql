create procedure spObterPedidoOnline
@Codigo int
as
  begin
    select * from Pedido where CodigoPedidoWS!=0 and
    Codigo = @Codigo
  end
  
  
  
  