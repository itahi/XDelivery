create table Pedido_Pagamento
(
Codigo int identity(1,1),
CodPedido int ,
CodPagamento int,
Valor decimal(10,2) not null,
Data datetime,
CodUsuario int
)
go
create procedure spAdicionarPedido_Pagamento
@CodPedido int ,
@CodPagamento int,
@Valor decimal(10,2),
@CodUsuario int
as
 begin
   insert into Pedido_Pagamento (CodPedido,CodPagamento,Valor,CodUsuario)
                          values (@CodPedido,@CodPagamento,@Valor,@CodUsuario)
 end
go
create procedure spObterPedido_Pagamento
@Codigo int
as
begin
 select * from Pedido_Pagamento where CodPedido=@Codigo
end