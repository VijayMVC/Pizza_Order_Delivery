use pizza_dilivery
create table pizza_price
(
pizza_id int not null foreign key references pizza (id),
size varchar(10) not null,
primary key (pizza_id, size),
price decimal(4,2) not null
);