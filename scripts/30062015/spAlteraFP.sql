
ALTER procedure [dbo].[spAlterarFormaPagamento]
@Codigo int,
@Descricao nvarchar(100),
@DescontoSN bit

as 
begin
update FormaPagamento set 
     Descricao=@Descricao ,
	 ParcelaSN = @DescontoSN
	         where Codigo=@Codigo
end

