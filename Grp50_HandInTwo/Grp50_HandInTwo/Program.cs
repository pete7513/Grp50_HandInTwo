using System;

namespace Ladeskab
{
   class Program
   {
      private static IDoor door;
      private static IDisplay Display;
      private static IReader RFID;
      private static IUsbCharger charger;
      private static IChargeControl chargeControl;
      private static ILog log;
      private static StationControl stationControl;

      static void Main(string[] args)
      {
         // Assemble your system here from all the classes
         door = new Door();
         Display = new Display();
         RFID = new rfidReader(); 
         charger = new UsbChargerSimulator();
         chargeControl = new ChargeControl(Display, charger);
         log = new Log_File();
         stationControl = new StationControl(door, Display, RFID, chargeControl, log);


         bool finish = false;
         do
         {
            string input;
            System.Console.WriteLine("Indtast E, O, C, R: ");


            Console.WriteLine(" [E]  Exit");
            Console.WriteLine(" [O]  Open door");
            Console.WriteLine(" [C]  Close door   \n" +
                              " [R]  RFID reader ");
            Console.WriteLine(" ----------------------------------- ");

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
                  System.Console.WriteLine("Indtast RFID id: ");
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
         Console.WriteLine(" [T]  Tilslut telefon   \n" +
                           " [F]  Frakoble telefon");
         Console.WriteLine(" ----------------------------------- ");

         var key = Console.ReadKey(true);

         switch (key.KeyChar)
         {
            case 'T':
            case 't':
               charger.Connected = true;
               break;

            case 'F':
            case 'f':
               charger.Connected = false;
               break;
         }
      }
   }
}