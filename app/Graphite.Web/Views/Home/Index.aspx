<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Home.HomeIndexViewModel>" %>

<asp:Content ContentPlaceHolderID="main" runat=server >
  <h1>The Graphite Blog Engine</h1>
  <% foreach (var post in Model.Posts) { %>
    <% Html.RenderPartial("PostView", post); %>
  <% } %>
</asp:Content>