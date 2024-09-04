using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


namespace Video_2_ASCII
{
    internal class PlayASCII
    {
        public static void RunASCII(List<string> asciiTXT, string mp3File, int frameRate, bool audio)
        {
            if (audio)
            {
                PlayVideoAudio(asciiTXT, mp3File, frameRate);
            }
            else
            {
                PlayVideoOnly(asciiTXT, frameRate);
            }
        }
        public static void LoadASCII(string asciiTXT, string mp3File, int frameRate, bool audio)
        {
            List<string> Asciilist = new List<string>();
            try
            {
                using (FileStream stream = new FileStream(asciiTXT, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Asciilist = (List<string>)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine($".ask File: ERROR: {ex.Message}");
                    Console.ReadKey();
                    return;
                }
            }
            if (audio)
            {
                PlayVideoAudio(Asciilist, mp3File, frameRate);
            }
            else
            {
                PlayVideoOnly(Asciilist, frameRate);
            }
        }
        public static void PlayVideoOnly(List<string> asciiFrames, int FRate)
        {
            for (int i = 0; i < asciiFrames.Count; i++)
            {
                Console.Clear();
                Console.Write(asciiFrames[i]);
                Thread.Sleep(FRate);
            }
            Program.Menu();

        }
        public static void PlayVideoAudio(List<string> asciiFrames, string MP3Path, int FRate)
        {
            try
            {
                var audioRender = new AudioFileReader(MP3Path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"music file: ERROR: {ex.Message}");
                Console.ReadKey();
                return;
            }

            using (var audioRender = new AudioFileReader(MP3Path))
            using (var outputDevice = new WaveOutEvent())
            {

                Stopwatch stopwatch = Stopwatch.StartNew();
                double frameRate = FRate;
                double totalDuration = asciiFrames.Count / frameRate;
                double audioDuration = audioRender.TotalTime.TotalSeconds;
                outputDevice.Init(audioRender);
                outputDevice.Play();
                //For Loop
                //The actuall writing of each individual frames
                for (int i = 0; i < asciiFrames.Count; i++)
                {
                    double expectedTime = i / frameRate;
                    while (stopwatch.Elapsed.TotalSeconds < expectedTime)
                    {
                        Thread.Sleep(1);
                    }
                    Console.Clear();
                    Console.Write(asciiFrames[i]);
                }

                stopwatch.Stop();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1);
                }
            }
            Program.Menu();
        }
    }
}
