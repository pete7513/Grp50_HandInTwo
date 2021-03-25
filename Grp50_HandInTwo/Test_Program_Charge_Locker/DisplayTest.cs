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


   }
}
