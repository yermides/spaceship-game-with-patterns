namespace Code.Ships.Common
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
        Teams Team { get; }
    }
}