using System;

namespace Ladeskab
{
    public interface IDoor
    {
        public void OnDoorOpen();
        public void OnDoorClose();
        public void LockDoor();
        public void UnlockDoor();

        public event EventHandler<CurrentDoorStatusEventArgs> doorStatusEventHandler;
    }
}