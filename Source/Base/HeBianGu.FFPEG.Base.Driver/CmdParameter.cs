﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
  m4a转wav
$ ffmpeg.exe -i input.m4a -ac 2 -ar 44100 -acodec pcm_s16le -f wav output.wav
从视频中提取声音
$ ffmpeg -i [input].mp4 -vn -ab 128k [output].mp3
分离视频音频流
//分离视频流
$ ffmpeg -i input_file -vcodec copy -an output_file_video
//分离音频流
$ ffmpeg -i input_file -acodec copy -vn output_file_audio
去掉视频里的声音（静音）
$ ffmpeg -i [input].mp4 -an [output].mp4
改变视频文件大小（分辨率）
$ ffmpeg -i [input].mp4 -s 640x480 -c:a copy [output].mp4
截取一段音频
//-ss:截取开始时间点， -t：要截取的视频长度（15秒）
$ ffmpeg -ss 00:00:15 -t 45 -i sampleaudio.mp3 croppedaudio.mp3
$ ffmpeg -i [input].mp4 -ss 00:00:00 -codec copy -t 15 [output].mp4
视频剪切
//-r 提取图像的频率，-ss 开始时间，-t 持续时间
$ ffmpeg –i test.avi –r 1 –f image2 image-%3d.jpeg        //提取图片
$ ffmpeg -ss 0:1:30 -t 0:0:20 -i input.avi -vcodec copy -acodec copy output.avi    //剪切视频
把一个视频分成多个部分
//0-59秒一部分，59秒以后一部分
$ ffmpeg -i input.mp4 -t 00:00:59 -c copy part1.mp4 -ss 00:00:59 -codec copy part2.mp4
查看ffmpeg支持的视频格式
$ ffmpeg -formats
mp4到wmv格式转换
$ ffmpeg -i input.mp4 -c:v libx264 output.wmv
webm转为mp4
$ ffmpeg -i input.webm -qscale 0 output.mp4
视频文件名写入txt
$ ffmpeg -i input.webm -qscale 0 output.mp4
对音频加减速
$ ffmpeg -i input.mkv -filter:a "atempo=2.0" -vn output.mkv
对视频加减速
$ ffmpeg -i input.mp4 -filter:v "setpts=0.125*PTS" output.mp4
 旋转视频
$ ffmpeg -i input.mp4 -filter:v 'transpose=1' rotated-video.mp4
$ ffmpeg -i input.mp4 -filter:v 'transpose=2,transpose=2' rotated-video.mp4
改变声音大小
$ ffmpeg -i input.wav -af 'volume=0.5' output.wav
加入字幕
$ ffmpeg -i movie.mp4 -i subtitles.srt -map 0 -map 1 -c copy -c:v libx264 -crf 23 -preset veryfast output.mkv
把单独的一个图片转为视频
$ ffmpeg -loop 1 -i image.png -c:v libx264 -t 30 -pix_fmt yuv420p video.mp4
把视频文件转为图片
$ ffmpeg -i movie.mp4 -r 0.25 frames_%04d.png
视频中提取帧
$ ffmpeg -ss 00:00:15 -i video.mp4 -vf scale=800:-1 -vframes 1 image.jpg
把视频转为GIF动态图
$ ffmpeg -i video.mp4 -vf scale=500:-1 -t 10 -r 10 image.gif
左右声道的录音合成为立体声
$ ffmpeg -i 1.wav -i 2.wav -filter_complex "amovie=1.wav [l]; amovie=2.wav [r]; [l] [r] amerge" 1_2.mp3
从视频里截图
$ ffmpeg -i test.avi -y -f image2 -ss 8 -t 0.001 -s 350x240 test.jpg
音视频文件的切割
$ ffmpeg -ss 00:00:10 -t 00:01:22 -i input.mp3 output.mp3
视频解复用
$ ffmpeg –i test.mp4 –vcodec copy –an –f m4v test.264
$ ffmpeg –i test.avi –vcodec copy –an –f m4v test.264
视频转码

//-bf B帧数目控制，-g 关键帧间隔控制，-s 分辨率控制

//转码为码流原始文件
$ ffmpeg –i test.mp4 –vcodec h264 –s 352*278 –an –f m4v test.264              
//转码为码流原始文件
$ ffmpeg –i test.mp4 –vcodec h264 –bf 0 –g 25 –s 352*278 –an –f m4v test.264
//转码为封装文件
$ ffmpeg –i test.avi -vcodec mpeg4 –vtag xvid –qsame test_xvid.avi

视频封装
$ ffmpeg –i video_file –i audio_file –vcodec copy –acodec copy output_file   
     
*/
namespace HeBianGu.FFPEG.Base.Driver
{
    class CmdParameter
    {
        /// <summary> m4a转wav </summary>
        public const string m4aTowav = "-i input.m4a -ac 2 -ar 44100 -acodec pcm_s16le -f wav output.wav";

        /// <summary> 从视频中提取声音 p1=input p2= output </summary>
        public const string mToSound = "-i {0} -vn -ab 128k {1}";

        /// <summary> 分离视频流 </summary>
        public const string mediaToMediaStream = "-i input_file -vcodec copy -an output_file_video";

        /// <summary> 分离音频流 </summary>
        public const string mediaToSoundStream = "-i input_file -acodec copy -vn output_file_audio";

        /// <summary> 去掉视频里的声音（静音） </summary>
        public const string deleteSound = "-i[input].mp4 -an[output].mp4";

