
ALTER procedure [dbo].[spObterBairro_RegiaoPorNome]
@Bairro nvarchar(max),
@Cidade nvarchar(max)
as 
begin
select
 * from RegiaoEntrega_Bairros 
where AtivoSN=1 
and Nome=@Bairro
and @Cidade =(select Cidade from Empresa)
end



