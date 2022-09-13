create database opmo
go
use opmo
go

create table Kupac(
IDKupca int primary key identity(1,1) not null,
Ime nvarchar(20) not null,
Prezime nvarchar(20) not null,
Adresa nvarchar(20) not null,
Telefon varchar(20) not null
)

create table Prodavac(
IDProdavca int primary key identity(1,1) not null,
Ime nvarchar(20) not null,
Prezime nvarchar(20) not null
)

create table Dostavljac(
IDDostavljaca int primary key identity(1,1) not null,
Ime nvarchar(20) not null,
Prezime nvarchar(20) not null,
Telefon varchar(10) not null
)

create table Instrument(
IDInstrumenta int primary key identity(1,1) not null,
Vrsta nvarchar(20) not null,
Model nvarchar(20) not null,
Cena varchar(20) not null
)

create table Porudzbina(
IDPorudzbine int primary key identity(1,1) not null,
Racun varchar(20) not null,
Datum date not null
)

alter table Kupac
add IDProdavca int references Prodavac(IDProdavca)
on delete no action
on update no action

alter table Kupac
add IDPorudzbine int references Porudzbina(IDPorudzbine)
on delete no action
on update no action

alter table Kupac
add IDInstrumenta int references Instrument(IDInstrumenta)
on delete no action
on update no action

alter table Prodavac
add IDInstrumenta int references Instrument(IDInstrumenta)
on delete no action
on update no action

alter table Dostavljac
add IDKupca int references Kupac(IDKupca)
on delete no action
on update no action

alter table Dostavljac
add IDPorudzbine int references Porudzbina(IDPorudzbine)
on delete no action
on update no action

alter table Porudzbina
add IDProdavca int references Prodavac(IDProdavca)
on delete no action
on update no action

alter table Porudzbina
add IDInstrumenta int references Instrument(IDInstrumenta)
on delete cascade
on update cascade