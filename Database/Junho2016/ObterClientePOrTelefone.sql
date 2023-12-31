
ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone nvarchar(100)

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero,DataNascimento,CodRegiao,TicketFidelidade,Telefone2
  FROM Pessoa WHERE 
  Telefone = Substring(@Telefone,3,9) or Telefone2 = Substring(@Telefone,3,9) or
  Substring(Telefone,3,9) = @Telefone or Substring(Telefone2,3,9)=@Telefone or
  Substring(Telefone,3,8) = @Telefone or Substring(Telefone2,3,8)=@Telefone or
  Substring(Telefone,1,8) = @Telefone or Substring(Telefone2,1,8)=@Telefone or
  Substring(Telefone,1,9) = @Telefone or Substring(Telefone2,1,9)=@Telefone
 END
