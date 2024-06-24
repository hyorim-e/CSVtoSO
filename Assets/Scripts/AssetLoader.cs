using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class AssetLoader<T> where T : UnityEngine.Object
{
    private static AssetLoader<T> instance = null;
    public static AssetLoader<T> Instance => instance ?? (instance = new AssetLoader<T>());

    private Predicate<AsyncOperationStatus> isSucceeded = (status) => AsyncOperationStatus.Succeeded == status;

    public async Task<T> LoadAddressableAssetAsync(string assetName)
    {
        T asset;

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetName);
        await handle.Task;  // �񵿱� �۾��� �Ϸ�� ������ ��ٸ�

        try
        {
            asset = isSucceeded(handle.Status) ? (T)handle.Result : default(T);
        }
        catch
        {
            throw new Exception($"Failed to load asset with name: {assetName}");
        }

        return asset;
    }

    public async Task<List<T>> LoadAddressableAssetsAsync(string label)
    {
        List<T> loadedAssetList = new List<T>();

        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);
        await handle.Task;  // �񵿱� �۾��� �Ϸ�� ������ ��ٸ�

        try
        {
            Action<Predicate<AsyncOperationStatus>> handleLoadedAssets =
                (isSucceeded) =>
                loadedAssetList.AddRange(isSucceeded(handle.Status) ?
                handle.Result : throw new Exception($"AsyncOperationStatus is not equal to Succeeded."));

            handleLoadedAssets(isSucceeded);
        }
        catch
        {
            throw new Exception($"Failed to load assets with label: {label}");
        }

        return loadedAssetList;
    }
}
