alter table Produto_Opcao add PrecoProcomocao decimal(10,2);
go
ALTER TABLE Opcao ALTER COLUMN Tipo  int;
go
ALTER procedure [dbo].[spAlterarOpcaoProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@PrecoProcomocao decimal(10,2),
@DataAlteracao datetime
as 
  begin
    Update Produto_Opcao set
	CodOpcao = @CodOpcao,
	Preco    =  @Preco,
	DataAlteracao = @DataAlteracao,
	PrecoProcomocao = @PrecoProcomocao
	where 
	CodProduto = @CodProduto and 
	CodOpcao = @CodOpcao

	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end
  go
  
ALTER procedure [dbo].[spAdicionarOpcaProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@DataAlteracao datetime,
@PrecoProcomocao decimal(10,2)
as 
  begin
     insert into Produto_Opcao ( CodProduto,CodOpcao,Preco,DataAlteracao,PrecoProcomocao)
	        values  ( @CodProduto,@CodOpcao,@Preco,@DataAlteracao,@PrecoProcomocao) 
	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end


