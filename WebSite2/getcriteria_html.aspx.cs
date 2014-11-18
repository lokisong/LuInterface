using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Data;
using System.Xml.Linq;

public partial class getcriteria_html : System.Web.UI.Page
{
    static int TotalRecords;
    static int TotalDisplayRecords;
    static int tables = 0;
    static int counttable = 0;
    static StringBuilder postings = new StringBuilder();
    static string sSearch1 = string.Empty;

    public int getTables()
    { return tables; }

    public int getCount()
    { return counttable; }


    public int getReqType()
    {
        return Convert.ToInt32(Request["ReqType"] == null ? null : Request["ReqType"]);
    }
    public string getKeywords()
    {
        return Request["sSearch_2"] == null ? null : Request["sSearch_2"];
    }
    public string getEmail()
    {
        return Request["email"] == null ? null : Request["email"];
    }
    public string getFrequency()
    {
        return Request["frequency"] == null ? null : Request["frequency"];
    }
    public string getrCounrty()
    {
        if (sSearch1 != null && sSearch1.Split('|').Length > 0)
        {
            return (sSearch1.Split('|')[0]);
        }
        else
        {
            return null;
        }

    }
    public string getrCity()
    {
        if (sSearch1 != null && sSearch1.Split('|').Length > 1)
        {
            return (sSearch1.Split('|')[1]);
        }
        else
        {
            return null;
        }
    }
    public string getrGrad()
    {
        if (sSearch1 != null && sSearch1.Split('|').Length > 2)
        {
            return (sSearch1.Split('|')[2]);
        }
        else
        {
            return null;
        }
    }
    public string getrBrand()
    {
        if (sSearch1 != null && sSearch1.Split('|').Length > 3)
        {
            return (sSearch1.Split('|')[3]);
        }
        else
        {
            return null;
        }
    }

    public string getrOrg()
    {
        if (sSearch1 != null && sSearch1.Split('|').Length > 4)
        {
            return (sSearch1.Split('|')[4]);
        }
        else
        {
            return null;
        }
    }
    public string getrDepartment()
    {
        if (sSearch1 != null && sSearch1.Split('|').Length > 5)
        {
            return (sSearch1.Split('|')[5]);
        }
        else
        {
            return null;
        }
    }
    public string getlanguage()
    {
        return Request["language"] == null ? "UK" : Request["language"];
    }
    public string getColumns()
    {
        return Request["Columns"] == null ? "postingStartDate|jobTitle|location" : Request["Columns"];
    }
    public string getEcho()
    {
        return Request["sEcho"] == null ? "0" : Request["sEcho"];
    }
    public string getPostingTargetId()
    {
        return Request["postingtargetid"] == null ? "0" : Request["postingtargetid"];
    }
    public int getDisplayStart()
    {
        return Convert.ToInt32(Request["iDisplayStart"] == "0" ? null : Request["iDisplayStart"]);
    }
    public int getDisplayLength()
    {
        return Convert.ToInt32(Request["iDisplayLength"] == "0" ? null : Request["iDisplayLength"]);
    }
    public void setTotalRecords(int i)
    {
        TotalRecords = i;
    }
    public void setTotalDisplayRecords(int i)
    {
        TotalDisplayRecords = i;
    }
    public int getSortCol()
    {
        return Convert.ToInt32(Request["iSortCol_0"] == "0" ? null : Request["iSortCol_0"]);
    }
    public string getSortDir()
    {
        return Request["sSortDir_0"] == null ? "ASCENDING" : Request["sSortDir_0"] == "asc" ? "ASCENDING" : "DESCENDING";
    }

    public string[] sortColumns = new string[] { "job_title", "LOV7", "LOV20", "DEPTLEVEL3", "posting_start_date" };
    //    public string[] sortColumns = new string[] { "job_title", "LOV7", "LOV20", "DEPTLEVEL3","posting_start_date" };
    public string totalResult;

