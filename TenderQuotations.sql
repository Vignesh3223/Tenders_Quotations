create database TenderQuotations

use TenderQuotations

create table Roles
(
RoleId int primary key identity(1,1),
RoleName varchar(10)
)

insert into Roles values('Admin'),('User')

create table Users
(
UserId int primary key identity(1,1),
CompanyName varchar(60),
Proprieator varchar(30),
Email varchar(75),
Address varchar(200),
ContactNumber varchar(15),
CompanySector varchar(30),
EstablishedDate varchar(20),
GSTIN varchar(15),
CRN int,
Password varchar(100),
ProfilePic varbinary(max),
RoleId int references Roles(RoleId)
)

create table Category
(
CategoryId int primary key identity(1,1),
CategoryName varchar(40)
)

insert into Category values('Manufacture'),('Construction'),('Healthcare'),('Transportation'),('Education'),
('Environmental'),('Communication')


create table Tenders 
(
TenderId int primary key identity(1,1),
TenderName varchar(50),
Referencenumber varchar(30), 
Description varchar(500),
CategoryId int references Category(CategoryId),
ProjectValue int,
TenderOpeningDate datetime,
TenderClosingDate datetime,
Location varchar(50),
Authority varchar(150),
ProjectStartDate datetime,
ProjectEndDate datetime,
ApplicationFee int
)

create table Quotations
(
QuotationId int primary key identity(1,1),
UserId int references Users(UserId),
CompanyName varchar(60),
Proprieator varchar(30),
TenderId int references Tenders(TenderId),
TenderName varchar(50),
Location varchar(50),
Authority varchar(150),
ProjectStartDate datetime,
ProjectEndDate datetime,
QuotedDate datetime,
QuoteAmount int
)

create table TendersTaken
(
TakenId int primary key identity(1,1),
QuotationId int references Quotations(QuotationId),
TenderName varchar(50),
CompanyName varchar(60),
Proprieator varchar(30),
QuoteAmount int,
Location varchar(50),
Authority varchar(150),
ProjectStartDate datetime,
ProjectEndDate datetime
)

create table Ads
(
AdId int primary key identity(1,1),
AdTitle varchar(100),
AdPoster varchar(150)
)

create trigger EncryptPassword 
on Users
after insert as begin
update Users set Password = ENCRYPTBYPASSPHRASE('PublicTenders',inserted.Password) from inserted where Users.UserId = inserted.UserId
end

alter table Users add LastLoginDate datetime

create Procedure [dbo].[Validate_User]
@Email nvarchar(75),
@Password nvarchar(100)
as begin
set nocount on
declare @UserId int,@LastLoginDate datetime,@RoleId int
select @UserId = UserId,@LastLoginDate = LastLoginDate,@RoleId = RoleId from Users where Email = @Email and 
Convert(varchar(100),DECRYPTBYPASSPHRASE('PublicTenders',Users.Password))= @Password
if @UserId is not null
begin
update Users set LastLoginDate = GETDATE() where UserId = @UserId
select @UserId[UserId],(select RoleName from Roles where RoleId = @RoleId)[Roles]
end
else
begin
select -1[UserId],''[Roles]
end
end

select * from Roles
select * from Users
select * from Category
select * from Tenders
select * from Quotations
select * from TendersTaken
select * from Ads

alter trigger TenderGiven
ON TendersTaken
after insert as begin
    update Tenders set IsTaken = 1 
    where TenderId in (select TenderId from inserted)
end
