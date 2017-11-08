use pizza_dilivery
create table order_pizza
(
order_id int not null references orders (order_id),
pizza_topping_id int not null references pizza_topping (pizza_topping_id),
total_price decimal(10,2) null,
order_status varchar(20) check (order_status in ('waiting', 'cooking', 'cooked', 'delivering', 'delivered')) default 'waiting',
);








