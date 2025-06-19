CREATE DATABASE datdoanonline;
USE datdoanonline;

-- Tài khoản khách hàng
CREATE TABLE KHACH_HANG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap VARCHAR(50) UNIQUE,
    MatKhau VARCHAR(255),
    HoTen VARCHAR(100),
    SDT VARCHAR(15),
    DiaChi TEXT
);

-- Danh mục món ăn (ví dụ: Đồ ăn, Đồ uống)
CREATE TABLE DANH_MUC (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TenDanhMuc VARCHAR(100)
);

-- Món ăn
CREATE TABLE MON_AN (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TenMon VARCHAR(100),
    Gia DECIMAL(10,2),
    MoTa TEXT,
    ID_DanhMuc INT,
    FOREIGN KEY (ID_DanhMuc) REFERENCES DANH_MUC(ID)
);

-- Khuyến mãi
CREATE TABLE KHUYEN_MAI (
    ID INT PRIMARY KEY IDENTITY(1,1),
    MaKhuyenMai VARCHAR(20) UNIQUE,
    MoTa TEXT,
    GiamGia DECIMAL(5,2), -- Tính theo %
    NgayBatDau DATE,
    NgayKetThuc DATE
);

-- Đơn hàng
CREATE TABLE DON_HANG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ID_KhachHang INT,
    NgayDat DATETIME,
    TrangThai ENUM('DangXuLy', 'DangGiao', 'DaHoanThanh', 'DaHuy') DEFAULT 'DangXuLy',
    TongTien DECIMAL(10,2),
    MaKhuyenMai VARCHAR(20),
    FOREIGN KEY (ID_KhachHang) REFERENCES KHACH_HANG(ID),
    FOREIGN KEY (MaKhuyenMai) REFERENCES KHUYEN_MAI(MaKhuyenMai)
);

-- Chi tiết đơn hàng
CREATE TABLE CHI_TIET_DON_HANG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ID_DonHang INT,
    ID_MonAn INT,
    SoLuong INT,
    DonGia DECIMAL(10,2),
    FOREIGN KEY (ID_DonHang) REFERENCES DON_HANG(ID),
    FOREIGN KEY (ID_MonAn) REFERENCES MON_AN(ID)
);

-- Phản hồi khách hàng
CREATE TABLE PHAN_HOI (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ID_KhachHang INT,
    NoiDung TEXT,
    ThoiGian DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ID_KhachHang) REFERENCES KHACH_HANG(ID)
);

-- Dữ liệu mẫu KHACH_HANG
INSERT INTO KHACH_HANG (TenDangNhap, MatKhau, HoTen, SDT, DiaChi) VALUES
('huybui', '123456', 'Bùi Huy', '0901234567', '12 Nguyễn Văn Cừ, Hà Nội'),
('linhnguyen', '123456', 'Nguyễn Linh', '0902345678', '25 Lê Lợi, Huế'),
('manhtran', '123456', 'Trần Mạnh', '0911223344', '88 Hai Bà Trưng, Đà Nẵng'),
('quynhanh', '123456', 'Anh Quỳnh', '0933456677', '19 Lê Duẩn, TP.HCM');

-- DANH_MUC
INSERT INTO DANH_MUC (TenDanhMuc) VALUES
('Bún'),
('Phở'),
('Cơm'),
('Pizza'),
('Mì trộn'),
('Mì cay'),
('Gà'),
('Hoa quả dầm'),
('Nước ép'),
('Trà sữa');


