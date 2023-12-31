ALTER PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max)
AS
	BEGIN
		UPDATE Grupo

		SET 
		NomeGrupo = @NomeGrupo,
		ImprimeCozinhaSN =@ImprimeCozinhaSN,
		OnlineSN= @OnlineSN,
		DataAlteracao =@DataAlteracao ,
		AtivoSN =@AtivoSN,
		NomeImpressora =@NomeImpressora
		WHERE Codigo = @Codigo

		update Produto set AtivoSN = @AtivoSN ,OnlineSN=@OnlineSN, DataAlteracao = @DataAlteracao
		   where 
		   Produto.GrupoProduto = @NomeGrupo and Produto.AtivoSN = @AtivoSN AND Produto.OnlineSN=@OnlineSN
	END	
