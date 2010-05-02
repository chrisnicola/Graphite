<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Login.LoginViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Login" %>

<asp:Content ContentPlaceHolderID="main" runat="server" >
<%
  using (Html.BeginForm<LoginController>(x => x.Create(Model))) {%>
  <div class="formInput"> 
	   <%= this.TextBox(x => x.Username).Label("Username: ")%>
  </div>
  <div class="formInput">
	  <%= this.Password(x => x.Password).Label("Password: ")%>
  </div>
  <%=Html.AntiForgeryToken()%>
  <%=this.SubmitButton("Login")%>
<%
  }%>
</asp:Content>
