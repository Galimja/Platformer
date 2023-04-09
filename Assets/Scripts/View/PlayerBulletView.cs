
namespace PlatformerMVC
{
    public class PlayerBulletView : LevelObjectView
    {
        private int _damagePoint = 20;

        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }
    }
}