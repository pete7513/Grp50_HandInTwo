namespace Ladeskab
{
    public interface IChargeControl
    {
        void StartCharge();
        void StopCharge();
        bool Connected { get; set; }
    }
}