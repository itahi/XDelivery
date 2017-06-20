Alter table Grupo add MultiploSabores nvarchar(max);
go
ALTER PROCEDURE [dbo].[spAdicionarGrupo]

	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max),
	@CodFamilia int,
	@MultiploSabores nvarchar(max)
	
as
	BEGIN
		INSERT INTO Grupo(NomeGrupo,ImprimeCozinhaSN,OnlineSN,DataAlteracao,AtivoSN,NomeImpressora,CodFamilia,MultiploSabores)
		Values(@NomeGrupo,@ImprimeCozinhaSN,@OnlineSN,@DataAlteracao,@AtivoSN,@NomeImpressora,@CodFamilia ,@MultiploSabores)
	END

	GO
ALTER PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max),
	@CodFamilia int,
	@MultiploSabores nvarchar(max)
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
		CodFamilia = @CodFamilia,
		MultiploSabores=@MultiploSabores
		WHERE Codigo = @Codigo
         exec spAlterarProdutoPorGrupo @AtivoSN,@OnlineSN,@Codigo,@NomeGrupo;
	END
	GO
ALTER PROCEDURE [dbo].[spObterGrupo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		ISNULL(AtivoSN,0) AS AtivoSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia ,
		IsNull(MultiploSabores,'') as MultiploSabores 
			FROM Grupo 
		where PaiSN is null or PaiSN=0	
		ORDER BY NomeGrupo ASC
		GO
ALTER PROCEDURE [dbo].[spObterGrupoAtivo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia ,
		IsNull(MultiploSabores,'') as MultiploSabores 
			FROM Grupo 
			where AtivoSN=1 and (PaiSN=0 or PaiSN is null)
		ORDER BY NomeGrupo ASC
		GO
ALTER PROCEDURE [dbo].[spObterGrupoPorCodigo]
@Codigo int
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		ISNULL(AtivoSN,0) AS AtivoSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia  ,
		IsNull(MultiploSabores,'') as MultiploSabores 
			FROM Grupo 
		where Codigo = @Codigo
		ORDER BY NomeGrupo ASC
		GO
ALTER PROCEDURE [dbo].[spObterGrupoPorNome]
@Nome nvarchar(100)
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN
		,
		IsNull(MultiploSabores,'') as MultiploSabores 
			FROM Grupo 
	    where NomeGrupo = @Nome

