select * from products;
insert into products
(name, price, mdate, edate, emailaddress)
values('Pen (Renault)', 100, '2025-08-23','2030-08-23', 'support@pen.com'),
('Eraser (Camlin)', 20, '2025-08-23','2030-08-23', 'support@eraser.com');

select * from Orders;
insert into orders(orderdate, orderamount)
values('2025-09-04', 0), ('2025-07-23', 0);

select * from orderdetails;
insert into OrderDetails(orderid, ProductId, Quantity, Rate)
values(2, 1, 30, 50), (2, 3, 40, 19);

insert into OrderDetails(orderid, ProductId, Quantity, Rate)
values(3, 1, 60, 40);

select p.name, o.id, o.orderdate, od.quantity, od.rate
from products p inner join orderdetails od
on p.Id = od.productid
inner join orders o
on o.Id = od.orderid;