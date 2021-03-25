using System;

namespace Ladeskab
{
    public interface IReader
    {
        public event EventHandler<RfidIDEventArgs> IDLoadedEvent;
        public void RfidRead(int id);
    }
}