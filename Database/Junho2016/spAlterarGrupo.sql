
ALTER PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max),
	@CodFamilia int
AS
	BEGIN
		UPDATE Grupo

		SET 
		NomeGrupo = @NomeGrupo,
		ImprimeCozinhaSN =@ImprimeCozinhaSN,
		OnlineSN= @OnlineSN,
		DataAlteracao =@DataAlteracao ,
		AtivoSN =@AtivoSN,
		NomeImpressora =@NomeImpressora,
		CodFamilia = @CodFamilia
		WHERE Codigo = @Codigo
         exec spAlterarProdutoPorGrupo @AtivoSN,@OnlineSN,@Codigo,@NomeGrupo;
	END	

