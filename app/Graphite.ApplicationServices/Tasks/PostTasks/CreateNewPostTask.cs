using System;
using Graphite.Core.EventAggregator;
using Graphite.Core.Events;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices.Tasks.PostTasks{
  public class CreateNewPostTask : ISubscriber <SubmitPostEvent>{
    public CreateNewPostTask(IPostRepository posts, IUserRepository users, ITagRepository tags) { OnEvent = CreateNewPost; }

    public void CreateNewPost(SubmitPostEvent newpost) {
        
    }

    public Action<SubmitPostEvent> OnEvent { get; private set; }
  }
}