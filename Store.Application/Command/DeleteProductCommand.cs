using MediatR;
using Store.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Command
{
    public record DeleteProductCommand(int productId) : IRequest;


    public class DeleteProductCommandHandler(IProductRepository productRepository)
        : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productRepository.DeleteProduct(request.productId);
        }
    }
}
