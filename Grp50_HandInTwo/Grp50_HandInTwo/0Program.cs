using System;

namespace Ladeskab
{
   class Program
   {
      private static IDoor door;
      private static IDisplay Display;
      private static IReader RFID;
      private static ILog log;
      private static IUsbCharger USBcharger;
      private static IChargeControl chargeControl;
      private static StationControl stationControl;

      static void Main(string[] args)
      {
         // Assemble your system here from all the classes
         door = new Door();
         Display = new Display();
         RFID = new rfidReader(); 
         USBcharger = new UsbChargerSimulator();
         chargeControl = new ChargeControl(Display, USBcharger);
         log = new Log_File();
         
         stationControl = new StationControl(door, Display, RFID, chargeControl, log);


         bool finish = false;
         do
         {
            string input;

            Display.ShowMenu(); 
            

            var key = Console.ReadKey(true);

            switch (key.KeyChar)
            {
               case 'E':
               case 'e':
                  finish = true;
                  break;

               case 'O':
               case 'o':
                  door.OnDoorOpen();
                  DoorOpen();
                  break;

               case 'C':
               case 'c':
                  door.OnDoorClose();
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

         } while (!finish);
      }

      private static void DoorOpen()
      {
         Display.PhoneOptions();
        
         var key = Console.ReadKey(true);

         switch (key.KeyChar)
         {
            case 'T':
            case 't':
               USBcharger.Connected = true;
               chargeControl.Connected = true;
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