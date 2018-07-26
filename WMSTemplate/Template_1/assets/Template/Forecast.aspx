<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forecast.aspx.cs" Inherits="Template_Forecast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        .success {
            background-color: yellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        Alpha :
        <asp:Label ID="LabelAlpha" runat="server"></asp:Label><br />
        Beta :
        <asp:Label ID="LabelBeta" runat="server"></asp:Label><br />
        Mean Square Error :
        <asp:Label ID="LabelMeanSquareError" runat="server"></asp:Label><br />
        Standard Error :
        <asp:Label ID="LabelStandardError" runat="server"></asp:Label><br />
        <br />

        <table border="1">
            <thead>
                <tr>
                    <td>Title</td>
                    <td>Actual</td>
                    <td>Forecast</td>
                    <td>Trend</td>
                    <td>Forecast Include Trend</td>
                    <td>Error</td>
                    <td>Sq. Error</td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterData" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Title") %></td>
                            <td><%# Eval("Actual") %></td>
                            <td><%# Eval("Forecast") %></td>
                            <td><%# Eval("Trend") %></td>
                            <td><%# Eval("ForecastIncludeTrend") %></td>
                            <td><%# Eval("Error") %></td>
                            <td><%# Eval("SquareError") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <br />

        <table border="1">
            <thead>
                <tr>
                    <td>Alpha</td>
                    <td>Beta</td>
                    <td>Mean Square Error</td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterAlphaBeta" runat="server">
                    <ItemTemplate>
                        <tr class='<%# (bool)Eval("Best") ? "success" : ""  %>'>
                            <td><%# Eval("Alpha") %></td>
                            <td><%# Eval("Beta") %></td>
                            <td><%# Eval("Value") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </form>
</body>
</html>
