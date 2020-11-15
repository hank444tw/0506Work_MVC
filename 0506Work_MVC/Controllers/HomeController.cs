using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using _0506Work_MVC.Models; //使用Models

namespace _0506Work_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NFU()
        {
            ViewData["change"] = false;
            ViewData["noUrl"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult NFU(fileData postData)
        {
            string url = postData.YT_url;
            string type = postData.type;

            if(url.IndexOf(@"https://www.youtube.com/") == -1) //檢查Url是否有包含https://www.youtube.com/
            {
                ViewData["noUrl"] = "請輸入正確的網址";
                ViewData["change"] = false;
                return View();
            }

            //進入python檔所在目錄
            string cmd = @"cd C:\Users\hankh\Desktop\ASP_NET_MVC\0506Work_MVC\0506Work_MVC\Python";

            //-------------------------------判斷是單支影片還是影片清單---------------------------------------
            string cmd_py = "";
            if (url.IndexOf("&list=") == -1) //-1代表沒有包含
                cmd_py = $@"python 0506Work.py --sub {type} --url {url} --name 1 & exit";
            else
            {
                string todo = url.Replace("&list=", "666=");
                cmd_py = $@"python 0506Work_list.py --sub {type} --url {todo} & exit";
            }
            //-------------------------------------------------------------------------------------------------
            //python 0506Work.py --sub mp4 --url https://www.youtube.com/watch?v=t7iExt2OXfc --name 1

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.RedirectStandardInput = true;
            p.Start();

            StreamWriter myStreamWriter = p.StandardInput;
            myStreamWriter.WriteLine(cmd);
            myStreamWriter.WriteLine(cmd_py);
            myStreamWriter.Close();


            //-----------------------------------取得影片名稱，並存入串列----------------------------------------
            string bigstr = p.StandardOutput.ReadToEnd(); //取得cmd文字
            string[] NSelect = Regex.Split(bigstr, "--分割--", RegexOptions.IgnoreCase); //正規表示式做分割

            int item = 1;
            List<fileData> fileData = new List<fileData>();
            for (int x = 0; x < NSelect.Count(); x++)
            {
                if (NSelect[x].IndexOf("\r\n") != -1)
                    continue;

                fileData.Add(new fileData
                {
                    id = item, //id由1開始，因為儲存在Server端的影片是1,2,3,4...命名，以利前端讀取
                    YT_url = url,
                    type = type,
                    YT_name = NSelect[x],
                });
                item++;
            }
            //----------------------------------------------------------------------------------------------------
            p.WaitForExit(); //等待程式執行完畢，並Exit
            p.Close(); //釋放Process記憶體

            ViewData["change"] = true;
            ViewData["type"] = type;
            return View(fileData);
        }

        [HttpPost]
        public ActionResult Download(fileData fileData) //下載
        {
            string fileName = fileData.YT_name;
            string id = fileData.id.ToString();
            string type = fileData.type;

            //Server端路徑
            string filePath = Server.MapPath($@"..\Download\{id}.{type}"); //相對路徑

            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition","attachment;filename =" + $"{fileName}.{type}"); //下載檔案名稱
            Response.AddHeader("Content-Length",fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding","binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
            return View();
        }
    }
}