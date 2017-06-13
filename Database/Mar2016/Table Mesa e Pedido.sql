alter table Pedido drop column NumeroMesa;
go
alter table Pedido add NumeroMesa nvarchar(10)
go

alter table Mesas add Constraint UK_NumeroMesa  unique (NumeroMesa) 





