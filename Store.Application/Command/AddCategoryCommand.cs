using MediatR;
using Store.Application.Interface;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Command
{
    public record AddCategoryCommand(Category category) : IRequest<Category>;

    public class AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<AddCategoryCommand, Category>
    {
        public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            return await categoryRepository.AddCategory(request.category);
        }
    }
}
