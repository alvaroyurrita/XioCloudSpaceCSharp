using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        private void ClearAllLightScenes()
        {
            _xioCloudRoomGui.Main.Lights.LightsScene1_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene2_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene3_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene4_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene5_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene6_Selected(false);
            // TODO: Clear all XIO Lights feedbacks
           
        }

        private ushort _currentLightScene;
        private ushort CurrentLightScene
        {
            get => _currentLightScene;
            set
            {
                if (_currentLightScene == value || value > 6) return;
                _logger.Debug("Current light scene changed. New scene: {Scene}", value);
                _currentLightScene = value;
                ClearAllLightScenes();
                switch (_currentLightScene)
                {
                    case 1:
                        _xioCloudRoomGui.Main.Lights.LightsScene1_Selected(true);
                        // TODO: Set the feedback for Lights On Scene and Any Light On
                        break;
                    case 2:
                        _xioCloudRoomGui.Main.Lights.LightsScene2_Selected(true);
                        // TODO: Set the feedback for Lights Scene 2 and Any Light On
                        break;
                    case 3:
                        _xioCloudRoomGui.Main.Lights.LightsScene3_Selected(true);
                        // TODO: Set the feedback for Lights Scene 3 and Any Light On
                        break;
                    case 4:
                        _xioCloudRoomGui.Main.Lights.LightsScene4_Selected(true);
                        // TODO: Set the feedback for Lights Scene 4 and Any Light On
                        break;
                    case 5:
                        _xioCloudRoomGui.Main.Lights.LightsScene5_Selected(true);
                        // TODO: Set the feedback for Lights Scene 5 and Any Light On
                        break;
                    case 6:
                        _xioCloudRoomGui.Main.Lights.LightsScene6_Selected(true);
                        // TODO: Set the feedback for Lights Scene 6 and Any Light On
                        break;
                    default:
                        break;
                }
            }
        }

        public readonly string[] LightSceneNames =
        {
            "", "On", "My Light Scene 2", "My Light Scene 3", "My Light Scene 4", "My Light Scene 5", "Off"
        };

        public void ChangeLightSceneName(ushort index, string name)
        {
            _logger.Debug("Changing Light Scene Name. Index: {Index}, Name: {Name}", index, name);
            if (index < 2 || index > 5)
            {
                _logger.Warning("Invalid light scene index: {Index}. Valid range is 2 to 5", index);
                return;
            }
            switch (index)
            {
                case 2:
                    LightSceneNames[2] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene2_Serial_CIP(name);
                    //TODO: Update XIO feedback for light scene 2 name
                    break;
                case 3:
                    LightSceneNames[3] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene3_Serial_CIP(name);
                    //TODO: Update XIO feedback for light scene 3 name
                    break;
                case 4:
                    LightSceneNames[4] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene4_Serial_CIP(name);
                    //TODO: Update XIO feedback for light scene 4 name
                    break;
                case 5:
                    LightSceneNames[5] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene5_Serial_CIP(name);
                    //TODO: Update XIO feedback for light scene 5 name
                    break;
            }
        }
    }
}