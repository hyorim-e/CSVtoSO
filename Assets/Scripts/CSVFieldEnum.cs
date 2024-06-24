public class CSVFieldEnum : IEnum
{
    public const int ENEMY_FIELD_NAME = 0;
    public const int ENEMY_FIELD_SPEED = 1;
    public const int ENEMY_FIELD_ATK = 2;
    public const int ENEMY_FIELD_HP = 3;

    public enum ENEMYFIELD
    {
        NAME = ENEMY_FIELD_NAME,
        SPEED = ENEMY_FIELD_SPEED,
        ATK = ENEMY_FIELD_ATK,
        HP = ENEMY_FIELD_HP
    }
}
