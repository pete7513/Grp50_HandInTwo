using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace UsbSimulator.Test
{

   [TestFixture]
   class HardwareSimulatorTest
   {
      private HardwareSimulator hardwareSimulator;
      private IDoor doorSub;

      [SetUp]
      public void Setup()
      {
         hardwareSimulator = new HardwareSimulator();
         hardwareSimulator.door = Substitute.For<IDoor>();
      }


      [Test]
      public void RFIDRead_NewID_OldIDIsNewID()
      {
         //Arrange
         ConsoleKeyInfo key = new ConsoleKeyInfo('O', ConsoleKey.O, false, false, false);

         //Act
         hardwareSimulator.switchMethod(key);

         //Assert
         doorSub.Received(1).OnDoorOpen();
      }
   }
}
