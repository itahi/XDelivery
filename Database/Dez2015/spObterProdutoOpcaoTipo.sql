CREATE procedure [dbo].[spObterProduto_OpcaoTipoPorCodigo]
@Codigo int
as 
  begin
    select * from Produto_OpcaoTipo
    where Codigo =@Codigo
  end
