namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommandDto
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Rate { get; set; }
    }
}
