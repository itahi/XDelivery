create procedure spObterInsumoResumidoGeral
@DataInicio datetime,
@DataFim datetime
as
 select 
 (I.Nome+' '+I.UnidadeMedida) as Insumo,
 sum(IE.Quantidade) as EstoqueAtual
  from Insumo_Estoque IE
 join Insumo I on I.Codigo = IE.CodInsumo
 where IE.DataAlteracao between @DataInicio and @DataFim
 group by I.Nome,UnidadeMedida
 