    protected void Page_Load(object sender, EventArgs e)
    {
        sSearch1 = Request["sSearch_1"];
        DataTable SearchResult = new DataTable();
        string soapRequest = string.Empty;
        string soapReqHead = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ws=""http://ws.mrted.com/"">
<soapenv:Header>
<wsse:Security soapenv:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
<wsse:UsernameToken wsu:Id=""UsernameToken-1"">
  <wsse:Username>Q8QFK026203F3VBQB8MV4V4K5:guest:FO</wsse:Username> 
  <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">guest</wsse:Password> 
  <wsse:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"">R+hhapAW4u9hSNlQ5Qmigg==</wsse:Nonce> 
  <wsu:Created>2013-03-13T10:30:42.052Z</wsu:Created> 
  </wsse:UsernameToken>
  </wsse:Security>
  </soapenv:Header>
<soapenv:Body>";
        string soapReqBody0 = "<ws:getAdvertisementsSortingColumns></ws:getAdvertisementsSortingColumns>";
        string soapReqBody1 = @"<ws:getAdvertisements>
         <ws:firstResult>" + getDisplayStart().ToString() + @"</ws:firstResult>
         <ws:maxResults>" + getDisplayLength().ToString() + @"</ws:maxResults>    
       <ws:searchCriteriaDto>
            <adLanguages>
               <language></language>
            </adLanguages>
            <categoryLists>
               <categoryList>
                  <categoryIds>
                     <categoryId/>
                  </categoryIds>
                  <order>A</order>
               </categoryList>
            </categoryLists>
            <contractTypes>
               <contractType></contractType>
            </contractTypes>
            <countries>
               <country></country>
            </countries>
            <customLovs>
<customLovGroup><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov>" + getrDepartment() + @"</customLovGroup>
<customLovGroup><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov>" + getrCounrty() + @"</customLov></customLovGroup>
<customLovGroup><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov>" + getrGrad() + @"</customLov></customLovGroup>
<customLovGroup><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov>" + getrCity() + @"</customLov></customLovGroup>
<customLovGroup><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov/><customLov>" + getrBrand() + @"</customLov></customLovGroup>
            </customLovs>
            <generalApplication/>
            <jobNumber/>
            <keywords/>
            <organizationIds>
               <organizationId>" + getrOrg() + @"
               </organizationId>
            </organizationIds>
            <postedSince/>
            <regions>
               <region/>
            </regions>
            <scheduleTypes>
               <scheduleType/>
            </scheduleTypes>
         </ws:searchCriteriaDto>
<ws:sortingDetailsDto> <columnName>" + sortColumns[getSortCol()] + @"</columnName> <sortType>" + getSortDir() + @"</sortType> </ws:sortingDetailsDto> <ws:langCode>" + getlanguage() + @"</ws:langCode>
</ws:getAdvertisements>";
        string soapReqBody11 = @"<ws:getAdvertisements>
         <ws:firstResult>" + getDisplayStart().ToString() + @"</ws:firstResult>
         <ws:maxResults>" + getDisplayLength().ToString() + @"</ws:maxResults>    
       <ws:searchCriteriaDto>
            <adLanguages>
               <language></language>
            </adLanguages>
            <categoryLists>
               <categoryList>
                  <categoryIds>
                     <categoryId/>
                  </categoryIds>
                  <order>A</order>
               </categoryList>
            </categoryLists>
            <contractTypes>
               <contractType></contractType>
            </contractTypes>
            <countries>
               <country></country>
            </countries>
            <customLovs>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup>" + LovlizeData(getrDepartment()) + @"</customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup>" + LovlizeData(getrCounrty()) + @"</customLovGroup>
<customLovGroup>" + LovlizeData(getrCity()) + @"</customLovGroup>
<customLovGroup>" + LovlizeData(getrBrand()) + @"</customLovGroup>
<customLovGroup>" + LovlizeData(getrGrad()) + @"</customLovGroup>
            </customLovs>
            <generalApplication/>
            <jobNumber/>
            <keywords/>
            <organizationIds>
               <organizationId>" + getrOrg() + @"
               </organizationId>
            </organizationIds>
            <postedSince/>
            <regions>
               <region/>
            </regions>
            <scheduleTypes>
               <scheduleType/>
            </scheduleTypes>
         </ws:searchCriteriaDto>
<ws:sortingDetailsDto> <columnName>" + sortColumns[getSortCol()] + @"</columnName> <sortType>" + getSortDir() + @"</sortType> </ws:sortingDetailsDto> <ws:langCode>CN</ws:langCode>
</ws:getAdvertisements>";
        string soapReqBody2 = @"<ws:getCriteria> <ws:langCode>CN</ws:langCode> <ws:searchCriteriaSorting> <categoryListsSorting>A</categoryListsSorting> <customLovsSorting>A</customLovsSorting> <standardLovsSorting>A</standardLovsSorting> </ws:searchCriteriaSorting> <ws:lovOrders>7,18,19,20,21</ws:lovOrders> </ws:getCriteria>";
        string soapReqBody3 = @"<ws:getAdvertisements>
         <ws:firstResult>" + getDisplayStart().ToString() + @"</ws:firstResult>
         <ws:maxResults>" + getDisplayLength().ToString() + @"</ws:maxResults>    
       <ws:searchCriteriaDto>
            <adLanguages>
               <language></language>
            </adLanguages>
            <categoryLists>
               <categoryList>
                  <categoryIds>
                     <categoryId/>
                  </categoryIds>
                  <order>A</order>
               </categoryList>
            </categoryLists>
            <contractTypes>
               <contractType></contractType>
            </contractTypes>
            <countries>
               <country></country>
            </countries>
            <customLovs>
            </customLovs>
            <generalApplication/>
            <jobNumber/>
            <keywords>" + StrReplace(getKeywords()) + @"</keywords>
            <organizationIds>
               <organizationId>
               </organizationId>
            </organizationIds>
            <postedSince/>
            <regions>
               <region/>
            </regions>
            <scheduleTypes>
               <scheduleType/>
            </scheduleTypes>
         </ws:searchCriteriaDto>
<ws:sortingDetailsDto> <columnName>" + sortColumns[getSortCol()] + @"</columnName> <sortType>" + getSortDir() + @"</sortType> </ws:sortingDetailsDto> <ws:langCode>CN</ws:langCode>
</ws:getAdvertisements>";
        string soapReqBody4 = @"<soapenv:Envelope xmlns:ws=""http://ws.mrted.com/"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
<soapenv:Header>
<wsse:Security xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" soapenv:mustUnderstand=""1"">
<wsse:UsernameToken wsu:Id=""UsernameToken-3"">
<wsse:Username>Q8QFK026203F3VBQB8MV4V4K5:guest:FO</wsse:Username>
<wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">guest</wsse:Password>
</wsse:UsernameToken>
</wsse:Security>
</soapenv:Header>
<soapenv:Body><ws:create> 
<ws:searchAgentDto>
<deliveryFrequency>" + getFrequency() + @"</deliveryFrequency>
<email>" + getEmail() + @"</email>
<expirationDate>" + DateTime.Now.AddMonths(1).Date.ToString("yyyy-MM-dd") + @"</expirationDate>
<searchCriteria>
            <adLanguages>
               <language></language>
            </adLanguages>
            <categoryLists>
               <categoryList>
                  <categoryIds>
                     <categoryId/>
                  </categoryIds>
                  <order>A</order>
               </categoryList>
            </categoryLists>
            <contractTypes>
               <contractType></contractType>
            </contractTypes>
            <countries>
               <country></country>
            </countries>
            <customLovs>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup>" + LovlizeData(getrDepartment()) + @"</customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup></customLovGroup>
<customLovGroup>" + LovlizeData(getrCounrty()) + @"</customLovGroup>
<customLovGroup>" + LovlizeData(getrCity()) + @"</customLovGroup>
<customLovGroup>" + LovlizeData(getrBrand()) + @"</customLovGroup>
<customLovGroup>" + LovlizeData(getrGrad()) + @"</customLovGroup>
            </customLovs>
            <generalApplication/>
            <jobNumber/>
            <keywords/>
            <organizationIds>" + OrglizeData(getrOrg()) + @"
            </organizationIds>
            <postedSince/>
            <regions>
               <region/>
            </regions>
            <scheduleTypes>
               <scheduleType/>
            </scheduleTypes>
</searchCriteria>
</ws:searchAgentDto>
<ws:sendEmail>true</ws:sendEmail>
<ws:language>CN</ws:language>
</ws:create></soapenv:Body> </soapenv:Envelope>";

        string soapReqBody5 = @"<ws:getAdvertisementById>
         <ws:postingTargetId>" + getPostingTargetId() + @"</ws:postingTargetId>
         <ws:langCode>CN</ws:langCode>
</ws:getAdvertisementById>";
        string SoapReqTail = @"</soapenv:Body>
  </soapenv:Envelope>";

        if (getReqType() == 0)
        {
            soapRequest = soapReqHead + soapReqBody0 + SoapReqTail;
            DataSet xmlDS = new DataSet();


            xmlDS.ReadXml(PostToTalentHub("https://apiapac.lumesse-talenthub.com/CareerPortal/SOAP/FoAdvert?api_key=y77ggkxeypaw94qsau3c2h8n", soapRequest));

        }

        //GetCriteria
        if (getReqType() == 1)
        {
            soapRequest = soapReqHead + soapReqBody2 + SoapReqTail;
            System.Xml.XmlTextReader objXML = new System.Xml.XmlTextReader(PostToTalentHub("https://apiapac.lumesse-talenthub.com/CareerPortal/SOAP/FoAdvert?api_key=y77ggkxeypaw94qsau3c2h8n", soapRequest));
            XmlDocument rspXml = new XmlDocument();
            rspXml.Load(objXML);
            XmlNodeList childlist = rspXml.SelectNodes("//customlovs/customLov");
            XmlNodeList orglist = rspXml.SelectNodes("//organizations/organization");
            string lov = string.Empty;
            if (Request["lovKey"].IndexOf("|") > 0)
            {
                foreach (string lovCount in Request["lovKey"].Split('|'))
                {
                    if (lovCount == "1" || lovCount == "2")
                    {
                        lov = GetLinkLov(childlist[Convert.ToInt32(lovCount)]);
                    }
                    else
                    {
                        lov = GetCheckLov(childlist[Convert.ToInt32(lovCount)]);
                    }
                    Response.Write(lov);
                    Response.Write("|");
                }
                Response.Write(GetOrgLov(orglist));
            }
            else
            {
                if (Request["lovKey"] == "1" || Request["lovKey"] == "2")
                {
                    lov = GetLinkLov(childlist[Convert.ToInt32(Request["lovKey"])]);
                }
                else
                {
                    lov = GetCheckLov(childlist[Convert.ToInt32(Request["lovKey"])]);
                }
                Response.Write(lov);
            }
            Response.End();
        }

        //GetDataTable 2:advanced search 3:keywords search
        if (getReqType() == 2 || getReqType() == 3)
        {
            if (getReqType() == 2)
            {
                soapRequest = soapReqHead + soapReqBody11 + SoapReqTail;
            }
            else
            {
                soapRequest = soapReqHead + soapReqBody3 + SoapReqTail;
            }
            DataSet xmlDS = new DataSet();
            xmlDS.ReadXml(PostToTalentHub("https://apiapac.lumesse-talenthub.com/CareerPortal/SOAP/FoAdvert?api_key=y77ggkxeypaw94qsau3c2h8n", soapRequest));

            DataTable xmlDT = xmlDS.Tables["advertisement"];
            DataTable resultDT = xmlDS.Tables["advertisementResult"];
            if (resultDT != null)
            {
                totalResult = resultDT.Rows[0][1].ToString();
            }
            if (xmlDT != null)
            {
                SearchResult.Columns.Add("jobTitle");
                SearchResult.Columns.Add("Function");
                SearchResult.Columns.Add("Hotel");
                SearchResult.Columns.Add("Location");
                SearchResult.Columns.Add("Postdate");
                SearchResult.Columns.Add("URL");
                SearchResult.Columns.Add("advertisement_Id");
                //link for job title
                //string jLink = "https://apac.recruitmentplatform.com/syndicated/lay/jsoutputinitrapido.cfm?component=lay9999_jdesc100a&option=52&sort=DESC&respnr=1&ID=P05FK026203F3VB8M688N8MVB&Resultsperpage=10&lg=UK&mask=ihgexternal&nPostingTargetID=";

                string jLink = "jd.html?reqtype=5&postingtargetid=";

                //string jLink1 = "<a target=_blank href=https://apac.recruitmentplatform.com/syndicated/lay/jsoutputinitrapido.cfm?component=lay9999_jdesc100a&option=52&sort=DESC&respnr=1&ID=P05FK026203F3VB8M688N8MVB&Resultsperpage=10&lg=UK&mask=ihgexternal&nPostingTargetID=";
                string jLink1 = "<a target=_blank href=jd.html?reqtype=5&postingtargetid=";
                string jLink2 = ">";
                string jLink3 = "</a>";
                //Add job title to result
                if (xmlDT.Columns["id"] != null && xmlDT.Columns["jobTitle"] != null && xmlDT.Columns["postingStartDate"] != null && xmlDT.Columns["advertisement_Id"] != null)
                {
                    foreach (DataRow dr in xmlDT.Rows)
                    {
                        SearchResult.Rows.Add(jLink1 + dr["id"] + jLink2 + dr["jobTitle"].ToString().Replace(",","，").Replace("\r\n","").TrimEnd()+ jLink3, "", "", "", dr["postingStartDate"].ToString().Substring(0, 10), jLink + dr["id"], dr["advertisement_Id"]);
                    }
                    //Add location & grad to result
                    if (xmlDS.Tables["customLov"] != null)
                    {
                        SearchResult = AddtoResult(xmlDS, "19", SearchResult, "Location");
                        SearchResult = AddtoResult(xmlDS, "7", SearchResult, "Function");
                        SearchResult = AddOrgtoResult(xmlDS, "26", SearchResult, "Hotel");
                    }
                    SearchResult.Columns.Remove("advertisement_Id");
                }
            }

            Response.Write(DataTableToJson(SearchResult, getEcho(), getDisplayStart(), totalResult));
            Response.End();
        }
        //JobAlert
        if (getReqType() == 4)
        {
            soapRequest = soapReqBody4;
            System.Xml.XmlTextReader objXML = new System.Xml.XmlTextReader(PostToTalentHub("https://apiapac.lumesse-talenthub.com/CareerPortal/SOAP/SearchAgent?api_key=y77ggkxeypaw94qsau3c2h8n", soapRequest));
            XmlDocument rspXml = new XmlDocument();
            rspXml.Load(objXML);
            Response.Write("success");
            Response.End();
        }
        if (getReqType() == 5)
        {
            soapRequest = soapReqHead + soapReqBody5 + SoapReqTail;
            System.Xml.XmlDocument objXML = new System.Xml.XmlDocument();         
            objXML.Load(PostToTalentHub("https://apiapac.lumesse-talenthub.com/CareerPortal/SOAP/FoAdvert?api_key=y77ggkxeypaw94qsau3c2h8n", soapRequest));
            XmlNamespaceManager xnm = new XmlNamespaceManager(new NameTable());
            xnm.AddNamespace("ns2", "http://ws.mrted.com/");
            Response.Write(GetJD(objXML.SelectSingleNode("//ns2:advertisement",xnm)));
            Response.End();
        }
    }

