
create PROCEDURE [dbo].spCancelarPedido

 @Codigo int,
 @Status nvarchar(100),
 @RealizadoEm Datetime
AS
 BEGIN
  UPDATE Pedido

  SET   
  [status] = @Status,
  RealizadoEm = @RealizadoEm

  where Codigo = @Codigo
END
