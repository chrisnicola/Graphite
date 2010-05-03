<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.DeletePostViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
  <% using (Html.BeginForm("Destroy", "Posts", FormMethod.Post, new {name = "Delete"})) { %>
  <%=Html.AntiForgeryToken() %>
  <%= Html.DeleteOverrideTag() %>
    <div>Are you sure you want to delete the post: "<%=Model.Title %>"?</div>
    <div><%= Html.ActionLink("Cancel", "Index")%> |  <a href="javascript: void(0);" onclick="document.Delete.submit();return false;">Delete</a></div>
  <% } %>
</asp:Content>