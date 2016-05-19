
using System.Collections;
using System;
using System.Collections.Generic;
namespace AudioPlayer.Storage
{
    public class Track : IComparable<Track>
    {

        public int CompareTo(Track other)
        {
            
            return this.name.CompareTo(other.name);
        }

        public Track(string name, string fileLocation, double volume, bool isReferance)
        {
            this.volume = volume;
            this.fileLocation = fileLocation;
            this.name = name;
            this.isReferance = isReferance;
        }

        public Track(string name, string fileLocation) : this(name, fileLocation, 75, false) {}

        public string fileLocation
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }

        public double volume
        {
            get;
            set;
        }

        public bool isReferance
        {
            get;
            set;
        }

    }















    public class PlayList : IComparable<PlayList>
    {
        public PlayList(string name)
        {

            this.name = name;
            this.backerTracks = new Dictionary<string, Track>();
        }

        public int CompareTo(PlayList other)
        {
            return this.name.CompareTo(other.name);
        }

        private IDictionary<string, Track> backerTracks;

        public ICollection<Track> trackCollenction
        {
            get
            {
                return backerTracks.Values;
            }
        }

        private Track[] backerTrackArray = new Track[0];
        public Track[] tracks
        {
            get
            {
                return backerTrackArray;
            }
        }

        public Track getTrack(string name)
        {
            if (Settings.isCurrentTrackAbbr(name) && backerTracks.Contains(new KeyValuePair<string, Track>(MainClass.currentTrack.name, MainClass.currentTrack)))
            {
                return MainClass.currentTrack;
            }

            if (Settings.isNextTrackAbbr(name) && backerTracks.Contains(new KeyValuePair<string, Track>(MainClass.nextTrack.name, MainClass.nextTrack)))
            {
                return MainClass.nextTrack;
            }
            if (Settings.isTrackAfterNextAbbr(name) && backerTracks.Contains(new KeyValuePair<string, Track>(MainClass.trackAfterNext.name, MainClass.trackAfterNext)))
            {
                return MainClass.trackAfterNext;
            }
            try
            {
                return backerTracks[name];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Track getNextTrack(Track input)
        {
            for (int i = 0; i < trackCollenction.Count; i++)
            {
                if (tracks[i] == input)
                {
                    if (i < trackCollenction.Count - 1)
                        return tracks[i + 1];
                    else
                        return tracks[0];
                }

            }
            Console.WriteLine("Error from playlist: track not in playlist. Ignore further output");
            return null;
        }

        public void addTrack(Track track, double weight)
        {
            /*if (Utils.Utils.contains(tracks, track))
            {
                Console.WriteLine("Error from play list: a track under that name already exists.  Ignore ferther output");
                return;
            }*/
            try
            {
                backerTracks.Add(track.name, track);
            }
            catch (Exception)
            {

            }
            Array.Resize(ref backerTrackArray, backerTrackArray.Length + 1);
            backerTrackArray[backerTrackArray.Length - 1] = track;
            Array.Sort(backerTrackArray);
            weights.Add(track, weight);
            if (weight > maxWeightStore)
            {
                maxWeightStore = weight;
            }
        }

        public void removeTrack(Track track)
        {
            /*if (!Utils.Utils.contains(tracks, track))
            {
                Console.WriteLine("Error from play list: a track under that name dosen't exists.  Ignore ferther output");
                return;
            }*/
            try
            {
                backerTracks.Remove(track.name);

                weights.Remove(track);
            }
            catch (Exception) { }
            backerTrackArray = (new List<Track>(backerTracks.Values)).ToArray();
        }

        public void addTrack(Track track)
        {
            addTrack(track, 50);
        }

        public void setWeight(Track track, double weight)
        {
            weights[track] = weight;
        }

        public string name
        {
            get;
            set;
        }

        private Hashtable weights = new Hashtable();
        
        public double getWeight(Track tr)
        {
            return (double) weights[tr];
        }
        private double maxWeightStore = 50;
        public double maxWeight
        {
            get
            {
                return maxWeightStore;

            }
        }
    }
}
