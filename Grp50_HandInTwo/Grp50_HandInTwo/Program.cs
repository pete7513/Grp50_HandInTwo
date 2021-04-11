namespace Ladeskab
{
   class Program
   {
      private static HardwareSimulator hardwareSimulator;

      public static IDoor door;
      public static IDisplay Display;
      public static IReader RFID;
      public static ILog log;
      public static IUsbCharger USBcharger;
      public static IChargeControl chargeControl;
      public static StationControl stationControl;

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



         hardwareSimulator = new HardwareSimulator(door,Display,RFID,USBcharger,chargeControl);

         hardwareSimulator.StartProgram();
      }
   }
}
