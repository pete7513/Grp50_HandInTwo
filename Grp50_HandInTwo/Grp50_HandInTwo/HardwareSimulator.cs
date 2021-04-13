using System;

namespace Ladeskab
{
    public class HardwareSimulator
    {

        public IDoor Door;
        public IDisplay Display;
        public IReader RFID;
        public IUsbCharger USBcharger;
        public IChargeControl ChargeControl;

        private bool finish = false;

        public HardwareSimulator(IDoor door, IDisplay display, IReader rfid, IUsbCharger usBcharger, IChargeControl chargeControl)
        {
            Door = door;
            Display = display;
            RFID = rfid;
            USBcharger = usBcharger;
            ChargeControl = chargeControl;
        }

        public void StartProgram()
        {
            do
            {
                Display.ShowMenu();

                var key = Console.ReadKey(true);

                switchMethod(key);

            } while (!finish);
        }

        public void switchMethod(ConsoleKeyInfo key)
        {
            switch (key.KeyChar)
            {
                case 'E':
                case 'e':
                    finish = true;
                    break;

                case 'O':
                case 'o':
                    Door.OnDoorOpen();
                    DoorOpen();
                    break;

                case 'C':
                case 'c':
                    Door.OnDoorClose();
                    DoorClosedWithPhoneConnected();
                    break;

                case 'R':
                case 'r':
                    Display.EnterRFIDId();
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    RFID.RfidRead(id);
                    break;

                default:
                    break;
            }
        }

        private void DoorOpen()
        {
            Display.PhoneOptions();

            var key = Console.ReadKey(true);

            switch (key.KeyChar)
            {
                case 'T':
                case 't':
                    //USBcharger.Connected = true;
                    ChargeControl.Connected = true;
                    PhoneConnected();
                    break;

                case 'F':
                case 'f':
                    //USBcharger.Connected = false;
                    ChargeControl.Connected = false;
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
                    Door.OnDoorOpen();
                    break;

                case 'R':
                case 'r':
                    Display.EnterRFIDId();
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
                    Door.OnDoorClose();
                    break;

                case 'F':
                case 'f':
                    USBcharger.Connected = false;
                    ChargeControl.Connected = false;
                    break;
            }
        }
    }
}