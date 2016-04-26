using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using OurMemory.Domain;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Models;
using OurMemory.Service.Interfaces;

namespace OurMemory.Hubs
{
    public class CommentHub : Hub
    {
        private readonly IUnityContainer _container;

        private UnityContainer _unityContainer;
        private static readonly Dictionary<string, Room> _dictionaryRoom = new Dictionary<string, Room>();

        public CommentHub(IUnityContainer container)
        {
            _container = container;
        }

        public Task JoinRoom(int id, string commentType)
        {
            var roomName = GetNameRoom(id, commentType);
            _dictionaryRoom.Add(Context.ConnectionId, new Room() { CommentType = commentType, Name = roomName, Id = id });

            Groups.Add(Context.ConnectionId, roomName);

            var commentService = GetService(commentType);
            ICollection<Comment> comments = commentService.GetComments(id);
            var commentsViewModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);

            return Clients.Caller.getAllComments(commentsViewModel);
        }


//        [AuthorizeClaims]
        public Task SendComment(string message)
        {
            if (!_dictionaryRoom.ContainsKey(Context.ConnectionId))
            {
                return Clients.Caller.getError("Error current user not room");
            }

            var room = _dictionaryRoom[Context.ConnectionId];

            var commentService = GetService(room.CommentType);
            var comment = commentService.AddComment(room.Id, message, Context.User.Identity.GetUserId());

            var commentViewModel = Mapper.Map<Comment, CommentViewModel>(comment);

            return Clients.Group(room.Name).getComment(commentViewModel);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (_dictionaryRoom.ContainsKey(Context.ConnectionId))
            {
                var room = _dictionaryRoom[Context.ConnectionId];
                Groups.Remove(Context.ConnectionId, room.Name);
                _dictionaryRoom.Remove(Context.ConnectionId);
            }

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