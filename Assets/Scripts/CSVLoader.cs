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
                    .Skip(1); // 첫 번째 줄(헤더 행) 무시
                #region StringSplitOptions.RemoveEmptyEntries 
                // 내용이 없는 공백이 연속으로 있을 경우 Split에 담을 배열에서 제거하겠다는 뜻
                // \n과 \r 두 기준으로 문자열 분리 시,
                // 읽어오는 파일에 \r\n이 있는 경우 이 둘 사이의 빈 문자열이 생성될 수 있다.
                // https://uipath.tistory.com/98
                #endregion

                foreach (var line in lines)
                {
                    string[] fields = line.Split(','); // 한 행을 ,을 기준으로 셀로 나눔 

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
        #region Enemy.csv의 경우

        string sName = fields[(int)ENEMYFIELD.NAME];

        float fSpeed = ParseData<float>(fields[(int)ENEMYFIELD.SPEED], field => Convert.ToSingle(field));
        int iAtk = ParseData<int>(fields[(int)ENEMYFIELD.ATK], field => Convert.ToInt32(field));
        int iHp = ParseData<int>(fields[(int)ENEMYFIELD.HP], field => Convert.ToInt32(field));

        T data = (T)Activator.CreateInstance(typeof(T), sName, fSpeed, iAtk, iHp);
        #endregion

        #region Item.csv의 경우

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
