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
   class TestStationControl2
   {

      //private FakeDoor fakeDoor;
      private IDoor _door;
      private IReader _reader;
      private IDisplay _display;
      private ILog _log;
      private IChargeControl _chargeControl;
      private IUsbCharger _usbCharger;

      private StationControl _uut;

      [SetUp]
      public void Setup()
      {
         _usbCharger = new UsbChargerSimulator();
         //fakeDoor = new FakeDoor();
         _door = Substitute.For<IDoor>();
         _reader = new rfidReader();
         _display = new Display();
         _log = new Log_File();
         _chargeControl = new ChargeControl(_display, _usbCharger);
         _uut = new StationControl(_door, _display, _reader, _chargeControl, _log);
      }

      [TestCase(123,false,0)]
      [TestCase(500,true,1)]
      [TestCase(646, false, 0)]
      [TestCase(100, true, 1)]
      public void RfidRead_DoorLockIsCall_(int id, bool ConnectedBool, int CalledTimes)
      
      {
         //Arrange
         _uut._state = StationControl.LadeskabState.Available;
         _chargeControl.Connected = ConnectedBool;
        
         //Act
         _reader.RfidRead(id);

         //Assert
         _door.Received(CalledTimes).LockDoor();
         //Assert.That(fakeDoor.IsActive_lockDoor, Is.True);
      }

      [TestCase(500, 499, true, 0)]
      [TestCase(123, 123, true, 1)]
      [TestCase(646, 646,true, 1)]
      [TestCase(100,199, true, 0)]
      public void RfidRead_DoorUnlockIsCall_(int id, int oldid, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _uut._state = StationControl.LadeskabState.Locked;
         _chargeControl.Connected = ConnectedBool;
         _uut._oldId = oldid;

         //Act
         _reader.RfidRead(id);

         //Assert
         _door.Received(CalledTimes).UnlockDoor();;
         //Assert.That(fakeDoor.IsActive_lockDoor, Is.True);
      }


      public void NewId_is_equal_To_OldID(int id)
      {
           _reader.RfidRead(id);
           


      }
    }


   public class FakeDoor: IDoor
   {
      public bool IsActive_UnlockDoor = false;
      public bool IsActive_lockDoor = false;

      public void OnDoorOpen()
      {
         throw new NotImplementedException();
      }

      public void OnDoorClose()
      {
         throw new NotImplementedException();
      }

      public void LockDoor()
      {
         IsActive_lockDoor = true;
      }

      public void UnlockDoor()
      {
         IsActive_UnlockDoor = true;
      }

      public event EventHandler<CurrentDoorStatusEventArgs> doorStatusEventHandler;



     

    }
}

