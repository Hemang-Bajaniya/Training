namespace ShopDB.Models

{
    // CUSTT03
    public class Customer
    {
        public int Id { get; set; } //t03f01
        public string Name { get; set; } = ""; //t03f02
        public string Email { get; set; } = ""; //t03f03

        public override string ToString()
        {
            return $"Customer Id: {Id}, Name: {Name}, Email: {Email}";
        }
    }
}