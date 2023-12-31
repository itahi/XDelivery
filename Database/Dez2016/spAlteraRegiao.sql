
ALTER procedure [dbo].[spAlteraRegiao]
  @Codigo int,
  @NomeRegiao nvarchar(20),
  @TaxaServico decimal(10,2),
  @DataAlteracao datetime,
  @OnlineSN bit,
  @AtivoSN bit,
  @valorMinimoFreteGratis decimal(10,2),
  @PrevisaoEntrega nvarchar(10)
as
  begin
    update RegiaoEntrega 
	set
	  NomeRegiao = @NomeRegiao,
	  TaxaServico = @TaxaServico,
	  DataAlteracao = @DataAlteracao,
	  OnlineSN =@OnlineSN,
	  valorMinimoFreteGratis =@valorMinimoFreteGratis,
	  PrevisaoEntrega = @PrevisaoEntrega,
	  AtivoSN = @AtivoSN
	  
    where 
	  Codigo = @Codigo
	   update RegiaoEntrega_Bairros set OnlineSN=@OnlineSN , AtivoSN=@AtivoSN , RegiaoEntrega_Bairros.DataCadastro=Getdate()
	  where
	   RegiaoEntrega_Bairros.CodRegiao = @Codigo 
  end
