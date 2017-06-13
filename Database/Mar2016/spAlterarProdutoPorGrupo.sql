create procedure spAlterarProdutoPorGrupo
@AtivoSN bit,
@OnlineSN bit,
@CodGrupo int
as
  begin
  update Produto set 
		AtivoSN = @AtivoSN ,
		OnlineSN=@OnlineSN,
		DataAlteracao = Getdate()
	   where 
       CodGrupo = @CodGrupo --and OnlineSN=@OnlineSN and AtivoSN=@AtivoSN
  end