    public String LovlizeData(string original)
    {
        if (original != null)
        {
            original = original.Replace(",", "</customLov><customLov>");
            return "<customLov>" + original + "</customLov>";
        }
        else
        {
            return "";
        }
    }
    public String OrglizeData(string original)
    {
        if (original != null)
        {
            original = original.Replace(",", "</organizationId><organizationId>");
            return "<organizationId>" + original + "</organizationId>";
        }
        else
        {
            return "";
        }
    }

    public DataTable AddtoResult(DataSet xmlDS, String lovOrd, DataTable SearchResult, String resultCol)
    {
        DataRow[] custLovDR = xmlDS.Tables["customLov"].Select("order='" + lovOrd + "'");
        string custIds = string.Empty;
        foreach (DataRow dr in custLovDR)
        {
            custIds += "'" + dr["customLov_Id"].ToString() + "'";
            custIds += ",";
        }
        if (custIds.Length > 1)
        {
            custIds = custIds.Substring(0, custIds.Length - 1);
            DataRow[] criteriaDR = xmlDS.Tables["criteria"].Select("customLov_Id in (" + custIds + ")");
            string criIds = string.Empty;
            foreach (DataRow dr in criteriaDR)
            {
                criIds += "'" + dr["criteria_Id"].ToString() + "'";
                criIds += ",";
            }

            if (criIds.Length > 1) { criIds = criIds.Substring(0, criIds.Length - 1); }

            DataRow[] criterionDR = xmlDS.Tables["criterion"].Select("criteria_Id in (" + criIds + ")");
            for (int i = 0; i < custLovDR.Length; i++)
            {
                custLovDR[i][0] = criterionDR[i]["label"];
            }
            int criIndex = 0;
            for (int j = 0; j < SearchResult.Rows.Count; j++)
            {
                if (SearchResult.Rows[j]["advertisement_Id"].ToString().Trim() == custLovDR[criIndex]["customLovs_Id"].ToString().Trim())
                {
                    SearchResult.Rows[j][resultCol] = custLovDR[criIndex][0];
                    criIndex++;
                    if (criIndex >= custLovDR.Length)
                    {
                        break;
                    }
                }
            }
        }
        return SearchResult;
    }
    //add organization
    public DataTable AddOrgtoResult(DataSet xmlDS, String lovOrd, DataTable SearchResult, String resultCol)
    {
        string custIds = string.Empty;
        DataRow[] tempdr;
        int maxlevel;
        foreach (DataRow dr in xmlDS.Tables["Organizations"].Rows)
        {
            tempdr = xmlDS.Tables["Organization"].Select("organizations_Id ='" + dr[0].ToString() + "'");
            maxlevel = tempdr.Length - 1;
            SearchResult.Select("advertisement_Id ='" + dr[1].ToString() + "'")[0][resultCol] = tempdr[maxlevel]["label"];
        }

        return SearchResult;
    }

