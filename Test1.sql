create database Test1
go
use Test1
go
create Table NganhHoc
(
	MaNganh char(4) primary key,
	TenNganh nvarchar(30)
)
go
Create Table SinhVien(
	MaSV char(10) primary key,
	HoTen nvarchar(50) Not null,
	GioiTinh nvarchar(5),
	NgaySinh date,
	Hinh varchar(50),
	MaNganh char(4) references NganhHoc(MaNganh)
)
go
create table HocPhan
(
	MaHP char(6) primary key,
	TenHP nvarchar(30) not null,
	SoTinChi int
)
go
create table DangKy(
	MaDK int identity(1,1) primary key,
	NgayDK date,
	MaSV char(10) references SinhVien(MaSV)
)
create table ChiTietDangKy(
	MaDK int references DangKy(MaDK),
	MaHP char(6) references HocPhan(MaHP),
	primary key(MaDK,MaHP)
)
go
---------------------
insert into NganhHoc(MaNganh,TenNganh) values('CNTT',N'Công nghệ thông tin')
insert into NganhHoc(MaNganh,TenNganh) values('QTKD', N'Quản trị kinh doanh')
go
---------------------
insert into SinhVien(MaSV,HoTen,GioiTinh,NgaySinh,Hinh,MaNganh) values('0123456789',N'Nguyễn Văn A',N'Nam','12/02/2000','/Content/images/sv1.jpg','CNTT')
insert into SinhVien(MaSV,HoTen,GioiTinh,NgaySinh,Hinh,MaNganh) values('9876543210',N'Nguyễn Thị B',N'Nữ','03/07/2000','/Content/images/sv2.jpg','QTKD')
go
---------------------
insert into HocPhan(MaHP,TenHP,SoTinChi) values('CNTT01',N'Lập trình C',3)
insert into HocPhan(MaHP,TenHP,SoTinChi) values('CNTT02',N'Cơ sở dữ liệu',2)
insert into HocPhan(MaHP,TenHP,SoTinChi) values('QTKD01',N'Kinh tế vi mô',2)
insert into HocPhan(MaHP,TenHP,SoTinChi) values('QTDK02',N'Xác suất thống kê 1',3)
go
select * from SinhVien
select * from NganhHoc
select * from HocPhan
select * from DangKy
select * from ChiTietDangKy
