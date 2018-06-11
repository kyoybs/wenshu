using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinformSpider
{
    public static class Extents
    {
        public static string Post(this MyWebClient web, string url, string queryString, bool clearHeads=false)
        {
            string postString = queryString;// WebUtility.UrlEncode( queryString);//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式 
            web.RequestConentLength = postData.Length;
            if (clearHeads)
            {
                web.Headers.Clear();
                web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            }
            
            byte[] responseData = web.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            return srcString;
        }
    }
}
