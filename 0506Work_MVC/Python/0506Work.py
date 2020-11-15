# -*- coding: utf-8 -*-
"""
Created on Mon Apr 29 20:32:03 2019

@author: hank
"""

#-------------- 從cmd執行該程式，並帶入參數---------------------
#import argparse 
#p=argparse.ArgumentParser()
#p.add_argument('--sub',required=False,default='')
#p.add_argument('--url',required=False,default='')
#p.add_argument('--name',required=False,default='')
#
#args=p.parse_args()
#
#sub = args.sub
#url = args.url
#name = args.name

sub = "mp4"
url = "https://www.youtube.com/watch?v=J4k-AN74mz4"
name = "132"
#--------------------------------------------------------------

from pytube import YouTube
import os
import moviepy.editor as mp #用來將MP4轉MP3

yt = YouTube(url)

filePath = r'C:\Users\hankh\Desktop\ASP_NET_MVC\0506Work_MVC\0506Work_MVC\Download'
yt.streams.filter(progressive=True).first().download(filePath,filename=name)

if sub == 'mp3':
    
    clip = mp.VideoFileClip(os.path.join(filePath, name+'.mp4'))
    clip.audio.write_audiofile(os.path.join(filePath, name+'.mp3'),verbose=False,logger=None) #logger為進度條屬性
    os.system("taskkill /F /IM ffmpeg-win64-v4.1.exe") #ffmpeg-win64-v4.1.exe會一直開啟著該MP4檔 占用記憶體 故強制關閉
    
print('--分割--'+yt.title+'--分割--')
