go

if not EXISTS ( select DataNascimento from Pessoa)
begin
alter table Pessoa add DataNascimento datetime
end
if not Exists(select Tipo from Pedido)
begin
alter table Pedido add Tipo nvarchar(20)
alter table Pedido add NumeroMesa nvarchar(20);
end
if not Exists( select UsaControleMesa,ImprimeViaEntrega from Configuracao)
begin
alter table Configuracao add UsaControleMesa bit;
alter table Configuracao add ImprimeViaEntrega bit default 0;
end
if not Exists( select DataInicio from Empresa)
begin
alter table Empresa add DataInicio datetime
end
alter table Entregador add Comissao decimal(5,2)



