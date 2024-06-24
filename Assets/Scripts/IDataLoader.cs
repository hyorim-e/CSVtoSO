using System.Collections.Generic;

// 데이터 로더 인터페이스
public interface IDataLoader<T>
{
    List<T> LoadData(string filePath);
}