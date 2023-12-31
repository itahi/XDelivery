
ALTER PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int,
	@NumeroPessoas int	
AS
    declare  @TipoMesa int
	set @TipoMesa = ( select CodigoMesa from Pedido where Codigo=@Codigo)
	if (@TipoMesa >0)
	 begin
	  exec spAlteraStatusMesa @TipoMesa,1
	 end
	Update Pedido 
	SET Finalizado = 1 , 
	HorarioFechamento =GetDate(),
	NumeroPessoas=@NumeroPessoas
	 WHERE Codigo = @Codigo
