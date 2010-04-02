using System;
using System.Collections.Generic;
using System.Linq;
using Graphite.Core.Contracts.DataInterfaces;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;
using Graphite.Core.Messages;
using SharpArchContrib.Castle.NHibernate;

namespace Graphite.ApplicationServices.Tasks{
  public class PostTasks : IPostTasks{
    readonly IPostRepository _posts;
    readonly IUserRepository _users;
    readonly ITagRepository _tags;

    public PostTasks(IPostRepository posts, IUserRepository users, ITagRepository tags) {
      _posts = posts;
      _users = users;
      _tags = tags;
    }

    public Post GetWithComments(Guid id) { return _posts.GetWithComments(id); }

    public IEnumerable<Post> GetAll() { return _posts.FindAll(); }

    public Post Get(Guid id) { return _posts.Get(id); }

    [Transaction]
    public Post SaveNewPost(PostCreateDetails details) {
      Post post = CreateNewPostFromDetails(details);
      post.Author = _users.GetUser(details.AuthorUserName);
      return _posts.Save(post);
    }

    [Transaction]
    public Post UpdatePost(PostEditDetails details) {
      Post post = _posts.Get(details.Id);
      post.Title = details.Title;
      post.ModifiedBy = _users.GetUser(details.AuthorUserName);
      post.Content = details.Content;
      post.AllowComments = details.AllowComments;
      post.DateModified = DateTime.Now;
      post.SetSlugForPost(details.Slug);
      post.DatePublished = details.DatePublished;
      post.Published = post.Published;
      UpdateTagsForPost(post, details.Tags);
      EnsurePostSlugIsUnique(post);
      return post;
    }

    [Transaction]
    public void Delete(Guid id) { _posts.Delete(id); }

    public IEnumerable<Post> GetRecentPublishedPosts(int i) { return _posts.GetRecentPublishedPosts(i); }

    public Post ImportPost(PostImportDetails details) {
      Post post = CreateNewPostFromDetails(details);
      post.Author = _users.GetUser(details.AuthorUserName);
      post.DateCreated = details.DateCreated;
      post.DateModified = details.DateModified;
      return _posts.Save(post);
    }

    Post CreateNewPostFromDetails(PostDetailsBase details) {
      var post = new Post {
        Title = details.Title,
        Content = details.Content,
        AllowComments = details.AllowComments,
        Published = details.Published,
        DateCreated = DateTime.Now,
        DateModified = DateTime.Now,
        DatePublished = details.DatePublished,
        Tags = GetTagsFromString(details.Tags)
      };
      if (details.Published) post.PublishOn(details.DatePublished);
      post.SetSlugForPost(details.Slug);
      EnsurePostSlugIsUnique(post);
      return post;
    }

    IList<Tag> GetTagsFromString(string tagstring) {
      if (string.IsNullOrEmpty(tagstring)) return new List<Tag>();
      string[] taglist = tagstring.Split(new[] {' ', ',', ';'});
      IEnumerable<Tag> tags = _tags.FindAll().Where(t => taglist.Contains(t.Name));
      IEnumerable<Tag> newtags = taglist.Where(t => tags.All(tag => tag.Name != t)).Select(t => new Tag {Name = t});
      return tags.Union(newtags).ToList();
    }

    void EnsurePostSlugIsUnique(Post post) {
      string slug = post.Slug;
      int n = 1;
      List<Post> posts = _posts.FindAll().Where(p => p.Slug == post.Slug).ToList();
      while (posts.Where(p => p.Slug == post.Slug && p.Id != post.Id).Count() > 0) {
        post.Slug = slug + n;
        n++;
      }
    }

    void UpdateTagsForPost(Post post, string tagstring) {
      IList<Tag> taglist = GetTagsFromString(tagstring);
      foreach (Tag tag in post.Tags) if (!taglist.Contains(tag)) post.Tags.Remove(tag);
      foreach (Tag tag in taglist) if (!post.Tags.Contains(tag)) post.Tags.Add(tag);
    }
  }
}