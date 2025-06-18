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
	create_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	foreign key (idnguoidung) references Users(idnguoidung)
);



