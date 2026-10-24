using MediatR;
using Store.Application.Interface;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Query
{
    public record GetCategoryByIdQuery(int categoryId) : IRequest<Category?>;

    public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategoryByIdQuery, Category?>
    {
        public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await categoryRepository.GetCategoryById(request.categoryId);
        }
    }

}


