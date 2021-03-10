public class rfidReader : IReader
{
    public void OnRfidRead(int id)
    {

    }
}

public interface IReader
{
    public void OnRfidRead(int id);
}