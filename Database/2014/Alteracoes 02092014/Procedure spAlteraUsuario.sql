
ALTER procedure [dbo].[spAlterarUsuario]
@codigo int,
@Nome nvarchar(max),
@Senha nvarchar(128),
@CancelaPedidosSN bit,
@AlteraProdutosSN bit,
@AdministradorSN bit,
@AcessaRelatoriosSN bit

as begin
update Usuario set 
Nome=@Nome,
Senha = @Senha,
CancelaPedidosSN=@CancelaPedidosSN ,
AlteraProdutosSN= @AlteraProdutosSN ,
AdministradorSN= @AdministradorSN ,
AcessaRelatoriosSN = @AcessaRelatoriosSN
where cod=@Codigo
   END

