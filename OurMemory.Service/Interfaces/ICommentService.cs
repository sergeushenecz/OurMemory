using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface ICommentService
    {
        ICollection<Comment> GetComments(int id);
        IEnumerable<CommentViewModel> GetCommentViewModels(ICollection<Comment> comments);
        Comment AddComment(int id, string message, string userGuidId);
    }
}