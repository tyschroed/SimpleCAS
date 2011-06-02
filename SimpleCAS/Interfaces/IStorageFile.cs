using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCAS.Interfaces
{
    /// <summary>
    /// Interface for specifying files which can be stored by a storage backend inheriting from StorageManager
    /// </summary>
    public interface IStorageFile
    {
        /// <summary>
        /// Represents the SHA-1 hash of the files contents, which is what will be used for storage and retrieval within the storage backend implementation.
        /// </summary>
        string StorageKey { get; set; }
    }
}
