alter table RegiaoEntrega add AtivoSN bit;
go
update RegiaoEntrega set AtivoSN=1;
go
ALTER procedure [dbo].[spAlteraRegiao]
  @Codigo int,
  @NomeRegiao nvarchar(8),
  @TaxaServico decimal(10,2),
  @DataAlteracao datetime,
  @OnlineSN bit,
  @AtivoSN bit
as
  begin
    update RegiaoEntrega 
	set
	  NomeRegiao = @NomeRegiao,
	  TaxaServico = @TaxaServico,
	  DataAlteracao = @DataAlteracao,
	  OnlineSN =@OnlineSN
	  
    where 
	  Codigo = @Codigo
	   update RegiaoEntrega_Bairros set OnlineSN=@OnlineSN , AtivoSN=@AtivoSN
	  where
	   RegiaoEntrega_Bairros.OnlineSN =@OnlineSN  and RegiaoEntrega_Bairros.AtivoSN=@AtivoSN;
  end


