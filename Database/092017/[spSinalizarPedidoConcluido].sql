
ALTER PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int,
	@NumeroPessoas int	
AS
    declare  @TipoMesa int
	set @TipoMesa = ( select CodigoMesa from Pedido where Codigo=@Codigo)
	declare @CodCupom nvarchar(max);
	set @CodCupom= ( select Cupom from Pedido where Codigo=@Codigo)
	declare @IdCupom int;
	set @IdCupom=(select Codigo from Cupom where CodCupom=@CodCupom);
	if (@TipoMesa >0)
	 begin
	  exec spAlteraStatusMesa @TipoMesa,1
	 end
	 if @IdCupom >0
	   begin
	     exec spAdicionarPedido_Cupom @Codigo,@IdCupom
	   end
	Update Pedido 
	SET Finalizado = 1 , 
	HorarioFechamento =GetDate(),
	NumeroPessoas=@NumeroPessoas,
	[status]='Finalizado'
	 WHERE Codigo = @Codigo

