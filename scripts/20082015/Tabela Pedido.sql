alter table Pedido add MargemGarcon decimal(10,2);
go
create procedure spMargemGarcon
@Codigo int ,
@MargemGarcon decimal(10,2)
as
 begin
  update Pedido set TotalPedido = TotalPedido + @MargemGarcon ,  MargemGarcon = @MargemGarcon
  where Codigo = @Codigo
 end
go
create procedure spRemoveDezPorCento
@Codigo int ,
@MargemGarcon decimal(10,2),
@TotalPedido decimal(10,2)
as
 begin
  update Pedido 
  set 
  TotalPedido = @TotalPedido,
  MargemGarcon = @MargemGarcon
  where Codigo = @Codigo
 end



 select * from Pedido order by Codigo desc

