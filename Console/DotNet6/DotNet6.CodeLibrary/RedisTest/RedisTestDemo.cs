using Newtonsoft.Json;
using StackExchange.Redis;

namespace DotNet6.CodeLibrary.RedisTest
{
    public class RedisTestDemo
    {
        public static async Task Run()
        {
            Console.WriteLine("运行情况可以使用Another Redis Desktop Manager查看");

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            var database = redis.GetDatabase();

            //字符串（strings）
            await StringTest(database);

            //散列（hashes）
            await HashSetTest(database);

            //列表(List)
            await ListTest(database);

            //集合(Set)
            await SetTest(database);

            //有序集合(sorted set)
            await SortSetTest(database);
        }

        private static async Task SortSetTest(IDatabase database)
        {
            var sortSetKey = "SortSetKey1";
            if (await database.KeyExistsAsync(sortSetKey))
                await database.KeyDeleteAsync(sortSetKey);

            await database.SortedSetAddAsync(sortSetKey, 111, 111);
            for (double i = 0; i < 10;)
            {
                await database.SortedSetAddAsync(sortSetKey, i.ToString(), i);
                i = i + (double)0.5;
            }

            var sortSetValues = await database.SortAsync(sortSetKey);
            Console.WriteLine($"{JsonConvert.SerializeObject(sortSetValues)}");


            if (await database.SortedSetAddAsync(sortSetKey, "999", 999))
                Console.WriteLine($"{sortSetKey}插入数据999(分数999)：成功");
            else
                Console.WriteLine($"{sortSetKey}插入数据999(分数999)：失败");
            //失败
            if (await database.SortedSetAddAsync(sortSetKey, "999", 999))
                Console.WriteLine($"{sortSetKey}插入数据999(分数999)：成功");
            else
                Console.WriteLine($"{sortSetKey}插入数据999(分数999)：失败");
            //成功 可以加入同分数不同值的数据
            if (await database.SortedSetAddAsync(sortSetKey, "888", 999))
                Console.WriteLine($"{sortSetKey}插入数据888(分数999)：成功");
            else
                Console.WriteLine($"{sortSetKey}插入数据888(分数999)：失败");
            //失败
            if (await database.SortedSetAddAsync(sortSetKey, "999", 888))
                Console.WriteLine($"{sortSetKey}插入数据999(分数888)：成功");
            else
                Console.WriteLine($"{sortSetKey}插入数据999(分数888)：失败");

            var length = await database.SortedSetLengthAsync(sortSetKey);

            //根据排名条件读取value
            var rankValues = await database.SortedSetRangeByRankAsync(sortSetKey, start: 0, stop: 10, order: Order.Descending);
            Console.WriteLine($"{JsonConvert.SerializeObject(rankValues)}");

            //条件同上，读取value+scroe
            var rankWithScoresValues = await database.SortedSetRangeByRankWithScoresAsync(sortSetKey, start: 0, stop: 10, order: Order.Descending);
            Console.WriteLine($"{JsonConvert.SerializeObject(rankWithScoresValues)}");

            //降序读取分数在0-10之间的数据value
            var scoreValues = await database.SortedSetRangeByScoreAsync(sortSetKey, start: 0, stop: 10, order: Order.Descending);
            Console.WriteLine($"{JsonConvert.SerializeObject(scoreValues)}");

            //条件同上，读取value+scroe
            var scoreWithScoresValues = database.SortedSetRangeByScoreWithScoresAsync(sortSetKey, start: 0, stop: 10, order: Order.Descending);
            Console.WriteLine($"{JsonConvert.SerializeObject(scoreWithScoresValues)}");

            //根据value筛选
            var rangeByValues = await database.SortedSetRangeByValueAsync(sortSetKey, 0, 998);
            Console.WriteLine($"{JsonConvert.SerializeObject(rangeByValues)}");

            //返回排名，默认从低到高，从0开始
            var rank1 = await database.SortedSetRankAsync(sortSetKey, "888");
            //不存在，返回null
            var rank2 = await database.SortedSetRankAsync(sortSetKey, "325435435");


            if (await database.SortedSetRemoveAsync(sortSetKey, "888"))
            {
                Console.WriteLine($"删除888:成功");
            }
            else
            {
                Console.WriteLine($"删除888:失败");
            }

            //var val = await database.SortedSetPopAsync(sortSetKey, Order.Ascending);

            //递增2
            var testMember = "testMember";
            for (int i = 0; i < 10; i++)
                await database.SortedSetIncrementAsync(sortSetKey, testMember, 2);

            for (int i = 0; i < 10; i++)
                await database.SortedSetDecrementAsync(sortSetKey, testMember, 3.6);
        }

