<%@ Control Language="C#" Inherits="ModelViewUserControl<IList<Graphite.Core.Domain.Comment>>" %>
<%@ Import Namespace="Graphite.Core.Domain" %>
<div class="comments">
  <% foreach (var comment in Model) { %>
	  <div class="comment">
      <%
       Html.RenderPartial("CommentView", comment); %>
	  </div>
  <% } %>
</div>