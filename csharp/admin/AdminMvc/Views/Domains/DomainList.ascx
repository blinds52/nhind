﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<DomainModel>>" %>
<%@ Import Namespace="AdminMvc.Models"%>
<%@ Import Namespace="MvcContrib.UI.Pager"%>
<%@ Import Namespace="MvcContrib.Pagination"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>

<%= Html.Grid(Model)
    .Attributes(new Dictionary<string, object>{{"class", "grid ui-widget ui-widget-content"}})
    .HeaderRowAttributes(new Dictionary<string, object>{{"class", "ui-widget-header"}})
    .Columns(
        column =>
            {
                column.For(d => d.Name);
                column.For(d => d.Status).Attributes(@class => "status");
                column.For(d => d.CreateDate);
                column.For(d => d.UpdateDate);
                column.For(d => Html.ActionLink("View", "Details", new {id = d.ID}, new { @class = "view-details"}));
                column.For(d => Html.ActionLink("Addresses", "Addresses", new { id = d.ID }));
                column.For(d => Html.ActionLink("Anchors", "Anchors", new { id = d.ID }));
                column.For(d => Html.ActionLink("Certificates", "Certificates", new { id = d.ID }));
                column.For(d => d.IsEnabled
                                    ? Html.ActionLink("Disable", "Disable", new {id = d.ID}, new {@class = "enable-disable-domain"})
                                    : Html.ActionLink("Enable", "Enable", new { id = d.ID }, new { @class = "enable-disable-domain" }));
                column.For(d => Html.ActionLink("Delete", "Delete", new { id = d.ID }, new { @class = "toolbar-button delete-domain" }));
            })%>
            
<%= Html.Pager((IPagination)Model) %>

<div id="confirm-dialog" style="display: none;"></div>

<script type="text/javascript" language="javascript">
    $(function() {
        $('a.delete-domain')
            .button({ icons: { primary: "ui-icon-trash" }, text: false })
            .click(function(event) { confirmDelete(event, $('#confirm-dialog'), $(this), 'Are you sure want to delete this domain?', 'Domain') });

        $('a.enable-disable-domain').click(enableDisableDomain);
        $('a.view-details').click(function(event) {
            showDetailsDialog(event, $(this));
        });
    });
</script>
