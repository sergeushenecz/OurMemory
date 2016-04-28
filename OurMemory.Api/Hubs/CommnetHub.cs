using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Models;
using OurMemory.Service.Interfaces;

namespace OurMemory.Hubs
{
    public class CommentHub : Hub
    {
        private readonly IUnityContainer _container;
        private readonly ICommentService _commentService;

        private UnityContainer _unityContainer;
        private static readonly Dictionary<string, Room> _dictionaryRoom = new Dictionary<string, Room>();

        public CommentHub(IUnityContainer container, ICommentService commentService)
        {
            _container = container;
            _commentService = commentService;
        }

        public Task JoinRoom(int id, string commentType)
        {
            if (_dictionaryRoom.ContainsKey(Context.ConnectionId))
            {
                return Clients.Caller.getError("Error: Current user already in a room");
            }

            var roomName = GetNameRoom(id, commentType);
            _dictionaryRoom.Add(Context.ConnectionId, new Room() { CommentType = commentType, Name = roomName, Id = id });

            Groups.Add(Context.ConnectionId, roomName);

            var commentService = GetService(commentType);
            IEnumerable<Comment> comments = commentService.GetComments(id);
            var commentsViewModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);

            return Clients.Caller.getAllComments(commentsViewModel);
        }
        public Task SendComment(string message)
        {
            if (!_dictionaryRoom.ContainsKey(Context.ConnectionId))
            {
                return Clients.Caller.getError("Error: Current user not room");
            }

            var room = _dictionaryRoom[Context.ConnectionId];

            var commentService = GetService(room.CommentType);
            var comment = commentService.AddComment(room.Id, message, Context.User.Identity.GetUserId());

            var commentViewModel = Mapper.Map<Comment, CommentViewModel>(comment);

            return Clients.Group(room.Name).getComment(commentViewModel);
        }


        public Task RemoveComment(int id)
        {
            if (!_dictionaryRoom.ContainsKey(Context.ConnectionId))
            {
                return Clients.Caller.getError("Error: Current user not room");
            }

            var room = _dictionaryRoom[Context.ConnectionId];

            var comment = _commentService.GetById(id);

            if (comment != null && comment.User.Id == Context.User.Identity.GetUserId())
            {
                comment.IsDeleted = true;
                _commentService.UpdateComment(comment);
                var commentViewModel = Mapper.Map<Comment, CommentViewModel>(comment);
                return Clients.Group(room.Name).getRemoveComment(commentViewModel);
            }


            return Clients.Caller.getError("Error: Current user not created comemnt or comment not exist");
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

        private IComment GetService(string type)
        {
            return _container.Resolve<IComment>(type);
        }

        private string GetNameRoom(int id, string type)
        {
            return id + type;
        }
    }
}