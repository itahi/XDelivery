
ALTER procedure [dbo].[spTransfeMesa]
@Codigo int,
@NumeroMesa nvarchar(max),
@CodUsuario int,
@TotalPedido decimal(10,2),
@CodigoMesa int,
@CodPessoa int,
@NewMesa  int,
@Data datetime
as
begin
  declare @CodPedidNovo int
  --Exec spAlterarTotalPedido @Codigo,0,'1 - Mesa',@NumeroMesa,@CodUsuario,'',0,'',0
  Exec spCancelarPedido  @Codigo,'Cancelado',@Data,@CodUsuario
  Exec spAdicionarPedido @CodPedidNovo output,@CodPessoa,@TotalPedido,'0,00','Dinheiro',@Data,'1 - Mesa','1',
  'Aberto','Balcao',@NewMesa,0,0,@CodUsuario,'','',0

  -- Insere items no pedido baseando-se no pedido que foi cancelado
  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
  select  @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesa + Item,ImpressoSN from ItemsPedido 
          where CodPedido=@Codigo
end