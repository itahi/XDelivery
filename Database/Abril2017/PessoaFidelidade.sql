create table Fidelidade_Promocao
(
  Codigo int identity(1,1),
  Nome nvarchar(max),
  AtivoSN bit,
  DataInicio date,
  DataFim date,
  Multiplicador int
)


create table Pessoa_Fidelidade
(
Codigo int identity(1,1),
CodPessoa int null,
CodPedido int null,
Ponto   int,
Tipo     char(1),
Data     datetime default getdate()

Constraint PK01_Codigo_Fidelidade primary key (Codigo),
Constraint FK01_CodPessoa_Fidelidade foreign  key (CodPessoa) References Pessoa(Codigo),
Constraint FK02_CodPedido_Fidelidade foreign  key (CodPedido) References Pedido(Codigo)
)
go
create procedure spInsereFidelidade
@CodPessoa int,
@CodPedido int,
@Ponto int
as 
  begin
     
     insert into Pessoa_Fidelidade (CodPessoa,CodPedido,Ponto,Tipo,Data)
	        values   (@CodPessoa,@CodPedido,@Ponto,'C',Getdate())
	  
  end
go
create procedure spObterFidelidadePessoa
@Codigo int 
as
  select isnull(sum(Ponto),0) as Pontos from Pessoa_Fidelidade where CodPessoa=@Codigo
go
create procedure spObterMenorPontuacaoProduto
as
  select Isnull(Min(PontoFidelidadeTroca),0) PontoFidelidadeTroca from Produto where PontoFidelidadeTroca>0
go
create procedure spObterProdutosDisponivelsPratroca
@Codigo int
  as
   select NomeProduto from Produto where PontoFidelidadeTroca>=@Codigo