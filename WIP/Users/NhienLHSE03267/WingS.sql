create database WingS
create table WS_User
(
UserID int identity(1,1) not null,
UserName varchar (24),
UserPassword varchar(24),
AccountType bit,
IsActive bit,
CreatedDate varchar(30),
LastLogin varchar(30),
Primary key (UserID),
Email varchar(100),
VerifyCode varchar(100),
)
create table User_information
(
 UserID int not null Primary key Foreign key references WS_User(UserID),
 FullName nvarchar (100),
 ProfileImage varchar(100),
 UserAddress varchar (256),
 Age int,
 Phone varchar(11),
 Gender char(1),
 Country nvarchar(100),
 FacebookUrl varchar(100),
 OrgnazationIDFollow varchar(1000),
 UserSignature nvarchar(200),
 Point int,
)
drop table User_information
insert into WS_User values ('NhienLH','123456',0,1,'14/09/2016 3:14pm','14/09/2016 3:14pm','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('NghiaDT','1234561@',1,1,'14/09/2016 3:14pm','14/09/2016 3:14pm','nghiadt@gmail.com','MNLS3ABCDEF')
insert into WS_User values ('DuyTN','123456',0,1,'14/09/2016 3:14pm','14/09/2016 3:14pm','duytn@gmail.com','MNLS3ABCD')
insert into WS_User values ('TuanDv','123456',0,1,'14/09/2016 3:14pm','14/09/2016 3:14pm','blacksnow055@gmail.com','MNLS3ABCDE')
insert into WS_User values ('TuanHA','123456',0,1,'14/09/2016 3:14pm','14/09/2016 3:14pm','blacksnow055@gmail.com','MNLS3ABCDVF')

insert into User_information values (1,'Lê Hồng Nhiên','Images/avatar1.png','Thạch Thất-Hà Nội',22,'01653778760','M','Việt Nam','facebook.com/black.snow.750','','Nhiên đẹp trai',30)
insert into User_information values (2,'Đào Trọng Nghĩa','Images/avatar2.png','Thạch Thất-Hà Nội',22,'0973842962','M','Việt Nam','facebook.com/Nghiadthp','','Nghĩa thộn',30)
insert into User_information values (3,'Đào Trọng Nghĩa','Images/avatar3.png','Thạch Thất-Hà Nội',22,'0973842962','M','Việt Nam','facebook.com/Nghiadthp','','Nghĩa thộn',30)
insert into User_information values (4,'Đào Trọng Nghĩa','Images/avatar4.png','Thạch Thất-Hà Nội',22,'0973842962','M','Việt Nam','facebook.com/Nghiadthp','','Nghĩa thộn',30)
insert into User_information values (5,'Đào Trọng Nghĩa','Images/avatar.png','Thạch Thất-Hà Nội',22,'0973842962','M','Việt Nam','facebook.com/Nghiadthp','','Nghĩa thộn',30)

select * from WS_User