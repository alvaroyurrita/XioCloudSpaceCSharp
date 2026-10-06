using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        private void ClearAllLightScenes()
        {
            ControllerXioCloudSpaceSlotDevice.Lighting.LightsOnSceneFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[2].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[3].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[4].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[5].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightsOffSceneFeedback.BoolValue = false;
            _xioCloudRoomGui.Main.Lights.LightsScene1_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene2_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene3_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene4_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene5_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene6_Selected(false);
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
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightsOnSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene1_Selected(true);
                        break;
                    case 2:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[2].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene2_Selected(true);
                        break;
                    case 3:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[3].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene3_Selected(true);
                        break;
                    case 4:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[4].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene4_Selected(true);
                        break;
                    case 5:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[5].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene5_Selected(true);
                        break;
                    case 6:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightsOffSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = false;
                        _xioCloudRoomGui.Main.Lights.LightsScene6_Selected(true);
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
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[2].StringValue = name;
                    break;
                case 3:
                    LightSceneNames[3] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene3_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[3].StringValue = name;
                    break;
                case 4:
                    LightSceneNames[4] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene4_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[4].StringValue = name;
                    break;
                case 5:
                    LightSceneNames[5] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene5_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[5].StringValue = name;
                    break;
            }
        }
    }
}