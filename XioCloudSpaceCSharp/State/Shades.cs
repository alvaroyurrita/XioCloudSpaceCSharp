using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        private void ClearAllShadesScenes()
        {
            _xioCloudRoomGui.Main.Shades.ShadesScene1_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene2_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene3_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene4_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene5_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene6_Selected(false);
            // TODO: Clear XIO feedbacks for shades scenes

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
                        _xioCloudRoomGui.Main.Shades.ShadesScene1_Selected(true);
                        //TODO: Set XIO Shade feedbacks for scene 1
                        break;
                    case 2:
                        _xioCloudRoomGui.Main.Shades.ShadesScene2_Selected(true);
                        //TODO: Set XIO Shade feedbacks for scene 2
                        break;
                    case 3:
                        _xioCloudRoomGui.Main.Shades.ShadesScene3_Selected(true);
                        //TODO: Set XIO Shade feedbacks for scene 3
                        break;
                    case 4:
                        _xioCloudRoomGui.Main.Shades.ShadesScene4_Selected(true);
                        //TODO: Set XIO Shade feedbacks for scene 4
                        break;
                    case 5:
                        _xioCloudRoomGui.Main.Shades.ShadesScene5_Selected(true);
                        //TODO: Set XIO Shade feedbacks for scene 5
                        break;
                    case 6:
                        _xioCloudRoomGui.Main.Shades.ShadesScene6_Selected(true);
                        //TODO: Set XIO Shade feedbacks for scene 6
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
                    //TODO: Update XIO feedback for shade scene 2 name
                    break;
                case 3:
                    ShadeSceneNames[3] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene3_Serial_CIP(name);
                    //TODO: Update XIO feedback for shade scene 3 name
                    break;
                case 4:
                    ShadeSceneNames[4] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene4_Serial_CIP(name);
                    //TODO: Update XIO feedback for shade scene 4 name
                    break;
                case 5:
                    ShadeSceneNames[5] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene5_Serial_CIP(name);
                    //TODO: Update XIO feedback for shade scene 5 name
                    break;
            }
        }
    }
}