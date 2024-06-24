// 데이터 검색 및 사용 인터페이스
public interface IDataSearch<T>
{
    T FindData<U>(U searchData) where U : IEnum;
}