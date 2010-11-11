﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AnchorUploadModel>" %>
<%@ Import Namespace="AdminMvc.Models"%>
<%@ Import Namespace="AdminMvc.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Certificate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Certificate</h2>

    <%= Html.ValidationSummary("Please correct the errors and try again.", new { @class = "ui-state-error", style = "padding: 0.5em" })%>

    <% using (Html.BeginForm("Add", "Anchors", null, FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
    
        <fieldset class="ui-widget-content">
        
            <span class="display-label">Owner</span>
            <span class="display-field"><%= Html.DisplayFor(m => m.Owner) %></span>
            <br class="clear" />

            <span class="display-label"><%= Html.LabelFor(m => m.Purpose) %></span>
            <span class="display-field"><%= Html.DropDownListFor(m => m.Purpose, AnchorUploadModel.PurposeTypeList) %></span>
            <br class="clear" />
        
            <span class="display-label">Certificate Path</span>
            <span class="display-field"><%= Html.File("certificateFile", new { @class = "ui-widget-content" })%></span>
            <br class="clear" />
            
            <span class="display-label"><%= Html.LabelFor(m => m.Password) %></span>
            <span class="display-field"><%= Html.PasswordFor(m => m.Password, new { @class = "ui-widget-content" })%></span>
            <span class="editor-validator"><%= Html.ValidationMessageFor(m => m.Password, "*", new { @class = "ui-state-error-text" })%></span>
            <br class="clear" />

            <span class="display-label"><%= Html.LabelFor(m => m.PasswordConfirm) %></span>
            <span class="display-field"><%= Html.PasswordFor(m => m.PasswordConfirm, new { @class = "ui-widget-content date-text" })%></span>
            <span class="editor-validator"><%= Html.ValidationMessageFor(m => m.PasswordConfirm, "*", new { @class = "ui-state-error-text" })%></span>
            <br class="clear" />

            <p>
                <%= Html.HiddenFor(m => m.Owner) %>
                <input type="submit" value="Save" />
                <%= Html.ActionLink("Cancel", "Index", new { owner = Model.Owner })%>
            </p>
        </fieldset>
    
    <% } %>

</asp:Content>
