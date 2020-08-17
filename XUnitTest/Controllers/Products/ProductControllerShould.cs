using Application.Features.ProductFeatures.Commands;
using FluentAssertions;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Controllers.v1;
using Xunit;

namespace XUnitTest.Controllers.Products
{
    public class ProductControllerShould
    {
        private readonly ProductController _productController;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CreateProductCommandDto _commandDto;

        public ProductControllerShould()
        {
            _commandDto = Any.CreateProductCommandDto();
            _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Any.RandomInt());
            _productController = new ProductController(_mediatorMock.Object);
        }

        [Fact]
        public async Task ReturnOkFromCreateProduct()
        {
            var response = await _productController.CreateProduct(_commandDto).ConfigureAwait(false);

            _mediatorMock.Verify(m => m.Send(It.Is<CreateProductCommand>(p => p.Barcode == _commandDto.Barcode
                                                                           && p.BuyingPrice == _commandDto.BuyingPrice
                                                                           && p.Description == _commandDto.Description
                                                                           && p.IsActive == _commandDto.IsActive
                                                                           && p.Name == _commandDto.Name
                                                                           && p.Rate == _commandDto.Rate),
                                             It.IsAny<CancellationToken>()), Times.Once);
            response.Should().NotBeNull();
        }
    }
}
