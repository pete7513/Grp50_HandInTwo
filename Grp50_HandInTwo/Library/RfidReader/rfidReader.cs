using System;

namespace Ladeskab
{

    public class rfidReader : IReader
    {
        public event EventHandler<RfidIDEventArgs> IDLoadedEvent;


        public void RfidRead(int loadedID)
        {
            IDLoaded(new RfidIDEventArgs {RFIDID = loadedID});
        }

        protected virtual void IDLoaded(RfidIDEventArgs e)
        {
            IDLoadedEvent?.Invoke(this, e);
        }
    }


    public class RfidIDEventArgs : EventArgs
    {
        // Rfid ID
        public int RFIDID { set; get; }
    }
}