
ALTER PROCEDURE [dbo].[spExcluirCliente]

	@Codigo int

AS
	BEGIN
	delete from Pessoa_Endereco where CodPessoa=@Codigo
		DELETE FROM 
			Pessoa
		WHERE 
			Codigo = @Codigo --Codigo Grupo
	END



