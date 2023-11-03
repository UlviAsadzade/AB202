create database Cinema

use Cinema

create table Movies(
Id int primary key identity,
Name nvarchar(50) not null,
Rate decimal(18,2) check(Rate>=0 and Rate<=10),
)

create table Directos(
Id int primary key identity,
Name nvarchar(50) not null,
Surname nvarchar(50) not null,
Age tinyint check(Age>17)
)

alter table Movies
Add DirectosId int foreign key references Directos(Id)

create table Actors(
Id int primary key identity,
Name nvarchar(50) not null,
Surname nvarchar(50) not null,
Age tinyint check(Age>17)
)

create table Genres(
Id int primary key identity,
Name nvarchar(50) not null,
)

create table MovieActor(
MoviesId int foreign key references Movies(Id),
ActorId int foreign key references Actors(Id)
)

create table MovieGenre(
MoviesId int foreign key references Movies(Id),
GenresId int foreign key references Genres(Id)
)

--1
select * from Movies
where Rate>8

--3
select * from Actors
union
select * from Directos

--4
select m.Name,m.Rate,(d.Name+' '+d.Surname) as 'Diectors Fullname' from Movies as m
join Directos as d
on d.Id=m.DirectosId

--6
select m.Name,m.Rate,(d.Name+' '+d.Surname) as 'Diectors Fullname',(a.Name+' '+a.Surname) as 'Actor Fullname'
from Movies as m
join Directos as d
on d.Id=m.DirectosId
join MovieActor as ma
on ma.MoviesId=m.Id
join Actors as a
on ma.ActorId=a.Id

--7
select m.Name,m.Rate,(d.Name+' '+d.Surname) as 'Diectors Fullname',(a.Name+' '+a.Surname) as 'Actor Fullname',g.Name as 'Genre'
from Movies as m
join Directos as d
on d.Id=m.DirectosId
join MovieActor as ma
on ma.MoviesId=m.Id
join Actors as a
on ma.ActorId=a.Id
join MovieGenre as mg
on mg.MoviesId=m.Id
join Genres as g
on g.Id=mg.GenresId


--4 

select count(*) from Movies

select d.Name,d.Surname, (select count(*) from Movies where Movies.DirectosId=d.Id) as'Movie Count' from Directos as d