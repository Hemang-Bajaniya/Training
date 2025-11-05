# ORM Performance Benchmark: EF Core vs Dapper vs Raw ADO.NET

while fetching query contains join of 4 table(Sales, Product, Customer, Category)

---

## Overview

This project evaluates three data access approaches:

| Method         | Technology       | Type       |
|----------------|------------------|------------|
| `EfCoreQuery`  | Entity Framework Core | Full ORM   |
| `DapperQuery`  | Dapper           | Micro-ORM  |
| `AdoNet_Query` | Raw ADO.NET      | Low-level  |

All methods execute the **same complex JOIN query** across 4 tables (`SALEST04`, `CUSTT03`, `PRODT02`, `CATET01`) and return **anonymous objects** with selected fields.

---

## Benchmark Results


| Method         | Mean Time     | Error       | StdDev      
|----------------|---------------|-------------|-------------
| **EfCoreQuery**    | **6,928.9 μs** | 132.76 μs   | 214.38 μs   
| **DapperQuery**    | **687.8 μs**   | 12.89 μs    | 22.91 μs    
| **AdoNet_Query**   | **722.5 μs**   | 14.41 μs    | 31.32 μs  


---

## Query Details

```sql
SELECT 
    s.T04F01 AS SaleId,
    c.T03F02 AS CustomerName,
    c.T03F03 AS Email,
    p.T02F03 AS ProductName,
    cat.T01F02 AS CategoryName,
    s.T04F04 AS Quantity,
    s.T04F05 AS Total,
    s.T04F06 AS SaleDate
FROM SALEST04 s
JOIN CUSTT03 c ON s.T04F03 = c.T03F01
JOIN PRODT02 p ON s.T04F02 = p.T02F01
JOIN CATET01 cat ON p.T02F02 = cat.T01F01;
```

# Maintainability

- **EFCore:**
    Entity Framework Core (EF Core) offers high maintainability through features like automatic migrations, schema management, and change tracking. Its higher abstraction level reduces boilerplate code by using LINQ for database queries and strong typing, which enhances consistency and reduces errors. 

- **Dapper:**
    Dapper is a lightweight micro-ORM that prioritizes speed and direct SQL control, which benefits performance-critical applications. However, this comes at the cost of maintainability. Since Dapper requires manual SQL query writing and lacks built-in migration or change tracking support, maintaining large or evolving databases can be cumbersome.

- **ADO.Net:**
    Dapper is a lightweight micro-ORM that prioritizes speed and direct SQL control, which benefits performance-critical applications. However, this comes at the cost of maintainability. Since Dapper requires manual SQL query writing and lacks built-in migration or change tracking support, maintaining large or evolving databases can be cumbersome.


***

## Security
| Technology | Security Strengths | Key Points |
|--------------|----------------------|--------------|
| **EF Core** | Highest | Built-in parameterized queries prevent SQL injection. Tightly integrated with ASP.NET Core's security features, reducing developer effort in safeguarding data. | 
| **Dapper** | High (if used correctly) | Requires developers to manually parameterize queries. Proper use of parameterized queries ensures it is as secure as EF Core; mistakes can lead to vulnerabilities. | 
| **ADO.NET** | High (with careful implementation) | Manual approach, so security depends entirely on the developer's discipline in using parameterized queries and input validation. If not done properly, it exposes significant risk. | 

**Summary:** EF Core offers the easiest and most secure out-of-the-box, while Dapper and ADO.NET demand explicit security measures, mainly parameterization, to avoid SQL injection.

***

## When to Choose Each

| Criteria | **EF Core** | **Dapper** | **ADO.NET** |
|---|---|---|---|
| **Best for** | Large, complex applications with evolving schemas, security as priority | High-performance, low-overhead applications needing fine control | Situations requiring maximum control, custom queries, or legacy system compatibility |
| **Security** | Highest, automatic parameterization | High, when implemented with parameterized queries | High, if developers rigorously parameterize and validate inputs |
| **Development Speed / Ease** | Very high, ORM abstractions reduce boilerplate | Moderate, but simpler than ADO.NET; risks from manual SQL | Lower, verbose and manual, but offers complete control |
| **Features & Flexibility** | Rich ORM features, migrations, relationships | Lightweight, flexible, but minimal features | Most flexible, but requires detailed management |



