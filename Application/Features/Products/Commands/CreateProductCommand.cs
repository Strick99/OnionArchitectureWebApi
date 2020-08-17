using MediatR;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Rate { get; set; }

        public CreateProductCommand(string name, string barcode, bool isActive, string description, decimal buyingPrice, decimal rate)
        {
            Name = name;
            Barcode = barcode;
            IsActive = isActive;
            Description = description;
            BuyingPrice = buyingPrice;
            Rate = rate;
        }
    }
}
