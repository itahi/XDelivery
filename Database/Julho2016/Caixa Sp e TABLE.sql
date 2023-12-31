alter table Caixa add Turno varchar(5);
go
alter table Caixa drop constraint PK01_CAIXA;

go
ALTER procedure [dbo].[spAbrirCaixa]
 @CodUsuario int,
 @Data date,
 @Estado bit,
 @Historico nvarchar(100),
 @ValorAbertura decimal(10,2),
 @Numero varchar(10),
 @Turno varchar(5)
 as
   begin
     insert into Caixa (CodUsuario,Data,Estado,Historico,ValorAbertura,Numero,Turno) 
	             values (@CodUsuario,@Data,@Estado,@Historico,@ValorAbertura,@Numero,@Turno) 
   end
GO
ALTER procedure [dbo].[spObterDadosCaixa]
@Data date,
@Numero varchar(10),
@Turno varchar(5)
 as 
   begin
     select
	   ISNULL(Codigo,0) as Codigo,
	   ISNULL(Numero,1) as Numero,
	   ISNULL(Data,getdate()) as Data,
	   ISNULL(CodUsuario,0) as CodUsuario,
	   ISNULL(Historico,'') as Historico,
	   ISNULL(ValorAbertura,0) as ValorAbertura,
	   ISNULL(ValorFechamento,0) as ValorFechamento,
	   ISNULL(Estado,0) as Estado
   from 
   Caixa
  where Data = @Data
    and Numero = @Numero 
    and Turno = @Turno
   end
