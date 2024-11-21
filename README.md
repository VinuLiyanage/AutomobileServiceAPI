# Web Service API for a Automobile Service Staion

This is Web Service API created for a Automobile Service Staion. (This is a testing project.)

<img src="https://github.com/VinuLiyanage/GarageAPI/blob/master/Entity%20relationships.png" alt="Entities">

<h2>Entities</h2>

<ul>
  <li>Customer -> Properties: $${\color{orange}{PK: Id}}$$, First Name, Last Name, Address Line1, Address Line 2, Address Line 3, City, State, Postal Code, Phone Number</li>
  <li>Order -> Properties: $${\color{orange}{PK: Id, FK: CustomerId}}$$, Order Status, Total, Tax, Sub Total</li>
  <li>Item -> Properties: $${\color{orange}{PK: Id}}$$, Name, Decription, Is Service</li>
  <li>OrdersItems -> Properties: $${\color{orange}{PK,FK: OrderId}}$$, $${\color{orange}{PK,FK: ItemId}}$$, Quantity, Total, Description</li>
</ul>

<h2>Relationships between the entities</h2>
<ul>
  <li>Customer - Order &emsp;---> A Customer has many Orders. ( <i>Primary Key: Id</i> ) </br>
      Order - Customer &emsp;---> A Order has one cusomer. ( <i>Primary Key: Id </i> | <i> Foreign Key: CustomerId</i> )</li>
  <li>Order - Item &emsp;&emsp;&emsp;&nbsp;---> A Order has many Items. ( <i>Primary Key: Id</i> ) </br>
      Item - Order &emsp;&emsp;&emsp;&nbsp;---> A Item can include in more Orders. ( <i>Primary Key: Id</i> )</br>
      OrdersItems &emsp;&emsp;&emsp;&nbsp;---> This is the Many to Many relationship entity. This entity has items added to the relavant order. ( <i>Primary Key: OrderId, ItemId </i> | <i> Foriegn Keys: OrderId, ItemId</i> )</br> 
  </li>
</ul> 
