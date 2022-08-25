using System;
using System.IO;

namespace xNet
{
    /// <summary>
    /// Represents the request body as a stream of data from a specific file.
    /// </summary>
    public class FileContent : StreamContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileContent"/> class and opens a file stream.
        /// </summary>
        /// <param name="pathToContent">The path to the file that will become the content of the request body.</param>
        /// <param name="bufferSize">The buffer size in bytes for the stream.</param>
        /// <exception cref="System.ArgumentNullException">The value of the <paramref name="pathToContent"/> parameter is <see langword="null"/>.</exception>
        /// <exception cref="System.ArgumentException">The value of the <paramref name="pathToContent"/> parameter is an empty string.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"> <paramref name="bufferSize"/> is less than 1.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceeds the maximum length allowed by the system. 
        /// For example, for Windows-based platforms, paths cannot exceed 248 characters, and file names cannot exceed 260 characters.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The value of the <paramref name="pathToContent"/> parameter points to a file that does not exist.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The <paramref name="pathToContent"/> parameter value points to an invalid path.</exception>
        /// <exception cref="System.IO.IOException">I/O error while working with file.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.UnauthorizedAccessException">
        /// The file read operation is not supported on the current platform.
        /// -or-
        /// The value of the <paramref name="pathToContent"/> parameter specifies a directory.
        /// -or-
        /// The caller does not have the required permission.
        /// </exception>
        /// <remarks>The content type is determined automatically based on the file extension.</remarks>
        public FileContent(string pathToContent, int bufferSize = 32768)
        {
            #region Parameter Check

            if (pathToContent == null)
            {
                throw new ArgumentNullException("pathToContent");
            }

            if (pathToContent.Length == 0)
            {
                throw ExceptionHelper.EmptyString("pathToContent");
            }

            if (bufferSize < 1)
            {
                throw ExceptionHelper.CanNotBeLess("bufferSize", 1);
            }

            #endregion

            _content = new FileStream(pathToContent, FileMode.Open, FileAccess.Read);
            _bufferSize = bufferSize;
            _initialStreamPosition = 0;

            _contentType = Http.DetermineMediaType(
                Path.GetExtension(pathToContent));
        }
    }
}