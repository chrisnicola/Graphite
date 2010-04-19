<%@ Control Language="C#" Inherits="ModelViewUserControl<Graphite.Core.Domain.Comment>" %>
<div class="header">
	<%= Model.Author%> | <a href="<%= Model.WebAddress%>"><%= Model.WebAddress%></a>
</div>
<div class="content"><%= Model.Content%></div>
<div clas="footer"><%= Model.DateCreated%></div>