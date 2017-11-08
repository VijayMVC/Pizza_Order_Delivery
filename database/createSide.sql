use pizza_dilivery
create table side
(
id int not null identity(1,1) primary key,
name varchar(25) not null,
price decimal(4,2) not null,
);