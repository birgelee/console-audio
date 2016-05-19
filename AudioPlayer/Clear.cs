using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Clear : Command
    {
        public Clear()
        {
            nameStore = "clear";
        }
        public override bool preform(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Console cleared");
            return true;
        }
    }
}
