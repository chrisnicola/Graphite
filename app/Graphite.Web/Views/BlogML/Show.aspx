<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

<% using (Html.BeginForm("Import","BlogML", FormMethod.Post, new {enctype= "multipart/form-data"})) {%>
   BlogML File: <input type="file" name="blogml" id="blogml" /> <input type="submit" value="Import" />
<%}%>