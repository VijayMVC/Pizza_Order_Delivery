use pizza_dilivery
create table orders
(
order_id int identity(1,1) primary key,
order_date smalldatetime default GETDATE(),
customer_id int foreign key references customer (id),
)



