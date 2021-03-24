using System;

namespace Ladeskab
{
   public class HardwareSimulator
   {
      public IDoor door;
      public IDisplay Display;
      public IReader RFID;
      public ILog log;
      public IUsbCharger USBcharger;
      public IChargeControl chargeControl;
      public StationControl stationControl;

      private bool finish = false; 


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
         do
         {
            string input;

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
               door.OnDoorOpen();
               DoorOpen(); //evt. en fake klasse her ift. test 
               break;

            case 'C':
            case 'c':
               door.OnDoorClose();
               DoorClosedWithPhoneConnected();
               break;

            case 'R':
            case 'r':
               Display.IndtastRFIDId();
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
               USBcharger.Connected = true;
               chargeControl.Connected = true;
               PhoneConnected();
               break;

            case 'F':
            case 'f':
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
               Display.IndtastRFIDId();
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
               break;

            case 'F':
            case 'f':
               USBcharger.Connected = false;
               chargeControl.Connected = false;
               break;
         }
      }

   }
}