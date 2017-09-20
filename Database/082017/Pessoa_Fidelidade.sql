drop table Pessoa_Fidelidade
go
create table Pessoa_Fidelidade
(
CodPessoa  int not null,
CodProduto int,
Pontos     int,
Data       datetime,
CodUsuario int,
CodPedido  int 
Constraint FK01_CodPessoaFidelidade Foreign key (CodPessoa) references Pessoa(Codigo),
Constraint FK02_CodProdutoFidelidade Foreign key (CodProduto) references Produto(Codigo)
)
go
create procedure spAdicionarPontosFidelidade
@CodPessoa int,
@CodProduto int,
@Pontos int,
@CodUsuario int,
@CodPedido  int
as
begin
insert into Pessoa_Fidelidade (CodPessoa,CodProduto,CodUsuario,Pontos,Data,CodPedido) values
                              (@CodPessoa,@CodProduto,@CodUsuario,@Pontos,GETDATE(),@CodPedido)  
end
go
create procedure spObterItensPedidoPonto
@Codigo int
as 
select 
P.Codigo,cast((P.PontoFidelidadeVenda*I.Quantidade) as int) as PontoFidelidadeVenda  ,
(select CodPessoa from Pedido where Codigo=@Codigo) as CodPessoa,
(select sum(PrecoTotalItem) from ItemsPedido where CodPedido=@Codigo) as TotalItens
 from ItemsPedido I
join Produto P on P.Codigo=I.CodProduto
where P.PontoFidelidadeVenda>0
and I.CodPedido=@Codigo

