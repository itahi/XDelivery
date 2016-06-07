alter table Usuario add EditaPedidoSN bit;
alter table Usuario add VisualizaDadosClienteSN bit;
alter table Usuario add AbreFechaCaixaSN bit;
go
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
@DescontoPedidoSN bit
as begin

insert into Usuario (Nome,Senha,CancelaPedidosSN,AlteraProdutosSN,AdministradorSN,AcessaRelatoriosSN,
						FinalizaPedidoSN,DescontoMax,DescontoPedidoSN)
					values
					(@Nome,@Senha,@CancelaPedidosSN,@AlteraProdutosSN,@AdministradorSN,@AcessaRelatoriosSN,
					@FinalizaPedidoSN,@DescontoMax,@DescontoPedidoSN )
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
@DescontoMax decimal

as begin
update Usuario set 
Nome=@Nome,
Senha = @Senha,
CancelaPedidosSN=@CancelaPedidosSN ,
AlteraProdutosSN= @AlteraProdutosSN ,
AdministradorSN= @AdministradorSN ,
AcessaRelatoriosSN = @AcessaRelatoriosSN,
FinalizaPedidoSN=@FinalizaPedidoSN,
DescontoMax = @DescontoMax,
DescontoPedidoSN = @DescontoPedidoSN
where cod=@Codigo
   END
go
create procedure [dbo].[spLogin]
@Nome nvarchar(max),
@Senha nvarchar(max)
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
	ISNULL(AbreFechaCaixaSN,0) as AbreFechaCaixaSN
	 from Usuario
	 where Nome=@Nome and Senha=@Senha
end
go

create procedure [dbo].[spObterUsuarioPorCodigo]
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
	ISNULL(AbreFechaCaixaSN,0) as AbreFechaCaixaSN
 from Usuario
 where Cod=@Codigo
end