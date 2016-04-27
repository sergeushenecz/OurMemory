using OurMemory.Data.Infrastructure;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IRepository<Comment> commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public void UpdateComment(Comment comment)
        {
            _commentRepository.Update(comment);
            SaveComment();
        }

        public void SaveComment()
        {
            _unitOfWork.Commit();
        }

        public Comment GetById(int id)
        {
            var comment = _commentRepository.GetById(id);

            return comment.IsDeleted ? null : comment;
        }
    }


}