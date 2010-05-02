<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Users.EditUserViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Users" %>
<h2>Add New User</h2>

<%
  using (Html.BeginForm<UserController>(x => x.Update(Model), FormMethod.Post)) {%>
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
	<%=this.TextBox(x => x.OldPassword).Label("Old Password: ")%>
</div>
<div class="formInput">
	<%=this.TextBox(x => x.NewPassword).Label("New Password: ")%>
</div>
<div class="formInput">
	<%=this.TextBox(x => x.VerifyPassword).Label("Verify New Password: ")%>
</div>
  <%=Html.AntiForgeryToken()%>
  <%=this.Hidden("_method").Value("put")%>
  <%=Html.SubmitButton("Submit", "Submit")%>
<%
  }%>


