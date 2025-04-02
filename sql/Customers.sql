CREATE TABLE customers (
    
    id serial not null PRIMARY KEY ,
    name varchar(50) not null UNIQUE
);