using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ScreenSearchApi
{
    public class Scanner
    {
        private Bitmap SourcePic;

        private Bitmap SubPic;

        private int[,] SourceArr;

        private int[,] SubArr;

        private int transparentarr;

        public void SetTransColor(Color color)
        {
            transparentarr = BitConverter.ToInt32(new byte[4]
            {
                color.B,
                color.G,
                color.R,
                0
            }, 0);
        }

        public void SetSourcePic(byte[] rawbmp)
        {
            using (MemoryStream stream = new MemoryStream(rawbmp))
            {
                SourcePic = (Bitmap)Image.FromStream(stream);
                SourceArr = ConvertToArray(rawbmp);
            }
        }

        public void SetSourcePic(Image bmp)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                SourcePic = (Bitmap)bmp;
                SourcePic.Save(memoryStream, ImageFormat.Bmp);
                byte[] buffer = memoryStream.GetBuffer();
                SourceArr = ConvertToArray(buffer);
            }
        }

        public void SetSourcePic(string path)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                SourcePic = (Bitmap)Image.FromFile(path);
                SourcePic.Save(memoryStream, ImageFormat.Bmp);
                byte[] buffer = memoryStream.GetBuffer();
                SourceArr = ConvertToArray(buffer);
            }
        }

        public int TakeScreenShot()
        {
            using (Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        try
                        {
                            graphics.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                            bitmap.Save(memoryStream, ImageFormat.Bmp);
                            SourcePic = (Bitmap)Image.FromStream(memoryStream);
                            SourceArr = ConvertToArray(memoryStream.GetBuffer());
                            return 1;
                        }
                        catch (Exception)
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        public void SetSubPic(string path)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                SubPic = (Bitmap)Image.FromFile(path);
                SubPic.Save(memoryStream, ImageFormat.Bmp);
                byte[] buffer = memoryStream.GetBuffer();
                SubArr = ConvertToArray(buffer);
            }
        }

        public void SetSubPic(Image bmp)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                SubPic = (Bitmap)bmp;
                SubPic.Save(memoryStream, ImageFormat.Bmp);
                byte[] buffer = memoryStream.GetBuffer();
                SubArr = ConvertToArray(buffer);
            }
        }

        private int[,] ConvertToArray(byte[] rawdata)
        {
            int width = Math.Abs(BitConverter.ToInt32(rawdata, 18));
            int height = Math.Abs(BitConverter.ToInt32(rawdata, 22));
            int[,] array = new int[width, height];
            int pointer = BitConverter.ToInt32(rawdata, 10);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    array[j, i] = BitConverter.ToInt32(rawdata, pointer);
                    pointer += 4;
                }
            }
            return array;
        }


        public bool FindScreen(string subpicpath, out int TargetX, out int TargetY, out long Elapsed)
        {
            TakeScreenShot();
            SetSubPic(subpicpath);
            return Match(out TargetX, out TargetY, out Elapsed);
        }

        /// <summary>
        /// return coord at left down subpic
        /// </summary>
        /// <param name="TargetX">0 at left</param>
        /// <param name="TargetY">0 at top</param>
        /// <param name="Elapsed">time millisecond</param>
        /// <returns>false for fail true for success</returns>
        public bool Match(out int TargetX, out int TargetY, out long Elapsed)
        {
            TargetX = 0;
            TargetY = 0;
            Elapsed = 0L;
            if (TakeScreenShot() == 0)
            {
                return false;
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (TargetY = 0; TargetY < SourcePic.Height - SubPic.Height; TargetY++)
            {
                for (TargetX = 0; TargetX < SourcePic.Width - SubPic.Width; TargetX++)
                {
                    for (int j = 0; j < SubPic.Height; j++)
                    {
                        for (int i = 0; i < SubPic.Width; i++)
                        {
                            if (TargetX + SubPic.Width > SourcePic.Width) goto Out_Of_Width;

                            if (TargetY + SubPic.Height > SourcePic.Height) goto Out_Of_Height;

                            //if (SubArr[i, j] == transparentarr) continue;
                            if (SourceArr[TargetX + i, TargetY + j] != SubArr[i, j]) goto Next_Cycle;

                        }

                    }
                    stopwatch.Stop();
                    Elapsed = stopwatch.ElapsedMilliseconds;
                    TargetY = SourcePic.Height - TargetY;
                    return true;
                Next_Cycle:;
                }
            Out_Of_Width:;
            }
        Out_Of_Height:;
            TargetX = -1;
            TargetY = -1;
            stopwatch.Stop();
            Elapsed = stopwatch.ElapsedMilliseconds;
            return false;
        }
    }
}
