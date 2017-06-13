create table Pedido_Opcao
(
CodPedido int not null,
CodOpcao int not null,
CodProduto int not null,
Quantidade decimal(10,2),
Observacao nvarchar(max)

Constraint FK01_Pedido_Opcao foreign key(CodPedido) references Pedido(Codigo),
Constraint FK02_Pedido_Opcao foreign key(CodOpcao) references Opcao(Codigo),
Constraint FK03_Pedido_Opcao foreign key(CodProduto) references Produto(Codigo),

)
go
create procedure spAdicionarPedidoOpcao
@CodPedido int,
@CodOpcao int,
@CodProduto int ,
@Quantidade decimal(10,2),
@Observacao nvarchar(max)
as
  begin
    insert into Pedido_Opcao ( CodPedido,CodOpcao,CodProduto,Quantidade,Observacao)
              values ( @CodPedido,@CodOpcao,@CodProduto,@Quantidade,@Observacao)
  end
go
create procedure spAlterarPedidoOpcao
@CodPedido int,
@CodOpcao int,
@CodProduto int ,
@Quantidade decimal(10,2),
@Observacao nvarchar(max)
as 
  begin
    update Pedido_Opcao set
    CodOpcao = @CodOpcao,
    CodProduto = @CodProduto,
    Quantidade = @Quantidade,
    Observacao = @Observacao
    where CodPedido=@CodPedido and CodProduto=@CodProduto 
  end
go
create procedure spExcluirPedidoOpcao
@CodPedido int
 as
   begin
     delete from Pedido_Opcao where CodPedido=@CodPedido
   end  