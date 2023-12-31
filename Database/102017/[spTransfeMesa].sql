

ALTER procedure [dbo].[spTransfeMesa]
@Codigo int,
@NumeroMesa nvarchar(max),
@CodUsuario int,
@TotalPedido decimal(10,2),
@CodigoMesa int,
@CodPessoa int,
@NewMesa  int
as
begin
  declare @CodPedidNovo int;
  declare @CodPedidoDestino int;
  declare @CodPedidoOrigem int;
  declare @Cout int;
  declare @Data datetime;
  declare @StatusMesa nvarchar(max);
  set @StatusMesa ='Transferida p/ mesa' +@NumeroMesa;
  set @Data =Getdate();
  set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @NewMesa and Finalizado=0 and status='Aberto');
  set @CodPedidoOrigem = ( select Codigo from Pedido where CodigoMesa = 1 and Finalizado=0 and status='Aberto');
  set @Cout = ( select count(Codigo) from Pedido where CodigoMesa = @NewMesa and Finalizado=0 and status='Aberto')
  if (@Cout =0)
  begin
	  -- Cria o pedido 
	  Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,@TotalPedido,0.00,'Dinheiro',@Data,'1 - Mesa',@NumeroMesa,
	  'Aberto','Balcao',@NewMesa,0,0,@CodUsuario,'','',0,'',0,''
      
	  -- Insere items no pedido baseando-se no pedido que foi cancelado
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN,FidelidadeSN,DescontoPorcetagem)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesa + Item,ImpressoSN,0,0 from ItemsPedido 
			  where CodPedido=@Codigo
      exec spCancelarPedido  @Codigo,@StatusMesa,@CodUsuario;
  end
  else
     begin
	  Exec spCancelarPedido  @CodPedidoOrigem,@StatusMesa,@CodUsuario;
      insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN,FidelidadeSN,DescontoPorcetagem)
	  select                   @Codigo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesa + Item,ImpressoSN,0,0 from ItemsPedido 
			  where CodPedido=@CodPedidoOrigem
     end

	 exec spAlterarTotalPedidoApp @Codigo;
end

