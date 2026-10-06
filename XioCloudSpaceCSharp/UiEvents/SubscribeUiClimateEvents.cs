using XioCloudSpaceConstruct;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiClimateEvents()
        {
            var uiClimate = _xioCloudRoomGui.Main.Climate;
            var xioClimate = ControllerXioCloudSpaceSlotDevice.Climate;

            uiClimate.TemperatrueInCelsius_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("TemperatrueInCelsius_PressEvent. State {State}:", state);
                if (state)
                {
                    TemperatureInCelsius = true;
                }
            };

            uiClimate.TemperatureInFarenheit_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("TemperatureInFahrenheit_PressEvent. State {State}:", state);
                if (state)
                {
                    TemperatureInCelsius = false;
                }
            };

            uiClimate.TemperatureCurrent_LowerTouchEvent += (_, args) =>
            {
                var state = args.UShort();
                _logger.Debug("TemperatureCurrent_LowerTouchEvent. State {State}:", state);
                CurrentTemperature = state;
            };
            
            uiClimate.TemperatureSet_LowerTouchEvent += (_, args) =>
            {
                var state = args.UShort();
                _logger.Debug("TemperatureSet_LowerTouchEvent. State {State}:", state);
                SetTemperature = state;
            };

            uiClimate.HumidityCurrent_LowerTouchEvent += (_, args) =>
            {
                var state = args.UShort();
                _logger.Debug("HumidityCurrent_LowerTouchEvent. State {State}:", state);
                CurrentHumidity = state;
            };

            uiClimate.HumiditySet_LowerTouchEvent += (_, args) =>
            {
                var state = args.UShort();
                _logger.Debug("HumiditySet_LowerTouchEvent. State {State}:", state);
                SetHumidity = state;
            };
            
            uiClimate.ClimateMode_LowerTouchEvent += (_, args) =>
            {
                var state = args.UShort();
                _logger.Debug("ClimateMode_LowerTouchEvent. State {State}:", state);
                CurrentClimateMode = state;
            };
        }
    }
}