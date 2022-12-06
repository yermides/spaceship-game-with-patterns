namespace Code.Ships
{
    public interface IShip
    {
        void OnDamageReceived(bool hasDied);

        // void OnFiring()
        // void OnMove()
        // etc
    }

    // public interface IShipConfigurable 
    // {
    //     void Configure(IShip ship);
    // }
}