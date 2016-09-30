
insert into WS_User values ('NhienLH','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('Nghiadt','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('Nhienlh','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('Duytn','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('tuandv','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('black_snow','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')


--========================================================================================================================================
insert into User_information values (1,N'Lê Hồng Nhiên','Images/avatar1.png',N'Thạch Thất-Hà Nội',22,'01653778760','M',N'Việt Nam','facebook.com/black.snow.750','',N'Nhiên đẹp trai',30)
insert into User_information values (2,N'Đào Trọng Nghĩa','Images/avatar2.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)
insert into User_information values (3,N'Đào Trọng Nghĩa','Images/avatar3.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)
insert into User_information values (4,N'Đào Trọng Nghĩa','Images/avatar4.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)
insert into User_information values (5,N'Đào Trọng Nghĩa','Images/avatar.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)


--========================================================================================================================================
insert into Organazation values(1,N'Tổ chức Chữ Thâp đỏ',N'Tổ chức những sự kiện Y tế','','01653778760','blacksnow055@gmail.com',N'Thạch Thất-Hà Nội',1,130)
insert into Organazation values(2,N'Tổ chức vì trẻ em',N'Tổ chức những sự kiện từ thiện vì trẻ em','','01653778760','blacksnow055@gmail.com',N'Thạch Thất-Hà Nội',1,130)
insert into Organazation values(3,N'Tổ chức vì Giáo dục',N'Tổ chức những sự kiện từ thiện vì Giáo dục','','01653778760','blacksnow055@gmail.com',N'Thạch Thất-Hà Nội',1,130)


--========================================================================================================================================
insert into EventType values(N'Giáo dục',N'Tổ chức những nội dung giáo dục ')
insert into EventType values(N'Văn Hóa',N'Tổ chức những nội dung văn hóa')
insert into EventType values(N'Y tế',N'Tổ chức những nội dung giáo dục về y tế')


--========================================================================================================================================
insert into Event (CreatorID, EventType, EventName, Created_Date, Finish_Date, Updated_Date, Description, VideoUrl, TotalPoint, Status)values
(1,3,N'Vì trẻ em bị bệnh tim','2016-1-1','2016-6-1','2026-5-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','https://youtu.be/tx4qLvExBtU',100,1),
(3,1,N'Xây trường học cho học sinh nghèo miền núi Tây Bắc','2016-1-1','2016-6-1','2026-5-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','https://youtu.be/tx4qLvExBtU',200,1),
(2,2,N'Một trăm nghìn chiếc áo trắng','2016-1-1','2016-6-1','2026-5-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','https://youtu.be/tx4qLvExBtU',150,1),

(1,3,N'1000 ca phẫu thuật tim miễn phí','2016-6-1','2017-6-1','2026-12-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','https://youtu.be/tx4qLvExBtU',100,1),
(3,1,N'1000 căn nhà cho cựu chiến binh hoàn cảnh khó khắn','2016-6-1','2017-6-1','2026-12-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','https://youtu.be/tx4qLvExBtU',200,1),
(2,2,N'1000 bộ sách mới cho trẻ em ở làng trẻ SOS Hoa Phượng','2016-6-1','2017-6-1','2026-12-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','https://youtu.be/tx4qLvExBtU',150,1),

(1,3,N'2000 ca phẫu thuật tim miễn phí','2016-1-1','2016-6-1','2026-5-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','https://youtu.be/tx4qLvExBtU',100,1),
(3,1,N'2000 căn nhà cho cựu chiến binh hoàn cảnh khó khắn','2016-1-1','2016-6-1','2026-5-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','https://youtu.be/tx4qLvExBtU',200,1),
(2,2,N'2000 bộ sách mới cho trẻ em vùng cao','2016-1-1','2016-6-1','2026-5-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','https://youtu.be/tx4qLvExBtU',150,1)



--========================================================================================================================================
insert into Event_Statistic(EventId, Views, Likes) values
(1,11,11),
(2,22,22),
(3,33,33),
(4,44,44),
(5,55,55),
(6,66,66),
(7,77,77),
(8,88,88),
(9,99,99)



--========================================================================================================================================

insert into Thread( UserId, Title,  Content, VideoUrl, CreatedDate, UpdatedDate, Likes,  Views , [Status]) values
(1,N'Mẹ Việt Nam anh hùng Nguyễn Thị Bảy sống trong căn nhà sập sệ trục năm nay',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm tới trường hợp của mẹ Việt Nam anh hùng Nguyễn Thị Bảy, giúp mẹ có một tuổi già được an bình','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',11,111,1),
(2,N'Cuộc sống khó khăn của chàng trai khuyết tập đánh giày nuôi mẹ',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm tới trường hợp của của anh, giúp mẹ có một cuộc sống được an bình','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',22,222,1),
(3,N'Nghệ thuật ca trù ngày một mai một',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm chung tay bảo tồn di sản văn hóa dân tộc','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',66,666,1),
(4,N'Em Tạ Ngọc Duy đang cần sự giúp đỡ của các nhà hảo tâm',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm tới trường hợp của em, giúp em  có một tuổi già được an bình','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',33,333,1),
(5,N'Em Lê Hồng Nhiên đang trong hoàn cảnh ngàn cân treo sợi tóc',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm tới trường hợp của em, giúp em  có một tuổi già được an bình','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',44,444,1),
(6,N'Em Tuấn Đổ đang trong cơn nguy kịch vì mỗi cắn chỗ hiểm',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm tới trường hợp của em, giúp em  có một tuổi già được an bình','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',55,555,1),

(1,N'ReUpload: Mẹ Việt Nam anh hùng Nguyễn Thị Bảy sống trong căn nhà sập sệ trục năm nay',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm tới trường hợp của mẹ Việt Nam anh hùng Nguyễn Thị Bảy, giúp mẹ có một tuổi già được an bình','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',11,111,1),
(2,N'ReUpload: Cuộc sống khó khăn của chàng trai khuyết tập đánh giày nuôi mẹ',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm tới trường hợp của của anh, giúp mẹ có một cuộc sống được an bình','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',22,222,1),
(3,N'ReUpload: Nghệ thuật ca trù ngày một mai một',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm chung tay bảo tồn di sản văn hóa dân tộc','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',11,111,1)


--========================================================================================================================================

