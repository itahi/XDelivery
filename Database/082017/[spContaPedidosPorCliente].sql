
ALTER procedure [dbo].[spContaPedidosPorCliente]
@Codigo int
  as
    begin
	select count(Codigo) as Quanti
	from 
	 Pedido
   where CodPessoa = @Codigo and Finalizado=1 and
   Tipo='0 - Entrega'
	end




