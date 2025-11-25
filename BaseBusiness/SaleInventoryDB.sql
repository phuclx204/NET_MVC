CREATE DATABASE SalesInventoryDB;
GO

USE SalesInventoryDB;
GO

CREATE TABLE users (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100),
	avatar NVARCHAR(255),
    email NVARCHAR(100) UNIQUE,
    phone NVARCHAR(15),
    password NVARCHAR(255),
    status TINYINT DEFAULT 1,
	created_by BIGINT,
	updated_by BIGINT,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME
);

CREATE TABLE roles (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50),
    description NVARCHAR(255)
);

CREATE TABLE role_user (
    user_id BIGINT,
    role_id BIGINT,
    PRIMARY KEY (user_id, role_id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (role_id) REFERENCES roles(id)
);

CREATE TABLE customers (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100),
	gender TINYINT, 
	dob DATE,
	tier NVARCHAR(20),
    phone NVARCHAR(15),
    email NVARCHAR(100),
    address NVARCHAR(255),
	note NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME
);

CREATE TABLE suppliers (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(150),
	tax_code NVARCHAR(255),
	contact_person NVARCHAR(100),
    phone NVARCHAR(15),
    email NVARCHAR(100),
    address NVARCHAR(255),
	note NVARCHAR(255)
);

CREATE TABLE categories (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100),
	slug NVARCHAR(255),
	sort_order INT,
    parent_id BIGINT NULL,
    status TINYINT DEFAULT 1,
	created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME,
    FOREIGN KEY (parent_id) REFERENCES categories(id)
);

CREATE TABLE brands (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100),
	country NVARCHAR(50),
	logo NVARCHAR(255),
    status TINYINT DEFAULT 1,
	created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME
);

CREATE TABLE colors (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50),
    code NVARCHAR(10),  -- #FF0000
    status TINYINT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME
);

CREATE TABLE sizes (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(20), -- S, M, L, XL, ...
    status TINYINT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME
);

CREATE TABLE products (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(150),
	thumbnail NVARCHAR(255),
	weight DECIMAL(10,2),
	material NVARCHAR(100),
	origin NVARCHAR(100),
    category_id BIGINT,
    brand_id BIGINT,
    description NVARCHAR(MAX),
    status TINYINT DEFAULT 1,
	created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME,
    FOREIGN KEY (category_id) REFERENCES categories(id),
    FOREIGN KEY (brand_id) REFERENCES brands(id)
);

CREATE TABLE product_details (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
	barcode NVARCHAR(50),
    product_id BIGINT,
    size_id BIGINT,
    color_id BIGINT,
    price DECIMAL(12,2),
    cost_price DECIMAL(12,2),
	min_stock INT,
    stock INT DEFAULT 0,
    sku NVARCHAR(50),
    status TINYINT DEFAULT 1,
	created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME,
    FOREIGN KEY (product_id) REFERENCES products(id),
    FOREIGN KEY (size_id) REFERENCES sizes(id),
    FOREIGN KEY (color_id) REFERENCES colors(id)
);

