using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ladeskab
{
   public interface ILog
   {
      void LockwriteToFile(int id);

      void UnlockWriteToFile(int id);
   }

   public class Log_File : ILog
   {
      private string logFile = "logfile.txt"; // Navnet på systemets log-fil

      public void LockwriteToFile(int id)
      {
         using (var writer = File.AppendText(logFile))
         {
            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
         }
      }

      public void UnlockWriteToFile(int id)
      {
         using (var writer = File.AppendText(logFile))
         {
            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
         }
      }
   }
}
