using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ShellDll;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Security.Policy;
using System.Drawing;
using System.ComponentModel;

namespace PaJaMa.WinControls.FileBrowser
{
    public class FileBrowserPluginWrapper : Component
    {
        #region Fields

        private ArrayList columnPlugins;
        private ArrayList viewPlugins;
        private ArrayList contextPlugins;

        #endregion

        public FileBrowserPluginWrapper()
        {
            columnPlugins = new ArrayList();
            viewPlugins = new ArrayList();
            contextPlugins = new ArrayList();

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            string pluginPath = Application.StartupPath + @"\plugins";

            if (Directory.Exists(pluginPath))
            {
                string[] files = Directory.GetFiles(pluginPath, "*.dll");

                foreach (string file in files)
                {
                    try
                    {
                        Assembly plugin = Assembly.LoadFile(file);

                        Type[] types = plugin.GetTypes();

                        foreach (Type type in types)
                        {
                            try
                            {
                                IFileBrowserPlugin FileBrowserPlugin =
                                    plugin.CreateInstance(type.ToString()) as IFileBrowserPlugin;

                                if (FileBrowserPlugin != null)
                                {
                                    if (FileBrowserPlugin is IColumnPlugin)
                                        columnPlugins.Add(FileBrowserPlugin);

                                    if (FileBrowserPlugin is IViewPlugin)
                                        viewPlugins.Add(FileBrowserPlugin);

                                    //if (FileBrowserPlugin is IContextPlugin)
                                        //contextPlugins.Add(FileBrowserPlugin);
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        #region Properties

        public ArrayList ColumnPlugins { get { return columnPlugins; } }
        public ArrayList ViewPlugins { get { return viewPlugins; } }
        public ArrayList ContextPlugins { get { return contextPlugins; } }

        #endregion
    }

    #region Plugins

    public interface IFileBrowserPlugin
    {
        string Name { get; }
        string Info { get; }
    }

    public interface IColumnPlugin : IFileBrowserPlugin
    {
        string[] ColumnNames { get; }
        
        HorizontalAlignment GetAlignment(string columnName);

        string GetFolderInfo(IDirInfoProvider provider, string columnName, ShellItem item);
        string GetFileInfo(IFileInfoProvider provider, string columnName, ShellItem item);
    }

    public interface IViewPlugin : IFileBrowserPlugin
    {
        string ViewName { get; }
        Control ViewControl { get; }

        void FolderSelected(IDirInfoProvider provider, ShellItem item);
        void FileSelected(IFileInfoProvider provider, ShellItem item);
        void Reset();
    }

    /*public interface IContextPlugin : IFileBrowserPlugin
    {
        string MenuText { get; }
        Icon MenuIcon { get; }
        string MenuInfo { get; }
        
        string[] Extensions { get; }

        void MenuSelected(IFileInfoProvider2 streamProvider, IDirectoryInfoProvider2 storageProvider, ShellItem[] items);
    }*/

    #region Provider Interfaces

    public interface IDirInfoProvider
    {
        ShellAPI.STATSTG GetDirInfo();
    }

    public interface IFileInfoProvider
    {
        ShellAPI.STATSTG GetFileInfo();
        Stream GetFileStream();
    }

    public interface IDirectoryInfoProvider2 : IDirInfoProvider
    {
        IStorage GetDirInfo(ShellItem item);
    }

    public interface IFileInfoProvider2 : IFileInfoProvider
    {
        IStream GetFileInfo(ShellItem item);
        Stream GetFileStream(ShellItem item);
    }

    #endregion

    #endregion
}
