﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="autocomplete" %>
<%@ OutputCache Duration="3600" VaryByParam="*" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script src="Scripts/jquery.chained.js" type="text/javascript"></script>
    <style type="text/css">#example_filter{display:none;}</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float:left">
    高级查询<br /><br />
    <div>省份:
        <select id="country" name="country">
        <option value="">--</option>
        </select>
        城市:
        <select id="city" name="city">
        <option value="">--</option>
        </select>
        
        <div id="checkfunction">
        职能:<br />
        </div>
        <div id="checkgrade">
        职位级别:<br />
        </div>
        <div id="checkbrand">
        酒店品牌:<br />
        </div>
        <div id="checkcorp">
        公司组织:<br />
        </div>
        <input type="button" id="sub" value="查询" onclick="submitSearch()"/>
    </div>
    <div>
    

    <table cellspacing="0" border="1" style="border-collapse:collapse;" id="example" rules="all" class="dataTable">
    <thead>
		<tr role="row">
        <th>职位名称</th><th>职能</th><th>酒店名称</th><th>城市</th><th>更新时间</th>
        </tr>
	</thead>
    <tbody></tbody>
</table>
    </div>
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
                url: "soaptest.aspx?ReqType=1&lovKey=0|1|2|3|4",
                type: "get",
                success: function (data) {
                    $("#checkfunction").append(data.split("|")[0]);
                    $("#country").append(data.split("|")[1]);
                    $("#city").append(data.split("|")[2]); $("#city").chained("#country");
                    $("#checkgrade").append(data.split("|")[4]);
                    $("#checkbrand").append(data.split("|")[3]);
                    $("#checkcorp").append(data.split("|")[5]);
                }
            });

            var oTable = $('#example').dataTable({
                "bProcessing": true,
                "bServerSide": true,
                "sAjaxSource": "soaptest.aspx?reqtype=2",
                "fnServerData": fnDataTablesPipeline
            });

            function getCheckList(cheklist) {
                var rs = '';
                if (cheklist.length >= 1) {
                    for (i = 0; i < cheklist.length; i++) {
                        rs += cheklist[i].defaultValue;
                        if (i < cheklist.length - 1) { rs += ','; }
                    }
                }
                return rs;
            }
            $('#sub').click(function () {
                oTable.fnFilter($('#country').val() + "|" + $('#city').val() + "|" + getCheckList($('#checkgrade :checked')) + "|" + getCheckList($('#checkbrand :checked')) + "|" + getCheckList($('#checkcorp :checked')) + "|" + getCheckList($('#checkfunction :checked')), 1);
            });


        });
		</script>
</body>
</html>
