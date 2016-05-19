using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;
using System.IO;
using System.Security.Policy;

namespace AudioPlayer
{
    class Add : Command
    {
        public Add()
        {
            nameStore = "add";
            helpInfoStore = "Adds a track to the current playlist and/or the main playlist in the library";
        }
        public override bool preform(string[] args)
        {
            if (args.Length < 2)
                return false;
            Params paramiters = new Params(args);
            bool isSingleReferance = false;
            if (paramiters.hasFlag("-ref"))
            {
                isSingleReferance = true;
            }
            if (paramiters.hasFlag("-**", "-*"))
            {

                string[] paths;

                if (paramiters.hasFlag("-*"))
                    paths = Directory.GetFiles(paramiters.args[0], "*.*").Where(s => Settings.endsWithExtension(s)).ToArray();
                else
                    paths = Directory.GetFiles(paramiters.args[0], "*.*", SearchOption.AllDirectories).Where(s => Settings.endsWithExtension(s)).ToArray();



                foreach (string path in paths)
                {

                    string[] pathParts = path.Split('\\', '/');
                    Track track;

                    if (!isSingleReferance)
                    {
                        string fileLocation = Directory.GetCurrentDirectory() + "\\" + MainClass.library.name + "-songs\\" + pathParts[pathParts.Length - 1];
                        File.Copy(path, fileLocation, true);
                        track = new Track(Utils.Utils.removeSupportedFileExtension(pathParts[pathParts.Length - 1]), fileLocation, Settings.DEFUALT_VOLUME, isSingleReferance);
                    }
                    else
                    {
                        track = new Track(Utils.Utils.removeSupportedFileExtension(pathParts[pathParts.Length - 1]), path, Settings.DEFUALT_VOLUME, isSingleReferance);
                    }

                    MainClass.library.playLists[0].addTrack(track);

                    if (MainClass.library.playLists[0] != MainClass.currentlyViewedPlayList)
                        MainClass.currentlyViewedPlayList.addTrack(track);
                    if (!isSingleReferance)
                        Console.WriteLine("New track added to playlist " + MainClass.currentlyViewedPlayList.name + " named \"" + track.name + "\" from " + path + " and copied to " + track.fileLocation + "\n");
                    else
                        Console.WriteLine("New track added to playlist " + MainClass.currentlyViewedPlayList.name + " named \"" + track.name + "\" and referanced from " + track.fileLocation + "\n");
                }
            }



            else if (MainClass.library.playLists[0].getTrack(args[1]) == null)
            {
                string fileLocaition = "";
                if (!File.Exists(paramiters.args[1]))
                {
                    try
                    {
                        string filePath = Utils.Utils.pullFromYoutube(paramiters.args[1]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No file found in that location");
                        return true;
                    }

                }

                else if (isSingleReferance)
                {
                    fileLocaition = paramiters.args[1];
                }
                else
                {
                    fileLocaition = Directory.GetCurrentDirectory() + "\\" + MainClass.library.name + "-songs\\" + paramiters.args[0] + Path.GetExtension(paramiters.args[1]);
                    File.Copy(paramiters.args[1], fileLocaition, true);
                }
                Track track = new Track(paramiters.args[0], fileLocaition, Settings.DEFUALT_VOLUME, isSingleReferance);
                MainClass.library.playLists[0].addTrack(track);
                if (MainClass.library.playLists[0] != MainClass.currentlyViewedPlayList)
                    MainClass.currentlyViewedPlayList.addTrack(track);
                Console.WriteLine("New track added to playlist " + MainClass.currentlyViewedPlayList.name + " named " + paramiters.args[0] + " and located at " + paramiters.args[1]);

            }
            else
            {
                Track tr = MainClass.library.playLists[0].getTrack(paramiters.args[0]);
                if (MainClass.currentlyViewedPlayList.getTrack(tr.name) == null)
                {
                    MainClass.currentlyViewedPlayList.addTrack(tr);
                    Console.WriteLine("Added track {0} to playlist {1}", tr.name, MainClass.currentlyViewedPlayList.name);
                }
                else
                    Console.WriteLine("There is already a track in this playlist under that name");
            }
            MainClass.library.saveToFile();
            return true;
        }

    }
}
