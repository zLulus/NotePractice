using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TestSqliteScript : MonoBehaviour
{
    string databasePath;
    // Start is called before the first frame update
    void Start()
    {
        databasePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Databases\\TestDatabse.db";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateTable()
    {
        using (var db = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite))
        {
            var count = db.CreateTable<UserInfo>();
            count = db.CreateTable<RandomEvent>();
        }

    }

    public void InsertTableData()
    {
        using (var db = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite))
        {
            var count = db.Insert(new UserInfo()
            {
                Name = "test1Name",
                Sex = SexEnum.Woman,
                Birthday = new DateTime(1990, 10, 2)
            }, typeof(UserInfo));
            List<UserInfo> users = new List<UserInfo>();
            users.Add(new UserInfo()
            {
                Name = "test2Name",
                Sex = SexEnum.Man,
                Birthday = new DateTime(2000, 10, 2)
            });
            users.Add(new UserInfo()
            {
                Name = "test3Name",
                Sex = SexEnum.Woman,
                Birthday = new DateTime(2004, 10, 2)
            });
            count = db.InsertAll(users, typeof(UserInfo));
        }

    }

    public void UpdateTableData()
    {
        using (var db = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite))
        {
            var count = db.Update(new UserInfo()
            {
                Id = 3,
                Name = "test1Name-Update",
                Sex = SexEnum.Woman,
                Birthday = new DateTime(1990, 10, 2)
            }, typeof(UserInfo));
        }
    }

    public void DeleteTableData()
    {
        using (var db = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite))
        {
            string sql = "select Id,Name,Sex,Birthday from UserInfo where Id =3";
            var data = new object[] { "Id", "Name", "Sex", "Birthday" };
            var deleteData = db.Query(new TableMapping(typeof(UserInfo)), sql, data).FirstOrDefault();
            if (deleteData != null)
            {
                var count = db.Delete(deleteData);
            }

            var count2 = db.Delete<UserInfo>(4);
        }
    }

    public void QueryTableData()
    {
        using (var db = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite))
        {
            string sql = "select Id,Question,[Order] from RandomEvent";
            var data = new object[] { "Id", "Question", "[Order]" };
            var datas = db.Query(new TableMapping(typeof(RandomEvent)), sql, data);

            //enum not work :(
            //var datas2 = db.Find<UserInfo>(user => user.Name.Contains("test"));
            var datas2 = db.Find<RandomEvent>(randomEvent => randomEvent.Question.Contains("q"));

            var data3 = db.Get<UserInfo>(1);
        }
    }
}
