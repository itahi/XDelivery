
ALTER procedure [dbo].[spAlterarOpcaoProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@PrecoProcomocao decimal(10,2),
@DataAlteracao datetime,
@CodTipo int
as 
  begin
    Update Produto_Opcao set
	CodOpcao = @CodOpcao,
	Preco    =  @Preco,
	DataAlteracao = @DataAlteracao,
	PrecoProcomocao = @PrecoProcomocao,
	CodTipo =@CodTipo
	where 
	CodProduto = @CodProduto and 
	CodOpcao = @CodOpcao

	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end

