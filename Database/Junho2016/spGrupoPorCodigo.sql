
create PROCEDURE [dbo].[spObterGrupoPorCodigo]
@Codigo int
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		ISNULL(AtivoSN,0) AS AtivoSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia 
			FROM Grupo 
		where Codigo = @Codigo
		ORDER BY NomeGrupo ASC
