
ALTER procedure [dbo].[spAdicionarOpcaProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@DataAlteracao datetime,
@PrecoProcomocao decimal(10,2),
@CodTipo int
as 
  begin
     insert into Produto_Opcao ( CodProduto,CodOpcao,Preco,DataAlteracao,PrecoProcomocao,CodTipo)
	        values  ( @CodProduto,@CodOpcao,@Preco,@DataAlteracao,@PrecoProcomocao,@CodTipo) 
	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end