    /// <summary>
    /// 发送SOAP请求
    /// </summary>
    /// <param name="m_URL"></param>
    /// <param name="m_PostMsg"></param>
    public Stream PostToTalentHub(string m_URL, string m_PostMsg)
    {
        //Create a request object 
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(m_URL);
        request.ContentType = "text/xml;charset=\"utf-8\"";
        request.Method = "POST";
        request.Headers.Add("SOAPAction", "\"\"");
        //request.Proxy = new WebProxy("proxydirectap.tycoelectronics.com", 80);
        //request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
        //request.AutomaticDecompression = DecompressionMethods.GZip;
        //DecompressionMethods.Deflate;
        // This is where you need to build the SOAP message 
        //m_PostMsg = "<soap:Envelope " etc. ........ 

        System.IO.StreamWriter objStream = new System.IO.StreamWriter(request.GetRequestStream(), Encoding.UTF8);
        objStream.Write(m_PostMsg);
        objStream.Close();
        objStream = null;


        HttpWebResponse objHTTPRes = default(HttpWebResponse);
        objHTTPRes = (HttpWebResponse)request.GetResponse();
        System.IO.Stream SourceStream = objHTTPRes.GetResponseStream();

        //ReadXmlResponse(objHTTPRes);

        //string ret = null;
        //System.IO.StreamReader sr = new System.IO.StreamReader(SourceStream);
        //ret = sr.ReadToEnd();

        //It's up to you how you save the response 
        //You could save it as an XML document

        //XmlDocument rspXml = new XmlDocument();
        //rspXml.Load("c:/ceritaria.xml");
        //System.Xml.XmlTextReader objXML = new System.Xml.XmlTextReader(SourceStream);

        //XmlNodeList childlist = rspXml.SelectNodes("//customlovs/customLov");
        //DataSet xmlDS = new DataSet();
        return SourceStream;

        //Or you could save it to a String
        //string ret = null;
        //System.IO.StreamReader sr = new System.IO.StreamReader(SourceStream);
        //ret = sr.ReadToEnd();
        //return ret;
    }

