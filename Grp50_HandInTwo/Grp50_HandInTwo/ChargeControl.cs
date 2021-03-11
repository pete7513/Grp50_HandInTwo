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
      }

      public void StartCharge()
      {
         // Ignore if already charging
         if (!_charging)
         {
            if (Connected && !_overload)
            {
               CurrentValue = 500;
            }
            else if (Connected && _overload)
            {
               CurrentValue = OverloadCurrent;
            }
            else if (!Connected)
            {
               CurrentValue = 0.0;
            }

            OnNewCurrent();
            _ticksSinceStart = 0;

            _charging = true;

            _timer.Start();
         }
      }

      public void StopCharge()
      {
         _timer.Stop();

         CurrentValue = 0.0;
         OnNewCurrent();

         _charging = false;
      }
   }
}