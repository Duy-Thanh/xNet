using System;
using System.IO;

namespace xNet
{
    /// <summary>
    /// Represents the request body as bytes.
    /// </summary>
    public class BytesContent : HttpContent
    {
        #region Fields (protected)

        /// <summary>The content of the request body.</summary>
        protected byte[] _content;
        /// <summary>The byte offset of the content of the request body.</summary>
        protected int _offset;
        /// <summary>The number of bytes of content to send.</summary>
        protected int _count;

        #endregion


        #region Constructors (public)

        /// <summary>
        /// Initializes a new instance of the class <see cref="BytesContent"/>.
        /// </summary>
        /// <param name="content">The content of the request body.</param>
        /// <exception cref="System.ArgumentNullException">The value of the <paramref name="content"/> parameter is <see langword="null"/>.</exception>
        /// <remarks>The default content type is: 'application/octet-stream'.</remarks>
        public BytesContent(byte[] content)
            : this(content, 0, content.Length) { }

        /// <summary>
        /// Initializes a new instance of the class <see cref="BytesContent"/>.
        /// </summary>
        /// <param name="content">The content of the request body.</param>
        /// <param name="offset">The byte offset for the content.</param>
        /// <param name="count">The number of bytes sent from the content.</param>
        /// <exception cref="System.ArgumentNullException">The value of the <paramref name="content"/> parameter is <see langword="null"/>.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="offset"/> parameter value is less than 0.
        /// -or-
        /// The value of the <paramref name="offset"/> parameter is greater than the length of the content.
        /// -or-
        /// The value of the <paramref name="count"/> parameter is less than 0.
        /// -or-
        /// <paramref name="count"/> value is greater than (content length - offset).</exception>
        /// <remarks>The default content type is 'application/octet-stream'.</remarks>
        public BytesContent(byte[] content, int offset, int count)
        {
            #region Parameter Check

            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (offset < 0)
            {
                throw ExceptionHelper.CanNotBeLess("offset", 0);
            }

            if (offset > content.Length)
            {
                throw ExceptionHelper.CanNotBeGreater("offset", content.Length);
            }

            if (count < 0)
            {
                throw ExceptionHelper.CanNotBeLess("count", 0);
            }

            if (count > (content.Length - offset))
            {
                throw ExceptionHelper.CanNotBeGreater("count", content.Length - offset);
            }

            #endregion

            _content = content;
            _offset = offset;
            _count = count;

            _contentType = "application/octet-stream";
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the class <see cref="BytesContent"/>.
        /// </summary>
        protected BytesContent() { }


        #region Methods (public)

        /// <summary>
        /// Counts and returns the length of the request body in bytes.
        /// </summary>
        /// <returns>The length of the request body in bytes.</returns>
        public override long CalculateContentLength()
        {
            return _content.LongLength;
        }

        /// <summary>
        /// Writes the request body data to the stream.
        /// </summary>
        /// <param name="stream">The stream where the request body data will be written.</param>
        public override void WriteTo(Stream stream)
        {
            stream.Write(_content, _offset, _count);
        }

        #endregion
    }
}
