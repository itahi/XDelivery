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
  declare @CodPedidNovo int;
  declare @CodPedidoDestino int;
  declare @CodPedidoOrigem int;
  declare @Cout int;
  set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @NewMesa and Finalizado=0 and status='Aberto');
  set @CodPedidoOrigem = ( select Codigo from Pedido where CodigoMesa = 1 and Finalizado=0 and status='Aberto');
  set @Cout = ( select count(Codigo) from Pedido where CodigoMesa = @NewMesa and Finalizado=0 and status='Aberto')
  --if (@CodPedidoDestino=0 or @CodPedidoDestino='' )
  if (@Cout =0)
  begin
	  Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,@TotalPedido,'0,00','Dinheiro',@Data,'1 - Mesa',@NumeroMesa,
	  'Aberto','Balcao',@NewMesa,0,0,@CodUsuario,'','',0
      
	  -- Insere items no pedido baseando-se no pedido que foi cancelado
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesa + Item,ImpressoSN from ItemsPedido 
			  where CodPedido=@Codigo
      exec spCancelarPedido  @Codigo,'Cancelado',@Data,@CodUsuario;
  end
  else
     begin
	  Exec spCancelarPedido  @CodPedidoOrigem,'Cancelado',@Data,@CodUsuario;
      insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @Codigo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesa + Item,ImpressoSN from ItemsPedido 
			  where CodPedido=@CodPedidoOrigem
     end

	 exec spAlterarTotalPedidoApp @Codigo;
end