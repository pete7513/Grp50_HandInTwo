using System;

namespace Ladeskab
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes

            IDoor door = new Door();
            IDisplay Display = new Display();
            IReader RFID = new rfidReader();
            IUsbCharger charger = new UsbChargerSimulator();
            IChargeControl chargeControl = new ChargeControl(Display,charger);
            StationControl stationControl = new StationControl(door,Display, RFID, chargeControl);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.RfidRead(id);
                        break;


                    default:
                        break;
                }

            } while (!finish);
        }
    }
}