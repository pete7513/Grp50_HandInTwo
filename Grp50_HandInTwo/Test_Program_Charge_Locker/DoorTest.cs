using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Test_Program_Charge_Locker
{
   [TestFixture]
   class DoorTest
   {
      //private FakeDoor fakeDoor;
      private Door uut;
      private CurrentDoorStatusEventArgs _receivedEventArgs;


      [SetUp]
      public void Setup()
      {
         _receivedEventArgs = null;

         //fakeDoor = new FakeDoor();
         uut = new Door();

         uut.doorStatusEventHandler += (o, args) =>
         {
            _receivedEventArgs = args;
         };
      }

      [Test]
      public void UnlockDoor_DoorlockSetToFalse_flase()
      {
         //Arrange

         //Act
         uut.UnlockDoor();

         //Assert
         Assert.That(uut.doorLocked, Is.False);
      }

      [Test]
      public void lockDoor_DoorlockSetToTrue_True()
      {
         //Arrange

         //Act
         uut.LockDoor();

         //Assert
         Assert.That(uut.doorLocked, Is.True);
      }

      [Test]
      public void SetupDoorlocked_IsFalse_False()
      {
         //Arrange

         //Act

         //Assert
         Assert.That(uut.doorLocked, Is.False);
      }

      [Test]
      public void OpenedDoor_IsDoorOpenIsTrue_True()
      {
         //Act
         uut.OnDoorOpen();

         //Assert
         Assert.That(_receivedEventArgs.IsDoorOpen_Status,Is.True);
      }

      [Test]
      public void OpenedDoor_EventFired_Fired()
      {
         //Act
         uut.OnDoorOpen();

         //Assert
         Assert.That(_receivedEventArgs.IsDoorOpen_Status, Is.Not.Null);
      }

      [Test]
      public void OnDoorClose_IsDoorOpenIsFalse_False()
      {
         //Act
         uut.OnDoorClose();

         //Assert
         Assert.That(_receivedEventArgs.IsDoorOpen_Status, Is.False);
      }

      [Test]
      public void OnDoorClose_EventFired_Fired()
      {
         //Act
         uut.OnDoorClose();

         //Assert
         Assert.That(_receivedEventArgs.IsDoorOpen_Status, Is.Not.Null);
      }



   }
}

