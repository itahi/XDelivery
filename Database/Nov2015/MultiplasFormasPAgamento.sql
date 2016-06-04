create table Pedido_Finalizacao
(
CodPedido int not null,
CodPagamento int not null,
ValorPagamento decimal(10,2)

Constraint FK01_CodPedidoFinalizacao foreign key (CodPedido) references Pedido(Codigo),
Constraint FK02CodPagamento foreign key (CodPagamento) references FormaPagamento(Codigo)
)
go
create procedure spAdicionarFinalizaPedido_Pedido
@CodPedido int,
@CodPagamento int,
@ValorPagamento decimal(10,2)
as 
 begin
   insert into Pedido_Finalizacao values (@CodPedido,@CodPagamento,@ValorPagamento)
 end
go

create procedure spAlteraFinalizaPedido_Pedido
@CodPedido int,
@CodPagamento int,
@ValorPagamento decimal(10,2)
 as
   begin
     update Pedido_Finalizacao set
      CodPagamento = @CodPagamento,
      ValorPagamento = @ValorPagamento
      where CodPedido = @CodPedido
   end
go
create procedure spExcluirFinalizaPedido_Pedido
@CodPedido int 
  as 
   begin
     delete from Pedido_Finalizacao where CodPedido = @CodPedido
   end   