public class EnemyClasses
{
    /*
    plans:
    class EminemMelee ------------- add: tank/warrior/thief/berzzzzerk
    class EminemArtilery ---------- add: mobile artilery/static artilery
    class EminemSchoolShooter ----- add: sniper/shotgunner/rofls man
    */

    public class MeleeTank
    {
        public float health = 1000f;
        public float speed = 0.45f;
        public float damageAbs = 0.45f; //Множитель проходимого урона. При атаке оружием с уроном x пройдёт x * damageAbs единиц урона.
        public float range = 3f;
    }

    public class MeleeWarrior
    {
        public float health = 1000f;
        public float speed = 0.75f;
        public float damageAbs = 0.75f;
        public float range = 2f;
    }

    public class MeleeThief
    {
        public float health = 1000f;
        public float speed = 1f;
        public float damageAbs = 0.9f;
        public float range = 1.5f;
    }

    public class MeleeBerzerk       //???
    {
        public float health = 1000f;
        public float speed = 0.85f;
        public float damageAbs = 0.60f;
        public float range = 2.5f;
    }

    public class StaticArtillery
    {
        public float health = 1000f;
        public float speed = 0f;
        public float damageAbs = 1f;
        public float range = 15f;
    }

    public class MobileArtillery
    {
        public float health = 1000f;
        public float speed = 0.25f;
        public float damageAbs = 1f;
        public float range = 10f;
    }

    public class Sniper
    {
        public float health = 1000f;
        public float speed = 1f;
        public float damageAbs = 0.95f;
        public float range = 15f;
    }

    public class SBEUdrobovikZavod
    {
        public float health = 1000f;
        public float speed = 0.9f;
        public float damageAbs = 0.8f;
        public float range = 5f;
    }

    public class RifleMan
    {
        public float health = 1000f;
        public float speed = 0.75f;
        public float damageAbs = 0.70f;
        public float range = 10f;
    }
}
