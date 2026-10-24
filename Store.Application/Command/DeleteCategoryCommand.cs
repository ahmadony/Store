using MediatR;
using Store.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Application.Command
{
    public record DeleteCategoryCommand(int categoryId) : IRequest;


    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryRepository.DeleteCategory(request.categoryId);
        }
    }
}
