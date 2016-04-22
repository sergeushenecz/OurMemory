using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Hubs
{
    public class CommentHub : Hub
    {
        private readonly IUnityContainer _container;

        private UnityContainer _unityContainer;

        public CommentHub(IUnityContainer container)
        {
            _container = container;
        }

        public Task JoinRoom(int id, string commentType)
        {
            var roomName = GetNameRoom(id, commentType);


            Groups.Add(Context.ConnectionId, roomName);
            var commentService = GetService(commentType);
            ICollection<Comment> comments = commentService.GetComments(id);
            var commentsViewModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);

            return Clients.Caller.getAllComments(commentsViewModel);

        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }

        public Task SendComment(int id, string commentType, string message)
        {
            var roomName = GetNameRoom(id, commentType);

            var commentService = GetService(commentType);
            var comment = commentService.AddComment(id, message, Context.User.Identity.GetUserId());

            var commentViewModel = Mapper.Map<Comment, CommentViewModel>(comment);

            return Clients.Group(roomName).getComment(commentViewModel);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnConnected()
        {
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



        private ICommentService GetService(string type)
        {
            return _container.Resolve<ICommentService>(type);
        }


        private string GetNameRoom(int id, string type)
        {
            return id + type;
        }
    }
}