# Point-of-Sales-System
This is a point of sales system developed using WPF
It consist of 3 projects 
examples which is main project or interface
POSDAL is the data acess layer that contains all the logic methods that query the database
POSModel contains the models/classes 
To use this project follow this steps
1. Open Examples project then go to App.config
2.Change the connection string from
  <connectionStrings>
    <add name="MyConn" connectionString="data source=Mxolisi;initial catalog=ADSAssignment;user id=UserForProject;password=12345;" />
  </connectionStrings>
  
  data sourse = Mxolisi; remove Mxolisi put your server username from Microsoft SQL Server make sure it correct
  Catalog is the database name i will post the backup file of the database restore it in Microsoft SQL Server
  here is the link on how to restore database 
  https://bit.ly/2MlzykO
  Follow method 2
  
  After restoring database create a user for database in id ="UserForProject" put your username you created
  password put your password created then save you changes.
  
  To create user go to database follow https://www.guru99.com/sql-server-create-user.html
  
  That all!!!!!.
  
  In the System they are two type of users supervisor and cashier
  Go to your table employees to find login details 
  To scan the product use product id find it in product table
For the database use backup file or scripts one of them.
