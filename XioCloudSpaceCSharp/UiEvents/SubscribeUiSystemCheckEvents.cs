using Crestron.SimplSharpPro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiSystemCheckEvents()
        {
            var uiMain = _xioCloudRoomGui.Main;
            var xioMain = ControllerXioCloudSpaceSlotDevice;
            var xioCheckStateActions = new Dictionary<XioCloudSpaceSlotBase.eSystemCheckState, Action>
            {
                { XioCloudSpaceSlotBase.eSystemCheckState.Idle, () => uiMain.CheckIdlePress_Selected(true) },
                {
                    XioCloudSpaceSlotBase.eSystemCheckState.Running,
                    () => uiMain.CheckInProgressPress_Selected(true)
                },
                { XioCloudSpaceSlotBase.eSystemCheckState.Success, () => uiMain.ChecCompletedPress_Selected(true) },
                { XioCloudSpaceSlotBase.eSystemCheckState.Failed, () => uiMain.CheckFailedPress_Selected(true) },
                { XioCloudSpaceSlotBase.eSystemCheckState.Error, () => uiMain.CheckWithErrorsPress_Selected(true) }
            };
            var currentSystemCheckMessage = string.Empty;

            _logger.Debug("Subscribing to UI Main System Check events...");
            uiMain.CheckIdlePress_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("CheckIdlePress_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Setting system check state to Idle");
                    SetSystemCheckState(XioCloudSpaceSlotBase.eSystemCheckState.Idle);
                }
            };
            uiMain.CheckInProgressPress_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("CheckInProgressPress_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Setting system check state to Running");
                    SetSystemCheckState(XioCloudSpaceSlotBase.eSystemCheckState.Running);
                    ;
                }
            };

            uiMain.ChecCompletedPress_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("CheckCompletedPress_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Setting system check state to Success");
                    SystemCheckEnd();
                    SetSystemCheckState(XioCloudSpaceSlotBase.eSystemCheckState.Success);
                }
            };
            uiMain.CheckFailedPress_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("CheckFailedPress_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Setting system check state to Failed");
                    SystemCheckEnd();
                    SetSystemCheckState(XioCloudSpaceSlotBase.eSystemCheckState.Failed);
                }
            };
            uiMain.CheckWithErrorsPress_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("CheckWithErrorsPress_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Setting system check state to Error");
                    SystemCheckEnd();
                    SetSystemCheckState(XioCloudSpaceSlotBase.eSystemCheckState.Error);
                }
            };
            uiMain.SystemCheckMessage_OutputTextEvent += (_, args) =>
            {
                var message = args.String();
                uiMain.SystemCheckMessage_IndirectText(message);
                currentSystemCheckMessage = message;
            };
            uiMain.SendSystemCheckMessage_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("SendSystemCheckMessage_PressEvent. State {State}:", state);
                if (state)
                {
                    xioMain.SystemCheckMessageFeedback.StringValue = currentSystemCheckMessage;
                }
            };
            return;

            void SystemCheckEnd()
            {
                _logger.Debug("SystemCheckEnd. Resetting system check state to Standby after 5 seconds");
                SetUiSystemCheckButtonsEnabled(false);
                uiMain.SystemCheckRequest_Selected(false);
                uiMain.SystemCheckMessage_IndirectText(string.Empty);
                currentSystemCheckMessage = string.Empty;
                Task.Run(async () =>
                    {
                        await Task.Delay(5000);
                        ClearSystemCheckButtons();
                    }
                );
            }

            void SetSystemCheckState(XioCloudSpaceSlotBase.eSystemCheckState state)
            {
                ClearSystemCheckButtons();
                xioMain.SystemCheckStateFeedback.Value = state;
                xioCheckStateActions[state]?.Invoke();
            }

            void ClearSystemCheckButtons()
            {
                uiMain.CheckIdlePress_Selected(false);
                uiMain.CheckInProgressPress_Selected(false);
                uiMain.ChecCompletedPress_Selected(false);
                uiMain.CheckFailedPress_Selected(false);
                uiMain.CheckWithErrorsPress_Selected(false);
            }
        }
        private void SetUiSystemCheckButtonsEnabled(bool enabled)
        {
            var uiMain = _xioCloudRoomGui.Main;
            uiMain.ChecCompletedPress_Enable(enabled);
            uiMain.CheckFailedPress_Enable(enabled);
            uiMain.CheckIdlePress_Enable(enabled);
            uiMain.CheckInProgressPress_Enable(enabled);
            uiMain.CheckWithErrorsPress_Enable(enabled);
            uiMain.SendSystemCheckMessage_Enable(enabled);
            uiMain.SystemCheckMessage_Enable(enabled);
            uiMain.SystemCheckRequest_Selected(enabled);
        }
    }
}