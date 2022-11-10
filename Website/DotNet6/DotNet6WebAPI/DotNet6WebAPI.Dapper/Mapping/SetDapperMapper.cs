using Dapper;
using DotNet6WebAPI.Domain.Students.Entities;
using System.ComponentModel;
using System.Reflection;

namespace DotNet6WebAPI.Dapper.Mapping
{
    public static class SetDapperMapper
    {
        public static void Set()
        {
            SqlMapper.SetTypeMap(typeof(Student), new CustomPropertyTypeMap(typeof(Student),
               (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower())));

        }

        static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            DescriptionAttribute? attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return (attrib?.Description ?? member.Name).ToLower();
        }
    }
}
