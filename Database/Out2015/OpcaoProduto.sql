
ALTER procedure [dbo].[spAdicionarOpcaProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@DataAlteracao datetime
as 
  begin
     insert into Produto_Opcao ( CodProduto,CodOpcao,Preco,DataAlteracao)
	        values  ( @CodProduto,@CodOpcao,@Preco,@DataAlteracao) 
	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end
  GO
ALTER procedure [dbo].[spAlterarOpcaoProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@DataAlteracao datetime
as 
  begin
    Update Produto_Opcao set
	CodOpcao = @CodOpcao,
	Preco    =  @Preco,
	DataAlteracao = @DataAlteracao
	where 
	CodProduto = @CodProduto and 
	CodOpcao = @CodOpcao

	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end
  GO
 ALTER procedure [dbo].[spExcluirOpcaoProduto]
  @CodProduto int,
  @CodOpcao   int
   as
     begin
	  delete from Produto_Opcao 
	    where
		CodProduto = @CodProduto and 
		CodOpcao   = @CodOpcao
 update Produto set DataAlteracao = Getdate()
	 where Produto.Codigo =@CodProduto

	 end


