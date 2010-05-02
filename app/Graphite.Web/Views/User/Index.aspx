<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="ModelViewPage<Graphite.Web.Controllers.Users.UserIndexViewModel>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<asp:Content runat="server" ContentPlaceHolderID="main" >
<%=
  Html.Grid(Model.Users.ToList()).AutoGenerateColumns().Columns(
    c => {
      c.For(x => Html.ActionLink<Graphite.Web.Controllers.Users.UserController>(ctl => ctl.Edit(x.Id), "Edit")).
        DoNotEncode();
      c.For(x => Html.ActionLink<Graphite.Web.Controllers.Users.UserController>(ctl => ctl.Delete(x.Id), "Delete")).
        DoNotEncode();
    }
    ) %>
<p>
    <%= Html.ActionLink<Graphite.Web.Controllers.Users.UserController>(x => x.New(null), "Create New") %>
</p>
</asp:Content>