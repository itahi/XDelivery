
ALTER procedure [dbo].[spTransfeMesaApp]
@CodigoMesaOrigem int,
@CodigoMesaDestino int
as
begin
-- Declarando váriaveis temporarias ---
  declare @DataAtual datetime;
  declare @CodPedidNovo int;
  declare @CodigoPedidoOrigem int ;
  declare @CodUser int;
  declare @CodPessoa int;
  declare @TotalPedido decimal
  declare @CodPedidoDestino int;
  declare @NumeroMesaOrigem nvarchar(max);
  declare @NumeroMesaDestino nvarchar(max);
  declare @Cout int;
  
  ----- Sets ----
  set @DataAtual = Getdate()
  set @CodPessoa = (select CodPessoa from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @TotalPedido = (select TotalPedido from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodUser = (select CodUsuario from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto');
  set @CodigoPedidoOrigem = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaOrigem and Finalizado=0 and status='Aberto');
  set @NumeroMesaDestino = (select NumeroMesa from Mesas where Codigo=@CodigoMesaDestino);
  set @NumeroMesaOrigem = (select NumeroMesa from Mesas where Codigo=@CodigoMesaOrigem);
  set @Cout = ( select count(Codigo) from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto')
  if (@Cout =0)
	  begin
	    Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,@TotalPedido,'0,00','Dinheiro',@DataAtual,'1 - Mesa',@NumeroMesaDestino,
	         'Aberto','Balcao',@CodigoMesaDestino,0,0,@CodUser,'','',0
	  -- Insere items no pedido baseando-se no pedido que foi cancelado
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN from ItemsPedido 
			  where CodPedido=@CodigoPedidoOrigem
      exec spCancelarPedido  @CodigoPedidoOrigem,'Cancelado',@DataAtual,@CodUser;
	  end
  else
     begin
      insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidoDestino,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN from ItemsPedido 
			where CodPedido=@CodigoPedidoOrigem
	   Exec spCancelarPedido  @CodigoPedidoOrigem,'Cancelado',@DataAtual,@CodUser;
     end

	 exec spAlterarTotalPedidoApp @CodPedidoDestino;
end