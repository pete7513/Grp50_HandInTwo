
using System;

namespace Ladeskab
{
   public interface IDisplay
    {
        void ConnectPhone();
        void ReadRFID();
        void RemovePhone();
        void WrongID();
    }

    public class Display: IDisplay
    {
        public void ConnectPhone()
        {
            Console.WriteLine("Tilslut telefon");
        }

        public void ReadRFID()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void RemovePhone()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        public void WrongID()
        {
            Console.WriteLine("Forkert RFID tag");
        }
    }


}