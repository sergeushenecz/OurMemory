using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface ICommentService
    {
        void UpdateComment(Comment article);
        void SaveComment();
        Comment GetById(int id);
    }
}