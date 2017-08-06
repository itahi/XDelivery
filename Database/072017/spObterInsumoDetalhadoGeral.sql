create procedure spObterInsumoDetalhadoGeral
@DataInicio datetime,
@DataFim datetime
as
 select 
 (I.Nome+' '+I.UnidadeMedida) as Insumo,
IE.Quantidade
  from Insumo_Estoque IE
 join Insumo I on I.Codigo = IE.CodInsumo
 where IE.DataAlteracao  between @DataInicio and @DataFim
 
 