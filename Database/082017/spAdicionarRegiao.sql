
ALTER PROCEDURE [dbo].[spAdicionaRegiao]

	@NomeRegiao nvarchar(max),
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



