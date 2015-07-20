create table DescontoPorDia
(
Codigo int not null,
DiaSemana nvarchar(20),
CodProduto int not null,
PrecoComDesconto int ,
constraint PK_COD_DESCONTO primary key (Codigo)
)