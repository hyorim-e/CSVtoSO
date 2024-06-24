public class EnemyEnum : IEnum
{
    public const int ENEMY_TYPE_ZOMBIE1 = 101;
    public const int ENEMY_TYPE_ZOMBIE2 = 102;
    public const int ENEMY_TYPE_SKELETON1 = 103;
    public const int ENEMY_TYPE_SKELETON2 = 104;

    public enum ENEMYTYPE
    {
        ZOMBIE1 = ENEMY_TYPE_ZOMBIE1,
        ZOMBIE2 = ENEMY_TYPE_ZOMBIE2,
        SKELETON1 = ENEMY_TYPE_SKELETON1,
        SKELETON2 = ENEMY_TYPE_SKELETON2,
    }
}