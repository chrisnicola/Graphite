<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.PostShowWithCommentsViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Comments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
  <% Html.RenderPartial("PostView", Model); %>
  <% Html.RenderPartial("Comments", Model.Comments); %>
  <% if (Model.AllowComments) {%>
   <form action="<%=Model.Id %>/comments" method="post" >
    <% Html.RenderPartial("CommentForm", Model.NewComment); %>
    </form>
  <%
     }%>
  <a href="<%=Model.Id %>/comments">test</a>
</asp:Content>