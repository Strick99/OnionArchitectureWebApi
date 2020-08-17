using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Barcode = request.Barcode,
                Name = request.Name,
                BuyingPrice = request.BuyingPrice,
                Rate = request.Rate,
                Description = request.Description
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}
