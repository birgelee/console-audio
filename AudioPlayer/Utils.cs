using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using YoutubeExtractor;

namespace AudioPlayer.Utils
{
    class Utils
    {
        private static Random random = new Random();
        public static Track randomTrack(PlayList pl)
        {
            if (pl.trackCollenction.Count == 0)
                return null;
            double maxWeight = pl.maxWeight;

            while (true)
            {
                Track tr = pl.tracks[random.Next(pl.trackCollenction.Count - 1)];

                if (random.NextDouble() * maxWeight <= pl.getWeight(tr))
                {
                    return tr;
                }
            }
        }

        public static void playTrack(Track tr)
        {


            Song song;
            var ctor = typeof(Song).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance, null,
            new[] { typeof(string), typeof(string), typeof(int) }, null);

            
            song = (Song)ctor.Invoke(new object[] { tr.name, tr.fileLocation, getLength(tr.fileLocation)});
            MediaPlayer.Play(song);
            MediaPlayer.Volume = (float)(tr.volume) / 100F;
            MainClass.currentTrack = tr;
            MainClass.songTime = song.Duration;
            MainClass.currentTime = new TimeSpan(0);
            append(tr, MainClass.history, out MainClass.history);
            MainClass.queingTrack = false;
            /*SoundEffect song = SoundEffect.FromStream(new FileStream(tr.fileLocation, FileMode.Open));
            if (MainClass.soundEffectInstance != null)
            MainClass.soundEffectInstance.Stop();
            MainClass.soundEffectInstance = song.CreateInstance();
            MainClass.soundEffectInstance.Volume = (float) (tr.volume)/100F;
            MainClass.soundEffectInstance.Play();
            MainClass.currentTrack = tr;
            append(tr, MainClass.history, out MainClass.history);
            MainClass.songTime = song.Duration;
            MainClass.currentTime = new TimeSpan();*/

        }

        public static void playNextTrack()
        {


            if (MainClass.nextTrack != null)
            {
                playTrack(MainClass.nextTrack);
                MainClass.nextTrack = MainClass.trackAfterNext;
                MainClass.trackAfterNext = getNextTrack(MainClass.nextTrack);
            }
            else
            {
                MediaPlayer.Stop();
                MainClass.currentSong = null;
                MainClass.currentTrack = null;
            }



        }

        public static Track getNextTrack(Track currentTrack)
        {
            switch (MainClass.playMode)
            {
                case SongState.Loop: return currentTrack;
                case SongState.Shuffle:
                    return randomTrack(MainClass.currentPlayList);
                case SongState.Stop:
                    return null;
                case SongState.PlayNext:
                    return MainClass.currentPlayList.getNextTrack(currentTrack);
                default:
                    return null;//This should be unreackable

            }
        }

        public static void regenerateNextTracks(Track currentTrack)
        {
            MainClass.nextTrack = getNextTrack(currentTrack);
            MainClass.trackAfterNext = getNextTrack(MainClass.nextTrack);
        }

        public static bool contains(Object[] array, Object instance)
        {
            foreach (Object obj in array)
            {
                if (obj.Equals(instance))
                {
                    return true;
                }

            }
            return false;
        }

        public static string[] stableizer(string[] input)
        {
            if (input.Length > 0)
            {
                if (input[0].Equals(""))
                {
                    if (input.Length == 1)
                    {
                        input = new string[0];
                        return input;
                    }
                    string[] store = new string[input.Length - 1];
                    for (int i = 1; i < input.Length; i++)
                    {
                        store[i - 1] = input[i];
                    }
                    input = store;
                }
                if (input[input.Length - 1].Equals(""))
                {
                    if (input.Length == 1)
                    {
                        input = new string[0];
                        return input;
                    }
                    string[] store = new string[input.Length - 1];
                    for (int i = 0; i < store.Length; i++)
                    {
                        store[i] = input[i];
                    }
                    input = store;
                }
            }
            return input;
        }

        public static string clean(string st)
        {

            string[] parts = st.Split(':');
            foreach (string part in parts[1].Split('\\'))
            {
                if (!part.Equals(""))
                    st = st.Replace(part, Uri.EscapeUriString(part));
            }
            return st;
        }

        public static string[] commandParser(string command)
        {
            if (command.Contains("\""))
            {
                int numberOfQuotationMarks = command.Split('\"').Length - 1;
                if (numberOfQuotationMarks % 2 == 1)
                {
                    command = command.Insert(command.Length, "\"");
                    numberOfQuotationMarks++;
                }
                int numberOfQuotes = numberOfQuotationMarks / 2;
                string[] quoteContents = new string[numberOfQuotes];
                for (int i = 0; i < numberOfQuotes; i++)
                {
                    int indexOfOpenQuote = command.IndexOf('\"');
                    int indexOfCloseQuote = command.IndexOf('\"', indexOfOpenQuote + 1);
                    string quoteContent = command.Substring(indexOfOpenQuote + 1, indexOfCloseQuote - indexOfOpenQuote - 1);
                    quoteContents[i] = quoteContent;
                    command = command.Replace("\"" + quoteContent + "\"", "");
                }
                string[] args = command.Split(' ');
                int quoteNumber = 0;
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Equals(""))
                    {
                        args[i] = quoteContents[quoteNumber];
                        quoteNumber++;
                    }
                }
                return args;

            }
            else
            {
                return command.Split(' ');
            }
        }

        public static T[] append<T>(T ellement, T[] array, out T[] newArray)
        {
            newArray = new T[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            newArray[array.Length] = ellement;
            return newArray;
        }

        public static string removeSupportedFileExtension(string s)
        {
            foreach (string extension in Settings.SUPPORTED_FILE_TYPES)
            {
                s.Replace(extension, "");
            }
            return s;
        }

        public static int getLength(string path)
        {
            TagLib.File file = TagLib.File.Create(path);
            return (int) (file.Properties.Duration.TotalMilliseconds);
        }

        public static string pullFromYoutube(string url)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);
            var video = videoInfos.First();
            var downloadPath = Path.Combine(Directory.GetCurrentDirectory() + "\\" + MainClass.library.name + "-songs\\", video.Title + video.AudioExtension);
            var audioDownloader = new AudioDownloader(video, downloadPath);
            audioDownloader.Execute();
            return downloadPath;
        }
    }
}
