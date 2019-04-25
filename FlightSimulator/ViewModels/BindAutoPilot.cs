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

    class BindAutoPilot : BaseNotify
    {
        string line;

        // constructor
        public BindAutoPilot()
        {
            line = "";
        }

        // to create the property Line
        public string Line
        {
            get
            {
                // change color if needed, update the color
                NotifyPropertyChanged("Color");
                return line;
            }
            set
            {
                // update value
                line = value;
            }
        }

        // create the property Color
        public string Color
        {
            get
            {
                if (line == "")
                {
                    return "White";
                }
                else
                {
                    return "Pink";
                }
            }
            private set { }
        }

        private ICommand _okCommand;

        public ICommand OKCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OkClick()));
            }
        }

        // when clicking the button OK - open a thread, reset the line and change background to white
        private void OkClick()
        {
            Commands.Instance.openThread(line);
            line = "";
            NotifyPropertyChanged("Color");

        }

        private ICommand _clearCommand;

        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => ClearClick()));
            }
        }

        // when clicking the button Clear - reset the line and change background to white
        private void ClearClick()
        {
            line = "";
            NotifyPropertyChanged(line);
        }
    }
}
