alter table RegiaoEntrega add valorMinimoFreteGratis decimal(10,2)
go
ALTER PROCEDURE [dbo].[spAdicionaRegiao]

	@NomeRegiao nvarchar(8),
	@TaxaServico decimal(10,2),
	@DataAlteracao datetime,
	@OnlineSN bit,
	@AtivoSN bit,
	@valorMinimoFreteGratis decimal(10,2)
	
as
	BEGIN
		INSERT INTO RegiaoEntrega(NomeRegiao,TaxaServico,DataAlteracao,OnlineSN,AtivoSN,valorMinimoFreteGratis)
		Values(@NomeRegiao,@TaxaServico,@DataAlteracao,@OnlineSN,@AtivoSN,@valorMinimoFreteGratis)
	END
go
ALTER procedure [dbo].[spAlteraRegiao]
  @Codigo int,
  @NomeRegiao nvarchar(8),
  @TaxaServico decimal(10,2),
  @DataAlteracao datetime,
  @OnlineSN bit,
  @AtivoSN bit,
  @valorMinimoFreteGratis decimal(10,2)
as
  begin
    update RegiaoEntrega 
	set
	  NomeRegiao = @NomeRegiao,
	  TaxaServico = @TaxaServico,
	  DataAlteracao = @DataAlteracao,
	  OnlineSN =@OnlineSN,
	  valorMinimoFreteGratis =@valorMinimoFreteGratis 
	  
    where 
	  Codigo = @Codigo
	   update RegiaoEntrega_Bairros set OnlineSN=@OnlineSN , AtivoSN=@AtivoSN
	  where
	   RegiaoEntrega_Bairros.OnlineSN =@OnlineSN  and RegiaoEntrega_Bairros.AtivoSN=@AtivoSN;
  end
