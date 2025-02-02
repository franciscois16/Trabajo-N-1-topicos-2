
create procedure [dbo].[MantenedorCategorias]
(
	@catId varchar(15)='',
	@catNombre varchar(100)='',
	@modo char(1)=''
)
as

/*** MODO DE CREACION DE UNA CATEGORIA ***/
IF @modo='C'
BEGIN
	insert into Categoria 
	select @catId,@catNombre
END
/*** MODO DE MODIFICACION DE UNA CATEGORIA ***/
IF @modo='M'
BEGIN
	update Categoria set catNombre=@catNombre where catId=@catId
END
/*** MODO DE ELIMINACION DE CATEGORIA ***/
IF @modo='E'
BEGIN
	delete from Categoria where catId=@catId
END
/*** MODO PARA LISTAR LAS CATEGORIAS ***/
IF @modo='L'
BEGIN
	select catId,catNombre from Categoria
END
/*** MODO PARA OBTENER UNA CATEGORIA ***/
IF @modo='O'
BEGIN
	select catId,catNombre from Categoria where catId=@catId
END