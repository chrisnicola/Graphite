<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.PostEditModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
  <%using (Html.BeginForm<PostController>(x => x.Update(Model))) {%>
    <div>Created On: <%=Html.Encode(Model.DateCreated)%></div>
    <div>Last Edited: <%=Html.Encode(Model.DateModified)%></div>
    <%Html.RenderPartial("PostForm", Model);%>
    <%=Html.PutOverrideTag()%>
    <%=this.SubmitButton("Submit")%>
  <%}%>
</asp:Content>
