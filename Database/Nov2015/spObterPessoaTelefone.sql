
ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone nvarchar(100)

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero,DataNascimento,CodRegiao,TicketFidelidade,Telefone2
  FROM Pessoa WHERE Telefone = @Telefone or Telefone2=@Telefone
 END



