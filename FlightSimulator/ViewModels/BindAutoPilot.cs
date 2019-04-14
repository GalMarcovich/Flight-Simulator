using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class BindAutoPilot : BaseNotify
    {
        string[] input;
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
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick()
        {
            Parse(line);

            foreach (string arg in input)
            {
                MessageBox.Show(arg);
            }
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

        private void Parse(string line)
        {
            string[] newLine = { "\r\n" };
            input = line.Split(newLine, StringSplitOptions.None);
        }
    }
}
