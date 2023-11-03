create database Library
use Library

create table Authors(
Id int primary key identity,
Name nvarchar(30),
Surname nvarchar(30)
)

create table Books(
Id int primary key identity,
Name nvarchar(100) check(Len(Name)>2),
PageCount int,
AuthorId int foreign key references Authors(Id)
)

alter table Books
add IsDeleted bit default 0

alter view GetInfo
as
select b.Id,b.Name,b.PageCount,(a.Name+' '+a.Surname) as Fullname,b.IsDeleted from Books as b
join Authors as a
on a.Id=b.AuthorId


select * from GetInfo

create procedure usp_Seacrh @word nvarchar(50)
as
select * from GetInfo
where Name Like '%'+@word+'%' or Fullname Like '%'+@word+'%'

exec usp_Seacrh 'al'

create procedure usp_seachforpage @page int
as
select *from GetInfo
where Pagecount>@page

exec usp_seachforpage 150

create trigger TriggerforBook
on Books
after insert,update
as
begin
select * from GetInfo
end

insert into Books(Name,PageCount,AuthorId) values
('bla bla',10,1)

create procedure usp_InsertBook @name nvarchar(100),@page int,@id int
as
insert into Books(Name,PageCount,AuthorId) values
(@name,@page,@id)

exec usp_InsertBook 'yeni',20,2

create procedure usp_UpdateBook @id int, @name nvarchar(100)
as
update Books
set Name=@name
where Id=@id

exec usp_UpdateBook 1,'Aglamaq'

create trigger DeleteForBooks
on Books
instead of Delete
as
begin
Declare @id int
select @id=deleted.Id from deleted
update Books
set IsDeleted=1
where Id=@id
select * from GetInfo
end

delete from Books
where Id=4



select a.Id, a.Name+' '+a.Surname as Fullname,Count(b.Id) as BookCount, Max(b.PageCount) as MaxPage  from Authors as a
join Books as b
on b.AuthorId=a.Id
group by a.Id, a.Name+' '+a.Surname