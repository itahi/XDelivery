
 ALTER procedure [dbo].[spAdicionarPedidoStatus]
-- @Codigo int,
 @Nome nvarchar(100),
 @Status int,
  @AlertarSN bit,
 @EnviarSN bit
 --@DataAlteracao datetime
 
  as
   begin
      insert into PedidoStatus (Nome,EnviarSN,AlertarSN,[Status],DataAlteracao) 
                          values (@Nome,@EnviarSN,@AlertarSN,@Status,Getdate())
   end
