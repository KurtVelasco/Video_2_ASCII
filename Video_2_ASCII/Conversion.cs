using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_2_ASCII
{
    internal class Conversion
    {
        private string VIDEO_PATH = string.Empty;
        ////
        //The Ascii Characters are ordered from Light to Dark
        //You can assign custom char for differing degrees of shade
        //// 
        private static string DEFAULT_ASCII_SHADINGS = "@#S%?*+;:,. ";
        private static int[] ASCII_RES = { 200, 70 };

        public Conversion(string videoPath,string ascii, int[] resolution)
        {
            DEFAULT_ASCII_SHADINGS =ascii;
            ASCII_RES = resolution; 
            VIDEO_PATH = videoPath;
            VideoFileReader videoReader = new VideoFileReader();
            try
            {
                videoReader.Open(VIDEO_PATH);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.ReadKey();
                Program.Menu();

            }
        }
        public List<string> ConvertVidtoAscii()
        {
            VideoFileReader videoReader = new VideoFileReader();
            try
            {
                videoReader.Open(VIDEO_PATH);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.ReadKey();

            }

            List<string> asciiFrames = new List<string>();

            int frameCount = (int)videoReader.FrameCount;
            int width = ASCII_RES[0];
            int height = ASCII_RES[1];
            int count = 0;


            Bitmap resizedFrame = new Bitmap(width, height);

            for (int i = 0; i < frameCount; i++)
            {
                Bitmap frame = videoReader.ReadVideoFrame();
                if (frame == null) break;
                using (Graphics graphics = Graphics.FromImage(resizedFrame))
                {
                    graphics.DrawImage(frame, 0, 0, width, height);
                }

                StringBuilder asciiFrame = new StringBuilder();

                for (int y = 0; y < resizedFrame.Height; y++)
                {
                    for (int x = 0; x < resizedFrame.Width; x++)
                    {
                        Color pixelColor = resizedFrame.GetPixel(x, y);
                        int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        int index = gray * (DEFAULT_ASCII_SHADINGS.Length - 1) / 255;
                        asciiFrame.Append(DEFAULT_ASCII_SHADINGS[index]);
                    }
                    asciiFrame.AppendLine();
                }

                count++;
                Console.Write("\rFrame Converted: " + count);
                asciiFrames.Add(asciiFrame.ToString());

                frame.Dispose();
            }
            videoReader.Close();
            resizedFrame.Dispose();
            return asciiFrames;
        }
    }
}
