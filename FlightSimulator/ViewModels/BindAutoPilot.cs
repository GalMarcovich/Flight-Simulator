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
        private string text;
        string line;
        public BindAutoPilot()
        {
            line = "";
        }

        public string Line
        {
            get
            {
                // change color if needed
                NotifyPropertyChanged("Color");
                return line;
            }
            set
            {
                line = value;
            }
        }

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
        private void OkClick()
        {
            Commands.Instance.openThread(line);
      
        }

        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => ClearClick()));
            }
        }
        private void ClearClick()
        {
            line = "";
            NotifyPropertyChanged(line);
        }
    }
}