    private static XmlDocument ReadXmlResponse(WebResponse response)
    {
        StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        String retXml = sr.ReadToEnd();
        sr.Close();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(retXml);
        return doc;
    }

    public static DataSet ConvertXMLToDataSet(string xmlData)
    {
        StringReader stream = null;
        XmlTextReader reader = null;
        try
        {
            DataSet xmlDS = new DataSet();
            stream = new StringReader(xmlData);

            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);

            xmlDS.ReadXml(reader);
            return xmlDS;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }

    public static String GetLinkLov(XmlNode xmlLov)
    {
        StringBuilder reLov = new StringBuilder();
        XmlNode tempcri;
        if (xmlLov.SelectSingleNode("//parents") != null)
        {
            XmlNodeList xmlCriterion = xmlLov.Clone().SelectNodes("//criteria/criterion");
            foreach (XmlNode cri in xmlCriterion)
            {
                tempcri = cri.Clone();
                reLov.Append("<option value=\"");
                reLov.Append(tempcri.SelectSingleNode("//value").InnerText.ToString());
                reLov.Append("\" class=\"");
                try
                {
                    reLov.Append(tempcri.SelectSingleNode("//activators/criterion/value").InnerText.ToString());
                }
                catch { }
                reLov.Append("\">");
                reLov.Append(tempcri.SelectSingleNode("//label").InnerText.ToString());
                reLov.Append("</option>");
            }
        }
        else
        {
            XmlNodeList xmlCriterion = xmlLov.Clone().SelectNodes("//criteria/criterion");
            foreach (XmlNode cri in xmlCriterion)
            {
                tempcri = cri.Clone();
                reLov.Append("<option value=\"");
                reLov.Append(tempcri.SelectSingleNode("//value").InnerText.ToString());
                reLov.Append("\">");
                reLov.Append(tempcri.SelectSingleNode("//label").InnerText.ToString());
                reLov.Append("</option>");
            }

        }
        return reLov.ToString();
    }

