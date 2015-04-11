using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace FileSearchApp
{
    public delegate void FileCollect(FileDelegation file);
    public partial class Form1 : Form
    {
        #region global variables set
        public string sFolder = "";
        public DirectoryInfo dTarget;
        public List<FileInfo> fTarget;
        public string sKeyword = "";
        FileSystemx targetfs;

        #endregion global variables set
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            #region clear control items
            if (lvFiles.Items.Count != 0)
            {
                lvFiles.Items.Clear();
            }
            if (dgTargetFiles.Rows.Count != 0)
            {
                dgTargetFiles.Rows.Clear();
            }
            #endregion clear control items
            sKeyword = txtKeyWord.Text;     
            sFolder = txtFolder.Text;
            targetfs = new FileSystemx(sFolder);
            FileSearch myfilesearch = new FileSearch(targetfs, sKeyword);
            myfilesearch.myFileCollector = myfilesearch.myFileCollector + new FileCollect(GUI_Refresh);
            #region perform file search by key word
            Task filesearchTask = new Task(myfilesearch.getFittedFiles, new object());
            filesearchTask.Start();         
            #endregion         
        }
        public void GUI_Refresh(FileDelegation file)
        {
            if (file.Belonging == "SearchedCollection")
            {
                lvFiles.Items.Add(file.FullName.ToString());
                labFileSearched.Text = file.FullName.ToString();
            }
            if (file.Belonging == "FittedCollection")
            {
                dgTargetFiles.Rows.Add(file.FullName.ToString());
            }
        }
    }   
    public class FileSearch
    {
        private FileSystemx _targetfs;
        private string _keyword;
        public FileCollect myFileCollector;
        public FileSearch(FileSystemx targetfs, string keyword)
        {
            _targetfs = targetfs;
            _keyword = keyword;
            myFileCollector = new FileCollect(collectFiles);
        }
        public void getRecuriveFilesByBackgroupThread(object state)
        {
            getRecursiveFilesByBackGroupThread_detail(_targetfs, _targetfs.DirectoryTarget, _targetfs.FolderFilesTarget);
        }
        private void getRecursiveFilesByBackGroupThread_detail(FileSystemx targetfs, DirectoryInfo currentDirectory, List<FileInfo> files)
        {
            if (!Regex.IsMatch(currentDirectory.Name.ToString(), "bin", RegexOptions.IgnoreCase))
            {
                DirectoryInfo[] iterativeDirectories = currentDirectory.GetDirectories();
                if (currentDirectory.GetDirectories().Count<DirectoryInfo>() > 0)
                {
                    foreach (DirectoryInfo nextDirectory in iterativeDirectories)
                    {
                        getRecursiveFilesByBackGroupThread_detail(targetfs, nextDirectory, files);
                    }
                }
                foreach (FileInfo currentFile in currentDirectory.GetFiles())
                {
                    files.Add(currentFile);
                    targetfs.CurrentFilesCount = targetfs.CurrentFilesCount + 1;
                }
            }
        }
        public void getFittedFiles(object state)
        {
            getRecuriveFilesByBackgroupThread(new object());
            Int32 endloopindex = _targetfs.CurrentFilesCount;
            Int32 initfileindex = 0;
            string searchcontinueflag = "1";
            while (initfileindex < endloopindex)
            {
                #region method refined to iteratively apply regular expression on the target file
                using (StreamReader tmpSr = new StreamReader(_targetfs.FolderFilesTarget[initfileindex].FullName))
                {                   
                    myFileCollector(new FileDelegation(_targetfs.FolderFilesTarget[initfileindex].FullName.ToString(),"SearchedCollection"));
                    if (new FileInfo(_targetfs.FolderFilesTarget[initfileindex].FullName).Length > 100 * 1024 * 1024)
                    {
                        #region file size bigger than 100MB
                        if (!tmpSr.EndOfStream)
                        {
                            if (Regex.IsMatch(tmpSr.ReadLine(), _keyword, RegexOptions.IgnoreCase) && searchcontinueflag == "1")
                            {
                                myFileCollector(new FileDelegation(_targetfs.FolderFilesTarget[initfileindex].FullName.ToString(), "FittedCollection"));
                                searchcontinueflag = "0";
                            }
                        }
                        #endregion file size bigger than 100MB
                    }
                    else
                    {
                        if (Regex.IsMatch(tmpSr.ReadToEnd(), _keyword, RegexOptions.IgnoreCase))
                        {
                            myFileCollector(new FileDelegation(_targetfs.FolderFilesTarget[initfileindex].FullName.ToString(), "FittedCollection"));                           
                        }
                    }

                }
                #endregion method refined to iteratively apply regular expression on the target file
                initfileindex = initfileindex + 1;
                searchcontinueflag = "1";
            }
        }
        public void collectFiles(FileDelegation file)
        {
            if (file.Belonging == "SearchedCollection")
            {
                _targetfs.SearchedFiles.Add(new FileInfo(file.FullName.ToString()));
            }
            if (file.Belonging == "FittedCollection")
            {
                _targetfs.FittedFiles.Add(new FileInfo(file.FullName.ToString()));
            }
        }
    }
    public class FileSystemx
    {
        DirectoryInfo Folder;
        List<FileInfo> FolderFiles;
        Int32 _CurrentFilesCount = 0;
        List<FileInfo> _searchedFiles;
        List<FileInfo> _fittedFiles;
        public FileSystemx(string DirectoryPath)
        {
            Folder = new DirectoryInfo(DirectoryPath);
            FolderFiles = new List<FileInfo>();
            _searchedFiles = new List<FileInfo>();
            _fittedFiles = new List<FileInfo>();
        }
        public Int32 CurrentFilesCount { get { return _CurrentFilesCount; } set { _CurrentFilesCount = value; } }
        public DirectoryInfo DirectoryTarget { get { return Folder; } }
        public List<FileInfo> FolderFilesTarget { get { return FolderFiles; } }

        public List<FileInfo> SearchedFiles { get { return _searchedFiles; } set { _searchedFiles = value; } }
        public List<FileInfo> FittedFiles { get { return _fittedFiles; } set { _fittedFiles = value; } }

    }
    public class FileDelegation
    {
        private string _fullname;
        private string _belonging;
        public FileDelegation(string FullName, string Belonging)
        {
            _fullname = FullName;
            _belonging = Belonging;
        }
        public string FullName { get { return _fullname; } }
        public string Belonging { get { return _belonging; } }
    }
}