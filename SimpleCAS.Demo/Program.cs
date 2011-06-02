using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCAS.Manager;
using System.IO;

namespace SimpleCAS.Demo
{
    class Program
    {
        private static string TESTFILE = "TestFile.txt";
        private static string COPYFILE = "TestFileCopy.txt";

        static void Main(string[] args)
        {
            //Begin by creating the instance which we will use for all operations.
            StorageManager.Current = new FileSystemStorageManager(Directory.GetCurrentDirectory() + "\\Storage");

            StoredFile file = new StoredFile();
            //Wrap the metadata for the file in a class
            file.FileName = TESTFILE;
            file.ModifiedDate = DateTime.Now;
            file.StorageKey = PutFile(TESTFILE);


            //now if we want to fetch the contents that file, we use the hash we received above, or an object implementing the IStorageFile interface.
            using (var contentStream = StorageManager.Current.GetFileContent(file))
            {
                using (var writeStream = File.Create(COPYFILE))
                {
                    contentStream.CopyTo(writeStream);
                }
            }

            //now if we put this new file, with the same content but a different filename, we should get the same key back, and thus we will only be storing one copy.
            var storageKey = PutFile(COPYFILE);
            Console.WriteLine("Original Key: " + file.StorageKey);
            Console.WriteLine("Copied Key:   " + storageKey);
            Console.ReadLine();

            //now we will delete our file. Keep in mind that before you call this, you want to make absolutely sure that nobody is using this hash!!
            StorageManager.Current.DeleteFile(file);
        }

        public static string PutFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                //write the contents, and receive back the SHA-1 hash to refer to this later by. 
                return StorageManager.Current.PutFileContent(stream);
            }
        }
    }
}
