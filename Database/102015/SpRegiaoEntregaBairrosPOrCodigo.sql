create procedure [dbo].[spObterRegiaoEntrega_BairrosPorCodigo]
@Codigo int
	 as
	   begin
	   select CodRegiao,Nome,Cep  from RegiaoEntrega_Bairros
	   where 
	   CodRegiao = @Codigo
	   end
