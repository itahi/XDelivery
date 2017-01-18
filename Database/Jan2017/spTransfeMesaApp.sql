
create procedure [dbo].[spTransfeMesaApp]
@NumeroMesaOrigem nvarchar(10),
@NumeroMesaDestino nvarchar(10)
as
begin
-- Declarando váriaveis temporarias ---
  declare @DataAtual datetime;
  declare @CodPedidNovo int;
  declare @CodigoPedido int ;
  declare @CodUser int;
  declare @CodPessoa int;
  declare @TotalPedido decimal
  ----- Sets ----
  set @DataAtual = Getdate()
  set @CodPessoa = (select CodPessoa from Pedido where NumeroMesa=@NumeroMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @TotalPedido = (select TotalPedido from Pedido where NumeroMesa=@NumeroMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodUser = (select CodUsuario from Pedido where NumeroMesa=@NumeroMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodigoPedido = (select Codigo from Pedido where NumeroMesa=@NumeroMesaOrigem and Finalizado=0 and [status]='Aberto');
  Exec spCancelarPedido  @CodigoPedido,'Cancelado',@DataAtual,@CodUser
 
  Exec spAdicionarPedido @CodPedidNovo output,@CodPessoa,@TotalPedido,'0,00','Dinheiro',@DataAtual,'1 - Mesa','1',
  'Aberto','Balcao',@NumeroMesaDestino,0,0,@CodUser,'','',0

  -- Insere items no pedido baseando-se no pedido que foi cancelado
  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
  select  @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN from ItemsPedido 
          where CodPedido=@CodigoPedido
end