create table Pedido_Opcao
(
 CodProduto int ,
 CodOpcao int,
 CodPedido int,
 Quantidade decimal,
 Preco      decimal(10,2),
 Observacao nvarchar(100)

 Constraint FK01_Pedido_Opcao foreign key (CodProduto) references Produto(Codigo),
 Constraint FK02_Pedido_Opcao foreign key (CodOpcao) references Opcao(Codigo),
 Constraint FK03_Pedido_Opcao foreign key (CodPedido) references Pedido(Codigo)
)
go

ALTER procedure [dbo].[spAdicionarOpcaoPedido]
 @CodProduto int,
 @CodPedido  int,
 @CodOpcao   int,
 @Quantidade decimal,
 @Preco      decimal(10,2),
 @Observacao nvarchar(100)
 as
   begin
      insert into Pedido_Opcao (CodProduto,CodOpcao,CodPedido,Quantidade,Preco,Observacao)
	         values (@CodProduto,@CodOpcao,@CodPedido,@Quantidade,@Preco,@Observacao)
   end

