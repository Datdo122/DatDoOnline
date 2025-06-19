CREATE DATABASE datdoan
use datdoan

CREATE TABLE DANH_MUC (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TenDanhMuc VARCHAR(100)
);

CREATE TABLE MON_AN (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TenMon VARCHAR(100),
    Gia DECIMAL(10,2),
    MoTa TEXT,
    ID_DanhMuc INT,
    FOREIGN KEY (ID_DanhMuc) REFERENCES DANH_MUC(ID)
);

create table GioHang (
	tenmon nvarchar(50),
	giaban float,
	soluong int
);


-- Bảng DonHang
CREATE TABLE DonHang (
    iddonhang INT PRIMARY KEY,        -- Khóa chính
    tongtien FLOAT,
    trangthai NVARCHAR(50),
    create_at DATETIME DEFAULT GETDATE()
);

-- Bảng HoaDon (liên kết với DonHang)
CREATE TABLE HoaDon (
    idnguoidung INT,
    iddonhang INT,
    idmon INT,
    soluong INT DEFAULT 1,
    FOREIGN KEY (iddonhang) REFERENCES DonHang(iddonhang)
);


select * from MON_AN

CREATE PROCEDURE ThemOrCapNhatGioHang
    @iddonhang INT,
    @idmon INT,
    @soluong INT
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM HoaDon 
        WHERE iddonhang = @iddonhang AND idmon = @idmon
    )
    BEGIN
        -- Nếu số lượng <= 0 thì xóa món khỏi giỏ
        IF @soluong <= 0
        BEGIN
            DELETE FROM HoaDon
            WHERE iddonhang = @iddonhang AND idmon = @idmon;
        END
        ELSE
        BEGIN
            -- Cập nhật số lượng mới
            UPDATE HoaDon
            SET soluong = @soluong
            WHERE iddonhang = @iddonhang AND idmon = @idmon;
        END
    END
    ELSE
    BEGIN
        -- Chỉ thêm mới nếu số lượng hợp lệ
        IF @soluong > 0
        BEGIN
            INSERT INTO HoaDon(iddonhang, idmon, soluong)
            VALUES ( @iddonhang, @idmon, @soluong);
        END
    END
END


CREATE PROCEDURE XemGioHang
    @iddonhang INT
AS
BEGIN
    SELECT 
        ma.tenmon,
        hd.soluong
    FROM HoaDon hd
    JOIN MON_AN ma ON hd.idmon = ma.ID
    WHERE hd.iddonhang = @iddonhang;
END

CREATE PROCEDURE TinhTongTien
	@iddonhang INT
AS
BEGIN
    SELECT SUM(ma.Gia * hd.soluong) AS tongtien
    FROM HoaDon hd
    JOIN MON_AN ma ON hd.idmon = ma.ID
    WHERE hd.iddonhang = @iddonhang;
END
