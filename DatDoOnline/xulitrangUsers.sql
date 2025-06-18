--1.Đẩy dữ liệu lên bảng menu

create view V_DonHang
as
		select tenmon, loaimon, giaban, trangthai from Menu

select * from V_DonHang
drop view DonHang


--2.đẩy dữ liệu bảng hóa đơn
	--tinh va them tong tien theo hoa don
create proc sp_tongtienhoadon
	@idnguoidung int,
	@iddonhang int
as
	begin
		declare @tongtien float 
			select @tongtien = sum (Menu.giaban) 
			from HoaDon
			inner join Menu on HoaDon.idmon = Menu.idmon
			where HoaDon.idnguoidung = @idnguoidung
				and HoaDon.iddonhang = @iddonhang;
		if @tongtien is null
			set @tongtien = 0
		update DonHang set tongtien = @tongtien where iddonhang = @iddonhang
	end

exec sp_tongtienhoadon @idnguoidung = 1, @iddonhang = 1
