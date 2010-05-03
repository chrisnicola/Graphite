<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.PostIndexViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
  <% Html.RenderPartial("ListPosts", Model); %>
</asp:Content>