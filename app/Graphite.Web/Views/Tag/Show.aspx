<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Tags.TagViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>

<asp:Content runat="server" ContentPlaceHolderID="main" >
<h1>Posts Filed Under: <%=Model.Name%></h1>

<%
  foreach (PostShowViewModel post in Model.Posts) {%>
  <%
    Html.RenderPartial("PostView", post);%>
<%
  }%>
</asp:Content>