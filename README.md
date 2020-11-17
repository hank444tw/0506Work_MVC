# 線上YT下載網站 
_大四下_友鋒課作業_       

* 功能說明
  1. 透過Youtube影片連結，下載`mp4`或`mp3`檔案
  2. 可下載`Youtube播放清單`    
 
*  使用技術
  1. ASP.NET MVC
  2. C#
  3. javascript
  4. python  
  
* 程式架構
  |        | 說明 | 檔案 |
  |------- |:-----:|------:|
  | 前端   |  只有一個頁面，使用Razor語法判斷後端是否有傳送YT影片資訊。JS顯示Loading遮罩效果  |  [程式碼](https://github.com/hank444tw/0506Work_MVC/blob/master/0506Work_MVC/Views/Home/NFU.cshtml) |
  | 後端   |  執行cmd呼叫對應的python檔案，並接收回傳值，丟回前端  |  [程式碼](https://github.com/hank444tw/0506Work_MVC/blob/master/0506Work_MVC/Controllers/HomeController.cs)] |
  | python |  中3  |   右3 |
 

![image](https://github.com/hank444tw/0517Work_MVC/blob/master/Demo1.JPG "首頁")
![image](https://github.com/hank444tw/0517Work_MVC/blob/master/Demo2.jpg "mp4下載")
![image](https://github.com/hank444tw/0517Work_MVC/blob/master/Demo3.jpg "mp3下載")
