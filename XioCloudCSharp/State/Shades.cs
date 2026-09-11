using System;
using System.Threading.Tasks;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private void ClearAllShadesScenes()
        {
            ControllerXioCloudSpaceSlotDevice.Shades.ShadesClosedSceneFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[2].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[3].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[4].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[5].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadesOpenSceneFeedback.BoolValue = false;
            _xioCloudRoomGui.Main.Shades.ShadesScene1_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene2_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene3_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene4_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene5_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene6_Selected(false);
        }

        private ushort _currentShadeScene;
        private ushort CurrentShadeScene
        {
            get => _currentShadeScene;
            set
            {
                if (_currentShadeScene == value || value > 6) return;
                _logger.Debug("Current shades scene changed. New scene: {Scene}", value);
                _currentShadeScene = value;
                ClearAllShadesScenes();
                switch (_currentShadeScene)
                {
                    case 1:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadesOpenSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene1_Selected(true);
                        break;
                    case 2:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[2].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene2_Selected(true);
                        break;
                    case 3:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[3].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene3_Selected(true);
                        break;
                    case 4:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[4].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene4_Selected(true);
                        break;
                    case 5:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[5].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene5_Selected(true);
                        break;
                    case 6:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadesClosedSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = false;
                        _xioCloudRoomGui.Main.Shades.ShadesScene6_Selected(true);
                        break;
                    default:
                        break;
                }
            }
        }

        public readonly string[] ShadeSceneNames =
        {
            "", "Open", "My Shade Scene 2", "My Shade Scene 3", "My Shade Scene 4", "My Shade Scene 5", "Closed"
        };

        public void ChangeShadeSceneName(ushort index, string name)
        {
            _logger.Debug("Changing Shades Scene Name. Index: {Index}, Name: {Name}", index, name);
            if (index < 2 || index > 5)
            {
                _logger.Warning("Invalid shade scene index: {Index}. Valid range is 2 to 5", index);
                return;
            }
            switch (index)
            {
                case 2:
                    ShadeSceneNames[2] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene2_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[2].StringValue = name;
                    break;
                case 3:
                    ShadeSceneNames[3] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene3_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[3].StringValue = name;
                    break;
                case 4:
                    ShadeSceneNames[4] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene4_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[4].StringValue = name;
                    break;
                case 5:
                    ShadeSceneNames[5] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene5_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[5].StringValue = name;
                    break;
            }
        }
    }
}