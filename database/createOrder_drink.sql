use pizza_dilivery
create table order_drink
(
order_id int not null references orders (order_id),
drink_id int foreign key references drink (id),
);



