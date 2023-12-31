
ALTER PROCEDURE [dbo].[spObterGrupoAtivo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia 

			FROM Grupo 
			where AtivoSN=1 and (PaiSN=0 or PaiSN is null)
		ORDER BY NomeGrupo ASC
		
		
	--	select * from Grupo