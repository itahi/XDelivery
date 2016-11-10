create procedure [dbo].[spObterFormaPagamentoAtivo]
as 
begin
select 
ISNull(Codigo,1) as Codigo,
ISNull(Descricao,'Dinheiro') as Descricao,
ISNull(ParcelaSN ,0) as ParcelaSN,
ISNULL(GeraFinanceiro,0) AS GeraFinanceiro,
ISNULL(OnlineSN,0) AS OnlineSN,
IsNUll(CaminhoImagem,'') as CaminhoImagem,
isnull(AtivoSN,0) as AtivoSN

from FormaPagamento
where AtivoSN=1
end
