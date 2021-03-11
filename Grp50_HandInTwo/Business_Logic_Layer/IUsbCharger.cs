﻿using System;

namespace UsbSimulator
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }

    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        // Start charging
        void StartCharge();

        // Stop charging
        void StopCharge();
    }

    public class UsbCharger : IUsbCharger
    {
       public event EventHandler<CurrentEventArgs> CurrentValueEvent;

       public double CurrentValue { get; }

       public bool Connected { get; }

       public void StartCharge()
       {
          throw new NotImplementedException();
       }

       public void StopCharge()
       {
          throw new NotImplementedException();
       }
    }
}