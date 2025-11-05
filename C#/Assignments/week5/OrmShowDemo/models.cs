public class Category
{
    public int T01F01 { get; set; }
    public string T01F02 { get; set; } // name
    public DateTime T01F03 { get; set; } // updated_on
    public ICollection<Product> Products { get; set; }
}

public class Product
{
    public int T02F01 { get; set; }
    public int T02F02 { get; set; } // cat_id
    public string T02F03 { get; set; } // name
    public decimal T02F04 { get; set; } // price
    public Category Category { get; set; }
    public ICollection<Sale> Sales { get; set; }
}

public class Customer
{
    public int T03F01 { get; set; }
    public string T03F02 { get; set; } // name
    public string T03F03 { get; set; } // email
    public ICollection<Sale> Sales { get; set; }
}

public class Sale
{
    public int T04F01 { get; set; }
    public int T04F02 { get; set; } // ProductId
    public int T04F03 { get; set; } // CustomerId
    public int T04F04 { get; set; } // Quantity
    public decimal T04F05 { get; set; } // Total
    public DateTime T04F06 { get; set; } // SaleDate

    public Product Product { get; set; }
    public Customer Customer { get; set; }
}