create table Insumo_Estoque
(
Codigo int not null identity(1,1),
CodInsumo int not null,
Quantidade decimal (10,2) not null,
DataAlteracao datetime default getdate()

Constraint PK01_CodInsumoEstoque primary key (Codigo),
constraint FK01_CodInsumo foreign key (CodInsumo) references Insumo(Codigo)
)
go
create procedure spAdicionar_InsumoEstoque
@CodInsumo int,
@Quantidade decimal
as 
 begin
    insert into Insumo_Estoque (CodInsumo,Quantidade,DataAlteracao)
	                    values (@CodInsumo,@Quantidade,Getdate()) 
 end
