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
        void CurrentPowerValue(CurrentEventArgs e);
        void EnterRFIDId();
        void PhoneOptions();
        void ShowMenu();

        void PhoneCharging();

        void PhoneConnectedMenu();
    }
}