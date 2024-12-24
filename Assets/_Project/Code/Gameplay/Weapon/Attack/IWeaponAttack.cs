namespace _Project.Code.Gameplay.Weapon.Attack
{
    public interface IWeaponAttack
    {
        public void Attack();
        public void SetDamage(float damage);
        public void SetRange(float configRange);
        public void SetPenetrationDepth(int value);
    }
}