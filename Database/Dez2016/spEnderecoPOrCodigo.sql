
ALTER procedure [dbo].[spObterEnderecoPorCodigo]
@Codigo int
as 
begin
 select PE.Codigo,P.Nome,PE.Endereco,PE.Bairro,PE.Cidade,PE.UF,PE.PontoReferencia,
  PE.Complemento as Observacao,PE.Numero,Telefone,Telefone2,DataNascimento,
  isNull(TicketFidelidade,0) as TicketFidelidade ,
  PE.CodRegiao,DataCadastro,isnull([user_id],'') as [user_id] 
  ,isnull(DDD,'') as DDD
  ,isnull(Sexo,'') as Sexo,PFPJ,PE.Cep
  from Pessoa_Endereco PE
  join Pessoa P on P.Codigo = PE.CodPessoa 
  where PE.Codigo = @Codigo
  
  end
