using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCAS.Interfaces;
using System.IO;

namespace SimpleCAS.Manager
{
    /// <summary>
    /// Implementation of StorageManager for storing files within the filesystem. 
    /// </summary>
    public class FileSystemStorageManager : StorageManager
    {
        public FileSystemStorageManager(string documentRoot) : base()
        {
            DocumentRoot = documentRoot;
        }
        
        public string DocumentRoot { get; set; }

        public override Stream GetFileContent(string storageKey)
        {
            if(string.IsNullOrEmpty(storageKey))
                throw new ArgumentNullException("storageKey");

            return new FileStream(GetPath(storageKey).FullName, FileMode.Open, FileAccess.Read);
        }

        public override void DeleteFile(string storageKey)
        {
            var file = GetPath(storageKey);
            if (file.Exists)
                file.Delete();
            //if this was the last file in this directory, clean up these files. 
            var currentDirectory = file.Directory;
            while(currentDirectory.FullName != DocumentRoot) 
            {
                if (currentDirectory.GetFiles().LongLength == 0)
                {
                    currentDirectory.Delete();
                }
                else
                {
                    break;
                }
                currentDirectory = currentDirectory.Parent;
            }
            
        }
        public FileInfo GetPath(string storageKey)
        {
            if (storageKey == null)
                throw new ArgumentNullException("storageKey", "Invalid storage key provided.");

            //create subdirectories based on the first 4 values of the hash. We'll use this to distribute the files evenly
            return new FileInfo(string.Format("{0}\\{1}\\{2}\\{3}", DocumentRoot, storageKey.Substring(0, 2), storageKey.Substring(2, 2),storageKey.Substring(4, storageKey.Length - 4)));
        }

        public override string PutFileContent(Stream fileContent)
        {
            var key = this.CreateStorageKey(fileContent);

            FileInfo info = GetPath(key);

            if (!Directory.Exists(info.DirectoryName))
                Directory.CreateDirectory(info.DirectoryName);

            if (info.Exists)
                return key;

            using (var filestream = info.Create())
            {
                fileContent.Seek(0, SeekOrigin.Begin);    
                fileContent.CopyTo(filestream);
            }

            return key;
        }
    }
}
