
using System;

namespace Ladeskab
{
   public interface IDisplay
    {
        void ConnectPhone();
        void ReadRFID();
        void RemovePhone();
        void WrongID();

        void UnlockWithID();

        void NoConnection();
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

        public void UnlockWithID()
        {

            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void NoConnection()
        {
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }
    }


}