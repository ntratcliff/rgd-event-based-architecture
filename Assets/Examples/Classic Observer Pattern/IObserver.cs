namespace Examples.ClassicObserver
{
    public interface IObserver
    {
        void OnNotify(IEvent e);
    }
}
