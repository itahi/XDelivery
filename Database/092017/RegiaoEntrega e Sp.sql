alter table RegiaoEntrega add TaxaEntregador decimal(10,2);
go
update RegiaoEntrega set TaxaEntregador=0;
GO
ALTER PROCEDURE [dbo].[spAdicionaRegiao]
	@NomeRegiao nvarchar(max),
	@TaxaServico decimal(10,2),
	@DataAlteracao datetime,
	@OnlineSN bit,
	@AtivoSN bit,
	@valorMinimoFreteGratis decimal(10,2),
	@PrevisaoEntrega nvarchar(10),
	@TaxaEntregador decimal(10,2)
	
as
	BEGIN
		INSERT INTO RegiaoEntrega(NomeRegiao,TaxaServico,DataAlteracao,OnlineSN,AtivoSN,valorMinimoFreteGratis,PrevisaoEntrega,TaxaEntregador)
		Values(@NomeRegiao,@TaxaServico,@DataAlteracao,@OnlineSN,@AtivoSN,@valorMinimoFreteGratis,@PrevisaoEntrega,@TaxaEntregador)
	END
go
ALTER procedure [dbo].[spAlteraRegiao]
  @Codigo int,
  @NomeRegiao nvarchar(20),
  @TaxaServico decimal(10,2),
  @DataAlteracao datetime,
  @OnlineSN bit,
  @AtivoSN bit,
  @valorMinimoFreteGratis decimal(10,2),
  @PrevisaoEntrega nvarchar(10),
  @TaxaEntregador decimal(10,2)
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
	  AtivoSN = @AtivoSN,
	  TaxaEntregador=@TaxaEntregador
	  
    where 
	  Codigo = @Codigo
	  begin
	   update RegiaoEntrega_Bairros set OnlineSN=@OnlineSN , AtivoSN=@AtivoSN , RegiaoEntrega_Bairros.DataCadastro=Getdate()
	  where
	   RegiaoEntrega_Bairros.CodRegiao = @Codigo
	   end 
  end



