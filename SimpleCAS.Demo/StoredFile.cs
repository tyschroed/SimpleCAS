using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCAS.Interfaces;

namespace SimpleCAS.Demo
{
    /// <summary>
    /// This is the object which would be persisted somewhere...
    /// </summary>
    public class StoredFile : IStorageFile
    {
        public string FileName { get; set; }
        
        public DateTime ModifiedDate { get; set; }

        public string StorageKey { get; set; }
    }
}
