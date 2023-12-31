alter table HistoricoCancelamentos add CodPedido int not null;
alter table HistoricoCancelamentos add constraint FK01_CodPedido_Cancelado foreign key (CodPedido) references Pedido(Codigo);
go
ALTER procedure [dbo].[spAdicionaHistoricoCancelamento]
@CodPessoa int,
@Motivo nvarchar(100),
@CodMotivo int,
@Data date,
@CodPedido int
as
  begin
  insert into HistoricoCancelamentos (CodPessoa,Motivo,CodMotivo,Data,CodPedido)
         values (@CodPessoa,@Motivo,@CodMotivo,@Data,@CodPedido)

  end

