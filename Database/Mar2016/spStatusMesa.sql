
ALTER procedure [dbo].[spAlteraStatusMesa]
@Codigo int,
--@NumeroMesa nvarchar(100),
@StatusMesa int
  as
    begin
	update Mesas set StatusMesa = @StatusMesa
	 where Codigo = @Codigo --and Codigo = @Codigo
	end
