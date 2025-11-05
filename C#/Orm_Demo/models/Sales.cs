namespace ShopDB.Models
{
    public class Sale
    {
        public int Id { get; set; }          // T04F01
        public int ProductId { get; set; }       // T04F02
        public int CustomerId { get; set; }      // T04F03
        public int Quantity { get; set; }        // T04F04
        public decimal Total { get; set; }       // T04F05 (auto by trigger)
        public DateTime SaleDate { get; set; }   // T04F06

        public override string ToString()
        {
            return $"Sale Id: {Id}, ProdId: {ProductId}, custId: {CustomerId}, Qty: {Quantity}, Total: {Total}, SaleDate:{SaleDate:dd:MMM:yyyy}";
        }
    }
}
