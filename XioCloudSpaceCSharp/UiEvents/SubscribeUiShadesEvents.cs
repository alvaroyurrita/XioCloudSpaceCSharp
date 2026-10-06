using XioCloudSpaceConstruct;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiShadesEvents()
        {
            var uiShades = _xioCloudRoomGui.Main.Shades;
            var xioShades = ControllerXioCloudSpaceSlotDevice.Shades;
            var currentShadeSceneName = string.Empty;
            
            _logger.Debug("Subscribing to UI Main Shades events...");
            uiShades.ShadesScene1_PressEvent += (_, args) =>
            {
                SetShadeScene(args, 1);
            };
            uiShades.ShadesScene2_PressEvent += (_, args) =>
            {
                SetShadeScene(args, 2);
            };
            uiShades.ShadesScene3_PressEvent += (_, args) =>
            {
                SetShadeScene(args, 3);
            };
            uiShades.ShadesScene4_PressEvent += (_, args) =>
            {
                SetShadeScene(args, 4);
            };
            uiShades.ShadesScene5_PressEvent += (_, args) =>
            {
                SetShadeScene(args, 5);
            };
            uiShades.ShadesScene6_PressEvent += (_, args) =>
            {
                SetShadeScene(args, 6);
            };

            uiShades.ShadesSceneNameText_OutputTextEvent += (_, args) =>
            {
                currentShadeSceneName = args.String();
                _logger.Debug("ShadesSceneNameText_OutputTextEvent. Current Scene Name: {SceneName}:", currentShadeSceneName);
                uiShades.ShadesSceneNameText_IndirectText(currentShadeSceneName);
            };
            uiShades.ShadesRequestByNamePress_PressEvent += (_, args) =>
            {
                if (! string.IsNullOrEmpty(currentShadeSceneName))
                {
                   xioShades.ActiveShadeSceneName.StringValue = currentShadeSceneName;
                   currentShadeSceneName = string.Empty;
                   uiShades.ShadesSceneNameText_IndirectText(currentShadeSceneName);
                }
            };
            
            return;
            void SetShadeScene(UIEventArgs args, ushort scene)
            {
                var state = args.Boolean();
                _logger.Debug("ShadesScene{Scene}_PressEvent. State {State}:", scene, state); 
                if (state)
                {
                    _logger.Debug("Toggling Shades Scene {Scene}...", scene);
                    CurrentShadeScene = scene;
                }
                if (!string.IsNullOrEmpty(currentShadeSceneName))
                {
                    ChangeShadeSceneName(CurrentShadeScene, currentShadeSceneName);
                    currentShadeSceneName = string.Empty;
                    uiShades.ShadesSceneNameText_IndirectText(currentShadeSceneName);
                }
            }
        }

    }
}