
ALTER procedure [dbo].[spObterBairro_RegiaoPorNome]
@Bairro nvarchar(max)
as
select
 * from RegiaoEntrega_Bairros 
where AtivoSN=1 
and Nome=@Bairro