        private static async Task SetTest(IDatabase database)
        {
            var setKey = "SetKey1";
            var setKey2 = "SetKey2";
            if (await database.KeyExistsAsync(setKey))
                await database.KeyDeleteAsync(setKey);
            if (await database.KeyExistsAsync(setKey2))
                await database.KeyDeleteAsync(setKey2);

            await database.SetAddAsync(setKey, "1.223");
            await database.SetAddAsync(setKey, "2");
            for (decimal i = 0; i < 10;)
            {
                await database.SetAddAsync(setKey, i.ToString());
                i = i + (decimal)0.5;
            }
            if (await database.SetContainsAsync(setKey, 1))
                Console.WriteLine($"{setKey}包含value：1");
            else
                Console.WriteLine($"{setKey}不包含value：1");
            if (await database.SetContainsAsync(setKey, "2"))
                Console.WriteLine($"{setKey}包含value：2");
            else
                Console.WriteLine($"{setKey}不包含value：2");

            var length = await database.SetLengthAsync(setKey);
            var values = await database.SetMembersAsync(setKey);
            Console.WriteLine($"{JsonConvert.SerializeObject(values)}");

            //取出数据，数据被移除
            var val = await database.SetPopAsync(setKey);

            //随机取出一个数据，数据不被移除
            var randomValue = await database.SetRandomMemberAsync(setKey);
            //随机取出n个数据，数据不被移除
            var randomValues = await database.SetRandomMembersAsync(setKey, 5);

            //set不允许插入相同数据
            if (await database.SetAddAsync(setKey, "999"))
                Console.WriteLine($"{setKey}插入数据999：成功");
            else
                Console.WriteLine($"{setKey}插入数据999：失败");
            if (await database.SetAddAsync(setKey, "999"))
                Console.WriteLine($"{setKey}插入数据999：成功");
            else
                Console.WriteLine($"{setKey}插入数据999：失败");

            var scan1Result = database.SetScan(setKey, "2", 100, 0, 0);
            foreach (var redisValue in scan1Result)
            {
                Console.WriteLine($"{redisValue}");
            }
            await database.SetRemoveAsync(setKey, "999");

            if (database.SetMove(setKey, setKey2, "2"))
                Console.WriteLine($"将2从{setKey}迁移到{setKey2}：成功(在{setKey}中2被移除)");
            else
                Console.WriteLine($"将2从{setKey}迁移到{setKey2}：失败");
        }

        private static async Task ListTest(IDatabase database)
        {
            //push插入数据
            var listKey = "ListKey1";
            if (await database.KeyExistsAsync(listKey))
                await database.KeyDeleteAsync(listKey);

            database.ListLeftPush(listKey, "Value1");
            database.ListLeftPush(listKey, 2);
            database.ListLeftPush(listKey, 3.33);

            //指定数据后插入数据
            await database.ListInsertAfterAsync(listKey, 2, 4);

            //指定数据前插入数据
            await database.ListInsertBeforeAsync(listKey, 2, 5);

            //可以从队列两边取出数据
            //取ID最小的值
            var leftPopValue = await database.ListLeftPopAsync(listKey);
            //取ID最大的值
            var rightPopValue = await database.ListRightPopAsync(listKey);

            //可以从队列两边插入数据
            //ID最小
            await database.ListLeftPushAsync(listKey, 6);
            await database.ListLeftPushAsync(listKey, 8);
            //ID最大
            await database.ListRightPushAsync(listKey, 7.77);
            //小数数字不精确
            var val_7_77 = await database.ListGetByIndexAsync(listKey, 5);
            await database.ListRightPushAsync(listKey, "9.99");


            //修改指定位置value
            Console.WriteLine($"{ await database.ListGetByIndexAsync(listKey, 1)}");
            database.ListSetByIndex(listKey, 1, 999);
            Console.WriteLine($"{ await database.ListGetByIndexAsync(listKey, 1)}");

            var values = database.ListRange(listKey, 1, 10);
            Console.WriteLine($"{JsonConvert.SerializeObject(values)}");

            var length = database.ListLengthAsync(listKey);

            //根据值移除数据
            var deleteCount1 = await database.ListRemoveAsync(listKey, 999);

            //根据位置移除数据
            await database.ListTrimAsync(listKey, 0, 3);

            //List允许加入重复值
            database.ListLeftPush(listKey, "Value1");
            database.ListLeftPush(listKey, "Value1");
            database.ListLeftPush(listKey, "Value1");
        }

