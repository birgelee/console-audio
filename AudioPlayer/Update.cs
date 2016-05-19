using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using Microsoft.Xna.Framework.Media;

namespace AudioPlayer
{
    class Update
    {
        public static void onSongChange()
        {

            while (true)
            {
                switch (MediaPlayer.State)
                {

                    case MediaState.Stopped:
                        if (MainClass.playNext && MainClass.queingTrack == false)
                        Utils.Utils.playNextTrack();
                        break;
                    case MediaState.Playing:
                        MainClass.currentTime = MainClass.currentTime.Add(new TimeSpan(0, 0, 0, 1, 0));
                        break;
                    case MediaState.Paused:
                        break;

                }
                Thread.Sleep(1000);
            }

        }

    }
}

