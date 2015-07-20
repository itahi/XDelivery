ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone nvarchar(20)

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero,DataNascimento
  FROM Pessoa WHERE Telefone = @Telefone or Telefone2=@Telefone
 END
