create procedure spObterPedidosGeral
@DataInicio datetime,
@DataFim datetime
as
select 
PF.CodPedido ,
sum(PF.ValorPagamento) as 'Vlr Pedido',
(select Nome from Pessoa PS where PS.Codigo=P.CodPessoa) as 'Cliente'
 from Pedido_Finalizacao PF
 join Pedido P on P.Codigo=PF.CodPedido
 where 
 P.RealizadoEm  between @DataInicio and @DataFim and 
 P.Tipo!='1 - Mesa'
 group by PF.CodPedido,P.CodPessoa,PF.CodPagamento
 order by CodPedido