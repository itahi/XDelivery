
ALTER procedure [dbo].[spObterFPPorCodigo]
@Codigo int
as
 select 
 ISNULL(Codigo,0) as Codigo,
 ISNULL(Descricao,'Dinheiro') as Descricao,
 ISNULL(ParcelaSN,0) as ParcelaSN,
 IsNUll(CaminhoImagem,'') as CaminhoImagem,
 isnull(AtivoSN,0) as AtivoSN,
 isnull(GeraFinanceiro,0) as GeraFinanceiro
 from FormaPagamento
 where Codigo = @Codigo




