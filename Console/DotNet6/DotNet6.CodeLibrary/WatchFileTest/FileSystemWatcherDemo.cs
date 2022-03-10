namespace DotNet6.CodeLibrary.WatchFileTest
{
    /// <summary>
    /// https://blog.csdn.net/weixin_34280237/article/details/94634414
    /// </summary>
    public class FileSystemWatcherDemo
    {
        FileSystemWatcher watcher;
        public void Run()
        {
            watcher = new FileSystemWatcher();
            watcher = new FileSystemWatcher();
            watcher.Path = Path.Combine(Directory.GetCurrentDirectory(), "WatchFileTest");
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.DirectoryName;
            watcher.IncludeSubdirectories = true;
            watcher.Created += new FileSystemEventHandler(FileSystemWatcher_Created);
            watcher.Changed += new FileSystemEventHandler(FileSystemWatcher_Changed);
            watcher.Deleted += new FileSystemEventHandler(FileSystemWatcher_Deleted);
            watcher.Renamed += new RenamedEventHandler(FileSystemWatcher_Renamed);
            watcher.EnableRaisingEvents = true;
            Console.WriteLine($"开始监控文件变化");
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("重命名: OldPath:{0} NewPath:{1} OldFileName{2} NewFileName:{3}", e.OldFullPath, e.FullPath, e.OldName, e.Name);
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("删除:" + e.ChangeType + ";" + e.FullPath + ";" + e.Name);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("变更:" + e.ChangeType + ";" + e.FullPath + ";" + e.Name);
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("新增:" + e.ChangeType + ";" + e.FullPath + ";" + e.Name);
        }

        ~FileSystemWatcherDemo()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }
    }
}
