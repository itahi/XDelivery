USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEntregador]    Script Date: 17/08/2014 18:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAdicionarEntregador]

	@Nome nvarchar(50),
	@Comissao numeric(5,2)
	
as
	BEGIN
		INSERT INTO Entregador(Nome,Comissao)
		Values(@Nome,@Comissao)
	END



