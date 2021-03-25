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
   }
}

