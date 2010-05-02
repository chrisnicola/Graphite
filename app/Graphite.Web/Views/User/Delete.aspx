<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Users.DeleteUserViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Users" %>

<% using (Html.BeginForm<UserController>(x => x.Destroy(Model), FormMethod.Post, new {id = "Delete"})) { %>
	<%= Html.AntiForgeryToken() %>
	<%= this.Hidden("_method").Value("delete") %>
    <div>Are you sure you want to delete <%= Model.Username %>?</div>
    <div><%= Html.ActionLink("Cancel", "Index") %> |  <a href="javascript: void(0);" onclick="document.Delete.submit();return false;">Delete Post</a></div>
<% } %>


