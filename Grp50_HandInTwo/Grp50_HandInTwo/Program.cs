using System;

namespace Ladeskab
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes

            HardwareSimulator hardwareSimulator = new HardwareSimulator();

            hardwareSimulator.StartProgram();
        }


        public class HardwareSimulator
        {
            private IDoor door;
            private IDisplay Display;
            private IReader RFID;
            private ILog log;
            private IUsbCharger USBcharger;
            private IChargeControl chargeControl;
            private StationControl stationControl;


            public HardwareSimulator()
            {
                door = new Door();
                Display = new Display();
                RFID = new rfidReader();
                USBcharger = new UsbChargerSimulator();
                chargeControl = new ChargeControl(Display, USBcharger);
                log = new Log_File();

                stationControl = new StationControl(door, Display, RFID, chargeControl, log);
            }


            public void StartProgram()
            {
                bool finish = false;

                do
                {
                    string input;

                    Display.ShowStartMenu();

                    var key = Console.ReadKey(true);

                    switch (key.KeyChar)
                    {
                        //case 'E':
                        //case 'e':
                        //    finish = true;
                        //    break;

                        case 'O':
                        case 'o':
                            door.OnDoorOpen();
                            DoorOpen();
                            break;

                        case 'C':
                        case 'c':
                            door.OnDoorClose();
                            DoorClosedWithPhoneConnected();
                            break;

                        //case 'R':
                        //case 'r':
                        //   Display.IndtastRFIDId();
                        //   string idString = System.Console.ReadLine();

                        //   int id = Convert.ToInt32(idString);
                        //   RFID.RfidRead(id);
                        //   break;


                        default:
                            break;
                    }

                } while (!finish);
            }


            private void DoorOpen()
            {
                Display.PhoneOptions();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'C':
                    case 'c':
                        USBcharger.Connected = true;
                        chargeControl.Connected = true;
                        PhoneConnected();
                        break;

                    case 'D':
                    case 'd':
                        USBcharger.Connected = false;
                        chargeControl.Connected = false;
                        break;
                }
            }

            private void DoorClosedWithPhoneConnected()
            {
                Display.PhoneCharging();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'O':
                    case 'o':
                        door.OnDoorOpen();
                        break;

                    case 'R':
                    case 'r':
                        Display.ReadRFIDId();
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        RFID.RfidRead(id);
                        break;
                }
            }

            private void PhoneConnected()
            {
                Display.PhoneConnectedMenu();

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'C':
                    case 'c':
                        door.OnDoorClose();
                        PhoneConnectedAndDoorClosed();
                      break;

                    case 'F':
                    case 'f':
                        USBcharger.Connected = false;
                        chargeControl.Connected = false;
                        break;
                }
            }

            private void PhoneConnectedAndDoorClosed()
            {
                Display.ShowStartMenu();
                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'R':
                    case 'r':
                        Display.ReadRFIDId();
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        RFID.RfidRead(id);
                        break;

                    case 'O':
                    case 'o':
                        door.OnDoorOpen();
                        break;
                }
            }

            // Forsæt herfra.

            public void RFIDUsedAgain()
            {

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'R':
                    case 'r':
                        Display.ReadRFIDId();
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        RFID.RfidRead(id);
                        break;
                }
            }

        }
    }
}
