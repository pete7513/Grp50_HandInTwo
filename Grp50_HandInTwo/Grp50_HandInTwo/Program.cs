namespace Ladeskab
{
   class Program
   {
      private static HardwareSimulator hardwareSimulator;
      
      static void Main(string[] args)
      {
         // Assemble your system here from all the classes
         hardwareSimulator = new HardwareSimulator();

         hardwareSimulator.StartProgram();
      }
   }
}
