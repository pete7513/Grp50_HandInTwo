using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab;
using NSubstitute;
using NuGet.Frameworks;
using NUnit;
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

        private StationControl _uut;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _reader = Substitute.For<IReader>();
            _display = new Display();
            _log = new Log_File();
            _uut = new StationControl(_door, _display, _reader, _log);

        }


        [TestCase(12)]

        public void RFIDRead_NewID_OldIDIsNewID(int id )
        {

            _reader.IDLoadedEvent += Raise.EventWith(new RfidIDEventArgs {RFIDID = id});
            Assert.That(_uut._oldId,Is.EqualTo(id));
        }
    }
}
