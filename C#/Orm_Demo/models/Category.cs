namespace ShopDB.Models
{
    public class Category // catet01
    {
        public int Id { get; set; } //t01f01
        public string Name { get; set; } = ""; //t01f02
        public DateTime UpdatedOn { get; set; } = DateTime.Now; //t01f03

        public override string ToString()
        {
            return $"Category Id: {Id}, Name: {Name}, Last updated on: {UpdatedOn:dd-MMM-yyyy}";
        }
    }
}