
ALTER procedure [dbo].[spTransfereItemMesa]
@Codigo int,
@CodigoMesaDestino int
as
begin
    declare @CodPedidoOrigem int;
	declare @CodPedidoDestino int;
	declare @NumeroMesaOrigem nvarchar(max);
	declare @CodPedidNovo int;
	declare @CodPessoa int;
	declare @CodUsuario int;
	declare @DataAtual datetime;
	declare @Count int;
	declare @CodigoMesOrigem int;
	declare @NumeroMesaDestino nvarchar(max);
	
	set @DataAtual = Getdate();
	set @CodPedidoOrigem = ( select CodPedido from ItemsPedido where Codigo=@Codigo);
	set @CodPessoa = ( select CodPessoa from Pedido where Codigo=@CodPedidoOrigem);
	set @CodUsuario = ( select CodUsuario from Pedido where Codigo=@CodPedidoOrigem);
	set @CodigoMesOrigem = ( select CodigoMesa from Pedido where Codigo=@CodPedidoOrigem);
	set @NumeroMesaOrigem = ( select NumeroMesa from Pedido where Codigo = @CodPedidoOrigem);
	set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto'); 
	set @NumeroMesaDestino = ( select NumeroMesa from Mesas where Codigo=@CodigoMesaDestino)
	set @Count = ( select count(Codigo) from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto');
  
  if (@Count =0)
  begin
	  Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,0,0,'Dinheiro',@DataAtual,'1 - Mesa',@NumeroMesaDestino,
	  'Aberto','Balcao',@CodigoMesaDestino,0,0,@CodUsuario,'','',0,'',0,''
	  -- Insere items no pedido baseando-se no pedido
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN,FidelidadeSN,DescontoPorcetagem)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN ,0,0
	         from ItemsPedido 
	   where Codigo=@Codigo
   end
   else
     begin
	   -- Insere items no pedido baseando-se no pedido
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN,FidelidadeSN,DescontoPorcetagem)
	  select                   @CodPedidoDestino,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN,0,0
	         from ItemsPedido 
	   where Codigo=@Codigo
	 end
	    -- Remove o item do Pedido antigo
     delete from ItemsPedido where Codigo=@Codigo;
	 exec [spAlterarTotalPedidoApp] @Codigo;
	 exec [spAlterarTotalPedidoApp] @CodPedidoDestino;
end

