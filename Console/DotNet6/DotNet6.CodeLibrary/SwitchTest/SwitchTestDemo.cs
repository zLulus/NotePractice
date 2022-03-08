using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.SwitchTest
{
    public class SwitchTestDemo
    {
        public static void Run()
        {
            TagObject tagObject = new TagObject()
            {
                Tag1 = TagEnum.First,
                Tag2 = TagEnum.Two
            };

            Console.WriteLine($"Switch匹配：{GetTag_Exception_Patterns(5)}");
            Console.WriteLine($"Switch匹配_使用枚举：{GetTag_Expression_Patterns(TagEnum.First)}");

            Console.WriteLine($"Switch匹配_属性模式：{GetTag_Property_Patterns(tagObject)}");
            Console.WriteLine($"Switch匹配_位置模式：{GetTag_Positional_Patterns(tagObject)}");

            (TagEnum x, TagEnum _) = tagObject;
            Console.WriteLine($"解构： {x.ToString()}");
        }

        public static int GetTag_Expression_Patterns(TagEnum tag)
        {
            return tag switch
            {
                TagEnum.First => 1,
                TagEnum.Two => 2,
                TagEnum.Three => 3,
                _ => 0
            };
        }

        public static int GetTag_Property_Patterns(TagObject tagObject)
        {
            if (tagObject == null) return 0;

            return tagObject switch
            {
                { Tag1: TagEnum.First } => 1,
                { Tag1: TagEnum.Two } => 2,
                { Tag1: TagEnum.Three } => 3,
                _ => 0
            };
        }

        public static int GetTag_Positional_Patterns(TagObject tagObject)
        {
            if (tagObject == null) return 0;

            return tagObject switch
            {
                var (x, y) when x == TagEnum.First && y == TagEnum.Two
                    => 3,
                _ => 0
            };
        }

        public static int GetTag_Exception_Patterns(int a)
        {
            return a switch
            {
                100 => 10,
                _ => 99,
            };
        }
    }

    [Flags]
    public enum TagEnum
    {
        First = 0x01,
        Two = 0x10,
        Three = 0x100
    }

    public class TagObject
    {
        public TagEnum Tag1 { get; set; }
        public TagEnum Tag2 { get; set; }

        public TagObject()
        {
        }

        //这里构造方法与解构方法是展示8.0的位置模式
        public TagObject(TagEnum tag1, TagEnum tag2)
        {
            (this.Tag1, this.Tag2) = (tag1, tag2);
        }

        public void Deconstruct(out TagEnum tag1, out TagEnum tag2)
        {
            (tag1, tag2) = (this.Tag1, this.Tag2);
        }

        public void Deconstruct(out TagEnum tag1)
        {
            tag1 = this.Tag1;
        }
    }
}
