create procedure spInsereBoyPedido
@CodPedido int ,
@CodMotoBoy int
as
 update Pedido 
 set CodMotoboy = @CodMotoBoy
 where Codigo = @CodPedido

