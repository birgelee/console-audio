using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;

namespace AudioPlayer
{
    class ChangePlayList : Command
    {
        public ChangePlayList()
        {
            nameStore = "changepl";
            aliases = new string[1];
            aliases[0] = "cpl";
        }
        public override bool preform(string[] args)
        {
            if (args.Length < 2)
                return false;
            if (MainClass.library[args[1]] != null)
            {

                MainClass.currentlyViewedPlayList = MainClass.library[args[1]];
                Console.WriteLine("Changed to playlist " + args[1]);
            }
            else
                Console.WriteLine("There is no playlist under the name " + args[1]);

            return true;
        }

        public override string[] getAliases()
        {
            return aliases;
        }
    }
    class AddPlayList : Command
    {
        public AddPlayList()
        {
            nameStore = "addpl";
            aliases = new string[2];
            aliases[0] = "apl";
            aliases[1] = "addplaylist";
        }
        public override bool preform(string[] args)
        {
            if (args.Length < 2)
                return false;
            PlayList pl = new PlayList(args[1]);
            PlayList[] playLists = MainClass.library.playLists;
            for (int i = 0; i < playLists.Length; i++)
            {
                if (playLists[i].name.Equals(pl.name))
                {
                    Console.WriteLine("A plylist under that name already exists");
                    return true;
                }

            }
            MainClass.library.addPlayList(pl);
            MainClass.currentlyViewedPlayList = pl;
            MainClass.library.saveToFile();
            Console.WriteLine("Added playlist " + pl.name);
            return true;
        }

        public override string[] getAliases()
        {
            return aliases;
        }
    }

    class RemovePlayList : Command
    {


        public RemovePlayList()
        {
            nameStore = "removepl";
            aliases = new string[2];
            aliases[0] = "rpl";
            aliases[1] = "removeplaylist";
        }

        public override bool preform(string[] args)
        {
            if (args.Length < 2)
                return false;
            PlayList removedPlayList = MainClass.library[args[1]];
            if (removedPlayList == null)
            {
                Console.WriteLine("There is no playlist under that name");
                return true;
            }
            if (removedPlayList == MainClass.library.playLists[0])
            {
                Console.WriteLine("The master playlist can not be removed");
                return true;
            }

            MainClass.library.removePlayList(removedPlayList);
            
            MainClass.library.saveToFile();
            Console.WriteLine("Removed playlist " + removedPlayList.name);
            return true;
        }

        public override string[] getAliases()
        {
            return aliases;
        }
    }
}
