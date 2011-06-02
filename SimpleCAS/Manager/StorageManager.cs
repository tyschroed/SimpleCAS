using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCAS.Interfaces;
using System.Security.Cryptography;
using System.IO;

namespace SimpleCAS.Manager
{
    /// <summary>
    /// Class for handling basic file storage operations in an abstract manner.
    /// </summary>
    public abstract class StorageManager
    {
        /// <summary>
        /// Generates a file storage key for the given file contents. 
        /// </summary>
        /// <param name="fileContent">Contents of the file which should be used to generated the key from.</param>
        /// <returns>SHA-1 hash of the file contents, which will be used to refer to this file in the future.</returns>
        public virtual string CreateStorageKey(Stream fileContent)
        {
            var hashAlgorith = new SHA1Managed();
            
            return Convert.ToBase64String(hashAlgorith.ComputeHash(fileContent));
        }

        /// <summary>
        /// Retrieves the file contents from storage.
        /// </summary>
        /// <param name="storageKey">Storage key to retrieve file contents for.</param>
        /// <returns></returns>
        public virtual Stream GetFileContent(string storageKey)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Retrieves the file contents from storage.
        /// </summary>
        /// <param name="storedFile">Stored file to retrieve contents of.</param>
        /// <returns></returns>
        public virtual Stream GetFileContent(IStorageFile storedFile)
        {
            return GetFileContent(storedFile.StorageKey);
        }
        /// <summary>
        /// Deletes the given file from storage. Make sure that all references to this file are gone, across all clients, before calling this. 
        /// </summary>
        /// <param name="storageKey">Storage key of file to delete.</param>
        public virtual void DeleteFile(string storageKey)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Deletes the given file from storage. Make sure that all references to this file are gone, across all clients, before calling this. 
        /// </summary>
        /// <param name="storedFile">Stored file to delete.</param>
        public virtual void DeleteFile(IStorageFile storedFile)
        {
            DeleteFile(storedFile.StorageKey);
        }
        /// <summary>
        /// Adds the file contents to storage, giving back the storageKey with which to refer to the file later by. 
        /// </summary>
        /// <param name="fileContent">Contents of the file to be stored.</param>
        /// <returns>Storage key of file to refer to it later.</returns>
        public virtual string PutFileContent(Stream fileContent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The storage manager that is configured for this context. Null if none has been configured. 
        /// </summary>
        public static StorageManager Current;

    }
}
