use pizza_dilivery
create table pizza_ingredient
(
pizza_id int not null foreign key references pizza (id),
ingredient_id int not null foreign key references ingredient(id),
);