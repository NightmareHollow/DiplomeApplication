using System;
using System.Collections.Generic;

namespace GameCore.SceneManagement.ScenesControl.SceneCommands
{
    public class SceneLoaderCommand
    {
        private readonly List<SceneCommand> commands = new List<SceneCommand>();
        private Action completeCallback;

        public SceneLoaderCommand(Action completeCallback, params SceneCommand[] sceneCommands)
        {
            this.completeCallback = completeCallback;
            
            commands.AddRange(sceneCommands);
        }
        
        
    }
}