create procedure [dbo].[spObterBairro_RegiaoPorNome]
@Nome nvarchar(max)
as 
begin

select * from RegiaoEntrega_Bairros where Nome=@Nome
end