        /// <summary> 改变视频文件大小（分辨率） </summary>
        public const string changeSize = "-i[input].mp4 -s 640x480 -c:a copy[output].mp4";

        /// <summary> 截取一段音频 -ss:截取开始时间点， -t：要截取的视频长度（15秒） </summary>
        public const string cutSoundFromTo = "-ss 00:00:15 -t 45 -i sampleaudio.mp3 croppedaudio.mp3";

        public const string cutSoundFromTo1 = "-i[input].mp4 -ss 00:00:00 -codec copy -t 15 [output].mp4";

        /// <summary> 视频剪切 -r 提取图像的频率，-ss 开始时间，-t 持续时间 </summary>
        public const string cutMediaFromTo = "–i test.avi –r 1 –f image2 image-%3d.jpeg";

        /// <summary> 提取图片 </summary>
        public const string getImage = "-ss 0:1:30 -t 0:0:20 -i input.avi -vcodec copy -acodec copy output.avi";

        /// <summary> 剪切视频把一个视频分成多个部分0-59秒一部分，59秒以后一部分 </summary>
        public const string cutMediaSplit = "-i input.mp4 -t 00:00:59 -c copy part1.mp4 -ss 00:00:59 -codec copy part2.mp4";

        /// <summary> 查看ffmpeg支持的视频格式</summary>
        public const string getFormats = "-formats";

        /// <summary> mp4到wmv格式转换</summary>
        public const string mp4_to_wmv = "-i input.mp4 -c:v libx264 output.wmv";

        /// <summary> webm转为mp4 </summary>
        public const string webm_to_mp4 = "-i input.webm -qscale 0 output.mp4";

        /// <summary> 视频文件名写入txt </summary>
        public const string write_to_txt = "-i input.webm -qscale 0 output.mp4";

        /// <summary> 对音频加减速 </summary>
        public string speed_sound_change = "-i input.mkv -filter:a \"atempo=2.0\" -vn output.mkv";

        /// <summary> 对视频加减速 </summary>
        public const string speed_media_change = "-i input.mp4 -filter:v \"setpts=0.125*PTS\" output.mp4";

        /// <summary> 旋转视频 </summary>
        public const string rotate = "-i input.mp4 -filter:v 'transpose=1' rotated-video.mp4";

        public const string rotate1 = "-i input.mp4 -filter:v 'transpose=2,transpose=2' rotated-video.mp4";

        /// <summary> 改变声音大小 </summary>
        public const string sound_size = "-i input.wav -af 'volume=0.5' output.wav";

        /// <summary> 加入字幕 </summary>
        public const string add_srt = "-i movie.mp4 -i subtitles.srt -map 0 -map 1 -c copy -c:v libx264 -crf 23 -preset veryfast output.mkv";

        /// <summary> 把单独的一个图片转为视频 </summary>
        public const string image_to_media = "-loop 1 -i image.png -c:v libx264 -t 30 -pix_fmt yuv420p video.mp4";

        /// <summary> 把视频文件转为图片 </summary>
        public const string media_to_image = "-i movie.mp4 -r 0.25 frames_%04d.png";

        /// <summary> 视频中提取帧 </summary>
        public const string get_frames = "-ss 00:00:15 -i video.mp4 -vf scale = 800:-1 -vframes 1 image.jpg";
        /// <summary> 音视频文件的切割 </summary>
        public const string cutmedia = "-i video.mp4 -vf scale = 500:-1 -t 10 -r 10 image.gif";

        /// <summary> 左右声道的录音合成为立体声 </summary>
        public const string merge_sound = "-i 1.wav -i 2.wav -filter_complex \"amovie=1.wav[l]; amovie=2.wav[r]; [l] [r] amerge\" 1_2.mp3";

        /// <summary> 从视频里截图 </summary>
        public const string screen_shot = "-i test.avi -y -f image2 -ss 8 -t 0.001 -s 350x240 test.jpg";

        /// <summary> 音视频文件的切割 </summary>
        public const string cut_sound = "-ss 00:00:10 -t 00:01:22 -i input.mp3 output.mp3";

        /// <summary> 视频解复用 </summary>
        public const string copy_media = "–i test.mp4 –vcodec copy –an –f m4v test.264";
        public const string copy_media1 = "–i test.avi –vcodec copy –an –f m4v test.264";

        /// <summary> 视频转码 转码为码流原始文件  -bf B帧数目控制，-g 关键帧间隔控制，-s 分辨率控</summary>
        public const string transcode_to_prmitivefile = "–i test.mp4 –vcodec h264 –s 352*278 –an –f m4v test.264";

        /// <summary> 视频转码 转码为码流原始文件  -bf B帧数目控制，-g 关键帧间隔控制，-s 分辨率控</summary>
        public const string transcode_to_prmitivefile1 = "–i test.mp4 –vcodec h264 –bf 0 –g 25 –s 352*278 –an –f m4v test.264";

        /// <summary> 视频转码 转码为封装文件  -bf B帧数目控制，-g 关键帧间隔控制，-s 分辨率控</summary>
        public const string transcode_to_package = "–i test.avi -vcodec mpeg4 –vtag xvid –qsame test_xvid.avi";

        /// <summary> 视频封装 </summary>
        public const string mediaToSountStream = "–i video_file –i audio_file –vcodec copy –acodec copy output_file";

    }
}
