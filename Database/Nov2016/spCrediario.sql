create procedure spObterCrediarioDataDetalhado
@DataInicio date,
@DataFim date
as
begin
  select 
H.Tipo,
Data,
CodPessoa,
Historico,
Valor,
P.Nome ,
P.Telefone
  from HistoricoPessoa  H
  join Pessoa P on P.Codigo=H.CodPessoa
 where 
 Data between @DataInicio and @DataFim
end
go
create procedure spObterCrediarioDataResumido
@DataI date,
@DataF date
 as
 select 
CodPessoa,
(Select NOme from Pessoa where Codigo=H.CodPessoa) as Nome,
(Select Telefone from Pessoa where Codigo=H.CodPessoa) as Telefone,
Sum(Valor) 'Valor Devedor'
  from HistoricoPessoa  H
  join Pessoa P on P.Codigo=H.CodPessoa
where Data between @DataI and @DataF
group by CodPessoa
