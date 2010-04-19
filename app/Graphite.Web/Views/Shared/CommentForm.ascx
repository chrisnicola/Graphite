<%@ Control Language="C#" Inherits="ModelViewUserControl<Graphite.Core.Domain.Comment>" %>

	<link type="text/css" href="/Scripts/themes/base/ui.all.css" rel="stylesheet" />
	<script type="text/javascript" src="/Scripts/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="/Scripts/ui/ui.core.js"></script>
	<script type="text/javascript" src="/Scripts/ckeditor/ckeditor.js"></script>

<p>
	Name: 
	<%=Html.TextBox("NewComment.Author", Model.Author)%>
</p>
<p>
	Email: 
 <%=Html.TextBox("NewComment.EmailAddress", Model.EmailAddress)%>
</p>
<p>
	Website: 
 <%=Html.TextBox("NewComment.WebAddress", Model.WebAddress)%>
</p>
<p>
	Content: <br/>
	<%=Html.TextArea("NewComment.Content", Model.Content, new { @class = "ckeditor" })%>
</p>
!{Html.AntiForgeryToken()}