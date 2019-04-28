using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using FlightSimulator.Views;

namespace FlightSimulator.ViewModels
{
    class BindSettings
    {
        private ICommand _settingsCommand;

        private ICommand _connectCommand;

        private ICommand _disConnectCommand;

        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => OnClick()));
            }
            set
            {

            }
        }

        // when pressing the settings button - open the window of the settings
        private void OnClick()
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        public ICommand DisConnectCommand
        {
            get
            {
                return _disConnectCommand ?? (_disConnectCommand = new CommandHandler(() => OnClickDisConnect()));
            }
        }

        // when pressing the disConnect button - change the boolean "shouldStop" to true and disConnect
        private void OnClickDisConnect()
        {
            Info.Instance.shouldStop = true;
            Commands.Instance.disConnect();
        }
       
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => OnClickConnect()));
            }
            set
            {

            }
        }

        // when pressing the connect button - connect
        private void OnClickConnect()
        {
            // if not connected - open a new thread and connect
            if (!Commands.Instance.getIsConnect())
            {
                new Thread(() =>
                {
                    Info.Instance.connect();
                    Commands.Instance.connect();
                }).Start();
            }
            // if already connected - first disConnect and then connect again
            else
            {
                new Thread(() =>
                {
                    Commands.Instance.disConnect();
                    Commands.Instance.connect();
                }).Start();
            }
        }
    }
}

