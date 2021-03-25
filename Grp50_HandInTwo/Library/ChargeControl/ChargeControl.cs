using System;

namespace Ladeskab
{
   public class ChargeControl : IChargeControl
   {
      private readonly IDisplay _display;
      private readonly IUsbCharger _usbCharger;

      public bool Connected { get; set; }

      public ChargeControl(IDisplay display, IUsbCharger usbCharger)
      {
         _display = display;
         _usbCharger = usbCharger;
         _usbCharger.CurrentValueEvent += _usbCharger_CurrentValueEvent;
      }

      private void _usbCharger_CurrentValueEvent(object sender, CurrentEventArgs e)
      {
         if (e.Current == 0) { }
         else if (e.Current > 0 && e.Current <= 5)
            _display.ChargeComplete();
         else if (e.Current > 5 && e.Current <= 500)
            _display.Charging();
         else if (e.Current > 500)
         _display.ChargingError();
         
         _display.CurrentPowerValue(e);
      }

      public void StartCharge()
      {
         _usbCharger.Connected = Connected;
         _usbCharger.StartCharge();
      }

      public void StopCharge()
      {
         _usbCharger.Connected = Connected;
         _usbCharger.StopCharge();
      }
   }
}