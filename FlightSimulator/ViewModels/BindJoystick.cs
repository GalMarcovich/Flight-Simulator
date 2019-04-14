using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using System.Windows;
using System.Windows.Input;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.ViewModels
{
    class BindJoystick : BaseNotify
    {
        string line;

        public BindJoystick()
        {
            line = "";
        }

        public string Line
        {
            set
            {
                string input = value.ToString();
                line = input.Substring(0, 3);
            }
        }

        private ICommand _throttleCommand;
        public ICommand ThrottleCommand
        {
            get
            {
                return _throttleCommand ?? (_throttleCommand = new CommandHandler(() => throttleSlider()));
            }
        }
        private void throttleSlider()
        {
            string throttleLine = "set controls/engines/current-engine/throttle ";
            throttleLine += line;
            throttleLine += "\r\n";
            Commands.Instance.openThread(throttleLine);

        }

        private ICommand _rudderCommand;
        public ICommand RudderCommand
        {
            get
            {
                return _rudderCommand ?? (_rudderCommand = new CommandHandler(() => RudderSlider()));
            }
        }
        private void RudderSlider()
        {
            string rudderLine = "set controls/flight/rudder ";
            rudderLine += line;
            rudderLine += "\r\n";
            Commands.Instance.openThread(rudderLine);
        }
    }
}
