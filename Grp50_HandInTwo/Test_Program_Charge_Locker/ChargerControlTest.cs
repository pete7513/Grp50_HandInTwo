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
   class ChargerControlTest
   {
      private ChargeControl uut;
      private IDisplay display;
      private IUsbCharger usbCharger;


      [SetUp]
      public void Setup()
      {
         display = Substitute.For<IDisplay>();
         usbCharger = Substitute.For<IUsbCharger>();

         uut = new ChargeControl(display, usbCharger);
      }

      [Test]
      public void StartCharge_StartChargeInUSBcharger_True()
      {
         //Arrange

         //Act
         uut.StartCharge();

         //Assert
        usbCharger.Received(1).StartCharge();
      }

      [Test]
      public void StartCharge_StartChargeInUSBcharger_false()
      {
         //Arrange

         //Act

         //Assert
         usbCharger.Received(0).StartCharge();
      }

      [Test]
      public void StopCharge_StopChargeInUSBcharger_True()
      {
         //Arrange

         //Act
         uut.StopCharge();

         //Assert
         usbCharger.Received(1).StopCharge();
      }

      [Test]
      public void StopCharge_StopChargeInUSBcharger_False()
      {
         //Arrange

         //Act
         uut.StopCharge();

         //Assert
         usbCharger.Received(1).StopCharge();
      }

      [TestCase(480, 1)]
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

         ////Act
         usbCharger.CurrentValueEvent += Raise.EventWith((new CurrentEventArgs() { Current = Current }));

         ////Assert
         display.Received(Called).Charging();
      }

      [TestCase(480, 0)]
      [TestCase(470, 0)]
      [TestCase(500, 0)]
      [TestCase(501, 1)]
      [TestCase(510, 1)]
      [TestCase(678, 1)]

      public void ChargingERROR_WhenNewEvetIsFiredDisplayIsCalled_DisplayMethodChargingErrorIsCalled(double Current,
         int Called)
      {
         //Arrange

         ////Act
         usbCharger.CurrentValueEvent += Raise.EventWith((new CurrentEventArgs() { Current = Current }));

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
      public void ChargeComplete_WhenNewEvetIsFiredDisplayIsCalled_DisplayMethodChargeCompleteIsCalled(double Current,
         int Called)
      {
         //Arrange

         ////Act
         usbCharger.CurrentValueEvent += Raise.EventWith((new CurrentEventArgs() { Current = Current }));

         //Assert
         display.Received(Called).ChargeComplete();
      }
   }
}

