alter table Produto_Estoque add NomeProduto nvarchar(max);
alter table Produto_Estoque add CodPedido int;
go
create procedure spDeletarEstoque
@CodPedido int,
@CodProduto int,
@NomeProduto nvarchar(max)
as
  begin
     delete from Produto_Estoque 
	 where CodProduto=@CodProduto and
	 CodPedido=@CodPedido and NomeProduto=@NomeProduto
  end