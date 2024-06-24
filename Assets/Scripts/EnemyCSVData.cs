using System;

[Serializable]
public class EnemyCSVData : CSVData
{
    public string name;
    public float speed;
    public int atk;
    public int hp;

    public EnemyCSVData(string sName, float fSpeed, int iAtk, int iHp)
    {
        this.name = sName;
        this.speed = fSpeed;
        this.atk = iAtk;
        this.hp = iHp;
    }
}
