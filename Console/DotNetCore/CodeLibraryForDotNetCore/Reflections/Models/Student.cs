using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Reflections.Models
{
    public class Student
    {
        //静态字段
        public static string SchoolName { get; set; }
        internal static string SchoolAddress { get; set; }
        protected static DateTime SchoolEstablishmentTime { get; set; }
        private static int SchoolBuildingCount { get; set; }
        //不同访问级别的字段
        public string name;
        internal string address;
        protected int age;
        private double height;
        //不同访问级别的属性
        public string Name { get; set; }
        internal string Address { get; set; }
        protected int Age { get; set; }
        private double Height { get; set; }
        //不同访问级别的方法
        public string GetName()
        {
            return Name;
        }
        internal string GetAddress()
        {
            return Address;
        }
        protected int GetAge()
        {
            return Age;
        }
        private double GetHeight()
        {
            return Height;
        }
    }
}
