USE CoffeeShop;
/*
CREATE DATABASE CoffeeShop;

DROP TABLE Items;

DROP TABLE Customers;

CREATE TABLE Customers(
	CustomerID INT IDENTITY(1001,1),
	CustomerName VARCHAR(255) UNIQUE NOT NULL,
	ContactNo VARCHAR(255),
	Address VARCHAR(255)
	CONSTRAINT PK_Customer PRIMARY KEY (CustomerId)
);

CREATE TABLE Items(
	ItemID INT IDENTITY(101,2),
	ItemName VARCHAR(255) NOT NULL UNIQUE,
	ItemPrice FLOAT NOT NULL DEFAULT 10
	CONSTRAINT PK_Item PRIMARY KEY (ItemID)
);

DROP TABLE CustomerOrders;

CREATE TABLE CustomerOrders(
	CustomerOrderID INT IDENTITY(101,1),
	CustomerID INT,
	ItemID INT,
	Quantity INT DEFAULT 1,
	
	CONSTRAINT PK_Order PRIMARY KEY (CustomerOrderID),
	CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerID),
	CONSTRAINT FK_Order_Item FOREIGN KEY (ItemId) REFERENCES Items(ItemID)
	
);

INSERT INTO Customers ( CustomerName, ContactNo, Address ) VALUES ( 'Maruf', '018', '25/A WestShewrapara' );
INSERT INTO Customers ( CustomerName, ContactNo, Address ) VALUES ( 'K Maruf', '019', '25/A WestShewrapara' );
INSERT INTO Customers ( CustomerName, ContactNo, Address ) VALUES ( 'Kuddus', '012', 'Mirpur' );
INSERT INTO Customers ( CustomerName, ContactNo, Address ) VALUES ( 'Mokles', '017', 'Vanga Bari' );

DELETE FROM Customers;
DELETE FROM Items;
SELECT COUNT(CustomerID) FROM Customer WHERE CustomerName = 'MaruF';
DELETE FROM Customers WHERE CustomerID = 1001;
UPDATE Customers SET CustomerName = 'dd',ContactNo = 0, Address = '' WHERE CustomerID = 1001;

INSERT INTO Items (ItemName, ItemPrice) VALUES ('Hot Coffee', 80);
INSERT INTO Items (ItemName, ItemPrice) VALUES ('Cold Coffee', 100);
INSERT INTO Items (ItemName, ItemPrice) VALUES ('Chocolate Coffee', 150);
INSERT INTO Items (ItemName, ItemPrice) VALUES ('Chilli Coffee', 200);
INSERT INTO Items (ItemName, ItemPrice) VALUES ('Diabetes Coffee', 110);
INSERT INTO Items (ItemName, ItemPrice) VALUES ('Kuddus Coffee', 500);

SELECT * FROM Customers;
SELECT * FROM Items;
SELECT * FROM CustomerOrders;

INSERT INTO CustomerOrders ( CustomerID, ItemID, Quantity ) VALUES ( 1009, 105, 2 );
INSERT INTO CustomerOrders ( CustomerID, ItemID, Quantity ) VALUES ( 1010, 105, 1 );
INSERT INTO CustomerOrders ( CustomerID, ItemID, Quantity ) VALUES ( 1011, 111, 2 );
INSERT INTO CustomerOrders ( CustomerID, ItemID, Quantity ) VALUES ( 1011, 115, 4 );

DELETE FROM CustomerOrders WHERE CustomerID = 1001;

SELECT CustomerOrderID AS OrderID, CustomerName, ItemName, Quantity, Quantity*ItemPrice AS TotalPrice 
FROM (( CustomerOrders
LEFT JOIN Customers ON CustomerOrders.CustomerID = Customers.CustomerID )
LEFT JOIN Items ON CustomerOrders.ItemID = Items.ItemID);

*/

SELECT CustomerOrderID AS OrderID, CustomerName, ItemName, Quantity, Quantity*ItemPrice AS TotalPrice FROM (( CustomerOrders INNER JOIN Customers ON CustomerOrders.CustomerID = 1011 AND Customers.CustomerID= 1011 ) LEFT JOIN Items ON CustomerOrders.ItemID = Items.ItemID);


