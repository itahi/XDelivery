
ALTER procedure [dbo].[spAdicionarFormaPagamento]
@Descricao nvarchar(100),
@DescontoSN bit


as
begin 
Insert into FormaPagamento(Descricao,ParcelaSN)
            Values (@Descricao,@DescontoSN)

end

