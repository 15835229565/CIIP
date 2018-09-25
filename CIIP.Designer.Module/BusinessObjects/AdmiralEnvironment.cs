using System;
using System.IO;
using CIIP.Module.BusinessObjects.SYS.Logic;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CIIP
{

    public class AdmiralEnvironment
    {
        static AdmiralEnvironment()
        {
            var appPath = new FileInfo( new Uri( typeof (AdmiralEnvironment).Assembly.CodeBase).AbsolutePath).Directory.FullName;
            // AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //UserDefineBusinessFile = new FileInfo(appPath + "\\Runtime\\BusinessObject\\RuntimeBusinessObject.dll");
            //if (!UserDefineBusinessFile.Exists)
            //{
            //    if (!UserDefineBusinessFile.Directory.Exists)
            //    {
            //        UserDefineBusinessFile.Directory.Create();
            //    }
            //    UserDefineBusinessTempFile = UserDefineBusinessFile;
            //}
            //else
            //{
            //    UserDefineBusinessTempFile = new FileInfo(appPath + "\\Runtime\\BusinessObject\\RuntimeBusinessObjectTemp.dll");
            //}

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
            ConnectionStringConfigFileInfo = new FileInfo(appPath + "\\connection.cfg");
        }
        
        public static FileInfo ConnectionStringConfigFileInfo { get; }

        public static string ApplicationPath { get; }
        //static string ConnectionStringConfigPath { get; }

        public static string ReadConnectionString()
        {
            ConnectionStringConfigFileInfo.Refresh();
            if (!ConnectionStringConfigFileInfo.Exists)
                return "";
            return File.ReadAllText(ConnectionStringConfigFileInfo.FullName);
        }

        public static void WriteConnectionString(string connectionString)
        {
            File.WriteAllText(ConnectionStringConfigFileInfo.FullName, connectionString);
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

        
        ///// <summary>
        ///// dc.dll ������,���浽��������ļ���
        ///// </summary>
        //public static FileInfo DomainComponentDefineConfig { get; set; }

        public static bool IsWeb { get; set; }
        public static bool IsWindows { get; set; }
    }
}