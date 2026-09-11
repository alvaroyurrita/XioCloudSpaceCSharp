using System;
using System.Threading.Tasks;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private bool _temperatureInCelsius;
        public bool TemperatureInCelsius
        {
            get => _temperatureInCelsius;
            set
            {
                _temperatureInCelsius = value;
                _logger.Debug("Temperature unit changed. Is Celsius: {IsCelsius}", value);
                _xioCloudRoomGui.Main.Climate.TemperatureInFarenheit_Selected(!_temperatureInCelsius);
                _xioCloudRoomGui.Main.Climate.TemperatrueInCelsius_Selected(_temperatureInCelsius);
                ControllerXioCloudSpaceSlotDevice.Climate.TemperatureInCelsiusFeedback.BoolValue =
                    _temperatureInCelsius;
                UpdateTemperatureDisplay();
                UpdateSetTemperatureDisplay();
            }
        }

        private ushort _currentTemperature;
        public ushort CurrentTemperature
        {
            get => _currentTemperature;
            set
            {
                if (_currentTemperature == value) return;
                _logger.Debug("Current temperature changed. New temperature: {Temperature}", value);
                _currentTemperature = value;
                _xioCloudRoomGui.Main.Climate.TemperatureCurrent_LowerTouchfb(_currentTemperature);
                UpdateTemperatureDisplay();
            }
        }

        private void UpdateTemperatureDisplay()
        {
            if (TemperatureInCelsius)
            {
                _xioCloudRoomGui.Main.Climate.TemperatureCurrentText_Indirect(
                    $"{HelperClasses.FahrenheitToCelsius(CurrentTemperature) / 10.0:F1} °C"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.CurrentTemperatureFeedback.UShortValue =
                    HelperClasses.FahrenheitToCelsius(CurrentTemperature);
            }
            else
            {
                _xioCloudRoomGui.Main.Climate.TemperatureCurrentText_Indirect(
                    $"{_currentTemperature / 10.0:F1} °F"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.CurrentTemperatureFeedback.UShortValue = _currentTemperature;
            }
        }

        private ushort _setTemperature ;
        public ushort SetTemperature
        {
            get => _setTemperature;
            set
            {
                if (_setTemperature == value) return;
                _logger.Debug("Set temperature changed. New temperature: {Temperature}", value);
                _setTemperature = value;
                _xioCloudRoomGui.Main.Climate.TemperatureSet_LowerTouchfb(_setTemperature);
                UpdateSetTemperatureDisplay();
            }
        }
        
        private void UpdateSetTemperatureDisplay()
        {
            if (TemperatureInCelsius)
            {
                _xioCloudRoomGui.Main.Climate.TemperatureSetText_Indirect(
                    $"{HelperClasses.FahrenheitToCelsius(SetTemperature) / 10.0:F1} °C"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.TemperatureSetpointFeedback.UShortValue =
                    HelperClasses.FahrenheitToCelsius(SetTemperature);
            }
            else
            {
                _xioCloudRoomGui.Main.Climate.TemperatureSetText_Indirect(
                    $"{SetTemperature / 10.0:F1} °F"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.TemperatureSetpointFeedback.UShortValue = SetTemperature;
            }
        }

        private ushort _currentHumidity = 500;
        public ushort CurrentHumidity
        {
            get => _currentHumidity;
            set
            {
                if (_currentHumidity == value) return;
                _logger.Debug("Current humidity changed. New humidity: {Humidity}", value);
                _currentHumidity = value;
                _xioCloudRoomGui.Main.Climate.HumidityCurrent_LowerTouchfb(_currentHumidity);
                ControllerXioCloudSpaceSlotDevice.Climate.CurrentHumidityFeedback.UShortValue = _currentHumidity;
            }
        }
        
        private ushort _setHumidity = 500;
        public ushort SetHumidity
        {
            get => _setHumidity;
            set
            {
                if (_setHumidity == value) return;
                _logger.Debug("Set humidity changed. New humidity: {Humidity}", value);
                _setHumidity = value;
                _xioCloudRoomGui.Main.Climate.HumiditySet_LowerTouchfb(_setHumidity);
                ControllerXioCloudSpaceSlotDevice.Climate.HumiditySetpointFeedback.UShortValue = _setHumidity;
            }
        }

        public readonly string[] ClimateModes = { "", "Off", "Heat", "Aux Heat", "Cool", "Auto" };

        private ushort _currentClimateMode = 0;
        public ushort CurrentClimateMode
        {
            get => _currentClimateMode;
            set
            {
                if (_currentClimateMode == value || value == 0 || value > 5) return;
                _logger.Debug("Current climate mode changed. New mode: {Mode}", ClimateModes[value]);
                _currentClimateMode = value;
                _xioCloudRoomGui.Main.Climate.ClimateMode_LowerTouchfb(_currentClimateMode);
                _xioCloudRoomGui.Main.Climate.ClientModeText_Indirect(ClimateModes[_currentClimateMode]);
                ControllerXioCloudSpaceSlotDevice.Climate.ClimateModeFeedback.UShortValue = _currentClimateMode;
            }
        }
    }
}