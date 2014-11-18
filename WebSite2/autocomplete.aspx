﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="autocomplete.aspx.cs" Inherits="autocomplete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script src="Scripts/jquery.chained.js" type="text/javascript"></script>

</head>
    <body>
    <form id="form1" runat="server">
        <div>Province
        <select id="country" name="country">
        <option value="">--</option>
        </select>
        City
        <select id="city" name="city">
        <option value="">--</option>
        </select>
        Job Grad
        <select id="grad" name="grad">
        <option value="">--</option>
        </select>
        Brand
        <select id="brand" name="brand">
        <option value="">--</option>
        </select>
        <asp:GridView ID="DataGV" runat="server">
        </asp:GridView>

    </div>
    <div>
    <input type="button" id="sub" value="search" onclick="submitSearch()"/>

    <table cellspacing="0" border="1" style="border-collapse:collapse;" id="example" rules="all" class="dataTable">
    <thead>
		<tr role="row">
        <th>ApplicationUrl</th><th>categoryLists</th><th>compensationMax</th><th>compensationMin</th><th>advert_Id</th><th>generalApplicaiton</th><th>id</th><th>jobNumber</th><th>jobTitle</th><th>Language</th><th>location</th><th>postingEnd</th><th>postingStart</th><th>postingStatus</th><th>showCompensation</th><th>showRecruiter</th><th>siteLanguage</th><th>status</th><th>advertId</th>
        </tr>
	</thead>
    <tbody></tbody>
</table>
    </div>
    </form>
    <script type="text/javascript" charset="utf-8">
        var oCache = {
            iCacheLower: -1
        };

        function fnSetKey(aoData, sKey, mValue) {
            for (var i = 0, iLen = aoData.length; i < iLen; i++) {
                if (aoData[i].name == sKey) {
                    aoData[i].value = mValue;
                }
            }
        }

        function fnGetKey(aoData, sKey) {
            for (var i = 0, iLen = aoData.length; i < iLen; i++) {
                if (aoData[i].name == sKey) {
                    return aoData[i].value;
                }
            }
            return null;
        }

        function fnDataTablesPipeline(sSource, aoData, fnCallback, oSettings) {
            var iPipe = 5; /* Ajust the pipe size */

            var bNeedServer = false;
            var sEcho = fnGetKey(aoData, "sEcho");
            var iRequestStart = fnGetKey(aoData, "iDisplayStart");
            var iRequestLength = fnGetKey(aoData, "iDisplayLength");
            var iRequestEnd = iRequestStart + iRequestLength;
            oCache.iDisplayStart = iRequestStart;

            /* outside pipeline? */
            if (oCache.iCacheLower < 0 || iRequestStart < oCache.iCacheLower || iRequestEnd > oCache.iCacheUpper) {
                bNeedServer = true;
            }

            /* sorting etc changed? */
            if (oCache.lastRequest && !bNeedServer) {
                for (var i = 0, iLen = aoData.length; i < iLen; i++) {
                    if (aoData[i].name != "iDisplayStart" && aoData[i].name != "iDisplayLength" && aoData[i].name != "sEcho") {
                        if (aoData[i].value != oCache.lastRequest[i].value) {
                            bNeedServer = true;
                            break;
                        }
                    }
                }
            }

            /* Store the request for checking next time around */
            oCache.lastRequest = aoData.slice();

            if (bNeedServer) {
                if (iRequestStart < oCache.iCacheLower) {
                    iRequestStart = iRequestStart - (iRequestLength * (iPipe - 1));
                    if (iRequestStart < 0) {
                        iRequestStart = 0;
                    }
                }

                oCache.iCacheLower = iRequestStart;
                oCache.iCacheUpper = iRequestStart + (iRequestLength * iPipe);
                oCache.iDisplayLength = fnGetKey(aoData, "iDisplayLength");
                fnSetKey(aoData, "iDisplayStart", iRequestStart);
                fnSetKey(aoData, "iDisplayLength", iRequestLength * iPipe);

                oSettings.jqXHR = $.getJSON(sSource, aoData, function (json) {
                    /* Callback processing */
                    oCache.lastJson = jQuery.extend(true, {}, json);

                    if (oCache.iCacheLower != oCache.iDisplayStart) {
                        json.aaData.splice(0, oCache.iDisplayStart - oCache.iCacheLower);
                    }
                    json.aaData.splice(oCache.iDisplayLength, json.aaData.length);

                    fnCallback(json)
                });
            }
            else {
                json = jQuery.extend(true, {}, oCache.lastJson);
                json.sEcho = sEcho; /* Update the echo for each response */
                json.aaData.splice(0, iRequestStart - oCache.iCacheLower);
                json.aaData.splice(iRequestLength, json.aaData.length);
                fnCallback(json);
                return;
            }
        }

        $(document).ready(function () {

            $.ajax({
                url: "soaptest.aspx?ReqType=1&lovKey=0",
                type: "get",
                success: function (data) { $("#country").html(data); }
            });
            $.ajax({
                url: "soaptest.aspx?ReqType=1&lovKey=2",
                type: "get",
                success: function (data) { $("#city").html(data); $("#city").chained("#country"); }
            });
            $.ajax({
                url: "soaptest.aspx?ReqType=1&lovKey=1",
                type: "get",
                success: function (data) { $("#grad").html(data); }
            });
            $.ajax({
                url: "soaptest.aspx?ReqType=1&lovKey=3",
                type: "get",
                success: function (data) { $("#brand").html(data); }
            });
            var oTable = $('#example').dataTable({
                "bProcessing": true,
                "bServerSide": true,
                "sAjaxSource": "soaptest.aspx?reqtype=2",
                "fnServerData": fnDataTablesPipeline
            });


            $('#sub').click(function () {
                oTable.fnFilter($('#country').val(),1);
            });

            
        });
		</script>
</body>
</html>