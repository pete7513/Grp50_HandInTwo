using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Test_Program_Charge_Locker
{

    [TestFixture]
    class TestStationControl
    {
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
            _door = Substitute.For<IDoor>();
            _reader = Substitute.For<IReader>();
            _display = new Display();
            _log = new Log_File();
            _chargeControl = new ChargeControl(_display,_usbCharger);
            _uut = new StationControl(_door, _display, _reader,_chargeControl, _log);
        }


        [TestCase(12)]
        [TestCase(500)]
        [TestCase(-20)]

        public void RFIDRead_NewID_OldIDIsNewID(int id)
        {
            _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs {RFIDID = id});
            Assert.That(_uut._oldId,Is.EqualTo(id));
        }
    }
}
