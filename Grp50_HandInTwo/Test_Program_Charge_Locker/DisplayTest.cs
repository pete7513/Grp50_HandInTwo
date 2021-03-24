using System;
using System.Collections.Generic;
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

        private StationControl _stationControl;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            _uut = Substitute.For<IDisplay>();
            _reader = new rfidReader();
            _uut = new Display();
            _log = new Log_File();
            _chargeControl = new ChargeControl(_uut, _usbCharger);
            _stationControl = new StationControl(_door, _uut, _reader, _chargeControl, _log);
        }

        [Test]
        public void 
    }
}
