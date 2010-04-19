<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.PostShowWithCommentsViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
<% Html.RenderPartial("PostView", Model); %>
<% Html.RenderPartial("Comments", Model.Comments); %>
</asp:Content>