
ALTER procedure [dbo].[spObterHistoricoPorPessoa]
@CodPessoa int 
as
begin
 select 
 Tipo,
Historico,
Valor
  from HistoricoPessoa  H
 where 
 CodPessoa=@CodPessoa
 END
GO
ALTER procedure [dbo].[spObterHistoricoPorPessoaPorData]
@CodPessoa int ,
@DataInicio date,
@DataFim date
as
begin
  select 
Tipo,
CodPessoa,
Historico,
Valor,
(select sum(Valor) from HistoricoPessoa His where His.CodPessoa=H.CodPessoa and His.Tipo='D') Debito,
(select sum(Valor) from HistoricoPessoa His where His.CodPessoa=H.CodPessoa and His.Tipo='C') Credito
  from HistoricoPessoa  H
  
 where 
 CodPessoa=@CodPessoa  
 and Data between @DataInicio and @DataFim
 
end
