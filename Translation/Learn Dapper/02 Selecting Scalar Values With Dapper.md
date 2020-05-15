Dapper提供了两种选择标量（单个）值以及它们的异步方法。
| 方法 | 描述 |
| ------ | ------ |
| `ExecuteScalar` | 返回`动态(dynamic)`类型 | 
| `ExecuteScalar<T>` | 返回T类型参数指定的类型的实例 |
| `ExecuteScalarAsync` | 异步返回`动态(dynamic)`类型 |
| `ExecuteScalarAsync<T>` | 异步返回T类型参数指定的类型的实例 | 
# ExecuteScalar 方法
```C#
using (var connection = new SQLiteConnection(connString))
{
    var sql = "select count(*) from products";
    var count = connection.ExecuteScalar(sql);
    Console.WriteLine($"Total products: {count}");
}
```
如果要对返回的值执行任何操作，则需要将其 强制转换 或 转换 为期望的类型。 或者，使用带有通用参数的`ExecuteScalar`版本，并显式指定返回类型：
```c#
using (var connection = new SQLiteConnection(connString))
{
    var sql = "select count(*) from products";
    var count = connection.ExecuteScalar<int>(sql);
    Console.WriteLine($"Total products: {count}");
}
```
# 异步操作
```C#
using (var connection = new SQLiteConnection(connString))
{
    var sql = "select count(*) from products";
    var count = await connection.ExecuteScalarAsync(sql);
    Console.WriteLine($"Total products: {count}");
}
```
```C#
using (var connection = new SQLiteConnection(connString))
{
    var sql = "select count(*) from products";
    var count = await connection.ExecuteScalarAsync<int>(sql);
    Console.WriteLine($"Total products: {count}");
}
```
