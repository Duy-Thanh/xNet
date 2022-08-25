using System;
using System.Collections.Generic;
using System.Text;

namespace xNet
{
     /// <summary>
     /// Represents the request body as request parameters.
     /// </summary>
     public class FormUrlEncodedContent : BytesContent
    {
/// <summary>
        /// Initializes a new instance of the class <see cref="FormUrlEncodedContent"/>.
        /// </summary>
        /// <param name="content">Content of the request body as request parameters.</param>
        /// <param name="dontEscape">Indicates whether parameter values ​​should be encoded.</param>
        /// <param name="encoding">Encoding used to convert request parameters. If the parameter value is <see langword="null"/> then the value <see cref="System.Text.Encoding.UTF8"/> will be used.</param>
        /// <exception cref="System.ArgumentNullException">The value of the <paramref name="content"/> parameter is <see langword="null"/>.</exception>
        /// <remarks>The default content type is 'application/x-www-form-urlencoded'.</remarks>
        public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> content, bool dontEscape = false, Encoding encoding = null)
        {
            #region Parameter Check

            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            #endregion

            string queryString = Http.ToPostQueryString(content, dontEscape, encoding);

            _content = Encoding.ASCII.GetBytes(queryString);
            _offset = 0;
            _count = _content.Length;

            _contentType = "application/x-www-form-urlencoded";
        }
    }
}