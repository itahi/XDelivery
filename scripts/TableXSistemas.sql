create procedure spAdicionaX
@Data date
as
 insert into XSistemas (Data) values (@Data)



 create view vwObterXSistemas
 as
 select count(Codigo) as Contador from XSistemas
