
ALTER procedure [dbo].[spObterPedidoStatus]
 as 
   begin
     select P.Codigo,P.Nome,P.Status from PedidoStatus P
   end