    public static String GetCheckLov(XmlNode xmlLov)
    {
        StringBuilder reLov = new StringBuilder();
        XmlNode tempcri;
        XmlNodeList xmlCriterion = xmlLov.Clone().SelectNodes("//criteria/criterion");
        foreach (XmlNode cri in xmlCriterion)
        {
            tempcri = cri.Clone();
            reLov.Append("<input type=\"checkbox\" name=\"cf\" value=\"");
            reLov.Append(tempcri.SelectSingleNode("//value").InnerText.ToString());
            reLov.Append("\"/>");
            reLov.Append(tempcri.SelectSingleNode("//label").InnerText.ToString());
        }
        return reLov.ToString();
    }

    public static String GetOrgLov(XmlNodeList xmlLov)
    {
        StringBuilder reLov = new StringBuilder();
        XmlNode tempcri;


        //foreach (XmlNode cri in xmlLov)
        //    {
        //        tempcri = cri.Clone();
        //        reLov.Append("<option value=\"");
        //        reLov.Append(tempcri.SelectSingleNode("//value").InnerText.ToString());

        //        reLov.Append("\">");
        //        reLov.Append(tempcri.SelectSingleNode("//label").InnerText.ToString());
        //        reLov.Append("</option>");
        //    }
        foreach (XmlNode cri in xmlLov)
        {
            tempcri = cri.Clone();
            reLov.Append("<input type=\"checkbox\" name=\"cf\" value=\"");
            reLov.Append(tempcri.SelectSingleNode("//value").InnerText.ToString());

            reLov.Append("\"/>");
            reLov.Append(tempcri.SelectSingleNode("//label").InnerText.ToString());
        }
        return reLov.ToString();
    }

