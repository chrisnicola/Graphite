<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Posts.PostEditModel>" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
  <%using (Html.BeginForm("Update","Post")) {%>
    <div>Created On: <%=Html.Encode(Model.DateCreated)%></div>
    <div>Last Edited: <%=Html.Encode(Model.DateModified)%></div>
    <%Html.RenderPartial("PostForm", Model);%>
    <%=Html.PutOverrideTag()%>
    <%=this.SubmitButton("Submit")%>
  <%}%>
</asp:Content>
