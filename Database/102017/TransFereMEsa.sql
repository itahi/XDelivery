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
	  'Aberto','Balcao',@CodigoMesaDestino,0,0,@CodUsuario,'','',0,'',0,'',''
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

GO


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
  declare @Data datetime;
  declare @StatusMesa nvarchar(max);
 
  ----- Sets ----
  set @DataAtual = Getdate()
  set @CodPessoa = (select CodPessoa from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @TotalPedido = (select TotalPedido from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodUser = (select CodUsuario from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto');
  set @CodigoPedidoOrigem = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaOrigem and Finalizado=0 and status='Aberto');
  set @NumeroMesaDestino = (select NumeroMesa from Mesas where Codigo=@CodigoMesaDestino);
  set @NumeroMesaOrigem = (select NumeroMesa from Mesas where Codigo=@CodigoMesaOrigem);
   set @StatusMesa ='Transferida p/ mesa' +@NumeroMesaDestino;
  set @Data =Getdate();
  set @Cout = ( select count(Codigo) from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto')
  if (@Cout =0)
	  begin
	    Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,@TotalPedido,0.00,'Dinheiro',@DataAtual,'1 - Mesa',@NumeroMesaDestino,
	         'Aberto','Balcao',@CodigoMesaDestino,0,0,@CodUser,'','',0,'',0,'',''
	 
	  -- Insere items no pedido baseando-se no pedido que foi cancelado
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,isnull(ImpressoSN,1) as ImpressoSN from ItemsPedido 
			  where CodPedido=@CodigoPedidoOrigem
      exec spCancelarPedido  @CodigoPedidoOrigem,@StatusMesa,@CodUser;
	  end
  else
     begin
      insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidoDestino,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN from ItemsPedido 
			where CodPedido=@CodigoPedidoOrigem
	   
	   Exec spCancelarPedido  @CodigoPedidoOrigem,@StatusMesa,@CodUser;
     end

	 exec spAlterarTotalPedidoApp @CodPedidoDestino;
end

GO

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
	  'Aberto','Balcao',@NewMesa,0,0,@CodUsuario,'','',0,'',0,'',''
      
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
end