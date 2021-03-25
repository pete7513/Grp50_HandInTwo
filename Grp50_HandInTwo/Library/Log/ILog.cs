namespace Ladeskab
{
    public interface ILog
    {
        void LockwriteToFile(int id);

        void UnlockWriteToFile(int id);
    }
}