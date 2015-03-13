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

namespace FileSearch
{
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
            if ( lvFiles.Items.Count !=0)
            {
                lvFiles.Items.Clear();
            }
            if (dgTargetFiles.Rows.Count != 0)
            {
                dgTargetFiles.Rows.Clear();
            }
            #endregion clear control items

            //#region original show up FileSearch result
            //sFolder = txtFolder.Text;
            sKeyword = txtKeyWord.Text;
            //dTarget = new DirectoryInfo(sFolder);
            //fTarget = new List<FileInfo>();
            //getRecursiveFiles(dTarget, fTarget);
            
            //foreach (FileInfo indFile in fTarget)
            //{
            //    lvFiles.Items.Add(indFile.FullName);
            //    //if (Regex.IsMatch(new StreamReader(indFile.FullName).ReadToEnd(), sKeyword, RegexOptions.IgnoreCase))
            //    //{
            //    //    int Rowindex = dgTargetFiles.Rows.Add();
            //    //    dgTargetFiles.Rows[Rowindex].Cells["FullPathName"].Value = indFile.FullName;
            //    //}
            //}
            //#endregion original show up FileSearch result

            #region backgroup thread used to get the Recursive Files in a folder
             sFolder = txtFolder.Text;
            targetfs = new FileSystemx(sFolder);            
            ThreadPool.QueueUserWorkItem(getRecuriveFilesByBackgroupThread, new object());
            Thread.Sleep(1000);
             Int32 endloopindex = targetfs.CurrentFilesCount;
             Int32 initfileindex = 0;
                while ( lvFiles.Items.Count  != targetfs.CurrentFilesCount)
                {
                    if ( lvFiles.Items.Count != 0 )
                    {
                        lvFiles.Clear();
                    }
                    while ( initfileindex < endloopindex)
                    {
                        lvFiles.Items.Add( targetfs.FolderFilesTarget[initfileindex].FullName);
                        //lvFiles.Refresh();

                        string searchcontinueflag = "1";

                        #region original method to iterate to apply regular expression on the target file
                       // StreamReader tmpSr = new StreamReader(targetfs.FolderFilesTarget[initfileindex].FullName);
                       //StringBuilder tmpStr = new StringBuilder(tmpSr.ReadToEnd());
                       //try
                       //{
                       //    if (Regex.IsMatch(tmpStr.ToString(), sKeyword, RegexOptions.IgnoreCase))
                       //    {
                       //        int Rowindex = dgTargetFiles.Rows.Add();
                       //        dgTargetFiles.Rows[Rowindex].Cells["FullPathName"].Value = targetfs.FolderFilesTarget[initfileindex].FullName;
                       //        dgTargetFiles.Show();
                       //    }
                       //}
                       //catch (Exception ex)
                       //{
                       //    Console.WriteLine("error: " + ex.Message);
                       //}
                       //finally
                       //{
                       //    tmpStr.Clear();                           
                       //    tmpSr.Dispose();
                       //    GC.Collect();
                       //}
                        #endregion  original method to iterate to apply regular expression on the target file
                       
                       #region method refined to iteratively apply regular expression on the target file
                        using (StreamReader tmpSr = new StreamReader(targetfs.FolderFilesTarget[initfileindex].FullName))
                        {
                            if (new FileInfo(targetfs.FolderFilesTarget[initfileindex].FullName).Length > 100 * 1024 * 1024)
                            {
                                #region file size bigger than 100MB
                                if (!tmpSr.EndOfStream)
                                {
                                    if (Regex.IsMatch(tmpSr.ReadLine(), sKeyword, RegexOptions.IgnoreCase) && searchcontinueflag == "1")
                                    {
                                        dgTargetFiles.Rows.Add(targetfs.FolderFilesTarget[initfileindex].FullName);
                                        searchcontinueflag = "0";
                                    }
                                }
                                #endregion file size bigger than 100MB
                            }
                            else
                            {
                                if (Regex.IsMatch(tmpSr.ReadToEnd(), sKeyword, RegexOptions.IgnoreCase))
                                {
                                    dgTargetFiles.Rows.Add(targetfs.FolderFilesTarget[initfileindex].FullName);
                                    //searchcontinueflag = "0";
                                }
                            }

                        }
                       #endregion method refined to iteratively apply regular expression on the target file

                       initfileindex = initfileindex +1 ;
                    }

                      Thread.Sleep(1000);
                    if ( endloopindex != targetfs.CurrentFilesCount)
                    {
                        endloopindex = targetfs.CurrentFilesCount;
                        initfileindex = 0;
                    }
                    
                }

            #endregion backgroup thread used to get the Recursive Files in a folder



                //foreach (FileInfo indFile in targetfs.FolderFilesTarget)
                //{

                //    if (Regex.IsMatch(new StreamReader(indFile.FullName).ReadToEnd(), sKeyword, RegexOptions.IgnoreCase))
                //    {
                //        int Rowindex = dgTargetFiles.Rows.Add();
                //        dgTargetFiles.Rows[Rowindex].Cells["FullPathName"].Value = indFile.FullName;
                //        dgTargetFiles.Show();
                //    }
                //}

            
        }

        private void getRecursiveFiles(DirectoryInfo currentDirectory, List<FileInfo> files)
        {
            if (!Regex.IsMatch(currentDirectory.Name.ToString(), "bin", RegexOptions.IgnoreCase))
            {
                DirectoryInfo[] iterativeDirectories = currentDirectory.GetDirectories();
                if (currentDirectory.GetDirectories().Count<DirectoryInfo>() > 0)
                {
                    foreach (DirectoryInfo nextDirectory in iterativeDirectories)
                    {
                        getRecursiveFiles(nextDirectory, files);
                    }
                }
                foreach (FileInfo currentFile in currentDirectory.GetFiles())
                {
                    files.Add(currentFile);

                }
            }          
        }

        public void getRecuriveFilesByBackgroupThread(object state)
        {
          getRecursiveFilesByBackGroupThread_detail( targetfs, targetfs.DirectoryTarget, targetfs.FolderFilesTarget);
        }
         private void getRecursiveFilesByBackGroupThread_detail(FileSystemx targetfs, DirectoryInfo currentDirectory, List<FileInfo> files )
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



    }

    public class FileSystemx
    {
        DirectoryInfo Folder;
        List<FileInfo> FolderFiles;
        Int32 _CurrentFilesCount = 0;
        public FileSystemx(string DirectoryPath)
        {
            Folder = new DirectoryInfo(DirectoryPath);
            FolderFiles = new List<FileInfo>();
        }
        public Int32 CurrentFilesCount { get { return _CurrentFilesCount; } set { _CurrentFilesCount = value; } }
        public DirectoryInfo DirectoryTarget { get { return Folder ;} }
        public List<FileInfo> FolderFilesTarget { get{return FolderFiles;}}
    }
}
