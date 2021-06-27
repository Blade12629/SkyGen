using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKMapGenerator.Ultima
{
    public  abstract class UOFile : IDisposable
    {
        public bool IsDisposed { get; private set; }
        public string Path { get; set; }

        protected FileStream _stream;

        public UOFile(string path)
        {
            Path = path;
        }

        ~UOFile()
        {
            Dispose(false);
        }

        public abstract void Load();
        public abstract void Save();

        public void Open()
        {
            _stream = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        public void Create()
        {
            _stream = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        public void OpenOrCreate()
        {
            _stream = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        public bool FileExists()
        {
            return File.Exists(Path);
        }

        public void Delete()
        {
            File.Delete(Path);
        }

        public void CopyTo(string dest)
        {
            File.Copy(Path, dest);
        }

        public void Close()
        {
            _stream?.Close();
            _stream = null;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
            {
                IsDisposed = true;

                _stream?.Dispose();
                _stream = null;
            }
        }
    }
}
