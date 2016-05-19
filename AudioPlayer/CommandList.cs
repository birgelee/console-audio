using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{

    class CommandList
    {
        Command[] backerArray = new Command[18];

        public CommandList()
        {
            backerArray[0] = new Play();
            backerArray[1] = new Status();
            backerArray[2] = new Stop();
            backerArray[3] = new Pause();
            backerArray[4] = new Resume();
            backerArray[5] = new Add();
            backerArray[6] = new ChangePlayList();
            backerArray[7] = new AddPlayList();
            backerArray[8] = new Weigh();
            backerArray[9] = new View();
            backerArray[10] = new Remove();
            backerArray[11] = new Rename();
            backerArray[12] = new PlayMode();
            backerArray[13] = new Reshuffle();
            backerArray[14] = new Skip();
            backerArray[15] = new RemovePlayList();
            backerArray[16] = new Volume();
            backerArray[17] = new History();
        }

        public Command this[string commandName]
        {
            get
            {
                foreach (Command cm in backerArray)
                {
                    
                    if (cm.name.Equals(commandName) || Utils.Utils.contains(cm.getAliases(), commandName))
                    {
                        return cm;
                    }

                }
                return null;
            }
        }

        public void printCommands()
        {
            Console.WriteLine("----------------Commands----------------\n");
            foreach (Command command in backerArray)
            {
                Console.WriteLine(command.name + "\n");
            }
        }

    }
}
