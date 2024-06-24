using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static CSVFieldEnum;

public class CSVLoader<T> : IDataLoader<T>, IDataSearch<T> where T : CSVData
{
    private static CSVLoader<T> instance = null;
    public static CSVLoader<T> Instance => instance ?? (instance = new CSVLoader<T>());

    private List<T> dataList;

    public List<T> LoadData(string sFilePath)
    {
        dataList = new List<T>();

        try
        {
            using (StreamReader reader = new StreamReader(sFilePath))
            {
                var lines = reader
                    .ReadToEnd()
                    .Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1); // ù ��° ��(��� ��) ����
                #region StringSplitOptions.RemoveEmptyEntries 
                // ������ ���� ������ �������� ���� ��� Split�� ���� �迭���� �����ϰڴٴ� ��
                // \n�� \r �� �������� ���ڿ� �и� ��,
                // �о���� ���Ͽ� \r\n�� �ִ� ��� �� �� ������ �� ���ڿ��� ������ �� �ִ�.
                // https://uipath.tistory.com/98
                #endregion

                foreach (var line in lines)
                {
                    string[] fields = line.Split(','); // �� ���� ,�� �������� ���� ���� 

                    dataList.Add(CreateData(fields));
                }
            }
        }
        catch
        {

        }

        return dataList;
    }

    public T CreateData(string[] fields)
    {
        #region Enemy.csv�� ���

        string sName = fields[(int)ENEMYFIELD.NAME];

        float fSpeed = ParseData<float>(fields[(int)ENEMYFIELD.SPEED], field => Convert.ToSingle(field));
        int iAtk = ParseData<int>(fields[(int)ENEMYFIELD.ATK], field => Convert.ToInt32(field));
        int iHp = ParseData<int>(fields[(int)ENEMYFIELD.HP], field => Convert.ToInt32(field));

        T data = (T)Activator.CreateInstance(typeof(T), sName, fSpeed, iAtk, iHp);
        #endregion

        #region Item.csv�� ���

        #endregion

        return data;
    }

    public T FindData<U>(U searchData) where U : IEnum
    {
        return default(T);
    }

    public U ParseData<U>(string sData, Func<string, U> parseFunction)
    {
        try
        {
            return parseFunction(sData);
        }
        catch
        {
            throw new Exception($"Failed to parse '{sData}' to {typeof(U)}");
        }
    }
}
