// ������ �˻� �� ��� �������̽�
public interface IDataSearch<T>
{
    T FindData<U>(U searchData) where U : IEnum;
}