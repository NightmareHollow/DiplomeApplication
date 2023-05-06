using GameCore.SceneManagement.Infrastructure;

namespace GameCore.SceneManagement.ScenesControl.SceneCommands
{
    public struct SceneCommand
    {
        public SceneType TargetSceneType { get; }
        public SceneCommandType CommandType { get; }
        
        
        public SceneCommand(SceneType targetSceneType, SceneCommandType commandType)
        {
            TargetSceneType = targetSceneType;
            CommandType = commandType;
        }
    }
}