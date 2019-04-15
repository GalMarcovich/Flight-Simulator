﻿using System;
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
    class BindJoystick
    {
        public float ThrottleCommand
        {
            set
            {
                string throttleLine = "set controls/engines/current-engine/throttle ";
                throttleLine += value;
                throttleLine += "\r\n";
                Commands.Instance.openThread(throttleLine);
            }
        }

        public float RudderCommand
        {
            set
            {
                string rudderLine = "set controls/flight/rudder ";
                rudderLine += value;
                rudderLine += "\r\n";
                Commands.Instance.openThread(rudderLine);
            }
        }

        public float ElevatorCommand
        {
            set
            {
                string ElevatorLine = "set /controls/flight/elevator ";
                ElevatorLine += value;
                ElevatorLine += "\r\n";
                Commands.Instance.openThread(ElevatorLine);
            }
        }

        public float AileronCommand
        {
            set
            {
                string AileronLine = "set /controls/flight/aileron ";
                AileronLine += value;
                AileronLine += "\r\n";
                Commands.Instance.openThread(AileronLine);
            }
        }

    }
}
