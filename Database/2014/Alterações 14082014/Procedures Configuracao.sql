
/****** Object:  StoredProcedure [dbo].[spAlterarConfiguracao]    Script Date: 14/08/2014 22:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spAlterarConfiguracao]
@cod int,
@ImpViaCozinha bit,
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo bit,
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega bit

AS 
	Begin
	Update Configuracao set
	ImpViaCozinha = @ImpViaCozinha,
	UsaDataNascimento = @UsaDataNascimento,
	UsaLoginSenha = @UsaLoginSenha,
	QtdCaracteresImp = @QtdCaracteresImp,
	ControlaEntregador = @ControlaEntregador,
	ProdutoPorCodigo = @ProdutoPorCodigo,
	Usa2Telefones   = @Usa2Telefones,
	UsaControleMesa = @UsaControleMesa,
	ImprimeViaEntrega = @ImprimeViaEntrega
	
	where cod=@cod
	end
	GO
ALTER procedure [dbo].[spAdicionarConfiguracao]
@ImpViaCozinha bit,
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo bit,
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega bit

as 
begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,ControlaEntregador,ProdutoPorCodigo,Usa2Telefones,UsaControleMesa,ImprimeViaEntrega) values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones,@UsaControleMesa,@ImprimeViaEntrega)
end


GO
