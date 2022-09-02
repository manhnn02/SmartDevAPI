# SmartDevAPI

## Instruction

1. Please using endpoint 'Login' to login and get access token (all endpoint need authorized).

Sample data for users list can use for login (name/username/pass):
  - Demo1/demo1@mail.test/123456  
  - Demo2/demo2@mail.test/123456
  
2. using Postman and add token to 'Authorization:BearerToken' to work with API
- To get books list by logged in user: /api/Books/GetBooksByUserID (optional parameter 'bookStatus': null=GetAll, 0=Unread, 1=Read)
- To search book by name: /api/Books/SearchBookByName (parameter: bookName)
- To add a book to books list: /api/Books/AddReadBook (parameter: string bookName, string bookDescription, bool status)
- To get all users list: /api/Users/

Please note that token will expire after 5 minutes.

Note: run SmartDev.sql to create database, table structure with sample data. and update connection string in appsettings.json
