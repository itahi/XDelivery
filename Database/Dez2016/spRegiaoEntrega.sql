alter table RegiaoEntrega add PrevisaoEntrega nvarchar(10);
GO
ALTER PROCEDURE [dbo].[spAdicionaRegiao]

	@NomeRegiao nvarchar(8),
	@TaxaServico decimal(10,2),
	@DataAlteracao datetime,
	@OnlineSN bit,
	@AtivoSN bit,
	@valorMinimoFreteGratis decimal(10,2),
	@PrevisaoEntrega nvarchar(10)
	
as
	BEGIN
		INSERT INTO RegiaoEntrega(NomeRegiao,TaxaServico,DataAlteracao,OnlineSN,AtivoSN,valorMinimoFreteGratis,PrevisaoEntrega)
		Values(@NomeRegiao,@TaxaServico,@DataAlteracao,@OnlineSN,@AtivoSN,@valorMinimoFreteGratis,@PrevisaoEntrega)
	END
	GO
ALTER procedure [dbo].[spAlteraRegiao]
  @Codigo int,
  @NomeRegiao nvarchar(8),
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
	  PrevisaoEntrega = @PrevisaoEntrega
	  
    where 
	  Codigo = @Codigo
	   update RegiaoEntrega_Bairros set OnlineSN=@OnlineSN , AtivoSN=@AtivoSN , RegiaoEntrega_Bairros.DataCadastro=Getdate()
	  where
	   RegiaoEntrega_Bairros.CodRegiao = @Codigo 
  end
