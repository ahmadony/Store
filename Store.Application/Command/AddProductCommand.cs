using MediatR;
using Store.Application.Interface;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Command
{
    public record AddProductCommand(Product Product) : IRequest<Product>;


    public class AddProductCommandHandler(IProductRepository productRepository)
        : IRequestHandler<AddProductCommand, Product>
    {
        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            return await productRepository.AddProduct(request.Product);
        }
    }
}
