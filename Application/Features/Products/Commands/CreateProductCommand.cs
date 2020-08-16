using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        [FromBody]
        public string Name { get; set; }
        [FromBody]
        public string Barcode { get; set; }
        [FromBody]
        public string Description { get; set; }
        [FromBody]
        public decimal BuyingPrice { get; set; }
        [FromBody]
        public decimal Rate { get; set; }

        public CreateProductCommand(string name, string barcode, string description, decimal buyingPrice, decimal rate)
        {
            Name = name;
            Barcode = barcode;
            Description = description;
            BuyingPrice = buyingPrice;
            Rate = rate;
        }
    }
}
