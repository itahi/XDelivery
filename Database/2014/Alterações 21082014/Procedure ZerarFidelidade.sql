create procedure spZerarFidelidade
@CodPessoa int,
@Ticket int
as 
begin
Update Pessoa 
set 
TicketFidelidade = @Ticket
where 
Codigo=@CodPessoa
end
