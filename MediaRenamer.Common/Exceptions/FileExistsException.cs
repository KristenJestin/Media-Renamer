using System;
using System.IO;

namespace MediaRenamer.Common.Exceptions
{

    public class FileExistsException : IOException
    {
        public FileExistsException() : base() { }
        public FileExistsException(string? message) : base(message) { }
        public FileExistsException(string? message, Exception? innerException) : base(message, innerException) { }
        public FileExistsException(string? message, string? fileName) : base(message)
        {
            FileName = fileName;
        }
        public FileExistsException(string? message, string? fileName, Exception? innerException) : base(message, innerException)
        {
            FileName = fileName;
        }

        public string? FileName { get; }
    }
}