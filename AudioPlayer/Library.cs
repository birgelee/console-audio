using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;
using System.IO;

namespace AudioPlayer
{
    class Library
    {
        public static Library loadFromFile(string path)
        {
            if (!File.Exists(path))
                return new Library("Default");
            StreamReader sr = new StreamReader(path);

            string inputData = sr.ReadToEnd();
            if (inputData.Equals(""))
            {
                sr.Close();
                return new Library("Default");
            }

            string[] fileSections = inputData.Split('<');
            fileSections = Utils.Utils.stableizer(fileSections);
            
            Library lib = new Library(path.Split('.')[0]);

            string[] idAndValue = fileSections[0].Split('{');

            Track[] tracks = getTracksTromString(idAndValue[1]);

            lib.playLists = getPlayLists(fileSections, tracks);



            sr.Close();
            return lib;
        }


        private static PlayList[] getPlayLists(string[] dataStrings, Track[] tracks)
        {
            dataStrings = Utils.Utils.stableizer(dataStrings);
            PlayList[] playlists = new PlayList[dataStrings.Length - 1];
            PlayList master = new PlayList("master");
            string[] masterWeights = dataStrings[1].Split('{')[1].Split('\n');
            masterWeights = Utils.Utils.stableizer(masterWeights);
            for (int i = 0; i < tracks.Length; i++)
            {
                master.addTrack(tracks[i], Double.Parse(masterWeights[i]));
            }

            playlists[0] = master;
            for (int i = 2; i < dataStrings.Length; i++) //Loops through all playlists
            {
                string[] idAndValue = dataStrings[i].Split('{');
                PlayList playlist = new PlayList(idAndValue[0]);
                string[] trackStrings = idAndValue[1].Split('\n');
                trackStrings = Utils.Utils.stableizer(trackStrings);

                    foreach (string st in trackStrings) //Adds each track
                    {
                        if (st.Equals(""))
                            continue;
                        string[] trackIdAndWeight = st.Split('\t');
                        Track track = master.getTrack(trackIdAndWeight[0]);
                        playlist.addTrack(track, Double.Parse(trackIdAndWeight[1]));
                    }

                playlists[i - 1] = playlist;
            }


            return playlists;

        }
        private static Track[] getTracksTromString(string trackString)
        {

            string[] trackStrings = trackString.Split('\n');
            
            trackStrings = Utils.Utils.stableizer(trackStrings);
            int numberOfTracks = trackStrings.Length;


            Track[] tracks = new Track[numberOfTracks];
            for (int i = 0; i < trackStrings.Length; i++)
            {
                
                string[] parts = trackStrings[i].Split('\t');
                Track track = new Track(parts[0], parts[1], Double.Parse(parts[2]), Boolean.Parse(parts[3]));
                tracks[i] = track;
            }
            return tracks;
        }

        public void saveToFile()
        {
            StreamWriter sw = new StreamWriter(this.name + ".store");
            sw.Write("<track_ref{");//Stores all tracks and their file locations
            foreach (Track tr in this.playLists[0].trackCollenction)
            {
                sw.Write(tr.name + "\t" + tr.fileLocation + "\t" + tr.volume + "\t" + tr.isReferance + "\n");
            }

            sw.Write("<master{");
            foreach (Track tr in this.playLists[0].trackCollenction)
            {
                sw.Write(playLists[0].getWeight(tr) + "\n");
            }

            for (int i = 1; i < playLists.Length; i++)
            {
                sw.Write("<" + playLists[i].name + "{");
                foreach (Track tr in playLists[i].trackCollenction)
                {
                    sw.Write(tr.name + "\t" + playLists[i].getWeight(tr) + "\n");
                }
            }
            sw.Close();
        }

        public Library(string name)
        {
            this.name = name;
            playLists = new PlayList[1];
            playLists[0] = new PlayList("master");
        }

        public string name
        {
            get;
            set;
        }

        private PlayList[] backerPlayLists;

        public PlayList[] playLists
        {
            get
            {
                return backerPlayLists;
            }
            set
            {
                backerPlayLists = value;
            }
        }

        public PlayList this[string name]
        {
            

            get
            {
                if (name.Equals(Settings.CURRENT_TRACK_ABBR) || name.Equals(Settings.CURRENT_TRACK_ABBR2))
                {
                    return MainClass.currentPlayList;
                }
                foreach (PlayList pl in playLists)
                {

                    if (pl.name == name)
                    {
                        return pl;
                    }

                }
                return null;
            }

            set
            {
                if (name.Equals(Settings.CURRENT_TRACK_ABBR) || name.Equals(Settings.CURRENT_TRACK_ABBR2))
                {
                    for (int i = 0; i < playLists.Length; i++)
                    {

                        if (playLists[i] == MainClass.currentPlayList)
                        {
                            playLists[i] = value;
                        }

                    }
                }

                for (int i = 0; i < playLists.Length; i++)
                {

                    if (playLists[i].name.Equals(name))
                    {
                        playLists[i] = value;
                    }

                }
            }
        }

        public void addPlayList(PlayList playList)
        {
            Array.Resize<PlayList>(ref backerPlayLists, playLists.Length + 1);
            playLists[playLists.Length - 1] = playList;
            PlayList masterPlayList = playLists[0];
            Array.Sort(playLists);
            int mastersIndex = Array.IndexOf<PlayList>(playLists, masterPlayList);
            PlayList previousEllement = playLists[0];
            for (int i = 1; i <= mastersIndex; i++)
            {
                PlayList currentEllement = playLists[i];
                playLists[i] = previousEllement;
                previousEllement = currentEllement;

            }
            playLists[0] = masterPlayList;
        }

        public void removePlayList(PlayList playList)
        {
            int removedIndex = Array.IndexOf(playLists, playList);
            for (int i = removedIndex; i < playLists.Length - 1; i++)
            {
                playLists[i] = playLists[i + 1];
            }

            Array.Resize<PlayList>(ref backerPlayLists, playLists.Length - 1);

        }

    }
}
