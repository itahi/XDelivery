
ALTER PROCEDURE [dbo].[spCancelarPedido]

 @Codigo int,
 @Status nvarchar(100),
 @RealizadoEm Datetime,
 @CodUsuario int
AS
 BEGIN
  UPDATE Pedido

  SET   
  [status] = @Status,
  RealizadoEm = @RealizadoEm,
  CodUsuario = @CodUsuario
  where Codigo = @Codigo
  exec spExcluirFinalizaPedido_Pedido @Codigo
END





select * from Pedido
