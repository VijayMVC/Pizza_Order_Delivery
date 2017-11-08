use pizza_dilivery
create table order_side
(
order_id int not null references orders (order_id),
side_id int foreign key references side (id),
);
