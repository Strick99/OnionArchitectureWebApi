using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.SaveChangesAsync();
        }

        //private static IEnumerable<ChangeEventDetail> GetChanges(this VendorDatabaseContext context, long rateCategoryId)
        //{
        //    IEnumerable<PropertyValues> currentValues =
        //        context.ChangeTracker.Entries<RateCategoryAssociation>()
        //        .Where(c => c.Entity.RateCategoryId == rateCategoryId)
        //        .Select(c => c.CurrentValues);

        //    var originalIncludes = Enumerable.Empty<PropertyValues>();
        //    var originalExcludes = Enumerable.Empty<PropertyValues>();
        //    var currentIncludes = currentValues.Where(v => (bool)v[nameof(RateCategoryAssociation.Include)]);
        //    var currentExcludes = currentValues.Where(v => !(bool)v[nameof(RateCategoryAssociation.Include)]);

        //    var includedRateCategoryChangeEvent = currentIncludes.GetSimpleCollectionEntityChanges(originalIncludes, nameof(RateCategoryAssociation.Sequence), nameof(RateCategoryAssociation.StandardRateCategoryCode), fieldNameOverride: "IncludedRateCategories");
        //    var excludedRateCategoryChangeEvent = currentExcludes.GetSimpleCollectionEntityChanges(originalExcludes, nameof(RateCategoryAssociation.Sequence), nameof(RateCategoryAssociation.StandardRateCategoryCode), fieldNameOverride: "ExcludedRateCategories");

        //    return new[] { includedRateCategoryChangeEvent, excludedRateCategoryChangeEvent }.Where(ce => ce != null);
        //}
    }
}
