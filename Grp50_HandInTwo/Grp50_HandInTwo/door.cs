using System;

namespace Ladeskab
{
    public class CurrentDoorStatusEventArgs : EventArgs
    {
        // Status bool for door
        public bool doorStatus { set; get; }
    }

    public class Door : IDoor
    {
        private bool statebool;
        private bool doorLocked;

        public event EventHandler<CurrentDoorStatusEventArgs> doorStatusEventHandler;

        public void OnDoorOpen()
        {
            statebool = true;
            doorStatusEventHandler?.Invoke(this, new CurrentDoorStatusEventArgs {doorStatus = true});
        }

        public void OnDoorClose()
        {
            statebool = false;
            doorStatusEventHandler?.Invoke(this, new CurrentDoorStatusEventArgs { doorStatus = false });
        }

        public void LockDoor()
        {
            doorLocked = true;
        }

        public void UnlockDoor()
        {
            doorLocked = false;
        }

        
    }

    public interface IDoor
    {
        public void OnDoorOpen();
        public void OnDoorClose();
        public void LockDoor();
        public void UnlockDoor();

        public event EventHandler<CurrentDoorStatusEventArgs> doorStatusEventHandler;
    }
}