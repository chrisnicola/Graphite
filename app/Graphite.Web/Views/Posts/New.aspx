<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.PostNewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
<% using (Html.BeginForm("Create", "Posts")) { %>
  <% this.RenderPartial("PostForm", x=> x as PostEditModelBase); %>
  <input type="submit" value="Save" /> 
<% } %>
</asp:Content>