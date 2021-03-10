public class Door :IDoor
{
    private bool statebool;
    private bool doorLocked;
    public void OnDoorOpen()
    {
        statebool = true;
    }

    public void OnDoorClose()
    {
        statebool = false;
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
}