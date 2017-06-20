alter procedure spObterProdutividadeGarcon
@DataI date,
@DataF date
as
select 
I.Quantidade,
I.PrecoTotalItem ,
 I.CodProduto, 
 I.NomeProduto
 , U.Cod
,U.Nome as Garcon
 from
ItemsPedido I
join Pedido P on P.Codigo = I.CodPedido
left join Usuario U on U.Cod = I.CodUsuario
where RealizadoEm between @DataI and @DataF and Tipo='1 - Mesa'

