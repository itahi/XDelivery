USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[spObterUsuario]    Script Date: 16/06/2015 22:30:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spObterUsuario]
as begin
select 
Cod as Codigo,
ISNULL(Nome,0) as Nome,
ISNULL(Senha,0) as Senha,
ISNULL(CancelaPedidosSN,0) as CancelaPedidosSN,
ISNULL(AlteraProdutosSN,0) as AlteraProdutosSN,
ISNULL(AdministradorSN,0) as AdministradorSN,
ISNULL(AcessaRelatoriosSN,0) as AcessaRelatoriosSN,
ISNULL(DescontoPedidoSN,0) as DescontoPedidoSN,
ISNULL(FinalizaPedidoSN,0) as FinalizaPedidoSN,
ISNULL(DescontoMax,0) as DescontoMax
 from Usuario
end
