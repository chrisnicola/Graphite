<content:head>
	<link type="text/css" href="/Scripts/themes/base/ui.all.css" rel="stylesheet" />
	<script type="text/javascript" src="/Scripts/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="/Scripts/ui/ui.core.js"></script>
	<script type="text/javascript" src="/Scripts/ckeditor/ckeditor.js"></script>
</content>
<p>
	Name: 
	<%=Html.TextBox("NewComment.Author", newcomment.Author)%>
</p>
<p>
	Email: 
 <%=Html.TextBox("NewComment.EmailAddress", newcomment.EmailAddress)%>
</p>
<p>
	Website: 
 <%=Html.TextBox("NewComment.WebAddress", newcomment.WebAddress)%>
</p>
<p>
	Content: <br/>
	<%=Html.TextArea("NewComment.Content", newcomment.Content, new { @class = "ckeditor" })%>
</p>
!{Html.AntiForgeryToken()}