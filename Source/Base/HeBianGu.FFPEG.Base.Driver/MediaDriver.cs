using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.FFPEG.Base.Driver
{
    public class MediaDriver : CmdDriver
    {

        public MediaDriver(string exePath)
        {
            _exePath = exePath;
        }
        public string _exePath { get; set; }

        public void MediaToSound(string inputPath, string outPath)
        {
            string param = string.Format(CmdParameter.mToSound, inputPath, outPath);

            string result = this.Execute(_exePath, param);

            Debug.WriteLine(result);

        }
    }
}
