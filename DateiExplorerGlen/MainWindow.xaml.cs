using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DateiExplorerGlen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentPath= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public List<string> BeforePath; 
        public MainWindow()
        {
            InitializeComponent();
            string[] folder = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            for (int i = 0; i < folder.Length; i++)
            {
                ListView.Items.Add(System.IO.Path.GetFileName(folder[i]));
            }
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            for (int i = 0; i < files.Length; i++)
            {
                ListView.Items.Add(System.IO.Path.GetFileName(files[i]));
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem Item = sender as ListViewItem;
            string ItemPath = System.IO.Path.Combine(CurrentPath, Item.ToString());
            // Unterscheidung zwischen File und Folder
            // Problem: Path wird nicht richtig weitergegeben bzw ist Falsch
            if (ItemPath != null && Directory.Exists(ItemPath))
                UpdateWindowFolder(ItemPath);
            if(ItemPath != null && File.Exists(ItemPath))
                UpdateWindowExecuteFile(ItemPath);
        }
   
        public void UpdateWindowFolder(string Path)
        {
            // erster Versuch die Subfolder aus aus dem Path zu kriegen
            //string[] folder = Directory.GetDirectories(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),Path));
            string[] folder = Directory.GetDirectories(Path);
            ListView.Items.Clear();
            for (int i = 0; i < folder.Length; i++)
            {
                ListView.Items.Add(System.IO.Path.GetFileName(folder[i]));
                BeforePath.Add(CurrentPath);
                CurrentPath = Path;
            }
        }
        public void UpdateWindowExecuteFile(string Path)
        {
            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = Path;
                process.Start();
                BeforePath.Add(CurrentPath);
            }
        }
    }
}
