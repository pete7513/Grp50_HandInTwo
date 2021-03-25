using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Test_Program_Charge_Locker
{
   [TestFixture]
   class ChargerControl_Display_Test
   {
      private ChargeControl uut;
      private IDisplay display;
      private FakeUsbCharger usbCharger;


      [SetUp]
      public void Setup()
      {
         display = Substitute.For<IDisplay>();
         usbCharger = new FakeUsbCharger();

         uut = new ChargeControl(display,usbCharger);
      }

      [TestCase(480,1)]
      [TestCase(470, 1)]
      [TestCase(460, 1)]
      [TestCase(350, 1)]
      [TestCase(250, 1)]
      [TestCase(150, 1)]
      [TestCase(50, 1)]
      [TestCase(10, 1)]
      [TestCase(6, 1)]
      [TestCase(5, 0)]
      [TestCase(4.99, 0)]
      [TestCase(4, 0)]
      [TestCase(500, 1)]
      [TestCase(501, 0)]
      [TestCase(57000, 0)]
      public void Charging_WhenNewEvetIsFiredDisplayIsCalled_DisplayMethodChargingIsCalled(double Current, int Called)
      {
         //Arrange
         usbCharger.CurrentValue = Current;

         //Act
         usbCharger.OnNewCurrent();

         //Assert
         display.Received(Called).Charging();
      }


      [TestCase(480, 0)]
      [TestCase(470, 0)]
      [TestCase(500, 0)]
      [TestCase(678, 1)]
      public void ChargingERROR_WhenNewEvetIsFiredDisplayIsCalled_DisplayMethodChargingErrorIsCalled(double Current, int Called)
      {
         //Arrange
         usbCharger.CurrentValue = Current;

         //Act
         usbCharger.OnNewCurrent();

         //Assert
         display.Received(Called).ChargingError();
      }


      [TestCase(6, 0)]
      [TestCase(5, 1)]
      [TestCase(4.99, 1)]
      [TestCase(4, 1)]
      [TestCase(1, 1)]
      [TestCase(0.23, 1)]
      [TestCase(0.000001, 1)]
      [TestCase(0, 0)]
      public void ChargeComplete_WhenNewEvetIsFiredDisplayIsCalled_DisplayMethodChargeCompleteIsCalled(double Current, int Called)
      {
         //Arrange
         usbCharger.CurrentValue = Current;

         //Act
         usbCharger.OnNewCurrent();

         //Assert
         display.Received(Called).ChargeComplete();
      }
   }

   public class FakeUsbCharger
   {
      public event EventHandler<CurrentEventArgs> CurrentValueEvent;
      public double CurrentValue { get; set; }
      
      public void OnNewCurrent()
      {
         CurrentValueEvent?.Invoke(this, new CurrentEventArgs() { Current = this.CurrentValue });
      }
   }
}

