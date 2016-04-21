using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Hubs
{
    public class CommentHub : Hub
    {
        private readonly IArticleService _articleService;

        public CommentHub(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public Task JoinRoom(int Id)
        {
            Groups.Add(Context.ConnectionId, Id.ToString());
            ICollection<Comment> comments = _articleService.GetById(Id)?.Comments;

            var commentsViewModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);

            return Clients.Caller.getAllComments(commentsViewModel);

            
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }

        public Task SendComment(int Id, string message)
        {
            var article = _articleService.GetById(Id);

            var userId = Context.User.Identity.GetUserId();

            var comment = new Comment()
            {
                Article = article,
                User = null,
                Message = message,
                UpdatedDateTime = DateTime.UtcNow
            };

            article.Comments.Add(comment);
            _articleService.UpdateArticle(article);

            var commentsViewModel = Mapper.Map<Comment, CommentViewModel>(comment);

            return Clients.Group(Id.ToString()).getComment(commentsViewModel);
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            var a = 1;
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnConnected()
        {

            var a = 1;
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            // Add your own code here.
            // For example: in a chat application, you might have marked the
            // user as offline after a period of inactivity; in that case 
            // mark the user as online again.
            return base.OnReconnected();
        }
    }
}