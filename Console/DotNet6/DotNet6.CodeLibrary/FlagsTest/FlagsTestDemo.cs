namespace DotNet6.CodeLibrary.FlagsTest
{
    [Flags]
    internal enum PermissionType
    {
        Add = 0x1000,
        Update = 0x0100,
        Delete = 0x0010,
        Query = 0x0001,
    }

    /// <summary>
    /// https://blog.csdn.net/lindexi_gd/article/details/60744821
    /// </summary>
    public class FlagsTestDemo
    {
        public static void Run()
        {
            var a= PermissionType.Add | PermissionType.Update;
            if (a.HasFlag(PermissionType.Add))
            {
                Console.WriteLine("有Add权限");
            }
            if (!a.HasFlag(PermissionType.Delete))
            {
                Console.WriteLine("没有Delete权限");
            }
        }
    }
}
