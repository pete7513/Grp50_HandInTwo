
using System;
using System.Threading;

namespace Ladeskab
{
    public interface IDisplay
    {
        //void ConnectPhone();
        void ReadRFID();
        void RemovePhone();
        void WrongID();
        void UnlockWithID();
        void NoConnection();
        void CurrentPowerValue(CurrentEventArgs e);
        void ReadRFIDId();
        void PhoneOptions();
        void ShowStartMenu();
        void OpenWithRFIDTag();

        void PhoneCharging();

        void PhoneConnectedMenu();

    }

    public class Display : IDisplay
    {
        private int count = 0;

        //public void ConnectPhone()
        //{
        //   Console.WriteLine("Tilslut telefon");
        //}

        public void ReadRFID()
        {
            Console.WriteLine("Read RFID");
        }

        public void RemovePhone()
        {
            Console.WriteLine("Take your phone and close the door.");
        }

        public void WrongID()
        {
            Console.WriteLine("Wrong RFID tag");
        }

        public void UnlockWithID()
        {

            Console.WriteLine("The Mobile Charging Station is locked and you phone is charging." +
                              "\nUse your RFID to open.");
        }

        public void NoConnection()
        {
            Console.WriteLine("Your phone is not properly connected. Try again.");
        }

        public void CurrentPowerValue(CurrentEventArgs e)
        {
            Console.WriteLine("Current Power Value: " + e.Current);
            ++count;

            if (count == 10)
            {
                Console.Clear();
                OpenWithRFIDTag();
                //ShowMenu();
                count = 0;
            }

        }

        public void OpenWithRFIDTag()
        {
            Console.WriteLine("Open the Mobile Charging Station with the RFID tag\n [R] RFID reader");
        }

        public void ReadRFIDId()
        {
            Console.WriteLine("Enter RFID id:");
        }

        public void PhoneOptions()
        {
            Console.WriteLine("Door Open");
            Console.WriteLine(" [C]  Connect phone   \n" +
                            " [D]  Disconnect phone");
            Console.WriteLine(" ----------------------------------- ");

        }

        public void PhoneCharging()
        {
            Console.WriteLine(" [O]  Open door \n" +
                              " [R] RFID reader");
            Console.WriteLine(" ----------------------------------- ");
        }

        public void ShowStartMenu()
        {
            System.Console.WriteLine("Press O or R: ");

            Console.WriteLine(" [O]  Open door");
            Console.WriteLine(" [R]  RFID reader ");
            Console.WriteLine(" ----------------------------------- ");
        }

        public void PhoneConnectedMenu()
        {
            Console.WriteLine("Phone connected");
            Console.WriteLine(" [C]  Close door   \n" +
                              " [D]  Disconnect phone");
            Console.WriteLine(" ----------------------------------- ");
        }

    }
}