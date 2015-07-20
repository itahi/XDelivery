USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spExcluirGrupo]    Script Date: 22/06/2014 15:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spExcluirGrupo]

	@Codigo int

AS
	BEGIN
		DELETE FROM 
			Grupo
		WHERE 
			Codigo = @Codigo --Codigo Grupo
	END
