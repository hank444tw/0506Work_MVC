# -*- coding: utf-8 -*-
"""
Created on Sun May  5 11:00:18 2019

@author: hank
"""

#---------------- 從cmd執行該程式，並帶入參數---------------------
import argparse 
p=argparse.ArgumentParser()
p.add_argument('--sub',required=False,default='')
p.add_argument('--url',required=False,default='')

args=p.parse_args()

sub = args.sub
url = args.url

#url = 'https://www.youtube.com/watch?v=2-9v34Bhlww&list=PLZHTcUWHH9ZOcnz2Kwjb-c2kJUry_HUxr'
#sub = 'mp4'
#------------------------------------------------------------

url = url.replace('666=','&list=') #將'666='換成'list='，因為'&'在cmd裏頭會出事

import requests
import re
import os
from pytube import YouTube
import moviepy.editor as mp #用來將MP4轉MP3

path = r'C:\Users\hankh\Desktop\ASP_NET_MVC\0506Work_MVC\0506Work_MVC\Download'

html = requests.get(url)
resl = re.findall(r'/watch[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%?=~_|]',html.text)
             
videourlList=[]
for temurl in resl:
    if 'list=' and 'index=' in temurl: 
        if temurl not in videourlList: #剔除重複的
            print(temurl)
            videourlList.append(temurl)

n = 1
for video in videourlList:
    yt=YouTube('https://www.youtube.com'+video)
    print('--分割--' + yt.title + '--分割--') #輸出影片標題
    yt.streams.filter(progressive=True).first().download(path,filename=(str(n)))
    n=n+1

if sub == 'mp3':
    for item in range(n):
        if item == 0:
            continue
        clip = mp.VideoFileClip(os.path.join(path, str(item)+'.mp4'))
        clip.audio.write_audiofile(os.path.join(path, str(item)+'.mp3'),verbose=False,logger=None) #logger為進度條屬性

    os.system("taskkill /F /IM ffmpeg-win64-v4.1.exe") #ffmpeg-win64-v4.1.exe會一直開啟著該MP4檔 占用記憶體 故強制關閉