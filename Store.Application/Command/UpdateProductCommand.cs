using MediatR;
using Store.Application.Interface;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Command
{
    public record UpdateProductCommand(Product Product) : IRequest<Product>;


    public class UpdateProductCommandHandler(IProductRepository productRepository)
        : IRequestHandler<UpdateProductCommand, Product>
    {
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await productRepository.UpdateProduct(request.Product);
        }
    }
}
