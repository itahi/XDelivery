
ALTER procedure [dbo].[spObterEntregasMotoboy]
@DataInicio datetime,
@DataFim datetime
as
select 
E.Codigo,
E.Nome,
PS.Nome as 'Cliente',
PS.Telefone as 'Telefone',
 DATEPART(hour, P.RealizadoEM) as 'Hora'
from Pedido P
join Entregador E  on E.Codigo=P.CodMotoboy 
join Pessoa PS on PS.Codigo = P.CodPessoa
where CodMotoboy is not null
and RealizadoEM between @DataInicio and @DataFim





