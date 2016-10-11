

insert into WS_User values ('NhienLH','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('Nghiadt','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('tuanha','e10adc3949ba59abbe56e057f20f883e',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('Duytn','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('tuandv','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')
insert into WS_User values ('black_snow','123456',0,1,1,'2026-11-11 11:12:01','2026-11-11 11:12:01','blacksnow055@gmail.com','MNLS3ABCD')


--========================================================================================================================================
insert into User_information values (1,N'Lê Hồng Nhiên','Images/avatar1.png',N'Thạch Thất-Hà Nội',22,'01653778760','M',N'Việt Nam','facebook.com/black.snow.750','',N'Nhiên đẹp trai',30)
insert into User_information values (2,N'Đào Trọng Nghĩa','Images/avatar2.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)
insert into User_information values (3,N'Đào Trọng Nghĩa','Images/avatar3.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)
insert into User_information values (4,N'Đào Trọng Nghĩa','Images/avatar4.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)
insert into User_information values (5,N'Đào Trọng Nghĩa','Images/avatar.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)
insert into User_information values (6,N'Nhiên đẹp trai','Images/avatar.png',N'Thạch Thất-Hà Nội',22,'0973842962','M',N'Việt Nam','facebook.com/Nghiadthp','',N'Nghĩa thộn',30)


--========================================================================================================================================
insert into Organazation values(1,N'Tổ chức Chữ Thâp đỏ',N'Tổ chức những sự kiện Y tế','http://hd.wallpaperswide.com/thumbs/red_cross_japan_relief-t2.jpg','01653778760','blacksnow055@gmail.com',N'Thạch Thất-Hà Nội',1,11)
insert into Organazation values(2,N'Tổ chức vì trẻ em',N'Tổ chức những sự kiện từ thiện vì trẻ em','http://hd.wallpaperswide.com/thumbs/baby_15-t2.jpg','01653778760','blacksnow055@gmail.com',N'Thạch Thất-Hà Nội',1,22)
insert into Organazation values(3,N'Tổ chức vì Giáo dục',N'Tổ chức những sự kiện từ thiện vì Giáo dục','http://hd.wallpaperswide.com/thumbs/atlas_book-t2.jpg','01653778760','blacksnow055@gmail.com',N'Thạch Thất-Hà Nội',1,33)


--========================================================================================================================================
insert into EventType values(N'Giáo dục',N'Tổ chức những nội dung giáo dục ')
insert into EventType values(N'Văn Hóa',N'Tổ chức những nội dung văn hóa')
insert into EventType values(N'Y tế',N'Tổ chức những nội dung giáo dục về y tế')


--========================================================================================================================================
insert into Event (CreatorID, EventType, EventName, Created_Date, Start_Date, Finish_Date, Updated_Date, Description, Location, VideoUrl, TotalPoint, Status, Views, Likes)values
(1,3,N'Vì trẻ em bị bệnh tim','2016-1-1','2016-1-1','2016-6-1','2026-5-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','hà nội','https://youtu.be/tx4qLvExBtU',100,1,230,2300),
(3,1,N'Xây trường học cho học sinh nghèo miền núi Tây Bắc','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','hà nội','https://youtu.be/tx4qLvExBtU',200,1,230,2332),
(2,2,N'Một trăm nghìn chiếc áo trắng','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','hà nội','https://youtu.be/tx4qLvExBtU',150,1,235,2320),

(1,3,N'1000 ca phẫu thuật tim miễn phí','2016-6-1','2017-6-1','2016-6-1','2026-12-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','hà nội','https://youtu.be/tx4qLvExBtU',100,1,232,1200),
(3,1,N'1000 căn nhà cho cựu chiến binh hoàn cảnh khó khắn','2016-6-1','2016-6-1','2017-6-1','2026-12-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','hà nội','https://youtu.be/tx4qLvExBtU',200,1,564,2965),
(2,2,N'1000 bộ sách mới cho trẻ em ở làng trẻ SOS Hoa Phượng','2016-6-1','2016-6-1','2017-6-1','2026-12-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','hà nội','https://youtu.be/tx4qLvExBtU',150,1,230,2323),

(1,3,N'2000 ca phẫu thuật tim miễn phí','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','hà nội','https://youtu.be/tx4qLvExBtU',100,1,130,1300),
(3,1,N'2000 căn nhà cho cựu chiến binh hoàn cảnh khó khắn','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','hà nội','https://youtu.be/tx4qLvExBtU',200,1,130,2420),
(2,2,N'2000 bộ sách mới cho trẻ em vùng cao','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','hà nội','https://youtu.be/tx4qLvExBtU',150,1,230,4300),

(1,3,N'Vì trẻ em bị bệnh tim','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','hà nội','https://youtu.be/tx4qLvExBtU',100,1,230,2300),
(3,1,N'Xây trường học cho học sinh nghèo miền núi Tây Bắc','2016-6-1','2016-1-1','2016-6-1','2026-5-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','hà nội','https://youtu.be/tx4qLvExBtU',200,1,230,2332),
(2,2,N'Một trăm nghìn chiếc áo trắng','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','hà nội','https://youtu.be/tx4qLvExBtU',150,1,235,2320),

(1,3,N'1000 ca phẫu thuật tim miễn phí','2016-6-1','2017-6-1','2016-6-1','2026-12-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','hà nội','https://youtu.be/tx4qLvExBtU',100,1,232,1200),
(3,1,N'1000 căn nhà cho cựu chiến binh hoàn cảnh khó khắn','2016-6-1','2016-6-1','2017-6-1','2026-12-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','hà nội','https://youtu.be/tx4qLvExBtU',200,1,564,2965),
(2,2,N'1000 bộ sách mới cho trẻ em ở làng trẻ SOS Hoa Phượng','2016-6-1','2016-6-1','2017-6-1','2026-12-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','hà nội','https://youtu.be/tx4qLvExBtU',150,1,230,2323),

(1,3,N'2000 ca phẫu thuật tim miễn phí','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Tổ chức những sự kiện cho trẻ em bị bệnh tim trên khắp đất nước, Ncần những nhà hảo tâm quyên góp.','hà nội','https://youtu.be/tx4qLvExBtU',100,1,130,1300),
(3,1,N'2000 căn nhà cho cựu chiến binh hoàn cảnh khó khắn','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Kêu gọi xây dựng quỹ để có thể xây trường học cho các học sinh nghèo vùng núi phía Tây Bắc','hà nội','https://youtu.be/tx4qLvExBtU',200,1,130,2420),
(2,2,N'2000 bộ sách mới cho trẻ em vùng cao','2016-1-1','2016-6-1','2016-6-1','2026-5-1',N'Mong được các nhà hảo tâm ủng hộ tiền hoặc áo trắng giúp cho các em học sinh nghèo có tà áo mới đón ngày khai trường','hà nội','https://youtu.be/tx4qLvExBtU',150,1,230,4300)




--========================================================================================================================================



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
(3,N'ReUpload: Nghệ thuật ca trù ngày một mai một',N'Mong các nhà hảo tâm cũng như các tổ chức quan tâm chung tay bảo tồn di sản văn hóa dân tộc','https://youtu.be/tx4qLvExBtU','2016-7-7','2016-7-10',11,111,1),

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

insert into ThreadAlbumImage(ThreadId,ImageUrl,status) values
(1,'http://hd.wallpaperswide.com/thumbs/unicef_haiti_wallpaper_2560x1440-t2.jpg',1),
(1,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(2,'http://hd.wallpaperswide.com/thumbs/homeless_armenian_qristine-t2.jpg',1),
(2,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(3,'http://hd.wallpaperswide.com/thumbs/green_united_states_of_america-t2.jpg',1),
(3,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(4,'http://hd.wallpaperswide.com/thumbs/earth_day-t2.jpg',1),
(4,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(5,'http://hd.wallpaperswide.com/thumbs/earth_day_2-t2.jpg',1),
(5,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(6,'http://hd.wallpaperswide.com/thumbs/help_for_a_stormtrooper-t2.jpg',1),
(6,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(7,'http://hd.wallpaperswide.com/thumbs/save_the_earth-t2.jpg',1),
(7,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(8,'http://hd.wallpaperswide.com/thumbs/the_world_is_yours-t2.jpg',1),
(8,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(9,'http://hd.wallpaperswide.com/thumbs/humanity-t2.jpg',1),
(9,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(10,'http://hd.wallpaperswide.com/thumbs/help_for_a_stormtrooper-t2.jpg',1),
(10,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(11,'http://hd.wallpaperswide.com/thumbs/save_the_earth-t2.jpg',1),
(11,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(12,'http://hd.wallpaperswide.com/thumbs/the_world_is_yours-t2.jpg',1),
(12,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(13,'http://hd.wallpaperswide.com/thumbs/save_the_earth-t2.jpg',1),
(13,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(14,'http://hd.wallpaperswide.com/thumbs/the_world_is_yours-t2.jpg',1),
(14,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(15,'http://hd.wallpaperswide.com/thumbs/humanity-t2.jpg',1),
(15,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(16,'http://hd.wallpaperswide.com/thumbs/help_for_a_stormtrooper-t2.jpg',1),
(16,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(17,'http://hd.wallpaperswide.com/thumbs/save_the_earth-t2.jpg',1),
(17,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1),
(18,'http://hd.wallpaperswide.com/thumbs/the_world_is_yours-t2.jpg',1),
(18,'http://hd.wallpaperswide.com/thumbs/i_love_you_3-t2.jpg',1)

--========================================================================================================================================
insert into EventAlbumImage( EventId, ImageUrl, status) values
(1,'http://hd.wallpaperswide.com/thumbs/young_fellows-t2.jpg',1),
(1,'',0),
(2,'http://hd.wallpaperswide.com/thumbs/cute_husky_puppy-t2.jpg',1),
(2,'',0),
(3,'http://hd.wallpaperswide.com/thumbs/lovely_playful_kitten-t2.jpg',1),
(3,'',0),
(4,'http://hd.wallpaperswide.com/thumbs/cat_lying_on_back-t2.jpg',1),
(4,'',0),
(5,'http://hd.wallpaperswide.com/thumbs/funny_lazy_cat-t2.jpg',1),
(5,'',0),
(6,'http://hd.wallpaperswide.com/thumbs/cat_118-t2.jpg',1),
(6,'',0),
(7,'http://hd.wallpaperswide.com/thumbs/red_fox_face-t2.jpg',1),
(7,'',0),
(8,'http://hd.wallpaperswide.com/thumbs/best_hd-t2.jpg',1),
(8,'',0),
(9,'http://hd.wallpaperswide.com/thumbs/french_bulldog_2-t2.jpg',1),
(9,'',0),
(10,'http://hd.wallpaperswide.com/thumbs/help_for_a_stormtrooper-t2.jpg',1),
(10,'',0),
(11,'http://hd.wallpaperswide.com/thumbs/save_the_earth-t2.jpg',1),
(11,'',0),
(12,'http://hd.wallpaperswide.com/thumbs/the_world_is_yours-t2.jpg',1),
(12,'',0),
(13,'http://hd.wallpaperswide.com/thumbs/save_the_earth-t2.jpg',1),
(13,'',0),
(14,'http://hd.wallpaperswide.com/thumbs/the_world_is_yours-t2.jpg',1),
(14,'',0),
(15,'http://hd.wallpaperswide.com/thumbs/humanity-t2.jpg',1),
(15,'',0),
(16,'http://hd.wallpaperswide.com/thumbs/help_for_a_stormtrooper-t2.jpg',1),
(16,'',0),
(17,'http://hd.wallpaperswide.com/thumbs/save_the_earth-t2.jpg',1),
(17,'',0),
(18,'http://hd.wallpaperswide.com/thumbs/the_world_is_yours-t2.jpg',1),
(18,'',0)

--========================================================================================================================================





--========================================================================================================================================