using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using TestLog4Net;

public partial class logtest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //第一种记录用法
        //（1）FormMain是类名称
        //（2）第二个参数是字符串信息
        LogHelper.WriteLog(typeof(String), "测试Log4Net日志是否写入");


        //第二种记录用法
        //（1）FormMain是类名称
        //（2）第二个参数是需要捕捉的异常块
        //try { 

        //}catch(Exception ex){

        //    LogHelper.WriteLog(typeof(FormMain), ex);

        //}

    }
}