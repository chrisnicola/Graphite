<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.PostNewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
<% using (Html.BeginForm<PostController>(x => x.Create(Model))) { %>
  <% this.RenderPartial("PostForm", x=>x); %>
  <input type="submit" value="Save" /> 
<% } %>
</asp:Content>