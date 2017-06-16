
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