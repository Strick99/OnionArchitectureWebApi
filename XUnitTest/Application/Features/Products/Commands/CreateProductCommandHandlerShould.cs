using Application.Features.ProductFeatures.Commands;
using Application.Interfaces;
using Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest.Application.Features.Products.Commands
{
    public class CreateProductCommandHandlerShould
    {
        private readonly CreateProductCommand _command;
        private readonly CreateProductCommandHandler _handler;
        private readonly Mock<IApplicationDbContext> _contextMock;

        public CreateProductCommandHandlerShould()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _command = new CreateProductCommand(Any.RandomString(), Any.RandomString(), true, Any.RandomString(), Any.RandomDecimal(), Any.RandomDecimal());
            _handler = new CreateProductCommandHandler(_contextMock.Object);            
            _contextMock.Setup(x => x.Products.Add(new Product()));
            _contextMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            //_context = GetContext();
        }        

        [Fact]
        public async Task CreateProductOnDatabase()
        {
            Product expectedProduct = Any.ProductEntity(_command.Name, _command.Barcode, _command.IsActive, _command.Description, _command.Rate, _command.BuyingPrice);

            await _handler.Handle(_command, CancellationToken.None).ConfigureAwait(false);

            _contextMock.Verify(m => m.Products.Add(It.Is<Product>(p => p.Barcode == expectedProduct.Barcode
                                                                     && p.BuyingPrice == expectedProduct.BuyingPrice
                                                                     && p.Description == expectedProduct.Description
                                                                     && p.IsActive == expectedProduct.IsActive
                                                                     && p.Name == expectedProduct.Name
                                                                     && p.Rate == expectedProduct.Rate
                                                                  )
                                                    ), Times.Once);

            _contextMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        //private ApplicationDbContext GetContext()
        //{
        //    DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //                      .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        //                      .Options;

        //    var context = new ApplicationDbContext(options);

        //    return context;
        //}
    }
}
