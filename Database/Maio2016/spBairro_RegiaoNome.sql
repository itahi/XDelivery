
ALTER procedure [dbo].[spObterBairro_RegiaoPorNome]
@Nome nvarchar(max)
as 
begin

select * from RegiaoEntrega_Bairros where AtivoSN=1 and Nome=@Nome
end