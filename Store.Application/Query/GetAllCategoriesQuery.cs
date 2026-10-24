using MediatR;
using Store.Application.Interface;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Query
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<Category>>;

    public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepositories)
        : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
    {
        public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await categoryRepositories.GetAllCategories();
        }
    }
}
