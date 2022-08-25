using System.IO;

namespace xNet
{
    /// <summary> 
    /// Represents the request body. Released immediately after dispatch. 
    /// </summary>    
    public abstract class HttpContent
    {
        /// <summary>MIME content type.</summary>
        protected string _contentType = string.Empty;


        /// <summary>
        /// Gets or sets the content MIME type.
        /// </summary>
        public string ContentType
        {
            get
            {
                return _contentType;
            }
            set
            {
                _contentType = value ?? string.Empty;
            }
        }


        #region Methods (public)

        /// <summary>
        /// Counts and returns the length of the request body in bytes.
        /// </summary>
        /// <returns>The length of the request body in bytes.</returns>
        public abstract long CalculateContentLength();

        /// <summary>
        /// Writes the request body data to the stream.
        /// </summary>
        /// <param name="stream">The stream where the request body data will be written.</param>
        public abstract void WriteTo(Stream stream);

        /// <summary>
        /// Releases all resources used by the current class instance <see cref="HttpContent"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion


        /// <summary>
        /// Releases the unmanaged (and optionally managed) resources used by the <see cref="HttpContent"/> object.
        /// </summary>
        /// <param name="disposing">The <see langword="true"/> value allows you to release managed and unmanaged resources; 
        /// the <see langword="false"/> value only releases unmanaged resources.</param>
        protected virtual void Dispose(bool disposing) { }
    }
}