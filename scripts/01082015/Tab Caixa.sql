drop table Caixa
go
create table Caixa
(
Codigo int identity (1,1),
Data date ,
CodUsuario int null,
Historico nvarchar(max),
Numero nvarchar(10),
--Tipo  char(1),
ValorAbertura decimal(10,2),
ValorFechamento decimal(10,2),
Estado bit
Constraint FK01_CODUSERCAIXA foreign  key (CodUsuario) references Usuario(Cod),
Constraint FK02_NUMCAIXA  foreign  key (Numero) references CAIXACADASTRO(NUMERO),
constraint PK02_CAIXA primary key (Numero),
CONSTRAINT UK01_CAIXA UNIQUE(NUMERO,data)
)

drop table CaixaMovimento
go

create table CaixaMovimento
(
Codigo int not null identity(1,1),
CodCaixa nvarchar(10) not null,
Data datetime,
Historico nvarchar(100),
NumeroDocumento nvarchar(50),
CodFormaPagamento int,
Valor decimal(10,2),
Tipo char(1),
CodUsuario int null

Constraint PK01_CAIXAMOVIMENTO PRIMARY KEY(CODIGO),

CONSTRAINT FK01_CAIXAMOVIMENTO FOREIGN KEY(CODCAIXA) REFERENCES CAIXA(Numero),
CONSTRAINT FK02_CodFormaPagamento FOREIGN KEY(CodFormaPagamento) REFERENCES FORMAPAGAMENTO(CODIGO),
CONSTRAINT FK03_CodUsuario FOREIGN KEY(CodUsuario) REFERENCES Usuario(Cod),
)

