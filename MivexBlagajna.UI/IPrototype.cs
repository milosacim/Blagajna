namespace MivexBlagajna.UI
{
    public interface IPrototype<T>
    {
        T Clone();

        T DeepClone();
    }
}
