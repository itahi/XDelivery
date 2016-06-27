
ALTER procedure [dbo].[spAlterarProdutoPorGrupo]
@AtivoSN bit,
@OnlineSN bit,
@CodGrupo int,
@NomeGrupo nvarchar(max)
as
  begin
  update Produto set 
		AtivoSN = @AtivoSN ,
		OnlineSN=@OnlineSN,
		GrupoProduto = @NomeGrupo,
		DataAlteracao = Getdate()
	   where 
       CodGrupo = @CodGrupo
  end


