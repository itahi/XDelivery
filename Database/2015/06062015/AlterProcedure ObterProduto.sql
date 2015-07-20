USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCodigo]    Script Date: 06/06/2015 17:38:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto
	FROM Produto WHERE  AtivoSN= 1 and Codigo = @Codigo;
