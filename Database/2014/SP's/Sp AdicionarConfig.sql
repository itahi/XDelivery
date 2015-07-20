USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarConfiguracao]    Script Date: 14/06/2014 10:41:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spAdicionarConfiguracao]
@ImpViaCozinha bit,
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo bit,
@Usa2Telefones bit

as begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,ControlaEntregador,ProdutoPorCodigo,Usa2Telefones) values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones)
end

					