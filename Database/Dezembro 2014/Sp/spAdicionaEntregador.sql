
create procedure [dbo].[spAlterarEntregador]
@Codigo int,
@Nome nvarchar(100),
@Comissao decimal(5,2)


as
begin
Update
Entregador set
nome = @Nome,
Comissao =@Comissao 
where Codigo=@Codigo
end
