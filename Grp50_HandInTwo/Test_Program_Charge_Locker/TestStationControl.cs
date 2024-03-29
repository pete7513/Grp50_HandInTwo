﻿using System;
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
         _usbCharger = Substitute.For<IUsbCharger>();
         _door = Substitute.For<IDoor>();
         _reader = Substitute.For<IReader>();
         _display = Substitute.For<IDisplay>();
         _log = Substitute.For<ILog>();
         _chargeControl = Substitute.For<IChargeControl>();

         _uut = new StationControl(_door, _display, _reader, _chargeControl, _log);
      }


      [TestCase(123, false, 0)]
      [TestCase(500, true, 1)]
      [TestCase(646, false, 0)]
      [TestCase(100, true, 1)]
      public void RfidRead_DoorLockIsCall_(int id, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _door.Received(CalledTimes).LockDoor();
      }

      [TestCase(500, 499, true, 0)]
      [TestCase(123, 123, true, 1)]
      [TestCase(646, 646, true, 1)]
      [TestCase(100, 199, true, 0)]
      public void RfidRead_DoorUnlockIsCall_(int id, int oldid, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         //First read and lock
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = oldid });

         //Second read and open 
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _door.Received(CalledTimes).UnlockDoor();
      }

      [TestCase(500, false, 0)]
      [TestCase(123, true, 1)]
      [TestCase(646, true, 1)]
      [TestCase(100, true, 1)]
      public void RfidDetected_IfIdDetectedAndLadeSkabAvaliableLogWiteToFileIsCalled_LogfileIsCalled(int id, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _log.Received(CalledTimes).LockwriteToFile(id);
      }

      [TestCase(500,  false, 0)]
      [TestCase(123, true, 1)]
      [TestCase(646, true, 1)]
      [TestCase(100, true, 1)]
      public void RfidDetected_IfIdDetectedAndLadeSkabAvaliableDisplayUnklockWothID_DisplayIsCalled(int id, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _display.Received(CalledTimes).UnlockWithID();
      }

      [TestCase(500, false, 0)]
      [TestCase(123, true, 1)]
      [TestCase(646, true, 1)]
      [TestCase(100, true, 1)]
      public void RfidDetected_IfIdDetectedAndLadeSkabAvaliableChargeControlStartCharge_ChargeControlIsCalled(int id, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _chargeControl.Received(CalledTimes).StartCharge();
      }

      [TestCase(500, false, 1)]
      [TestCase(123, false, 1)]
      [TestCase(646, false, 1)]
      [TestCase(100, true, 0)]
      public void RfidDetected_IfIdDetectedAndLadeSkabAvaliableButConnectedIsFalse_DisplayIsCalled(int id, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _display.Received(CalledTimes).NoConnection();
      }



      [TestCase(500, 499, true, 0)]
      [TestCase(123, 123, true, 1)]
      [TestCase(646, 646, true, 1)]
      [TestCase(100, 199, true, 0)]
      public void RfidDetected_IfIdDetectedAndLadeSkabLockedDisplayRemovePhone_DisplayIsCalled(int id, int oldid, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = oldid });

         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _display.Received(CalledTimes).RemovePhone();
      }

      [TestCase(500, 499, true, 0)]
      [TestCase(123, 123, true, 1)]
      [TestCase(646, 646, true, 1)]
      [TestCase(100, 199, true, 0)]
      public void RfidDetected_IfIdDetectedAndLadeSkabLockedlogUnlockWriteToFile_LogIsCalled(int id, int oldid, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;
         
         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = oldid });

         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _log.Received(CalledTimes).UnlockWriteToFile(id);
      }


      [TestCase(500, 499, true, 0)]
      [TestCase(123, 123, true, 1)]
      [TestCase(646, 646, true, 1)]
      [TestCase(100, 199, true, 0)]
      public void RfidDetected_IfIdDetectedAndLadeSkabLockedChargeControlStopCharge_ChargeControIsCalled(int id, int oldid, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = oldid });

         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _chargeControl.Received(CalledTimes).StopCharge();
      }

      [TestCase(500, 499, true, 1)]
      [TestCase(123, 123, true, 0)]
      [TestCase(646, 646, true, 0)]
      [TestCase(100, 199, true, 1)]
      public void RfidDetected_IfWrongIdDetectedAndLadeSkabLockedDisplayWrongID_DisplayIsCalled(int id, int oldid, bool ConnectedBool, int CalledTimes)
      {
         //Arrange
         _chargeControl.Connected = ConnectedBool;

         //Act
         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = oldid });

         _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs() { RFIDID = id });

         //Assert
         _display.Received(CalledTimes).WrongID();
      }

      [TestCase(true)]
      public void _door_doorStatusEventHandler_IfDoorOpenedCallsConnectPhone_DisplayIsCalled(bool DoorStatus)
      {
            //Arrange

            //Act
            _door.doorStatusEventHandler += Raise.EventWith((new CurrentDoorStatusEventArgs() {IsDoorOpen_Status = DoorStatus}));

            //Assert
            _display.Received().ConnectPhone();
      }

      [TestCase(false)]
      public void _door_doorStatusEventHandler_IfDoorOpenedCallsConnectPhone_DisplayIsNotCalled(bool DoorStatus)
      {
          //Arrange

          //Act
          _door.doorStatusEventHandler += Raise.EventWith((new CurrentDoorStatusEventArgs() { IsDoorOpen_Status = DoorStatus }));

          //Assert
          _display.Received().ReadRFID();
      }
   }
}