CREATE TABLE stock_in (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
	code NVARCHAR(50),
    supplier_id BIGINT,
    user_id BIGINT,
    total_amount DECIMAL(12,2),
	note NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME,
    FOREIGN KEY (supplier_id) REFERENCES suppliers(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE stock_in_detail (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    stock_in_id BIGINT,
    product_detail_id BIGINT,
    quantity INT,
    price DECIMAL(12,2),
    total DECIMAL(12,2),
    FOREIGN KEY (stock_in_id) REFERENCES stock_in(id),
    FOREIGN KEY (product_detail_id) REFERENCES product_details(id)
);

CREATE TABLE vouchers (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    code NVARCHAR(50),
	user_id BIGINT,
	usage_limit INT, --so lan su dung
	used_count INT, --so lan da dung
    description NVARCHAR(255),
    discount_type NVARCHAR(10), -- tongtien / phantram
    discount_value DECIMAL(12,2),
	max_discount DECIMAL(12,2),
    min_order DECIMAL(12,2),
    start_date DATETIME,
    end_date DATETIME,
    status TINYINT,
    FOREIGN KEY (user_id) REFERENCES users(id),
);

CREATE TABLE orders (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    customer_id BIGINT,
    user_id BIGINT,
    voucher_id BIGINT NULL,
	code NVARCHAR(50),
    total_amount DECIMAL(12,2),
    discount_amount DECIMAL(12,2),
    final_amount DECIMAL(12,2),
	payment_status NVARCHAR(30),
	shipping_fee DECIMAL(12,2),
    status NVARCHAR(30),
	note NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
	updated_at DATETIME,
    FOREIGN KEY (customer_id) REFERENCES customers(id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (voucher_id) REFERENCES vouchers(id)
);

CREATE TABLE order_items (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    order_id BIGINT,
    product_detail_id BIGINT,
	discount DECIMAL(12,2),
    quantity INT,
    price DECIMAL(12,2),
    total DECIMAL(12,2),
    FOREIGN KEY (order_id) REFERENCES orders(id),
    FOREIGN KEY (product_detail_id) REFERENCES product_details(id)
);
CREATE TABLE carts (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    customer_id BIGINT,
    total_amount DECIMAL(12,2) DEFAULT 0,
	item_count INT DEFAULT 0,
	cart_status NVARCHAR(20) DEFAULT 'PENDING', --PENDING/CHECKOUT/ABANDONED
    created_at DATETIME DEFAULT GETDATE(),
	updated_at DATETIME,
    FOREIGN KEY (customer_id) REFERENCES customers(id)
);
CREATE TABLE cart_items (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    cart_id BIGINT,
	product_detai_id BIGINT,
	quantity INT DEFAULT 1,
	price DECIMAL(12,2),
    total DECIMAL(12,2),
    created_at DATETIME DEFAULT GETDATE(),
	updated_at DATETIME,
	CONSTRAINT uq_cart_item UNIQUE (cart_id, product_detai_id),
    FOREIGN KEY (cart_id) REFERENCES carts(id),
    FOREIGN KEY (product_detai_id) REFERENCES product_details(id)
);

CREATE TABLE payments (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
	transaction_id NVARCHAR(100),
    order_id BIGINT,
    method NVARCHAR(50), -- cash, card, momo
    amount DECIMAL(12,2),
	status TINYINT,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(id)
);
CREATE TABLE product_images (
    id BIGINT IDENTITY PRIMARY KEY,
	product_id BIGINT,
    url NVARCHAR(255),
    amount DECIMAL(12,2),
	is_thumbnail BIT DEFAULT 0,
    FOREIGN KEY (product_id) REFERENCES products(id)
);
CREATE TABLE audit_log (
    id BIGINT IDENTITY PRIMARY KEY,
	user_id BIGINT,
    action NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),	
    FOREIGN KEY (user_id) REFERENCES users(id)
);

--insert db fake
INSERT INTO roles (name, description)
VALUES
    ('ADMIN', N'Quyền quản trị hệ thống'),
    ('SALES', N'Nhân viên bán hàng'),
    ('MANAGER', N'Quản lý cửa hàng'),
    ('ACCOUNTANT', N'Kế toán'),
    ('WAREHOUSE', N'Quản lý kho');
INSERT INTO users (name, email, phone, password, status) VALUES
(N'Nguyễn Văn A', 'a@sales.com', '0901000001', '123456', 1),
(N'Trần Thị B', 'b@sales.com', '0901000002', '123456', 1),
(N'Phạm Văn C', 'c@sales.com', '0901000003', '123456', 1),
(N'Lê Thị D', 'd@sales.com', '0901000004', '123456', 1),
(N'Hoàng Văn E', 'e@sales.com', '0901000005', '123456', 1);
INSERT INTO role_user (user_id, role_id) VALUES
(1,1),
(2,2),
(3,3),
(4,4),
(5,5);
INSERT INTO customers (name, phone, email, address) VALUES
(N'Nguyễn Văn An', '0911111111', 'cus1@gmail.com', 'HN'),
(N'Trần Thị Bưởi', '0911111112', 'cus2@gmail.com', 'HCM'),
(N'Lê Văn Cam', '0911111113', 'cus3@gmail.com', N'Đà Nẵng'),
(N'Phạm Thị Dung', '0911111114', 'cus4@gmail.com', 'HCM'),
(N'Hoàng Văn Em', '0911111115', 'cus5@gmail.com', N'Hải Phòng');
INSERT INTO suppliers (name, phone, email, address) VALUES
(N'Công ty May Việt Tiến', '0909000001', 'viettien@mail.com', 'HCM'),
(N'Công ty Dệt Thái Tuấn', '0909000002', 'thaituan@mail.com', 'HCM'),
(N'Công ty May An Phước', '0909000003', 'anphuoc@mail.com', 'HN'),
(N'Công ty Sợi T&T', '0909000004', 'tt@mail.com', N'Đồng Nai'),
(N'Công ty Dệt May 7', '0909000005', 'may7@mail.com', N'Cần Thơ');
INSERT INTO categories (name, parent_id, status) VALUES
(N'Nam', NULL, 1),
(N'Nữ', NULL, 1),
(N'Áo Nam', 1, 1),
(N'Quần Nam', 1, 1),
(N'Váy Nữ', 2, 1);
INSERT INTO brands (name, status) VALUES
('Nike', 1),
('Adidas', 1),
('Uniqlo', 1),
('Zara', 1),
('H&M', 1);
INSERT INTO colors (name, code, status) VALUES
('Red', '#FF0000', 1),
('Blue', '#0000FF', 1),
('Black', '#000000', 1),
('White', '#FFFFFF', 1),
('Green', '#00FF00', 1);
INSERT INTO sizes (name, status) VALUES
('S', 1),
('M', 1),
('L', 1),
('XL', 1),
('XXL', 1);
INSERT INTO products (name, category_id, brand_id, description, status) VALUES
(N'Áo thun nam cotton', 3, 3, N'Chất liệu cotton thoáng mát', 1),
(N'Quần jean nam slimfit', 4, 2, N'Co giãn 4 chiều', 1),
(N'Áo sơ mi nữ công sở', 2, 4, N'Phong cách sang trọng', 1),
(N'Váy nữ dáng dài', 5, 5, N'Mềm mại nhẹ nhàng', 1),
(N'Áo polo nam thể thao', 3, 1, N'Thấm hút mồ hôi', 1);
INSERT INTO product_details (product_id, size_id, color_id, price, cost_price, stock, sku, status) VALUES
(1, 1, 3, 199000, 120000, 30, 'ATC-S-BLK', 1),
(1, 2, 4, 199000, 120000, 20, 'ATC-M-WHT', 1),
(1, 3, 1, 199000, 120000, 25, 'ATC-L-RED', 1),
(2, 2, 3, 399000, 250000, 35, 'JNS-M-BLK', 1),
(2, 3, 4, 399000, 250000, 40, 'JNS-L-WHT', 1);
INSERT INTO stock_in (supplier_id, user_id, total_amount) VALUES
(1,1,5000000),
(2,1,3000000),
(3,2,4500000),
(4,3,2000000),
(5,4,3500000);
INSERT INTO stock_in_detail (stock_in_id, product_detail_id, quantity, price) VALUES
(1, 1, 50, 120000),
(1, 2, 40, 120000),
(2, 3, 35, 120000),
(3, 4, 30, 250000),
(4, 5, 25, 250000);
INSERT INTO vouchers (code, description, discount_type, discount_value, min_order, start_date, end_date, status) VALUES
('SALE10', N'Giảm 10%', 'phantram', 10, 200000, GETDATE(), DATEADD(day,30,GETDATE()), 1),
('SALE20', N'Giảm 20%', 'phantram', 20, 300000, GETDATE(), DATEADD(day,30,GETDATE()), 1),
('FREESHIP', N'Miễn phí ship', 'tongtien', 30000, 150000, GETDATE(), DATEADD(day,60,GETDATE()), 1),
('NEW50', N'Giảm 50k', 'tongtien', 50000, 250000, GETDATE(), DATEADD(day,20,GETDATE()), 1),
('VIP30', N'Giảm 30%', 'phantram', 30, 500000, GETDATE(), DATEADD(day,10,GETDATE()), 1);
INSERT INTO orders (customer_id, user_id, voucher_id, total_amount, discount_amount, final_amount, status) VALUES
(1,1,1,500000,50000,450000,'PAID'),
(2,2,2,700000,140000,560000,'PAID'),
(3,3,NULL,300000,0,300000,'PAID'),
(4,4,3,450000,30000,420000,'PAID'),
(5,5,NULL,250000,0,250000,'PAID');
INSERT INTO order_items (order_id, product_detail_id, quantity, price, total) VALUES
(1,1,2,199000,398000),
(2,4,1,399000,399000),
(3,2,1,199000,199000),
(4,3,2,199000,398000),
(5,5,1,399000,399000);
INSERT INTO payments (order_id, method, amount) VALUES
(1,'cash',450000),
(2,'card',560000),
(3,'momo',300000),
(4,'cash',420000),
(5,'card',250000);

