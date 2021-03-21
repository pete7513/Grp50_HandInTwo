using System;

namespace Ladeskab
{
    public class CurrentDoorStatusEventArgs : EventArgs
    {
        // Status bool for door
        public bool IsDoorOpen_Status { set; get; }
    }

    public class Door : IDoor
    {
       public bool statebool;
       public bool doorLocked = false;

        public event EventHandler<CurrentDoorStatusEventArgs> doorStatusEventHandler;

        public void OnDoorOpen()
        {
            statebool = true;
            doorStatusEventHandler?.Invoke(this, new CurrentDoorStatusEventArgs {IsDoorOpen_Status = true});
        }

        public void OnDoorClose()
        {
            statebool = false;
            doorStatusEventHandler?.Invoke(this, new CurrentDoorStatusEventArgs { IsDoorOpen_Status = false });
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