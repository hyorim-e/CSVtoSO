using UnityEditor;

public class SOGenerator
{
    public const string sCSVFilePath = "Assets/CSV/";
    public static readonly string[] sarrCSVFileNameList = { "Enemy", };
    private static CSVtoSO<EnemySO> csvToSO = new CSVtoSO<EnemySO>();

    [MenuItem("Custom Tools/Generate Asset")]
    public static void GenerateAsset()
    {
        //LoadData("Assets/CSV/Enemy.csv");
        foreach (string CSVFileName in sarrCSVFileNameList)
        {
            csvToSO.LoadData(sCSVFilePath + CSVFileName + ".csv");
        }
    }
}
