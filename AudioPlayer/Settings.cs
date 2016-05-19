using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Settings
    {

        public static string CURRENT_TRACK_ABBR = "<current>";
        public static string CURRENT_TRACK_ABBR2 = "<c>";
        public static string NEXT_TRACK_ABBR = "<next>";
        public static string NEXT_TRACK_ABBR2 = "<n>";
        public static string TRACK_AFTER_NEXT_ABBR = "<after-next>";
        public static string TRACK_AFTER_NEXT_ABBR2 = "<a>";
        public static double DEFUALT_VOLUME = 75;

        public static string[] SUPPORTED_FILE_TYPES
        {
            get
            {
                string[] temp = new string[4];
                temp[0] = ".mp3";
                temp[1] = ".m4a";
                temp[2] = ".wav";
                temp[3] = ".mp4";
                return temp;
            }
        }

        public static bool endsWithExtension(string s)
        {
            foreach (string extension in SUPPORTED_FILE_TYPES)
            {
                if (s.EndsWith(extension))
                    return true;
            }
            return false;
        }


        public static bool isCurrentTrackAbbr(string input)
        {
            if (input.Equals(CURRENT_TRACK_ABBR) || input.Equals(CURRENT_TRACK_ABBR2))
                return true;
            return false;
        }

        public static bool isNextTrackAbbr(string input)
        {
            if (input.Equals(NEXT_TRACK_ABBR) || input.Equals(NEXT_TRACK_ABBR2))
                return true;
            return false;
        }

        public static bool isTrackAfterNextAbbr(string input)
        {
            if (input.Equals(TRACK_AFTER_NEXT_ABBR) || input.Equals(TRACK_AFTER_NEXT_ABBR2))
                return true;
            return false;
        }
    }
}
