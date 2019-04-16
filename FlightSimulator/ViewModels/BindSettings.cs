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
        private void OnClick()
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }


        public ICommand DisConnectCommand
        {
            get
            {
                return _disConnectCommand ?? (_disConnectCommand = new CommandHandler(() => OnClickConnect()));
            }
        }



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

        private void OnClickConnect()
        {
            if (!Commands.Instance.getIsConnect())
            {
                new Thread(() =>
            {

                Info.Instance.connect();
                Commands.Instance.connect();

            }).Start();
            }
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
