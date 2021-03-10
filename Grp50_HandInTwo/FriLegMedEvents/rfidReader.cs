using System;

namespace FriLegMedEvents
{


   public interface IReader
   {
      public void OnRfidRead(int id);
   }

   public class rfidReader : IReader
   {
      public void OnRfidRead(int id)
      {

      }
   }
}
