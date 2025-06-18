create database datdoanonline

create table Users (
	idnguoidung int primary key identity(1, 1),
	tennguoidung nvarchar(50),
	pw nvarchar(255),
	role int,
	mahoadon int
);

create table ThongTin_Users (
	idnguoidung int,
	hoten nvarchar(200),
	sdt nvarchar(20),
	email nvarchar(100),
	date datetime,
	diachi nvarchar(200),
	create_at DATETIME DEFAULT GETDATE(),
	foreign key (idnguoidung) references Users(idnguoidung)
);

create table Menu (
	idmon int primary key,
	tenmon nvarchar(100),
	loaimon nvarchar(50),
	giaban float,
	mota nvarchar(20),
	hinhanh nvarchar(50),
	trangthai nvarchar(50)
);

create table DonHang (
	iddonhang int primary key,
	tongtien float,
	trangthai nvarchar(50),
	create_at DATETIME DEFAULT GETDATE()
);

create table HoaDon (
	idnguoidung int unique,
	iddonhang int,
	idmon int,
	foreign key (idnguoidung) references Users(idnguoidung),
	foreign key (iddonhang) references DonHang(iddonhang),
);

create table GioHang (
	tenmon nvarchar(50),
	giaban float,
	soluong int
);

select * from DonHang

insert into HoaDon (idnguoidung, iddonhang, idmon) values
	(1, 1, 1),
	(1, 1, 2)

insert into DonHang (iddonhang, trangthai) values 
	(1, N'Đã Nhận Hàng')

INSERT INTO Menu (idmon, tenmon, loaimon, giaban, mota, hinhanh, trangthai)
VALUES 
(1, N'Phở Bò', N'Món nước', 50000, N'Ngon bổ rẻ', N'pho_bo.jpg', N'Còn hàng'),
(2, N'Cơm Tấm', N'Món chính', 45000, N'Cơm ngon', N'com_tam.jpg', N'Còn hàng'),
(3, N'Bún Chả', N'Món nước', 60000, N'Món Hà Nội', N'bun_cha.jpg', N'Còn hàng'),
(4, N'Gỏi Cuốn', N'Món khai vị', 30000, N'Rất tươi', N'goi_cuon.jpg', N'Còn hàng'),
(5, N'Bánh Mì Thịt', N'Món ăn nhanh', 20000, N'Rất tiện', N'banh_mi_thit.jpg', N'Còn hàng'),
(6, N'Cháo Gà', N'Món nước', 40000, N'Nóng hổi', N'chao_ga.jpg', N'Còn hàng'),
(7, N'Lẩu Thái', N'Món chính', 250000, N'Chua cay', N'lau_thai.jpg', N'Còn hàng'),
(8, N'Bánh Xèo', N'Món chính', 50000, N'Vàng giòn', N'banh_xeo.jpg', N'Còn hàng'),
(9, N'Mì Quảng', N'Món nước', 60000, N'Món miền Trung', N'mi_quang.jpg', N'Còn hàng'),
(10, N'Bún Bò Huế', N'Món nước', 55000, N'Món Huế', N'bun_bo_hue.jpg', N'Còn hàng'),
(11, N'Sinh Tố Dâu', N'Đồ uống', 25000, N'Tươi mát', N'sinh_to_dau.jpg', N'Còn hàng'),
(12, N'Cà Phê Sữa', N'Đồ uống', 20000, N'Đậm đà', N'ca_phe_sua.jpg', N'Còn hàng'),
(13, N'Nước Cam', N'Đồ uống', 30000, N'Bổ dưỡng', N'nuoc_cam.jpg', N'Còn hàng'),
(14, N'Kem Dừa', N'Món tráng miệng', 40000, N'Mát lạnh', N'kem_dua.jpg', N'Còn hàng'),
(15, N'Chè Thái', N'Món tráng miệng', 35000, N'Ngọt lịm', N'che_thai.jpg', N'Còn hàng');
s
--monnuoc, monchinh, monkhaivi, montrangmieng, douong

