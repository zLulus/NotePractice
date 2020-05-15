using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfDemo.FTP.Dtos;

namespace WpfDemo.FTP
{
    internal class FTPTool
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        private string host;
        /// <summary>
        /// ftp用户名
        /// </summary>
        private string userName;
        /// <summary>
        /// ftp密码
        /// </summary>
        private string password;

        private CultureInfo _myCultureInfo = new CultureInfo("en-US");

        private FtpWebRequest _ftpRequest = null;
        private FtpWebResponse _ftpResponse = null;
        private Stream _ftpStream = null;
        private int _bufferSize = 4096;

        public FTPTool(string host,string userName=null,string password=null)
        {
            this.host = host;
            this.userName = userName;
            this.password = password;
        }

        public void Rebind(string host, string userName, string password)
        {
            this.host = host;
            this.userName = userName;
            this.password = password;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="remoteFile"></param>
        /// <param name="localFile"></param>
        public void Upload(string remoteFile, string localFile)
        {
            try
            {

                _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);

                _ftpRequest.Credentials = new NetworkCredential(userName, password);

                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;

                _ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                _ftpStream = _ftpRequest.GetRequestStream();

                FileStream localFileStream = new FileStream(localFile, FileMode.Open);

                byte[] byteBuffer = new byte[_bufferSize];
                int bytesSent = localFileStream.Read(byteBuffer, 0, byteBuffer.Length);

                try
                {
                    while (bytesSent != 0)
                    {
                        _ftpStream.Write(byteBuffer, 0, bytesSent);
                        bytesSent = localFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.ToString());
#endif
                }

                localFileStream.Close();
                _ftpStream.Close();
                _ftpRequest = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="remoteFile"></param>
        /// <param name="localFile"></param>
        public void Download(string remoteFile, string localFile)
        {
            try
            {
                if (remoteFile.Contains("ftp"))
                {
                    _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(remoteFile);
                }
                else
                {
                    _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                }
                

                _ftpRequest.Credentials = new NetworkCredential(userName, password);

                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;

                _ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
                _ftpStream = _ftpResponse.GetResponseStream();

                FileStream localFileStream = new FileStream(localFile, FileMode.Create);

                byte[] byteBuffer = new byte[_bufferSize];
                int bytesRead = _ftpStream.Read(byteBuffer, 0, _bufferSize);
                try
                {
                    while (bytesRead > 0)
                    {
                        localFileStream.Write(byteBuffer, 0, bytesRead);
                        bytesRead = _ftpStream.Read(byteBuffer, 0, _bufferSize);
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.ToString());
#endif
                }
                localFileStream.Close();
                _ftpStream.Close();
                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deleteFile"></param>
        public void Delete(string deleteFile)
        {
            try
            {
                if (deleteFile.Contains("ftp"))
                {
                    _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(deleteFile);
                }
                else
                {
                    _ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
                }
               

                _ftpRequest.Credentials = new NetworkCredential(userName, password);

                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;

                _ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();

                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="currentFileNameAndPath"></param>
        /// <param name="newFileName"></param>
        public void Rename(string currentFileNameAndPath, string newFileName)
        {
            try
            {
                if (currentFileNameAndPath.Contains("ftp"))
                {
                    _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(currentFileNameAndPath);
                }
                else
                {
                    _ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
                }
                

                _ftpRequest.Credentials = new NetworkCredential(userName, password);

                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;

                _ftpRequest.Method = WebRequestMethods.Ftp.Rename;

                _ftpRequest.RenameTo = newFileName;

                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();

                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="newDirectory"></param>
        public void CreateDirectory(string newDirectory)
        {
            try
            {
                if (newDirectory.Contains("ftp"))
                {
                    _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(newDirectory);
                }
                else
                {
                    _ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory);
                }
                _ftpRequest.Credentials = new NetworkCredential(userName, password);

                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;

                _ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;

                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();

                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
        }

        /// <summary>
        /// 获得文件大小
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            if (filename.Contains("ftp"))
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(filename);
            }
            else
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(host + "/" + filename.TrimStart('/')));
            }
            reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(userName, password);
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            fileSize = response.ContentLength;

            ftpStream.Close();
            response.Close();
            return fileSize;
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path"></param>
        public void RemoveDirectory(string path)
        {
            try
            {
                if (path.Contains("ftp"))
                {
                    _ftpRequest = (FtpWebRequest)WebRequest.Create(path);
                }
                else
                {
                    _ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + path);
                }

                _ftpRequest.Credentials = new NetworkCredential(userName, password);

                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;

                _ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;

                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();

                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
                throw ex;
            }
        }

        /// <summary>
        /// 查看目录详情
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public List<RemoteEntity> DirectoryListDetailed(string directory)
        {
            List<RemoteEntity> listResult = new List<RemoteEntity>();
            try
            {
                _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);

                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;

                _ftpRequest.Credentials = new NetworkCredential(userName, password);
                _ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();

                _ftpStream = _ftpResponse.GetResponseStream();
                StreamReader ftpReader = new StreamReader(_ftpStream);

                string line = string.Empty;

                int fileStyle = -1;

                while (!string.IsNullOrEmpty(line = ftpReader.ReadLine()))
                {
                    if (fileStyle == -1)
                        fileStyle = GetFileListStyle(line);

                    if (fileStyle == -1)//未知格式FTP
                        continue;

                    RemoteEntity entity = null;

                    if (fileStyle == 1)
                    {
                        entity = ParseFileStructFromWindowsStyleRecord(line);
                    }
                    else
                    {
                        entity = ParseFileStructFromUnixStyleRecord(line);
                    }

                    entity.RelativePath = directory + "/" + entity.Name;
                    entity.Url = host + "/" + directory + "/" + entity.Name;


                    listResult.Add(entity);
                }

                ftpReader.Close();
                _ftpStream.Close();
                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
            return listResult;
        }

        /// <summary>
        /// 路径是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fContainTail"></param>
        /// <returns></returns>
        public bool IsPathExist(string path, bool fContainTail)
        {
            string[] sa = path.Trim('/').Split('/');
            string p = "";
            int Length = fContainTail ? sa.Length : (sa.Length - 1);
            for (int i = 0; i < Length; ++i)
            {
                var name = sa[i];
                if (string.IsNullOrEmpty(name.Trim()))
                    break;
                List<RemoteEntity> a = DirectoryListDetailed(p);
                p += "/" + name;
                //if (a.Find(t => t.RelativePath == sa[i]) == null)
                if (a.Find(t => isEqualIgnorCase(t.Name, name)) == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获得FTP 服务器 风格
        /// </summary>
        /// <param name="s"></param>
        /// <returns>0 UnixStyle 1 WindowsStyle -1 未知格式</returns>
        private int GetFileListStyle(string s)
        {
            if (s.Length > 10 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
            {
                return 0;
            }

            if (s.Length > 8 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
            {
                return 1;
            }
            return -1;
        }

        /// <summary>
        /// Windows 格式 FTP
        /// </summary>
        /// <param name="Record"></param>
        /// <returns></returns>
        private RemoteEntity ParseFileStructFromWindowsStyleRecord(string Record)
        {
            RemoteEntity f = new RemoteEntity();
            string processstr = Record.Trim();
            string dateStr = processstr.Substring(0, 8);
            processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
            string timeStr = processstr.Substring(0, 7);
            processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();
            f.CreateDate = DateTime.Parse(dateStr + " " + timeStr, _myCultureInfo).ToString();
            if (processstr.Substring(0, 5) == "<DIR>")
            {
                f.RType = 1;
                processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
            }
            else
            {
                processstr = processstr.Substring(processstr.IndexOf(' ')).TrimStart(' ');
            }
            f.Name = processstr;
            return f;
        }

        /// <summary>
        /// unix 格式 FTP
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private RemoteEntity ParseFileStructFromUnixStyleRecord(string record)
        {
            ///Assuming record style as
            /// dr-xr-xr-x   1 owner    group               0 Feb 25  2011 bussys
            RemoteEntity f = new RemoteEntity();
            string processstr = record.Trim();

            string flags = processstr.Substring(0, 9);
            //f.Flags = processstr.Substring(0, 9);
            f.RType = (flags[0] == 'd') ? 1 : 0;

            processstr = (processstr.Substring(11)).Trim();

            _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //skip one part

            string owner = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
            string group = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);

            _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //skip one part

            DateTime createTime = DateTime.Now;

            var dateString = _cutSubstringFromStringWithTrim(ref processstr, ' ', 8);
            DateTime.TryParse(dateString, out createTime);

            f.CreateDate = createTime.ToString();
            f.Name = processstr;   //Rest of the part is name
            return f;
        }

        private string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);

            s = (s.Substring(pos1)).Trim();

            return retString;
        }

        private static bool isEqualIgnorCase(String a, String b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;
            return a.Equals(b, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
