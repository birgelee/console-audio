using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;

namespace AudioPlayer
{
    class History : Command
    {
        public History()
        {
            nameStore = "history";
        }

        public override bool preform(string[] args)
        {
            if (args.Length > 1 && args[1].ToLower().Equals("clear"))
            {
                MainClass.history = new Track[0];
                Console.WriteLine("History cleared");
                return true;
            }
            Console.WriteLine("\n----------------Tracks Played----------------\n");
            for (int i = 0; i < MainClass.history.Length; i++)
            {
                Console.WriteLine(MainClass.history[i].name + "\n");
            }


            return true;
        }

    }
}
