using System.Collections.Generic;

// ������ �δ� �������̽�
public interface IDataLoader<T>
{
    List<T> LoadData(string filePath);
}