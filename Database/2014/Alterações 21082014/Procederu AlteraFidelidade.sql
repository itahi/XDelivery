
create procedure [dbo].[spAlteraFidelidade]
@CodPessoa int ,
@Ticket int
as 
Begin Update Pessoa
set 
TicketFidelidade =TicketFidelidade +@Ticket
where
Codigo = @CodPessoa
end