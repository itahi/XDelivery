
ALTER procedure [dbo].[spObterCrediarioData]
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