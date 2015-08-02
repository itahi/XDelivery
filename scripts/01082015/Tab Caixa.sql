--drop table Caixa
go
create table Caixa
(
Codigo int primary key identity (1,1),
Data date ,
CodUsuario int null,
Historico nvarchar(max),
--Tipo  char(1),
ValorAbertura decimal(10,2),
ValorFechamento decimal(10,2),
Estado bit
Constraint FK_CODUSERCAIXA foreign  key (CodUsuario) references Usuario(Cod)
)

create table CaixaMovimento
(
Codigo int primary key identity (1,1),
CodMovimento int null,
Valor decimal(10,2) not null,
Historico nvarchar(max),
Data datetime,
Tipo char(1),
CodFormaPagamento int null

Constraint FK_CODFPagamento foreign key (CodFormaPagamento) references FormaPagamento(Codigo)
)

