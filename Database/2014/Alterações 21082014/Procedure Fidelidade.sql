alter procedure spAlteraFidelidade
@CodPessoa int ,
@Ticket int
as 
Begin Update Pessoa
set 
TicketFidelidade =TicketFidelidade +@Ticket
where
Codigo = @CodPessoa
end

go
create procedure ObterFidelidade
@CodPessoa int ,
@Tickect int
as 
begin
select TicketFidelidade from Pessoa
where
Codigo = @CodPessoa
end