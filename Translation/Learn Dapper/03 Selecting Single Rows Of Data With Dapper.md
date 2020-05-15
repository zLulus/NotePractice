# 使用Dapper查询单行数据
Dapper提供了多种查询单行数据的方法，具体的选择取决于您要如何使用检索到的数据。   
| 方法 | 描述 | 异常内容 |
| ------ | ------ | ------ |
| QuerySingle | 仅在预期返回一行时使用。 返回`动态(dynamic )`类型 | `InvalidOperationException`，当查询返回零个或多个元素时 |
| QuerySingle<T> | 仅在预期返回一行时使用。 返回指定类型`T`类型的实例 | `InvalidOperationException`，当查询返回零个或多个元素时 |
| QuerySingleOrDefault | 当期望返回零或一行时使用。 返回`动态(dynamic )`类型或`null` | `InvalidOperationException`，当查询返回多个元素时 |
| QuerySingleOrDefault<T> | 当期望返回零或一行时使用。 返回指定类型`T`类型的实例，或者返回null | `InvalidOperationException`，当查询返回多个元素时 |
| QueryFirst | 以`动态(dynamic )`类型返回一个或多个行的第一行 | `InvalidOperationException`，当查询返回零个元素时 |
| QueryFirst<T> | 返回一个或多个行中的第一行作为指定类型`T`类型的实例 | `InvalidOperationException`，当查询返回零个元素时 |
| QueryFirstOrDefault | 以`动态(dynamic )`类型返回一个或多个行的第一行，如果没有返回结果，则返回`null` |  |
| QueryFirstOrDefault<T> | 返回一个或多个行的第一行作为指定类型`T`类型的实例；如果不返回任何结果，则返回`null` |  | 
