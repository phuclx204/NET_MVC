CREATE DATABASE SalesInventoryDB;
GO

USE SalesInventoryDB;
GO

CREATE TABLE users (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100),
    email NVARCHAR(100) UNIQUE,
    phone NVARCHAR(15),
    password NVARCHAR(255),
    status TINYINT DEFAULT 1,
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
    phone NVARCHAR(15),
    email NVARCHAR(100),
    address NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME
);

CREATE TABLE suppliers (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(150),
    phone NVARCHAR(15),
    email NVARCHAR(100),
    address NVARCHAR(255)
);

CREATE TABLE categories (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100),
    parent_id BIGINT NULL,
    status TINYINT DEFAULT 1,
    FOREIGN KEY (parent_id) REFERENCES categories(id)
);

CREATE TABLE brands (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100),
    status TINYINT DEFAULT 1
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
    category_id BIGINT,
    brand_id BIGINT,
    description NVARCHAR(MAX),
    status TINYINT DEFAULT 1,
    FOREIGN KEY (category_id) REFERENCES categories(id),
    FOREIGN KEY (brand_id) REFERENCES brands(id)
);

CREATE TABLE product_details (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    product_id BIGINT,
    size_id BIGINT,
    color_id BIGINT,
    price DECIMAL(12,2),
    cost_price DECIMAL(12,2),
    stock INT DEFAULT 0,
    sku NVARCHAR(50),
    status TINYINT DEFAULT 1,
    FOREIGN KEY (product_id) REFERENCES products(id),
    FOREIGN KEY (size_id) REFERENCES sizes(id),
    FOREIGN KEY (color_id) REFERENCES colors(id)
);

CREATE TABLE stock_in (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    supplier_id BIGINT,
    user_id BIGINT,
    total_amount DECIMAL(12,2),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (supplier_id) REFERENCES suppliers(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE stock_in_detail (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    stock_in_id BIGINT,
    product_detail_id BIGINT,
    quantity INT,
    price DECIMAL(12,2),
    FOREIGN KEY (stock_in_id) REFERENCES stock_in(id),
    FOREIGN KEY (product_detail_id) REFERENCES product_details(id)
);

CREATE TABLE vouchers (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    code NVARCHAR(50),
    description NVARCHAR(255),
    discount_type NVARCHAR(10), -- tongtien / phantram
    discount_value DECIMAL(12,2),
    min_order DECIMAL(12,2),
    start_date DATETIME,
    end_date DATETIME,
    status TINYINT
);

CREATE TABLE orders (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    customer_id BIGINT,
    user_id BIGINT,
    voucher_id BIGINT NULL,
    total_amount DECIMAL(12,2),
    discount_amount DECIMAL(12,2),
    final_amount DECIMAL(12,2),
    status NVARCHAR(30),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES customers(id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (voucher_id) REFERENCES vouchers(id)
);

CREATE TABLE order_items (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    order_id BIGINT,
    product_detail_id BIGINT,
    quantity INT,
    price DECIMAL(12,2),
    total DECIMAL(12,2),
    FOREIGN KEY (order_id) REFERENCES orders(id),
    FOREIGN KEY (product_detail_id) REFERENCES product_details(id)
);

CREATE TABLE payments (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    order_id BIGINT,
    method NVARCHAR(50), -- cash, card, momo
    amount DECIMAL(12,2),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(id)
);



--insert db fake
INSERT INTO roles (name, description)
VALUES
    ('ADMIN', 'Quyền quản trị hệ thống'),
    ('SALES', 'Nhân viên bán hàng'),
    ('WAREHOUSE', 'Quản lý kho');
INSERT INTO users (name, email, phone, password, status)
VALUES
    ('Admin User', 'admin@sales.com', '0900000001', '123456', 1),
    ('Nhân viên Bán hàng 1', 'sales1@sales.com', '0900000002', '123456', 1),
    ('Nhân viên Kho 1', 'warehouse1@sales.com', '0900000003', '123456', 1);

INSERT INTO role_user (user_id, role_id)
VALUES
    (1, 1),  -- Admin User → ADMIN
    (2, 2),  -- Sales Staff → SALES
    (3, 3);  -- Warehouse Staff → WAREHOUSE
   
INSERT INTO categories (name, parent_id, status)
VALUES
    ('Thời trang Nam', NULL, 1),              -- id = 1
    ('Thời trang Nữ', NULL, 1),               -- id = 2

    -- Con của Thời trang Nam
    ('Áo Nam', 1, 1),                          -- id = 3
    ('Quần Nam', 1, 1),                        -- id = 4

    -- Con của Áo Nam
    ('Áo thun Nam', 3, 1),                     -- id = 5
    ('Áo sơ mi Nam', 3, 1),                    -- id = 6

    -- Con của Thời trang Nữ
    ('Váy Nữ', 2, 1),                          -- id = 7
    ('Áo Nữ', 2, 1);                           -- id = 8


INSERT INTO brands (name, status)
VALUES
    ('Nike', 1),
    ('Adidas', 1),
    ('Uniqlo', 1),
    ('Zara', 1);
   
INSERT INTO sizes (name, status)
VALUES
    ('S', 1),
    ('M', 1),
    ('L', 1),
    ('XL', 1);
INSERT INTO colors (name, code, status)
VALUES
    ('Red', '#FF0000', 1),
    ('Blue', '#0000FF', 1),
    ('Black', '#000000', 1),
    ('White', '#FFFFFF', 1);
INSERT INTO products (name, category_id, brand_id, description, status)
VALUES
    ('Áo thun nam basic', 5, 3, 'Áo thun cotton 100%', 1),
    ('Áo sơ mi nam công sở', 6, 4, 'Chất liệu dễ chịu, thoáng mát', 1);
    
INSERT INTO product_details (product_id, size_id, color_id, price, cost_price, stock, sku, status)
VALUES
    (1, 1, 3, 199000, 120000, 50, 'TSN-S-BLK', 1),  -- S - Black
    (1, 2, 3, 199000, 120000, 40, 'TSN-M-BLK', 1),  -- M - Black
    (1, 3, 2, 199000, 120000, 35, 'TSN-L-BLU', 1);  -- L - Blue
INSERT INTO product_details (product_id, size_id, color_id, price, cost_price, stock, sku, status)
VALUES
    (2, 2, 4, 299000, 180000, 20, 'SMN-M-WHT', 1),
    (2, 3, 4, 299000, 180000, 25, 'SMN-L-WHT', 1);

INSERT INTO suppliers (name, phone, email, address)
VALUES
    ('Công ty May Việt Tiến', '0909123456', 'contact@viettien.com', 'HCM'),
    ('Công ty Dệt Thái Tuấn', '0909345678', 'info@thaituan.com', 'HCM');
INSERT INTO customers (name, phone, email, address)
VALUES
    ('Nguyễn Văn A', '0911111111', 'a@gmail.com', 'Hà Nội'),
    ('Trần Thị B', '0922222222', 'b@gmail.com', 'HCM');
INSERT INTO stock_in (supplier_id, user_id, total_amount)
VALUES
    (1, 1, 5000000);
INSERT INTO stock_in_detail (stock_in_id, product_detail_id, quantity, price)
VALUES
    (1, 1, 50, 120000),
    (1, 2, 40, 120000),
    (1, 3, 35, 120000);
   
   