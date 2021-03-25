using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Test_Program_Charge_Locker
{
    class DisplayTest
    {
        private IDoor _door;
        private IReader _reader;
        private IDisplay _uut;
        private ILog _log;
        private IChargeControl _chargeControl;
        private IUsbCharger _usbCharger;

        private IDisplay uut;

        private StationControl _stationControl;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            _uut = Substitute.For<IDisplay>();
            _reader = new rfidReader();
            _log = new Log_File();
            _door = new Door();

            _chargeControl = new ChargeControl(_uut, _usbCharger);

            _stationControl = new StationControl(_door, _uut, _reader, _chargeControl, _log);


            uut = new Display();
        }

        [Test]
        public void ConnectPhone_DoorOpenStatusTrue_Called()
        {
           //Arrange

           //Act
           _door.OnDoorOpen();

           //Assert

           _uut.Received(1).ConnectPhone();
        }

        [Test]
        public void ConnectPhone_DoorOpenStatusFalse_Called()
        {
           //Arrange

           //Act
           _door.OnDoorClose();

           //Assert
           _uut.Received(1).ReadRFID();
        }
        
        [Test]
        public void RemovePhone()
        {
           //Arrange
           var output = new StringWriter();
           Console.SetOut(output);

           //Act
           uut.RemovePhone();

           //Assert
           Assert.That(output.ToString(), Is.EqualTo("Tag din telefon ud af skabet og luk døren\r\n"));
        }

        [Test]
        public void NoConnection()
        {
           var output = new StringWriter();
           Console.SetOut(output);

           uut.NoConnection();

           Assert.That(output.ToString(), Is.EqualTo("Din telefon er ikke ordentlig tilsluttet. Prøv igen.\r\n"));
        }


   }
}
