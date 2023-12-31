alter table Grupo alter column MultiploSabores int;
go
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
		IsNull(MultiploSabores,0) as MultiploSabores 
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
		IsNull(MultiploSabores,0) as MultiploSabores 
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
		IsNull(MultiploSabores,0) as MultiploSabores 
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
		IsNull(MultiploSabores,0) as MultiploSabores 
			FROM Grupo 
	    where NomeGrupo = @Nome

