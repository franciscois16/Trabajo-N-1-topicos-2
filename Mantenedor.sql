

create procedure [dbo].[Mantenedor_Usuario]
(
	@usuId varchar(10)='',
	@usuNombre varchar(50)='',
	@modo char(1)=''
)
as

IF @modo='C'
BEGIN
	insert into Usuarios
	select @usuId,@usuNombre
END

IF @modo='L'
	select*from Usuarios
