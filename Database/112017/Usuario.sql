alter table Usuario add AtivoSN bit;
go
update Usuario set AtivoSN=1;
GO
ALTER procedure [dbo].[spAdicionarUsuario]
@Nome nvarchar(max),
@Senha nvarchar(128),
@CancelaPedidosSN bit,
@AlteraProdutosSN bit,
@AdministradorSN bit,
@AcessaRelatoriosSN bit,
@FinalizaPedidoSN bit,
@DescontoMax decimal,
@DescontoPedidoSN bit,
@EditaPedidoSN bit,
@VisualizaDadosClienteSN bit,
@AbreFechaCaixaSN bit,
@AlteraDadosClienteSN bit,
@AtivoSN bit
as begin

insert into Usuario (Nome,Senha,CancelaPedidosSN,AlteraProdutosSN,AdministradorSN,AcessaRelatoriosSN,
						FinalizaPedidoSN,DescontoMax,DescontoPedidoSN,EditaPedidoSN,VisualizaDadosClienteSN,
						AbreFechaCaixaSN,AlteraDadosClienteSN,AtivoSN)
					values
					(@Nome,@Senha,@CancelaPedidosSN,@AlteraProdutosSN,@AdministradorSN,@AcessaRelatoriosSN,
					@FinalizaPedidoSN,@DescontoMax,@DescontoPedidoSN,@EditaPedidoSN,@VisualizaDadosClienteSN,
						@AbreFechaCaixaSN,@AlteraDadosClienteSN,@AtivoSN )
  end
GO
ALTER procedure [dbo].[spAdicionarUsuarioDefault]
@Nome nvarchar(max),
@Senha nvarchar(128),
@AdministradorSN bit,
@AbreFechaCaixaSN bit,
@AtivoSN bit
as 
begin
insert into Usuario (Nome,Senha,AdministradorSN,AbreFechaCaixaSN,AtivoSN) 
             values (@Nome,@Senha,@AdministradorSN,@AbreFechaCaixaSN,@AtivoSN)
end
GO
ALTER procedure [dbo].[spAlterarUsuario]
@codigo int,
@Nome nvarchar(max),
@Senha nvarchar(128),
@CancelaPedidosSN bit,
@AlteraProdutosSN bit,
@AdministradorSN bit,
@AcessaRelatoriosSN bit,
@FinalizaPedidoSN bit,
@DescontoPedidoSN bit,
@DescontoMax decimal,
@EditaPedidoSN bit,
@VisualizaDadosClienteSN bit,
@AbreFechaCaixaSN bit,
@AlteraDadosClienteSN bit,
@AtivoSN bit

as 
begin
update Usuario set 
Nome=@Nome,
Senha = @Senha,
CancelaPedidosSN=@CancelaPedidosSN ,
AlteraProdutosSN= @AlteraProdutosSN ,
AdministradorSN= @AdministradorSN ,
AcessaRelatoriosSN = @AcessaRelatoriosSN,
FinalizaPedidoSN=@FinalizaPedidoSN,
DescontoMax = @DescontoMax,
DescontoPedidoSN = @DescontoPedidoSN,
EditaPedidoSN=@EditaPedidoSN ,
VisualizaDadosClienteSN= @VisualizaDadosClienteSN ,
AbreFechaCaixaSN =@AbreFechaCaixaSN ,
AlteraDadosClienteSN=@AlteraDadosClienteSN,
AtivoSN=@AtivoSN
where cod=@Codigo
END
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
ISNULL(DescontoMax,0) as DescontoMax,
ISNULL(EditaPedidoSN,0) as EditaPedidoSN,
ISNULL(VisualizaDadosClienteSN,0) as VisualizaDadosClienteSN,
ISNULL(AbreFechaCaixaSN,0) as AbreFechaCaixaSN,
ISNULL(AlteraDadosClienteSN,0) as AlteraDadosClienteSN,
ISNULL(AtivoSN,0) as AtivoSN
 from Usuario
end
GO
ALTER PROCEDURE [dbo].[spObterUsuarioApp]	
AS
BEGIN
	SELECT
		cod,
		Nome,
		Senha,
		AdministradorSN,
		FinalizaPedidoSN,
		isnull(AtivoSN,0) as AtivoSN
	FROM
		Usuario
	where AtivoSN=1
END
GO
ALTER procedure [dbo].[spObterUsuarioGrid]
as begin
select 
Cod,
ISNULL(Nome,0) as Nome
 from Usuario
end
GO
ALTER PROCEDURE [dbo].[spObterUsuarioLogin]	

@nome nvarchar(100),
@senha nvarchar(32)

AS
BEGIN
	SELECT
		cod,
		Nome,
		Senha,
		AdministradorSN,
		ISNULL(AtivoSN,0) as AtivoSN
	FROM
		Usuario where (Nome = @nome) AND (Senha = @senha)
END
GO
ALTER procedure [dbo].[spObterUsuarioPorCodigo]
@Codigo int
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
ISNULL(DescontoMax,0) as DescontoMax,
ISNULL(EditaPedidoSN,0) as EditaPedidoSN,
ISNULL(VisualizaDadosClienteSN,0) as VisualizaDadosClienteSN,
ISNULL(AbreFechaCaixaSN,0) as AbreFechaCaixaSN,
ISNULL(AlteraDadosClienteSN,0) as AlteraDadosClienteSN,
ISNULL(AtivoSN,0) as AtivoSN,
 from Usuario
 where Cod=@Codigo
end





go
create procedure spExcluirUsuario
@Codigo int
as
begin
 delete from Usuario where Cod=@Codigo
end