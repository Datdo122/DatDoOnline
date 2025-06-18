--1. ktra matkhau
create function ktradangnhap (@ten nvarchar(50), @pw nvarchar(255))
returns int
as
	begin
		declare @x nvarchar(255)
		select @x = pw from Users where tennguoidung = @ten;
		if (@x = @pw)
			return 1 
		return 0
	end

insert into Users (tennguoidung, pw, role, mahoadon) values
	('dat', '1', 1, 0),
	('dodat', '123', 0, 0)

select dbo.ktradangnhap ('dodat', '123') as tb

create function ktrarole (@ten nvarchar(50))
returns int
as
	begin
		declare @x int;
		select @x = role from Users where tennguoidung = @ten
		return @x;
	end

select dbo.ktrarole ('dat') as tb


--2. proc luu dang ki vao csdl

create proc luuUsers
	@ten nvarchar(50),
	@pw nvarchar(255)
as
	begin
		insert into Users (tennguoidung, pw, role, mahoadon) values
			(@ten, @pw, 0, 0)
	end

exec luuUsers @ten = 'dovandat', @pw = '12'
delete from Users where idnguoidung = 9

select * from Users

--Trigger ktra ten dang ki
create trigger ktratendangki 
on Users for insert
as
	begin
		declare @x nvarchar(50), @y int
		select @x = tennguoidung from inserted
		select @y = count(*) from Users where tennguoidung = @x;
		if (@y>=2)
			begin
				declare @tb int = 0;
				select @tb as tb;
				delete from Users where idnguoidung = (select idnguoidung from inserted)
			end;
		else
			begin
				declare @tb2 int = 1;
				select @tb2 as tb;
			end
	end