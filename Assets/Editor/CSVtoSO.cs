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
        List<EnemyCSVData> dataList = csvLoader.LoadData(sFilePath); // CSV에서 데이터를 읽어온다

        enemyDataDictionary = new Dictionary<EnemyEnum.ENEMYTYPE, Tuple<EnemyCSVData, GameObject>>(); // CSV에서 읽어온 데이터를 저장할 딕셔너리

        // CSV에서 읽어온 데이터 항목 별로 체크
        foreach (EnemyCSVData data in dataList)
        {
            // 1. 데이터의 이름이 Enum으로 변환되는지 체크
            EnemyEnum.ENEMYTYPE enemyType;
            try
            {
                enemyType = csvLoader.ParseData<EnemyEnum.ENEMYTYPE>(data.name.ToUpper(), data => Enum.Parse<EnemyEnum.ENEMYTYPE>(data));
            }
            catch
            {
                throw new Exception("cannot parse the data's name: " + data.name.ToUpper());
            }

            // 2. 데이터의 이름으로 Addressable에서 Prefab 가져오기
            try
            {

            }
            catch
            {
                throw new Exception("cannot load the addressable asset: " + data.name);
            }

            // 3. 가져온 CSV 데이터와 Prefab으로 구성한 튜플을 딕셔너리에 Enum 타입을 키로 하여 저장
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