-- MON_AN
INSERT INTO MON_AN (TenMon, Gia, MoTa, ID_DanhMuc) VALUES
('Bún đậu mắm tôm', 35000, 'bún, đậu, chả, lòng, nem', 1),
('Bún chả chan', 40000, 'Bún chả chan nước mắm', 1),
('Bún chả chấm', 35000, 'Bún chả chấm nước mắm', 1),
('Bún bò', 45000, 'Bát bún nước kèm thịt bò', 1),
('Bún ếch măng cay', 55000, 'Đùi ếch và măng', 1),
('Bún riêu cua', 45000, 'bún, riêu cua (được làm từ cua đồng xay, cà chua, đậu phụ rán, và các gia vị)', 1),
('Bún ốc', 65000, 'Bát bún nước kèm thịt ốc', 1),
('Phở bò', 45000, 'Bát nước có bánh phở và thịt bò', 2),
('Phở cuốn', 30000, 'Bánh phở cuốn với bánh đa nem', 2),
('Phở gà', 45000, 'Bát nước có bánh phở và thịt gà', 2),
('Phở xào', 45000, 'Bánh phở được xào', 2),
('Phở trộn', 65000, 'Bánh phở chín được trộn khô cùng topping', 2),
('Phở vịt quay Lạng Sơn', 105000, 'Vịt được quay tại Lạng Sơn', 2),
('Cơm rang dưa bò', 35000, 'Cơm sốt rang với trứng, hành, dưa muối, thịt bò', 3),
('Cơm rang dưa gà', 35000, 'Cơm sốt rang với trứng, hành, thịt gà', 3),
('Cơm thịt nướng', 40000, 'Cơm sốt rang với miếng thịt', 3),
('Cơm gà chiên mắm', 50000, 'Cơm gà giòn, nước mắm tỏi', 3),
('Cơm vịt quay', 60000, 'Cơm tran nước sốt, vịt quay', 3),
('Pizza gà', 45000, 'Pizza gà với màu sắc vô cùng bắt mắt ', 4),
('Pizza bò băm', 56000, 'best-seller', 4),
('Pizza hải sản', 80000, 'Pizza với nhiều hải sản', 4),
('Pizza Hawaii', 96000, 'pizza mang đậm hương vị biển Hawaii', 4),
('Pizza phô mai', 35000, 'Phô mai keo dẻo béo ngậy', 4),
('Mì trộn lạnh', 35000, 'Mì trộn tẩm đá', 5),
('Trứng ngâm sốt mì trộn', 45000, 'Mô tả giống tên món', 5),
('Mì trộn bò xào', 40000, 'Xào thịt bò xong trộn với mì', 5),
('Mì trộn rau củ chay', 30000, 'Thuộc loại món chay', 5),
('Mì trộn phô mai', 25000, 'Rẻ và béo', 5),
('Mì kim chi hải sản', 45000, 'kim chi, hải sản, mì', 6),
('Mì kim chi thập cẩm', 60000, 'thập cẩm nên có nhiều thứ', 6),
('Mì kim chi xúc xích cá viên', 35000, 'kim chi, xúc xích cá viên, mì', 6),
('Mì lẩu thái bò Seoul', 45000, 'mì lẩu thái nên cay lắm', 6),
('Mì kim chi hải sản', 45000, 'kim chi, hải sản, mì', 6),
('Mì lẩu thái thập cẩm Seoul', 60000, 'cay và có nhiều topping', 6),
('Canh gà đậu nấm kim châm', 45000, 'nó là canh', 7),
('Ức gà sốt cam', 60000, 'Ức gà ăn rất khó', 7),
('Mộn gà', 55000, 'nộm đó', 7),
('gà rang gừng nghệ', 100000, 'gà rang cùng gừng và nghệ', 7),
('Cà ri gà', 105000, 'Đùi gà với cà ri', 7),
('Mít thái dầm sữa chua', 15000, 'ngon', 8),
('Dưa vàng dầm sữa chua hạt chia', 25000, 'ngon, bổ', 8),
('Xoài cóc dầm', 30000, 'chua, cay, mặn', 8),
('Thanh long dầm bột Socola nguyên chất',45000, 'lạ', 8),
('Nước ép dưa hấu cờ đỏ sao vàng', 35000, 'Dưa hấu ép nguyên chất, ít đường', 9),
('Nước ép cam', 30000, 'Cam ép nguyên chất', 9),
('Nước ép lê mix với cần tây&ớt chuông', 55000, 'Nước ép độc lạ nguyên chất', 9),
('Nước ép lựu', 35000, 'Mùa lựu hơi hiếm nên đắt', 9),
('Nước ép bưởi', 15000, 'Ngon bổ rẻ', 9),
('Trà sữa trân châu đường đen', 30000, 'Trà sữa truyền thống với trân châu đen', 10),
('Trà sữa bạc hà', 35000, 'Trà sữa với nước bạc hà', 10),
('Trà sữa khoai môn', 40000, 'Trà sữa kèm khoai môn dằm', 10),
('Trà sữa Pudding đậu đỏ', 50000, 'Trà sữa, pudding đậu đỏ', 10),


-- KHUYEN_MAI
INSERT INTO KHUYEN_MAI (MaKhuyenMai, MoTa, GiamGia, NgayBatDau, NgayKetThuc) VALUES
('DEM10', 'Giảm 10% cho đơn hàng sau 22h', 10, '2025-06-01', '2025-12-31'),
('TANSINHVIEN', 'Giảm 20% cho đơn đầu tiên', 20, '2025-06-01', '2025-09-30'),
('FREESHIP', 'Giảm 15% cho đơn trên 100k', 15, '2025-06-01', '2025-12-31');

-- DON_HANG
INSERT INTO DON_HANG (ID_KhachHang, NgayDat, TrangThai, TongTien, MaKhuyenMai) VALUES
(1, '2025-06-19 22:30:00', 'DaHoanThanh', 81000, 'DEM10'),
(2, '2025-06-18 21:10:00', 'DangGiao', 89000, NULL),
(3, '2025-06-19 00:10:00', 'DangXuLy', 102000, 'FREESHIP'),
(4, '2025-06-17 23:50:00', 'DaHuy', 50000, NULL),
(1, '2025-06-19 01:00:00', 'DaHoanThanh', 72000, 'TANSINHVIEN');

-- CHI_TIET_DON_HANG
INSERT INTO CHI_TIET_DON_HANG (ID_DonHang, ID_MonAn, SoLuong, DonGia) VALUES
(1, 1, 1, 45000),
(1, 3, 1, 30000),
(1, 5, 1, 20000),
(2, 8, 1, 89000),
(3, 2, 2, 50000),
(3, 6, 1, 22000),
(4, 2, 1, 50000),
(5, 7, 1, 99000);

-- PHAN_HOI
INSERT INTO PHAN_HOI (ID_KhachHang, NoiDung, ThoiGian) VALUES
(1, 'Đồ ăn ngon, giao hàng nhanh!', ()),
(2, 'Trà sữa hơi ngọt quá, nhưng vẫn ổn.', NOW()),
(3, 'Combo nhiều món, giá hợp lý.', NOW()),
(4, 'Món gà bị nguội khi nhận, cần cải thiện.', NOW());