    public static String GetJD(XmlNode xmlLov)
    {
        StringBuilder reJD= new StringBuilder();
        XmlNodeList tempnl;
        reJD.Append("<div>");
        reJD.Append(xmlLov.SelectSingleNode("//jobTitle").InnerText.ToString());
        reJD.Append("</div>");
        reJD.Append("<div>");
        reJD.Append(xmlLov.SelectSingleNode("//jobNumber").InnerText.ToString());
        reJD.Append("</div>");
        reJD.Append("<div>");
        tempnl = xmlLov.SelectNodes("//organization/label");
        reJD.Append(tempnl[tempnl.Count - 1].InnerText.ToString());
        reJD.Append("</div>");
        reJD.Append("<div>");
        if (xmlLov.SelectSingleNode("//customLovs/customLov[order='20']/label") != null)
        {
            reJD.Append(xmlLov.SelectSingleNode("//customLovs/customLov[order='20']/label").InnerText.ToString());
            reJD.Append(":");
            reJD.Append(xmlLov.SelectSingleNode("//customLovs/customLov[order='20']/criteria/criterion/label").InnerText.ToString());
        }
        reJD.Append("</div>");
        reJD.Append("<div>");
        if (xmlLov.SelectSingleNode("//customLovs/customLov[order='7']/label") != null)
        {
            reJD.Append(xmlLov.SelectSingleNode("//customLovs/customLov[order='7']/label").InnerText.ToString());
            reJD.Append(":");
            reJD.Append(xmlLov.SelectSingleNode("//customLovs/customLov[order='7']/criteria/criterion/label").InnerText.ToString());
        }
        reJD.Append("</div>");
        reJD.Append("<div>");
        if (xmlLov.SelectSingleNode("//customLovs/customLov[order='21']/label") != null)
        {
            reJD.Append(xmlLov.SelectSingleNode("//customLovs/customLov[order='21']/label").InnerText.ToString());
            reJD.Append(":");
            reJD.Append(xmlLov.SelectSingleNode("//customLovs/customLov[order='21']/criteria/criterion/label").InnerText.ToString());
        }
        reJD.Append("</div>");
        tempnl = xmlLov.SelectNodes("//customFields/customField");
        foreach(XmlNode node in tempnl)
        {
            reJD.Append("<div>");
            if (node.ChildNodes[0] != null)
            {
                reJD.Append(node.ChildNodes[0].InnerText.ToString());
            }
            reJD.Append("</div>");
            reJD.Append("<div>");
            if (node.ChildNodes[2] != null)
            {
                reJD.Append(node.ChildNodes[2].InnerText.ToString());
            }
            reJD.Append("</div>");
        }
        reJD.Append("<a href='");
        reJD.Append(xmlLov.SelectSingleNode("//applicationUrl").InnerText.ToString());
        reJD.Append("'>申请</a>");        
        return reJD.ToString();
    }

