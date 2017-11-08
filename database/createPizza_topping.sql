use pizza_dilivery
create table pizza_topping
(
pizza_topping_id int identity(1,1) not null primary key,
pizza_id int not null,
pizza_size varchar(10) not null,
foreign key (pizza_id, pizza_size) references pizza_price (pizza_id, size),
topping_id int null references ingredient (id),
);
