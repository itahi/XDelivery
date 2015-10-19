USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[spObterItemsVendidos]    Script Date: 12/08/2014 15:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spObterItemsVendidos]
@DataInicio date,
@DataFim    date
as
	BEGIN
	SELECT CodProduto,NomeProduto,Sum(Quantidade)Quantidade,PrecoItem
  FROM ItemsPedido ITPE 
       join Pedido Pe on Pe.Codigo=ITPE.CodPedido
      
  where Pe.RealizadoEm  between @DataInicio and @DataFim and Pe.Finalizado=1
  group by CodProduto,NomeProduto,PrecoItem
     
 -- group by CodProduto,NomeProduto,RealizadoEm,Quantidade,PrecoItem
	END
	
	