        private static async Task HashSetTest(IDatabase database)
        {
            string usersInfoHash = "usersInfo";
            await database.HashSetAsync(usersInfoHash, new HashEntry[]
            {
                new HashEntry ("number","1"),
                new HashEntry ("name","lulu"),
                new HashEntry ("sex","woman")
            });
            if (await database.HashExistsAsync(usersInfoHash, "name"))
            {
                var count = database.HashLength(usersInfoHash);
                var nameValue = database.HashGet(usersInfoHash, "name");
                var keys = database.HashKeys(usersInfoHash);
                var values = await database.HashValuesAsync("usersInfo");

                await database.HashSetAsync(usersInfoHash, new HashEntry[] { new HashEntry("number", "2") });
                await database.HashSetAsync(usersInfoHash, new HashEntry[] { new HashEntry("age", "99") });
                count = database.HashLength(usersInfoHash);

                //key为HashIncrementTest的value，从1开始递增，调用一次HashIncrement+1
                var hashIncrementTestKey = "HashIncrementTest";
                for (int i = 0; i < 3; i++)
                    await database.HashIncrementAsync(usersInfoHash, hashIncrementTestKey);

                //每次调用递减1
                for (int i = 0; i < 5; i++)
                    await database.HashDecrementAsync(usersInfoHash, hashIncrementTestKey);

                if (await database.HashDeleteAsync(usersInfoHash, "name"))
                {
                    Console.WriteLine($"删除{usersInfoHash}中的name Field成功");
                }
                long deleteCount = await database.HashDeleteAsync(usersInfoHash, new RedisValue[] { "name", "number", "sex" });
                Console.WriteLine($"删除{usersInfoHash}中的name/number/sex Field中的{deleteCount}个");

            }

            //重命名
            if (await database.KeyExistsAsync(usersInfoHash))
                await database.KeyRenameAsync(usersInfoHash, $"rename_{usersInfoHash}");
        }

        private static async Task StringTest(IDatabase database)
        {
            var nameString = "name";
            if (await database.StringSetAsync(nameString, "lulu"))
            {
                var nameStringValue = await database.StringGetAsync(nameString);
                Console.WriteLine($"{nameString}的值为{nameStringValue}");

                //设置没有时间限制的key
                await database.KeyIdleTimeAsync(nameString);
                await CheckIfKeyExist(database, nameString);

                //设置5秒后超时的key
                await database.KeyExpireAsync(nameString, DateTime.Now.AddSeconds(5));
                await CheckIfKeyExist(database, nameString);

                await Task.Delay(5000);
                await CheckIfKeyExist(database, nameString);
            }

            var name_not_exist = await database.StringGetAsync("name_not_exist");
            if (name_not_exist.IsNull || name_not_exist.IsNullOrEmpty)
            {
                Console.WriteLine($"{nameof(name_not_exist)}为空");
            }
        }

        private static async Task CheckIfKeyExist(IDatabase database, string key)
        {
            if (await database.KeyExistsAsync(key))
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {key}存在");
            else
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {key}不存在");
        }
    }
}
