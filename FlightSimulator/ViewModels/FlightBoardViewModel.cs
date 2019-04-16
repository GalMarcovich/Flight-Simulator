using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {

        public FlightBoardViewModel()
        {
            Info.Instance.PropertyChanged += PropertyChangedvm;
        }
        private void PropertyChangedvm(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        public double Lon
        {
            get
            {
                return Info.Instance.Lon;
            }
        }

        public double Lat
        {
            get
            {
                return Info.Instance.Lat;
            }
        }
    }
}
