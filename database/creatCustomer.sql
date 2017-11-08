use pizza_dilivery
create table customer
(
id INT identity(1,1) NOT NULL PRIMARY KEY,
firstName varchar(15) NOT NULL,
lastName varchar(15) NOT NULL,
phone varchar(15) NOT NULL,
addre varchar(50) NOT NULL,
reward decimal(5,2) NOT NULL DEFAULT 0.00,
);






