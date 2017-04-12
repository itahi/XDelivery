create table Fidelidade_Promocao
(

)


create table Pessoa_Fidelidade
(
Codigo int identity(1,1),
CodPessoa int null,
CodPedido int null,
Ponto   int,
Tipo     char(1),
Data     datetime

Constraint PK01_Codigo_Fidelidade primary key (Codigo),
Constraint FK01_CodPessoa_Fidelidade foreign  key (CodPessoa) References Pessoa(Codigo),
Constraint FK02_CodPedido_Fidelidade foreign  key (CodPedido) References Pedido(Codigo)
)
go
create procedure spAdicionarPessoa_Fidelidade
@CodPessoa int ,
@CodPedido int ,
@Ponto   int,
@Tipo     char
as
  begin
    insert into Pessoa_Fidelidade (CodPessoa,CodPedido,Ponto,Tipo,Data) 
	       values (@CodPessoa,@CodPedido,@Ponto,@Tipo,GetDate())
  end
