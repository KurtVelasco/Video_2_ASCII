using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Video_2_ASCII
{
    internal class Program
    {
        public static List<string> ASCII_FRAME;
        public static bool ALLOW_EXPORT = false;
        public static bool GO_PLAY_IMMED = false;
        public static int CURRENT_FRAMERATE = 30;
        public static string CURRENT_EXTENSION = ".ask";
        public static string CURRENT_MUSIC = "bad.webm";
        public static string DEFAULT_ASCII_SHADINGS = " #S%?*+;:,. ";
        public static bool AUDIO_ONLY = true;

        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("******************************************\n\n" +
                                  "ASCII Simulator\n\n" +
                                  $"Framerate: {CURRENT_FRAMERATE}\n" +
                                  $"Extension: {CURRENT_EXTENSION}\n" +
                                  $"Music Path: {CURRENT_MUSIC}\n" +
                                  $"ASCII Shadings: {DEFAULT_ASCII_SHADINGS}\n" +
                                  $"Has Audio: {AUDIO_ONLY}\n" +
                                  "\n******************************************");
                Console.WriteLine("1. Convert Video to ASCII\n" +
                                  "2. Play .ask File\n" +
                                  "3. Change Configuration");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        VideoConversionMenu();
                        break;
                    case "2":
                        Console.Write("Input .ask file (Filepath): ");
                        PlayASCII.LoadASCII(Console.ReadLine(), CURRENT_MUSIC, CURRENT_FRAMERATE, AUDIO_ONLY);
                        break;
                    case "3":
                        ConfigurationMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void VideoConversionMenu()
        {
            Console.Write("Input videoPath: ");
            string videoPath = Console.ReadLine();

            Console.Write("Do you want to Export ASCII animation (default to 'N') Y/N: ");
            ALLOW_EXPORT = Console.ReadLine().ToLower() == "y";

            Console.Write("Do you want to play ASCII after conversion? (default to 'N') Y/N: ");
            GO_PLAY_IMMED = Console.ReadLine().ToLower() == "y";

            Conversion asciiConversion = new Conversion(videoPath);
            ASCII_FRAME = asciiConversion.ConvertVidtoAscii();

            if (ALLOW_EXPORT)
            {
                WriteASCII();
            }

            if (GO_PLAY_IMMED)
            {
                PlayASCII.RunASCII(ASCII_FRAME, CURRENT_MUSIC, CURRENT_FRAMERATE, AUDIO_ONLY);
            }
        }

        public static void ConfigurationMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                                 "ASCII Simulator\n\n" +
                                 $"Framerate: {CURRENT_FRAMERATE}\n" +
                                 $"Extension: {CURRENT_EXTENSION}\n" +
                                 $"Music Path: {CURRENT_MUSIC}\n" +
                                 $"ASCII Shadings: {DEFAULT_ASCII_SHADINGS}\n" +
                                 $"Has Audio: {AUDIO_ONLY}\n"
                                 );

                Console.WriteLine("******************************************\n\n" +
                                    "Config\n \n" + "" +
                                  "******************************************");
                Console.WriteLine("1. Change ASCII Playback Framerate\n"
                + "2. Change Audio file (MP3)\n"
                + "3. Toggle Audio in player\n"
                + "4. Return");
                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        Console.Write("New framerate: ");
                        if (int.TryParse(Console.ReadLine(), out int newFramerate))
                        {
                            CURRENT_FRAMERATE = newFramerate;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid number.");
                        }
                        break;
                    case "2":
                        Console.Write("New MP3 filepath: ");
                        CURRENT_MUSIC = Console.ReadLine();
                        break;
                    case "3":
                        AUDIO_ONLY = !AUDIO_ONLY;
                        break;
                    case "4":
                        Console.Write("New ASCI Shading <Light --- Dark>: ");
                        DEFAULT_ASCII_SHADINGS = Console.ReadLine();
                        return;
                    case "5":
                        return; 
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void WriteASCII()
        {
            try
            {
                using (FileStream stream = new FileStream("bad.ask", FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, ASCII_FRAME);
                }
                Console.WriteLine("File Written!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
    

