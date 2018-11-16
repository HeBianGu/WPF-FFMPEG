using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeBianGu.FFPEG.Base.Driver;

namespace HeBianGu.FFMPEG.UnitTest.DriverBase
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MediaDriver driver = new MediaDriver(@"F:\GitHub\WPF-FFMPEG\Document\ffmpeg\ffmpeg.exe");

            string input = @"F:\GitHub\WPF-FFMPEG\Document\ffmpeg\wjz.mp4";

            string outPut = @"F:\GitHub\WPF-FFMPEG\Document\ffmpeg\wjz.mp3";

            driver.MediaToSound(input, outPut);
        }
    }
}
