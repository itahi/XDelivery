
ALTER PROCEDURE [dbo].[spCancelarPedido]

 @Codigo int,
 @Status nvarchar(100),
 @CodUsuario int
AS
 BEGIN
 declare  @TipoMesa int
	set @TipoMesa = ( select CodigoMesa from Pedido where Codigo=@Codigo)
	if (@TipoMesa >0)
	 begin
	  exec spAlteraStatusMesa @TipoMesa,1
	 end
  UPDATE Pedido
  SET   
  [status] = @Status,
  RealizadoEm = GetDate(),
  CodUsuario = @CodUsuario,
  Finalizado = 1
  where Codigo = @Codigo
  exec spExcluirFinalizaPedido_Pedido @Codigo
END





