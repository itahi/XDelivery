 ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]
 @Telefone nvarchar(100)
as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,
  Numero,DataNascimento,CodRegiao,TicketFidelidade,Telefone2,[user_id],DDD,Sexo,PFPJ,
  (Select top 1 Codigo from Pessoa_Endereco where CodPessoa = Pessoa.Codigo order by Codigo asc) as CodEndereco
  FROM Pessoa 
  WHERE 
  Telefone = @Telefone or Telefone2 =@Telefone
 END
