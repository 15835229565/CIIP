using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using System.Configuration;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB.Exceptions;

namespace CIIP.Module.BusinessObjects.SYS
{
    public class BusinessBuilder
    {
        public static Version GetVersion(FileInfo file)
        {
            if (file != null && file.Exists)
            {
                try
                {
                    return AssemblyName.GetAssemblyName(file.FullName).Version;
                }
                catch
                {
                }
            }
            return null;
        }

        public static void Reset()
        {
            _instance = null;
        }

        private static BusinessBuilder _instance;

        public static BusinessBuilder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BusinessBuilder();
                }
                return _instance;
            }
        }

        public Assembly RuntimeDCAssembly { get; private set; }

        public void GetDeclaredExportedTypes(List<Type> type)
        {
            if (RuntimeDCAssembly != null)
            {
                type.AddRange(RuntimeDCAssembly.GetTypes().Where(x => x.IsPublic && x.IsInterface && x.GetCustomAttributes(typeof(DomainComponentAttribute), true).Length > 0));
            }
        }

        public Type Register()
        {
            Version tempFileVersion = null;
            //��ʱ�ļ��İ汾��
            if (AdmiralEnvironment.UserDefineBusinessFile.Exists)
            {
                tempFileVersion = GetVersion(AdmiralEnvironment.UserDefineBusinessFile);
            }
            else
            {
                tempFileVersion = new Version();
            }

            var conncfg = ConfigurationManager.ConnectionStrings["ConnectionString"];
            if (conncfg != null)
            {
                try
                {
                    #region �����ļ�

                    var conn = conncfg.ConnectionString;
                    var dataLayer = XpoDefault.GetDataLayer(conn, DevExpress.Xpo.DB.AutoCreateOption.None);
                    var session = new Session(dataLayer);
                    var bm = session.FindObject<BusinessModule>(new BinaryOperator("ModuleName", "RuntimeModule.dll"));

                    #endregion

                    if (bm != null)
                    {
                        var dbVersion = new Version(bm.Version);


                        //�������һ���汾
                        if (bm != null && bm.File != null && dbVersion > tempFileVersion)
                        {
                            using (var fs = AdmiralEnvironment.UserDefineBusinessFile.OpenWrite())
                            {
                                bm.File.SaveToStream(fs);
                                fs.Flush();
                                fs.Close();
                            }
                            ERPModule.IsNewVersion = true;
                        }
                    }
                }
                catch (UnableToOpenDatabaseException ex)
                {

                }


            }

            //�����ʱ�ļ����ڣ�˵����Ҫ����ʱ�ļ����ǵ���ʽ�汾
            //if (AdmiralEnvironment.UserDefineBusinessTempFile.Exists)
            //{
            //    AdmiralEnvironment.UserDefineBusinessTempFile.CopyTo(AdmiralEnvironment.UserDefineBusinessFile.FullName, true);
            //    AdmiralEnvironment.UserDefineBusinessTempFile.Delete();
            //}


            //��������ʱ���ɵ�ģ��
            if (AdmiralEnvironment.UserDefineBusinessFile.Exists)
            {
                try
                {
                    var asm = Assembly.LoadFrom(AdmiralEnvironment.UserDefineBusinessFile.FullName);
                    RuntimeDCAssembly = asm;
                    return asm.GetType("RuntimeModule");
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public List<Assembly> BusinessLogicAssemblies { get; } = new List<Assembly>();

        public void LoadAssemblies(List<BusinessModule> modules)
        {
            foreach (var m in modules)
            {
                var asm = AppDomain.CurrentDomain.Load(m.File.Content);
                BusinessLogicAssemblies.Add(asm);
                //AdmiralEnvironment.SaveBusinessLogic(m.File);
            }
        }

        public Type[] RegisteBusinessLogics()
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
                {
                    Debug.WriteLine(e.Name);
                    if (e.Name.StartsWith("AdmiralDynamicDC, "))
                    {
                        return AppDomain.CurrentDomain.GetAssemblies()
                            .FirstOrDefault(x => x.FullName.StartsWith("AdmiralDynamicDC, "));
                    }
                    return e.RequestingAssembly;
                };
                return BusinessLogicAssemblies.SelectMany(
                    y => y.GetTypes().Where(x => !x.IsAbstract && typeof (ModuleBase).IsAssignableFrom(x))).ToArray();
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public BusinessBuilder()
        {
            var binUri = new Uri(GetType().Assembly.CodeBase);
            var binInfo = new FileInfo(binUri.LocalPath).Directory;

            var appBase = binInfo.Parent.FullName;
            appBase += "\\Runtime";

            //RuntimeDCFileInfo = new FileInfo(dir + "\\Admiral.Runtime.DC.dll");


            XafDCFile = appBase + "\\Admiral.Xaf.DC.dat"; //new FileInfo();
            XafModelFile = new FileInfo(appBase + "\\Admiral.ERP.ModelApplication.dll");
            XafModuleVersionFileInfo = new FileInfo(appBase + "\\version.txt");
            Directory = appBase;
        }

        //public FileInfo DCFileInfo { get; private set; }

        //public FileInfo RuntimeDCFileInfo { get; private set; }

        public string XafDCFile { get; private set; }
        public FileInfo XafModelFile { get; private set; }
        public FileInfo XafModuleVersionFileInfo { get; private set; }
        public string Directory { get; set; }
    }
}