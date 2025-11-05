namespace ShopDB.Models
{
    public class Product // prodt02
    {
        public int Id { get; set; }  //t02f01
        public int CatId { get; set; } //t02f02
        public string Name { get; set; } = "";  //t02f03
        public decimal Price { get; set; } //t02f04

        public override string ToString()
        {
            return $"Product Id: {Id}, CatId: {CatId}, Name: {Name}, Price: {Price}";
        }
    }
}
