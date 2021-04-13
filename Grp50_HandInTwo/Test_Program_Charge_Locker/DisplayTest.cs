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
       private IDisplay uut;

        [SetUp]
        public void Setup()
        {
           uut = new Display();
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

        [Test]
        public void ConnectPhone()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.ConnectPhone();

            Assert.That(output.ToString(), Is.EqualTo("Tilslut telefon\r\n"));
        }

        [Test]
        public void ReadRFID()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.ReadRFID();

            Assert.That(output.ToString(), Is.EqualTo("Indlæs RFID\r\n"));
        }

        [Test]
        public void WrongID()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.WrongID();

            Assert.That(output.ToString(), Is.EqualTo("Forkert RFID tag\r\n"));
        }

        [Test]
        public void UnlockWithID()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.UnlockWithID();

            Assert.That(output.ToString(), Is.EqualTo("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.\r\n"));
        }

        [Test]
        public void CurrentPowerValue()
        {
            CurrentEventArgs eventArgs = new CurrentEventArgs() {Current = 8};
            
            var output = new StringWriter();
            Console.SetOut(output);

            uut.CurrentPowerValue(eventArgs);

            Assert.That(output.ToString(), Is.EqualTo("Strøm niveauet er: " + eventArgs.Current+"\r\n"));
        }

        [Test]
        public void EnterRFIDId()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.EnterRFIDId();

            Assert.That(output.ToString(), Is.EqualTo("Indtast RFID id: \r\n"));
        }

        [Test]
        public void PhoneOptions()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.PhoneOptions();

            Assert.That(output.ToString(), Is.EqualTo(" [T]  Tilslut telefon   \n" +
                                                      " [F]  Frakoble telefo +\n" +
                                                      "----------------------------------- \r\n"));
        }

        [Test]
        public void PhoneCharging()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.PhoneCharging();

            Assert.That(output.ToString(), Is.EqualTo(" [E]  Exit   \n" +
                                                       " [O]  Åben dør \n" +
                                                       " [R] RFID læser \n" +
                                                       " ----------------------------------- \r\n"));
        }

        [Test]
        public void ShowMenu()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.ShowMenu();

            Assert.That(output.ToString(), Is.EqualTo("Indtast E, O, C, R: \r\n" +
                                                      " [E]  Exit\r\n" +
                                                      " [O]  Åben dør\r\n" +
                                                      " [C]  Luk dør   \n" +
                                                      " [R]  RFID læser \r\n" +
                                                      " ----------------------------------- \r\n"));


        }

        [Test]
        public void PhoneConnectedMenu()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.PhoneConnectedMenu();

            Assert.That(output.ToString(), Is.EqualTo(" [C]  Luk dør   \n" +
                                                      " [F]  Frakoble telefon\r\n" +
                                                      " ----------------------------------- \r\n"));
        }

        [Test]
        public void ChargeComplete()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.ChargeComplete();

            Assert.That(output.ToString(), Is.EqualTo("Opladningen er fuldendt og ladningen kan derved stoppes:   "));
        }

        [Test]
        public void Charging()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.Charging();

            Assert.That(output.ToString(), Is.EqualTo("Ladningen er igang:"));
        }

        [Test]
        public void ChargingError()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            uut.ChargingError();

            Assert.That(output.ToString(), Is.EqualTo("!!!ERROR!!! Frakobel din telefon hurtigst:   "));
        }







    }
}
