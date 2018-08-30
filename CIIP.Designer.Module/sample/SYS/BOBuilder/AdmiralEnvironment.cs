using System;
using System.IO;
using DevExpress.Persistent.BaseImpl;

namespace CIIP.Module.BusinessObjects.SYS
{
    public class AdmiralEnvironment
    {
        static AdmiralEnvironment()
        {
            var appPath = new FileInfo( new Uri( typeof (AdmiralEnvironment).Assembly.CodeBase).AbsolutePath).Directory.FullName;
            // AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            UserDefineBusinessFile = new FileInfo(appPath + "\\Runtime\\BusinessObject\\RuntimeBusinessObject.dll");
            if (!UserDefineBusinessFile.Exists)
            {
                if (!UserDefineBusinessFile.Directory.Exists)
                {
                    UserDefineBusinessFile.Directory.Create();
                }
                UserDefineBusinessTempFile = UserDefineBusinessFile;
            }
            else
            {
                UserDefineBusinessTempFile = new FileInfo(appPath + "\\Runtime\\BusinessObject\\RuntimeBusinessObjectTemp.dll");
            }

            //Runtime = new DirectoryInfo(ApplicationBase + "\\Runtime");
            //UserDefineBusinessDirectoryInfo = new DirectoryInfo(Runtime.FullName + "\\DCD");

            //if (!UserDefineBusinessDirectoryInfo.Exists)
            //{
            //    UserDefineBusinessDirectoryInfo.Create();
            //}

            //if (DomainComponentDefineConfig.Exists)
            //{
            //    var sr = DomainComponentDefineConfig.OpenText();
            //    var fileName = sr.ReadToEnd();
            //    sr.Close();
            //    UserDefineBusinessFile = new FileInfo(fileName);
            //}
            //else
            //{
            //    //��û�����ɹ�dc����
            //}
            ApplicationPath = appPath;
        }

        public static string ApplicationPath { get; }

        public static string SaveBusinessLogic(FileData value)
        {
            var path = UserDefineBusinessFile.Directory.Parent.FullName + "\\" + value.FileName;
            var stream = File.OpenWrite(path);
            value.SaveToStream(stream);
            stream.Flush();
            stream.Close();
            return path;
        }

        

        ///// <summary>
        ///// Ӧ�ó�������·��
        ///// </summary>
        //public static string ApplicationBase { get; private set; }

        ///// <summary>
        ///// ����ʱ�������ļ�λ��,���û������ҵ������dll���ļ���
        ///// </summary>
        //public static DirectoryInfo Runtime { get; private set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public static DirectoryInfo UserDefineBusinessDirectoryInfo { get; private set; }

        /// <summary>
        /// ����ʱ�û������ҵ��dll�����о�
        /// </summary>
        public static FileInfo UserDefineBusinessFile { get; }
        public static FileInfo UserDefineBusinessTempFile { get; }
        

        ///// <summary>
        ///// dc.dll ������,���浽��������ļ���
        ///// </summary>
        //public static FileInfo DomainComponentDefineConfig { get; set; }
    }
}