
using System;

namespace Ladeskab
{

    public interface IDisplay
    {
        void ConnectPhone();
        void ReadRFID();
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
    }


}