    /// <summary>
    /// 改变列类型
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DataTable ChangeColumnType(DataTable dt)
    {

        DataTable tempdt = new DataTable();
        foreach (DataColumn dc in dt.Columns)
        {
            DataColumn tempdc = new DataColumn();
            if (dc.DataType != typeof(System.Int32))
            {
                tempdc.ColumnName = dc.ColumnName;
                tempdc.DataType = dc.DataType;
            }
            else
            {
                tempdc.ColumnName = dc.ColumnName;
                tempdc.DataType = typeof(String);
            }
            tempdt.Columns.Add(tempdc);
        }
        DataRow newrow;
        foreach (DataRow dr in dt.Rows)
        {
            newrow = tempdt.NewRow();
            newrow.ItemArray = dr.ItemArray;
            tempdt.Rows.Add(newrow);
        }
        return tempdt;

    }

    /// <summary>
    /// 将DT转换为JSON
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DataTableToJson(DataTable dt, String echo, int start, string dislpay)
    {
        List<string> result = new List<string>();
        StringBuilder Json = new StringBuilder();
        //&& dt.Rows.Count>=iDisplayStart
        if (dt == null || dt.Rows.Count == 0)
        {
            Json.Append("{");
            Json.Append("\"sEcho\":" + echo);
            Json.Append(",");
            Json.Append("\"iTotalRecords\":0");
            Json.Append(",");
            Json.Append("\"iTotalDisplayRecords\":0");
            Json.Append(",");
            Json.Append("\"aaData\":");
            Json.Append("[");
            Json.Append("]");
            Json.Append("}");
            return Json.ToString();
        }
        if (dt.Rows.Count > 0)
        {
            Json.Append("{");
            Json.Append("\"sEcho\":" + echo);
            Json.Append(",");
            Json.Append("\"iTotalRecords\":" + dislpay);
            Json.Append(",");
            Json.Append("\"iTotalDisplayRecords\":" + dislpay);
            Json.Append(",");
            Json.Append("\"aaData\":");
            Json.Append("[");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result = new List<string>();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    result.Add("\"" + dt.Rows[i][k].ToString() + "\"");

                }
                if (i == dt.Rows.Count - 1)
                {
                    AddNewJson(Json, result, dt);
                }
                else
                {
                    AddNewJson(Json, result, dt);
                    Json.Append(",");
                }
            }
            Json.Append("]");
            Json.Append("}");
        }
        return Json.ToString();
    }


    private static void AddNewJson(StringBuilder Json, List<string> result, DataTable dt)
    {
        Json.Append("[");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            //Json.Append("\"");
            //Json.Append(dt.Columns[i].ColumnName);
            //Json.Append("\":");
            if (result[i].Contains(","))
            {
                Json.Append(result[i]);
            }
            else
            {
                Json.Append(result[i]);
                if (i != dt.Columns.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("]");
    }

    private static string StrReplace(string input)
    {
        string output = string.Empty;
        if (input != null)
        {
            output = input.Replace("&", "%26");
        }
        return output;
    }
}