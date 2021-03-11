using System;

namespace Ladeskab
{
   public interface IChargeControl
   {
      void StartCharge();
      void StopCharge();
   }

   public class ChargeControl : IChargeControl
   {
      private readonly IDisplay _display;
      private readonly IUsbCharger _usbCharger;

      public ChargeControl(IDisplay display, IUsbCharger usbCharger)
      {
         _display = display;
         _usbCharger = usbCharger;
         _usbCharger.CurrentValueEvent += _usbCharger_CurrentValueEvent;
      }

      private void _usbCharger_CurrentValueEvent(object sender, CurrentEventArgs e)
      {
         Console.WriteLine("Current Power Value: " + e.Current);
      }

      public void StartCharge()
      {
         _usbCharger.StartCharge();
      }

      public void StopCharge()
      {
         _usbCharger.StopCharge();
      }
   }
}