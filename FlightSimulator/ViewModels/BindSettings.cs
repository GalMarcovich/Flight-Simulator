using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Commands command = new Commands();
            Info info = new Info();
        }
    }
}
