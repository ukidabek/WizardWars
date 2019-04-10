using BaseGameLogic.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class CommandManager : SingletonMonoBehaviour<CommandManager>
    {
        private Queue<Command> commands = new Queue<Command>();

        private void Update()
        {
            if(commands.Count > 0)
                while(commands.Count > 0)
                    commands.Dequeue().Execute();
        }

        public void EnqueueCommand(Command command)
        {
            commands.Enqueue(command);
        }
    }
}