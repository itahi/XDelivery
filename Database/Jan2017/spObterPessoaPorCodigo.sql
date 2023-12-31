
ALTER PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,isnull(PontoReferencia,'') PontoReferencia,
  isnull(Observacao,'')Observacao ,Numero,Telefone,Telefone2,DataNascimento,isnull(TicketFidelidade,0) TicketFidelidade,
  CodRegiao,DataCadastro,isnull([user_id],'') as[user_id] ,isnull(DDD,'') as DDD , isnull(Sexo,'') as Sexo ,PFPJ,
  (select top 1 Codigo from Pessoa_Endereco where CodPessoa = Pessoa.Codigo order by Codigo asc) as CodEndereco
  FROM Pessoa WHERE Codigo =@Codigo
 END
