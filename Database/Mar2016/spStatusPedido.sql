
ALTER procedure [dbo].[spStatusPedido] 
@Codigo int
 as 
   begin
     select PSM.CodStatus,PS.Nome from PedidoStatusMovimento PSM
     join PedidoStatus PS on PS.Codigo=PSM.CodStatus
     where CodPedido=@Codigo
     order by PSM.DataAlteracao desc
   end
   
