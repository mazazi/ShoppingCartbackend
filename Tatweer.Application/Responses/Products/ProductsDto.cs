namespace Tatweer.Application.Responses.Products
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public bool IsVisible { get; set; }
    }
}
