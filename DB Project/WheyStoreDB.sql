Create database Wheystore

drop database Wheystore

create table Role(
	roleID int identity(1, 1) primary key not null,
	roleName varchar(50) not null
)

create table Account (
	accountID int identity(1, 1) primary key not null,
	email varchar(255) not null,
	password varchar(255) not null,
	emailVerify bit default 0,
	role int references Role(roleID),
	status bit default 1
)

create table Customer (
	customerID int primary key references Account(accountID),
	name nvarchar(255) not null,
	[address] nvarchar(max) not null,
	phone varchar(20) not null,
	cashInWallet int,
	imageURL NVARCHAR(MAX)
)

create table Admin (
	adminID int primary key references Account(accountID),
	name nvarchar(255) not null,
	imageURL NVARCHAR(MAX),
	phone varchar(20)
)

create table Country (
	countryID int identity(1, 1) primary key not null,
	name nvarchar(255) not null,
	status bit default 1
)

create table Category (
	catID int identity(1, 1) primary key not null,
	name nvarchar(255) not null,
	description nvarchar(max),
	status bit default 1
)

create table Brand (
	brandID int identity(1, 1) primary key not null,
	name nvarchar(255) not null,
	status bit default 1
)

create table SubCategory (
	subCatID int identity(1, 1) primary key not null,
	name nvarchar(255) not null,
	description nvarchar(max),
	catID int references Category(catID),
	status bit default 1
)

create table Size (
	sizeID int identity(1, 1) primary key not null,
	description nvarchar(max),
	status bit default 1
)

create table Savor (
	savorID int identity(1, 1) primary key not null,
	description nvarchar(max),
	status bit default 1
)

create table Product(
	productID int identity(1, 1) primary key not null,
	name nvarchar(255) not null,
	description NVARCHAR(MAX),
	img varchar(255),
	subCatID int references SubCategory(subCatID),
	countryID int references Country(countryID),
	brandID int references Brand(brandID),	
	status bit default 1
)

create table ProductDetail(
	detailID int identity(1, 1) primary key not null, 
	productID int references Product(productID),
	sizeID int references Size(sizeID),
	name nvarchar(255) not null,
	img varchar(255),
	serving nvarchar(255),
	servingSize nvarchar(255),
	description NVARCHAR(MAX),	
	defaultPrice int,
	sellPrice int,
	status bit default 1
)

create table ProductSavor(
	detailID int references ProductDetail(detailID),
	savorID int references Savor(savorID),
	quantity int,
	status bit default 1,
	primary key(detailID,savorID)
)

create table FavoriteProduct(
	customerID int references Customer(customerID),
	detailID int references ProductDetail(detailID),
	status bit default 1,
	primary key(customerID, detailID)
)

create table Shipper(
	shipperID int identity(1, 1) primary key not null,
	name nvarchar(255) not null,
	company nvarchar(255),
	phone varchar(20)
)

create table [Order](
	orderID int identity(1, 1) primary key not null,
	customerID int references Customer(customerID),
	orderDate datetime default getdate(),
	shipperID int references Shipper(shipperID),
	location nvarchar(max),
	amount int,
	status bit default 1,
	description nvarchar(max)
)

create table [OrderDetail](
	orderID int references [Order](orderID),
	detailID int references ProductDetail(detailID),
	savorID int references Savor(savorID),
	quantity int,
	description nvarchar(max),
	primary key(orderID,detailID,savorID)
)
