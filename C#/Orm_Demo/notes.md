--ORM = Bridge between your C# code and your database.

 - Dapper.Contrib =>	Official extension library by Dapper contributors adding helper methods like Insert, Update, Delete, Get<T>.
    
- Dapper.SimpleCRUD =>	A popular open-source extension adding convenient CRUD extension methods tightly integrated with Dapper (e.g., Get, Insert, Update, Delete).

ORMLite

- lighweight obj-rel mapper
- Prod class -> db.select<Prod> -> MySQL db -> query exe -> rows -> mapped to each prod obj -> List<Prod>
- support code first approch and db first

Feature	Description
Lightweight	No proxy generation or context tracking.
Fast	Directly generates SQL without complex query parsing.
Simple API	Use LINQ-like syntax or raw SQL safely.
POCO-based	Classes directly represent tables.
Cross-Database	Works with MySQL, SQL Server, PostgreSQL, SQLite, etc.
Auto Table Creation	Can create tables automatically from POCOs.