create PROCEDURE [dbo].[spSinalizarPedidoConcluidoApp]
	@CodigoMesa int,
	@NumeroPessoas int	
AS
    declare  @TipoMesa int
	declare @CodPedido int;
	declare @CodPessoa int ;
	declare @DataAtual datetime;
	declare @TotalPedido decimal;
	declare @CodFormaPagamento int;
	declare @CodigoPedidoString nvarchar(max);
	declare @CodUser int;
	set @DataAtual=Getdate();
	set @CodigoPedidoString = Cast(@CodPedido as nvarchar(max));
	set @CodPedido =(select Codigo from Pedido where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto');
	set @CodPessoa =(select CodPessoa from Pedido where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto');
	set @TotalPedido=(select TotalPedido from Pedido where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto');
	set @TipoMesa = ( select CodigoMesa from Pedido where Codigo=@CodPedido);
    set @CodFormaPagamento =(select Codigo from FormaPagamento where Descricao =(select FormaPagamento from Pedido  where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto'));
	set @CodUser = (select CodUsuario from Pedido  where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto')
	if (@TipoMesa >0)
	 begin
	  exec spAlteraStatusMesa @TipoMesa,1
	 end
	Update Pedido 
	SET Finalizado = 1 , 
	HorarioFechamento =GetDate(),
	NumeroPessoas=@NumeroPessoas,
	[status]='Finalizado'
	 WHERE Codigo = @CodPedido
  exec spInserirMovimentoCaixa 1,@DataAtual,@CodigoPedidoString,@CodFormaPagamento,@TotalPedido,'E',@CodUser,'Noite';