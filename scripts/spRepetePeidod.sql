USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[sbObterUltimoPedido]    Script Date: 20/06/2015 17:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sbObterUltimoPedido]
  @CodPessoa int
  as
   begin
	select 
	top 1 Codigo,
		 CodPessoa,
		 TotalPedido,
		 (select FormaPagamento from Pedido PS where PS.Codigo = p.Codigo) as FP, 
		 max(RealizadoEm) as RealizadoEm
	from Pedido P
	where CodPessoa =@CodPessoa and Finalizado =1
	group by Codigo,CodPessoa,TotalPedido,RealizadoEm
	order by RealizadoEm desc
  end
