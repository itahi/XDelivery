
ALTER procedure [dbo].[spObterProduto_OpcaoTipoGeral]
--@Codigo int
as 
  begin
    select * from Produto_OpcaoTipo
    --where Codigo =@Codigo
    order by OrdenExibicao asc
  end

