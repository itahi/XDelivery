

create table Empresa_HorarioEntrega
(
Codigo int identity (1,1)not null ,
Limite_horario_pedido nvarchar(6),
Horario_entrega nvarchar(20),
OnlineSN bit

Constraint PK01_Empresa_HorarioEntrega primary key(Codigo)
)
go
create procedure spAdicionarEmpresa_HorarioEntrega
@Limite_horario_pedido nvarchar(6),
@Horario_entrega nvarchar(20),
@OnlineSN bit
as
 begin
 insert into Empresa_HorarioEntrega (Limite_horario_pedido,Horario_entrega,OnlineSN)
         values  (@Limite_horario_pedido,@Horario_entrega,@OnlineSN)
 end
 go
create procedure spAlterarEmpresa_HorarioEntrega
@Codigo int ,
@Limite_horario_pedido nvarchar(6),
@Horario_entrega nvarchar(20),
@OnlineSN bit
as
  begin
   update Empresa_HorarioEntrega set
   Limite_horario_pedido=  @Limite_horario_pedido ,
   Horario_entrega  = @Horario_entrega,
   OnlineSN =@OnlineSN
    where Codigo=@Codigo 
  end
  go
  create procedure spObterEmpresa_HorarioEntrega
   as
   begin
  select * from Empresa_HorarioEntrega
  end
  go
  create procedure spObterEmpresa_HorarioEntregaPorCodigo
  @Codigo int
   as
   begin
  select * from Empresa_HorarioEntrega where Codigo=@Codigo
  end

