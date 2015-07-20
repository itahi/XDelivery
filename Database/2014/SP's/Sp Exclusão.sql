USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spExcluirItemPedido]    Script Date: 04/06/2014 21:33:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].spExcluirProduto

	@CodProduto int

AS
	BEGIN
		DELETE FROM 
			Produto
		WHERE 
			Codigo = @CodProduto --Codigo Produto
	END
	GO

create PROCEDURE [dbo].spExcluirGrupo

	@CodGrupo int

AS
	BEGIN
		DELETE FROM 
			Grupo
		WHERE 
			Codigo = @CodGrupo --Codigo Grupo
	END
	GO
	create PROCEDURE [dbo].spExcluirCliente

	@CodCliente int

AS
	BEGIN
		DELETE FROM 
			Pessoa
		WHERE 
			Codigo = @CodCliente --Codigo Grupo
	END
	GO




