SimpleCAS
=========

SimpleCAS is a very simple CAS storage implementation written in C#. Usage is super simple:

1. Initialize the static storagemanager instance
`StorageManager.Current = new FileSystemStorageManager(Directory.GetCurrentDirectory() + "\\Storage");`
2. Store your file contents:
`                       using (var stream = File.OpenRead(fileName))
            {
                //write the contents, and receive back the SHA-1 hash to refer to this later by. 
                var storageKey = StorageManager.Current.PutFileContent(stream);
            }
`
3. Use that key to retrieve file contents later:
`var contentStream = StorageManager.Current.GetFileContent(storageKey)`


See the included SimpleCAS.Demo application for a little example console app that demonstrates this. 

-Tyler