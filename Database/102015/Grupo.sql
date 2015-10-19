alter table Grupo Add AtivoSN bit
go
update Grupo set AtivoSN = 1;
GO
ALTER PROCEDURE [dbo].[spAdicionarGrupo]

	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit
	
as
	BEGIN
		INSERT INTO Grupo(NomeGrupo,ImprimeCozinhaSN,OnlineSN,DataAlteracao,AtivoSN)
		Values(@NomeGrupo,@ImprimeCozinhaSN,@OnlineSN,@DataAlteracao,@AtivoSN )
	END
GO
ALTER PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50),
	
	@ImprimeCozinhaSN bit,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit
AS
	BEGIN
		UPDATE Grupo

		SET 
		NomeGrupo = @NomeGrupo,
		ImprimeCozinhaSN =@ImprimeCozinhaSN,
		OnlineSN= @OnlineSN,
		DataAlteracao =@DataAlteracao ,
		AtivoSN =@AtivoSN 
		WHERE Codigo = @Codigo

		update Produto set AtivoSN = @AtivoSN , DataAlteracao = @DataAlteracao
		   where Produto.GrupoProduto = @NomeGrupo
	END
	go
	ALTER PROCEDURE [dbo].[spObterGrupo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		ISNULL(AtivoSN,0) AS AtivoSN
			FROM Grupo 
		ORDER BY NomeGrupo ASC
go
create PROCEDURE [dbo].[spObterGrupoAtivo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN

			FROM Grupo 
			where AtivoSN=1
		ORDER BY NomeGrupo ASC
