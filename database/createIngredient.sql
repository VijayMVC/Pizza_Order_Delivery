use pizza_dilivery
create table ingredient
(
id int identity(1,1) not null primary key,
name varchar(15) not null,
amount int not null check (amount >= 0),
price decimal(4,2) not null
);

