drop table Caixa
go
create table Caixa
(
Codigo int identity (1,1),
Data date ,
CodUsuario int null,
Historico nvarchar(max),
Numero varchar(10),
--Tipo  char(1),
ValorAbertura decimal(10,2),
ValorFechamento decimal(10,2),
Estado bit
Constraint FK01_CODUSERCAIXA foreign  key (CodUsuario) references Usuario(Cod),
constraint PK01_CAIXA primary key (Codigo),
CONSTRAINT UK01_CAIXA UNIQUE(NUMERO,data)
)

drop table CaixaMovimento
go

create table CaixaMovimento
(
Codigo int not null identity(1,1),
CodCaixa int not null,
Data datetime,
Historico nvarchar(100),
NumeroDocumento nvarchar(50),
CodFormaPagamento int,
Valor decimal(10,2),
Tipo char(1),

Constraint PK01_CAIXAMOVIMENTO PRIMARY KEY(CODIGO),
CONSTRAINT FK01_CAIXAMOVIMENTO FOREIGN KEY(CODCAIXA) REFERENCES CAIXA(CODIGO),
CONSTRAINT FK02_CAIXAMOVIMENTO FOREIGN KEY(CodFormaPagamento) REFERENCES FORMAPAGAMENTO(CODIGO),
)

