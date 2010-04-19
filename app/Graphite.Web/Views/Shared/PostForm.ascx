<%@ Control Language="C#" Inherits="ModelViewUserControl<PostEditModelBase>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>

	<link type="text/css" href="/Scripts/themes/base/ui.all.css" rel="stylesheet" />
	<script type="text/javascript" src="/Scripts/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="/Scripts/ui/ui.core.js"></script>
	<script type="text/javascript" src="/Scripts/ui/ui.datepicker.js"></script>
	<script type="text/javascript" src="/Scripts/ckeditor/ckeditor.js"></script>
	<script type="text/javascript">
		$(function() {
			$(".datepicker").datepicker();
		%>);
	</script>

	<div>Author: <%= Model.AuthorUserName %></div>
	<div>
		<%= this.TextBox(m => m.Title).Label("Title: ")%>
	</div>
	<div>
		<%= this.TextArea(m => m.Content).Class("ckeditor")%>
	</div>
	<div>
		<%= this.TextBox(m => m.Tags).Label("Tags: ")%>
	</div>
	<div>
		<%= this.TextBox(m => m.DatePublished)
						.Value(Model.DatePublished ?? DateTime.Today)
							.Format("{0:dd-MMMM-yyyy%>")
								.Label("Publish Date: ")
									.Class("datepicker")
										.Attr("readonly", "readonly")%>
	</div>
	<div>
		<%= this.CheckBox(m => m.AllowComments).Label("Allow Comments: ")%>
	</div>
	<div>
		<%= this.CheckBox(m => m.Published).Label("Publish: ")%>
	</div>
	<%Html.AntiForgeryToken();%>