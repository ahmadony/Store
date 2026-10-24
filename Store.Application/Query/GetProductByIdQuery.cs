using MediatR;
using Store.Application.Interface;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Query
{
    public record GetProductByIdQuery(int ProductId) : IRequest<Product?>;


    public class GetProductByIdQueryHandler(IProductRepository productRepository)
        : IRequestHandler<GetProductByIdQuery, Product?>
    {
        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.GetProductById(request.ProductId);
        }
    }
}
