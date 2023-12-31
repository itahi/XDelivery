drop table Pedido_Status;
go
drop table Pedido_StatusMovimento;
go
alter table Pedido add CodigoPedidoWS int;
go
ALTER PROCEDURE [dbo].[spAdicionarPedido]
    @Codigo int output,
    @CodPessoa nvarchar(100),
    @TotalPedido decimal(10,2),
    @TrocoPara nvarchar(max),
    @FormaPagamento nvarchar(100),
    @RealizadoEm   datetime,
    @Tipo nvarchar(100),
    @NumeroMesa nvarchar(20),
    @Status     nvarchar(20),
    @PedidoOrigem nvarchar(10),
	@CodigoMesa int	,
	@DescontoValor decimal(10,2),
	@CodigoPedidoWS int ,
	@CodUsuario int
as
        BEGIN
		set @CodigoMesa= (select NumeroMesa from Mesas where Codigo = @CodigoMesa)
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,
            [Status],PedidoOrigem,CodigoMesa,DescontoValor,CodigoPedidoWS)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,
                @Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor,@CodigoPedidoWS
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
            
            exec spAdicionarPedidoStatusMovimento @Codigo , 1,@CodUsuario ,@RealizadoEm;
        END
go



create table PedidoStatus
(
Codigo int identity(1,1),
Nome  nvarchar(100),
EnviarSN bit,
AlertarSN bit,
[Status]  int not null,
DataAlteracao datetime,

Constraint PK01_PedidoStatus primary key (Codigo),
Constraint UK01_PedidoStatus unique ([Status]),

)

create table PedidoStatusMovimento
(
CodPedido int not null,
CodStatus int not null,
CodUsuario int not null,
DataAlteracao datetime,

Constraint FK01_PedidoStatusMovimento foreign key (CodPedido) references Pedido(Codigo),
Constraint FK02_PedidoStatusMovimento foreign key (CodStatus) references PedidoStatus([Status]),
Constraint FK03_PedidoStatusMovimento foreign key (CodUsuario) references Usuario(Cod)
)
 go
 create procedure spAdicionarPedidoStatus
-- @Codigo int,
 @Nome nvarchar(100),
 @EnviarSN bit,
 @AlertarSN bit,
 @Status int
  as
   begin
      insert into PedidoStatus (Nome,EnviarSN,AlertarSN,[Status],DataAlteracao) 
                          values (@Nome,@EnviarSN,@AlertarSN,@Status,Getdate())
   end
go
create procedure spAlterarPedidoStatus
 @Codigo int,
 @Nome nvarchar(100),
 @EnviarSN bit,
 @AlertarSN bit,
 @Status int 
   as 
     begin
       update PedidoStatus set 
         Nome = @Nome,
         EnviarSN =@EnviarSN,
         AlertarSN = @AlertarSN,
         [Status] = @Status, 
         DataAlteracao =Getdate()
           where Codigo =@Codigo
     end
  go
  create procedure spAdicionarPedidoStatusMovimento
  @CodPedido int,
  @CodStatus int,
  @CodUsuario int,
  @DataAlteracao datetime
  as 
    begin
      insert into PedidoStatusMovimento values (@CodPedido,@CodStatus,@CodUsuario,@DataAlteracao)
    end
    go
 create procedure spObterPedidoStatus
 as 
   begin
     select P.Codigo,P.Nome from PedidoStatus P
   end
     go
 create procedure spObterPedidoStatusPorCodigo
 @Codigo int
 as 
   begin
     select * from PedidoStatus 
      where Codigo =@Codigo
   end  