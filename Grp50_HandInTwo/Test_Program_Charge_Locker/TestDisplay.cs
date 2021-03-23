using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Test_Program_Charge_Locker
{
    class TestDisplay
    {

        //private FakeDoor fakeDoor;
        private IDoor _door;
        private IReader _reader;
        private IDisplay _display;
        private ILog _log;
        private IChargeControl _chargeControl;
        private IUsbCharger _usbCharger;

        private Display _uut;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            
            _door = Substitute.For<IDoor>();
            _reader = new rfidReader();
            _display = new Display();
            _log = new Log_File();
            _chargeControl = new ChargeControl(_display, _usbCharger);
            _uut = new Display();
        }




    }
}
