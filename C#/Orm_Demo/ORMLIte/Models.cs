using ServiceStack.DataAnnotations;
using System;

[Alias("catet01")]
public class Category
{
    [AutoIncrement]
    [PrimaryKey]
    [Alias("t01f01")]
    public int Id { get; set; }

    [Alias("t01f02")]
    public string Name { get; set; }

    [Alias("t01f03")]
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
}

[Alias("prodt02")]
public class Product
{
    [AutoIncrement]
    [PrimaryKey]
    [Alias("t02f01")]
    public int Id { get; set; }

    [Alias("t02f02")]
    public int CategoryId { get; set; }

    [Alias("t02f03")]
    public string Name { get; set; }

    [Alias("t02f04")]
    public decimal Price { get; set; }
}

[Alias("custt03")]
public class Customer
{
    [AutoIncrement]
    [PrimaryKey]
    [Alias("t03f01")]
    public int Id { get; set; }

    [Alias("t03f02")]
    public string? Name { get; set; }

    [Alias("t03f03")]
    public string? Email { get; set; }
}

[Alias("salest04")]
public class Sale
{
    [AutoIncrement]
    [PrimaryKey]
    [Alias("t04f01")]
    public int Id { get; set; }

    [Alias("t04f02")]
    public int ProductId { get; set; }

    [Alias("t04f03")]
    public int CustomerId { get; set; }

    [Alias("t04f04")]
    public int Quantity { get; set; }

    [Alias("t04f05")]
    public decimal Total { get; set; }

    [Alias("t04f06")]
    public DateTime SaleDate { get; set; }
}

[Alias("Empt05")]
class Employee
{
    [AutoIncrement]
    [PrimaryKey]
    [Alias("t05f01")]
    public int Id { get; set; }

    [Alias("t05f02")]
    public string Name { get; set; }

    [Alias("t05f03")]
    public decimal Salary { get; set; }
}