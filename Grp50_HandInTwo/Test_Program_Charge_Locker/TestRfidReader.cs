using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Test_Program_Charge_Locker
{

    [TestFixture]
    class TestRfidReader
    {
        
        
        private RfidIDEventArgs _receivedEventArgs;
        private rfidReader _uut;

        [SetUp]
        public void Setup()
        {

            _receivedEventArgs = null;

            //fakeDoor = new FakeDoor();
            _uut = new rfidReader();

            _uut.IDLoadedEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };


           
            
        }



        [TestCase(12)]
        [TestCase(323)]
        [TestCase(233232)]
        public void Read_RFID_Is_RFIDID(int id)
        {
            _uut.RfidRead(id);

            Assert.That(_receivedEventArgs.RFIDID, Is.Not.Null);
        }



        [TestCase(45)]
        [TestCase(23)]
        [TestCase(12312)]
        public void RFIDID_Is_Set(int id)
        {
            _uut.RfidRead(id);

            Assert.That(_receivedEventArgs.RFIDID,Is.EqualTo(id));
        }
    }
}
