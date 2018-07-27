<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Sample.aspx.cs" Inherits="plugins_sparkline_Sample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <p>
        Inline Sparkline: <span class="inlinesparkline">1,4,4,7,5,9,10</span>
    </p>
    <p>
        Sparkline with dynamic data: <span class="dynamicsparkline">Loading..</span>
    </p>
    <p>
        Bar chart with dynamic data: <span class="dynamicbar">Loading..</span>
    </p>
    <p>
        Bar chart with inline data: <span class="inlinebar">1,3,4,5,3,5</span>
    </p>

</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script type="text/javascript">
        $(function () {
            /** This code runs when everything has been loaded on the page */
            /* Inline sparklines take their values from the contents of the tag */
            $('.inlinesparkline').sparkline();

            /* Sparklines can also take their values from the first argument 
            passed to the sparkline() function */
            var myvalues = [10, 8, 5, 7, 4, 4, 1];
            $('.dynamicsparkline').sparkline(myvalues, {
                type: 'line',
                highlightLineColor: undefined
            });

            /* The second argument gives options such as chart type */
            $('.dynamicbar').sparkline(myvalues, { type: 'bar', barColor: 'green' });

            /* Use 'html' instead of an array of values to pass options 
            to a sparkline with data in the tag */
            $('.inlinebar').sparkline('html', { type: 'bar', barColor: 'red' });
        });
    </script>
</asp:Content>

