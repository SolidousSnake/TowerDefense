namespace _Project.Code.Gameplay.Unit
{
    public class PlayerHealth : Health
    {
        public void Initialize(float value)
        {
            SetMaxHealth(value);
            SetHealth(value);
        }
    }
}