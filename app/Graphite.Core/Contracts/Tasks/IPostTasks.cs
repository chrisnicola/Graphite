using System;
using System.Collections.Generic;
using Graphite.Core.Domain;
using Graphite.Core.Messages;

namespace Graphite.Core.Contracts.Tasks{
  public interface IPostTasks{
    Post GetWithComments(Guid id);

    IEnumerable<Post> GetAll();

    Post Get(Guid id);

    Post SaveNewPost(PostCreateDetails post);

    Post UpdatePost(PostEditDetails post);

    void Delete(Guid id);

    IEnumerable<Post> GetRecentPublishedPosts(int i);

    Post ImportPost(PostImportDetails postDetails);
  }
}