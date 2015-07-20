USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spAdicionaEmpresa]    Script Date: 21/06/2014 11:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].spAdicionarFormaPagamento
@Descricao nvarchar(100)


as
begin 
Insert into FormaPagamento(Descricao)
            Values (@Descricao)

end
go
create procedure spAlterarFormaPagamento
@Codigo int,
@Descricao nvarchar(100) 

as 
begin
update FormaPagamento set Descricao=@Descricao 
        where Codigo=@Codigo
end
go
create procedure spObterFormaPagamento
as 
begin
select Codigo,Descricao from FormaPagamento
end






