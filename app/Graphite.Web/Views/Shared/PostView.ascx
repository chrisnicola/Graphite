<%@ Control Language="C#" Inherits="ModelViewUserControl<PostShowViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>

  <div class="post">
	<h2 class="title"><%= Html.ActionLink<PostController>(c => c.Show(Model.Slug), Model.Title)%></h2>
	<div class="author">By ${post.AuthorRealName}</div>
	<div class="content"><%= Model.Content%></div>
	<div class="footer">
		<div>Published on <%= Model.DatePublished.HasValue ? Model.DatePublished.Value.ToShortDateString() : "Unpublished" %></div>
		<div class="tags">
			<%= this.GetLinksForTags(x => x.TagsList)%>
		</div>
	</div>
</div>