alter table Mesas add AtivoSN bit;
alter table Mesas add OnlineSN bit;
alter table Mesas add DataSincronismo datetime;
update Mesas set AtivoSN=1, OnlineSN=0, DataSincronismo=Getdate()+1
GO
ALTER procedure [dbo].[spAdicionarMesas]
@NumeroMesa nvarchar(10),
@StatusMesa int,
@AtivoSN bit,
@OnlineSN bit
  as
    begin
	 Insert into Mesas (NumeroMesa,StatusMesa,AtivoSN,OnlineSN)
	             Values (@NumeroMesa,@StatusMesa,@AtivoSN,@OnlineSN)
	end
go
ALTER procedure [dbo].[spAlteraMesas]
@Codigo int,
@StatusMesa int,
@NumeroMesa nvarchar(10),
@AtivoSN bit,
@OnlineSN bit
 as
   begin
    Update Mesas set 
	  StatusMesa = @StatusMesa,
	  NumeroMesa = @NumeroMesa,
	  AtivoSN = @AtivoSN,
	  OnlineSN= @OnlineSN,
	  DataAtualizacao = GETDATE()

	  where Codigo = @Codigo

   end



