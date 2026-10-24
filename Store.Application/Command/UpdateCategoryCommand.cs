using MediatR;
using Store.Application.Interface;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Command
{
    public record UpdateCategoryCommand(Category category) : IRequest<Category>;


    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<UpdateCategoryCommand, Category>
    {
        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await categoryRepository.UpdateCategory(request.category);
        }
    }
}
