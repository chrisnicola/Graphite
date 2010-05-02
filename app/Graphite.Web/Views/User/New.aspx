<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Users.NewUserViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Users" %>

<asp:Content runat="server" ContentPlaceHolderID="main">
<h2>Add New User</h2>
<%
  using (Html.BeginForm("Create","User", FormMethod.Post)) {%>
  <%=Html.AntiForgeryToken()%>
<div class="formInput">
 <%=this.TextBox(x => x.Username).Label("Username: ")%>
</div>
<div class="formInput">
<%=this.TextBox(x => x.RealName).Label("Real Name: ")%>
</div>
<div class="formInput">
	<%=this.TextBox(x => x.Email).Label("Email: ")%>
</div>
<div class="formInput">
	<%=this.TextBox(x => x.Password).Label("Password: ")%>
</div>
<div class="formInput">
	<%=this.TextBox(x => x.VerifyPassword).Label("Verify Password: ")%>
</div>
<%=Html.SubmitButton("Submit", "Submit")%>
<%
  }%>
</asp:Content>



