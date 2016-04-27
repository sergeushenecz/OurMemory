using System.Collections.Generic;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface IComment
    {
        IEnumerable<Comment> GetComments(int id);
        IEnumerable<CommentViewModel> GetCommentViewModels(ICollection<Comment> comments);
        Comment AddComment(int id, string message, string userGuidId);
    }
}