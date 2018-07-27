<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Template_Datatable_Default" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=2.0">

    <title>TableTools example - Basic initialisation</title>
    <link href="media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="media/css/dataTables.tableTools.min.css" rel="stylesheet" />
    <link href="media/css/dataTables.scroller.min.css" rel="stylesheet" />

    <script src="media/js/jquery.js"></script>
    <script src="media/js/jquery.dataTables.min.js"></script>
    <script src="media/js/dataTables.tableTools.min.js"></script>
    <script src="media/js/dataTables.scroller.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //$('#example').DataTable({
            //    dom: 'T<"clear">lfrtip',
            //    tableTools: {
            //        "sSwfPath": "./media/swf/copy_csv_xls_pdf.swf"
            //    },
            //    "sScrollY": "255px",
            //    "bScrollInfinite": true,
            //    "bPaginate": false,
            //    "bInfo": false
            //});

            //$.ajax({
            //    type: "POST",
            //    url: "Result.aspx",
            //    data: "{}",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (msg) {
            //        document.getElementById('myAnchor').innerHTML = msg.d;
            //        $('#example').dataTable({
            //            aaData: msg.d,
            //            bProcessing: true,
            //            bServerSide: true,
            //            ajax: msg.d,
            //            deferRender: true,
            //            dom: "frtiS",
            //            scrollY: 200,
            //            scrollCollapse: true
            //        });
            //    }
            //});

            $('#example').dataTable({
                bProcessing: true,
                //bServerSide: true,
                ajax: "Result.aspx",
                deferRender: true,
                dom: "frtiS",
                scrollY: 200,
                scrollCollapse: true
            });
        });
    </script>
</head>

<body>
    <div id="myAnchor">Microsoft</div>
    <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>First name</th>
                <th>Last name</th>
                <th>ZIP / Post code</th>
                <th>Country</th>
            </tr>
        </thead>
    </table>
</body>
</html>
