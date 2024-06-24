using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CSVtoSO<T> where T : ScriptableObject
{
    public Dictionary<EnemyEnum.ENEMYTYPE, Tuple<EnemyCSVData, GameObject>> enemyDataDictionary;
    private CSVLoader<EnemyCSVData> csvLoader = CSVLoader<EnemyCSVData>.Instance;

    public void GenerateSO<U, V>(Dictionary<U, V> dataDictionary, Func<Dictionary<U, V>, T> generateFunction, string sAssetName)

    {
        SaveAsset(generateFunction(dataDictionary), $"Assets/CSV/{sAssetName}.asset");
    }

    private void SaveAsset(T asset, string sPath)
    {
        AssetDatabase.CreateAsset(asset, sPath);
        AssetDatabase.SaveAssets();
    }

    public void LoadData(string sFilePath)
    {
        List<EnemyCSVData> dataList = csvLoader.LoadData(sFilePath); // CSV���� �����͸� �о�´�

        enemyDataDictionary = new Dictionary<EnemyEnum.ENEMYTYPE, Tuple<EnemyCSVData, GameObject>>(); // CSV���� �о�� �����͸� ������ ��ųʸ�

        // CSV���� �о�� ������ �׸� ���� üũ
        foreach (EnemyCSVData data in dataList)
        {
            // 1. �������� �̸��� Enum���� ��ȯ�Ǵ��� üũ
            EnemyEnum.ENEMYTYPE enemyType;
            try
            {
                enemyType = csvLoader.ParseData<EnemyEnum.ENEMYTYPE>(data.name.ToUpper(), data => Enum.Parse<EnemyEnum.ENEMYTYPE>(data));
            }
            catch
            {
                throw new Exception("cannot parse the data's name: " + data.name.ToUpper());
            }

            // 2. �������� �̸����� Addressable���� Prefab ��������
            try
            {

            }
            catch
            {
                throw new Exception("cannot load the addressable asset: " + data.name);
            }

            // 3. ������ CSV �����Ϳ� Prefab���� ������ Ʃ���� ��ųʸ��� Enum Ÿ���� Ű�� �Ͽ� ����
            try
            {
                enemyDataDictionary[enemyType] = new Tuple<EnemyCSVData, GameObject>(data, null);
            }
            catch
            {
                throw new InvalidOperationException("Cannot add the given tuple into enemyDataDictionary.");
            }

            try
            {

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        GenerateSO<EnemyEnum.ENEMYTYPE, Tuple<EnemyCSVData, GameObject>>(enemyDataDictionary, enemyDataDictionary => {
            EnemySO enemySO = ScriptableObject.CreateInstance<EnemySO>();
            foreach (KeyValuePair<EnemyEnum.ENEMYTYPE, Tuple<EnemyCSVData, GameObject>> enemy in enemyDataDictionary)
            {
                enemySO.enemyList.Add(enemy.Value.Item1);
            }
            return enemySO as T;
        }, "Enemy");
    }
}