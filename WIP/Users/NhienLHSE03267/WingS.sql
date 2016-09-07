create database WingS
create table WS_User
(
UserID int identity(1,1) not null,
UserName varchar (24),
UserPassword varchar(24),
AccountType int,
Primary key (UserID),
)
create table User_information
(
 UserID int not null Primary key Foreign key references WS_User(UserID),
 FullName varchar (100),
 Address varchar (256),
 Age int,
 Email varchar(100),
 Phone varchar(11),
 Gender char(1),
 OrgnazationIDFollow varchar(1000),
 Point int,
)
create table Organization_Information
(
 OrganazationID int not null Primary key Foreign key references WS_User(UserID),
 OrganizationName varchar(100),
 Introduction varchar(8000),
 Phone varchar(11),
 Email varchar(100),
 Address varchar (256),
 UserMemberID varchar(1000),
 Status bit, 
)

create table Organization_Joining_Request
(
 OrganazationJoiningRequestID int identity(1,1) not null primary key,
 UserID int  foreign key references WS_User(UserID),
 OrganazationID int  foreign key references Organization_Information(OrganazationID),
)