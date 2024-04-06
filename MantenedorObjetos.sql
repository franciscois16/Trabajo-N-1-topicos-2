create procedure [dbo].[MantenedorObjetos]
(
	@obId varchar(15)='',
	@obNombre varchar(100)='',
	@catId varchar(15)='',
	@obCantidad int=0,
	@obFecCreacion date='',
	@obVigencia char(1)='', -- S / N
	@modo char(1)=''
)
as

/*** MODO DE CREACION DE UN OBJETO ***/
IF @modo='C'
BEGIN
	insert into Objeto
	select @obId,@obNombre,@catId,@obCantidad,@obFecCreacion,@obVigencia
END
/*** MODO DE MODIFICACION DE UN OBJETO ***/
IF @modo='M'
BEGIN
	update Objeto set obNombre=@obNombre,catId=@catId,obVigencia=@obVigencia where obId=@obId
END
/*** MODO PARA CADUCAR UN OBJETO ***/
IF @modo='V'
BEGIN
	update Objeto set obVigencia=case when obVigencia='N' then 'S' else 'N'end where obId=@obId
END
/*** MODO DE ELIMINACION DE UN OBJETO ***/
IF @modo='E'
BEGIN
	delete from Objeto where obId=@obId
END
/*** MODO PARA LISTAR LOS OBJETOS ***/
IF @modo='L'
BEGIN
	select obId,obNombre,catId,obCantidad,obFecCreacion,obVigencia from Objeto
END
/*** MODO PARA OBTENER UN OBJETO ***/
IF @modo='O'
BEGIN
	select obId,obNombre,catId,obCantidad,obFecCreacion,obVigencia from Objeto where obId=@obId
END