using System;

namespace MM.VisualStudioOnline
{
    /// <summary>
    ///     Represents errors that occur during a Visual Studio Online API execution process
    /// </summary>
    public class VisualStudioOnlineApiException : Exception
    {
        #region Constructors

        /// <summary>
        ///     Creates a new instance of <see cref="VisualStudioOnlineApiException" />
        /// </summary>
        /// <param name="message"> </param>
        public VisualStudioOnlineApiException(string message) : base(message) { }

        /// <summary>
        ///     Creates a new instance of <see cref="VisualStudioOnlineApiException" />
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="innerException"> </param>
        public VisualStudioOnlineApiException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        ///     Creates a new instance of <see cref="VisualStudioOnlineApiException" /> , providing a string.Format for the message
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="args"> </param>
        public VisualStudioOnlineApiException(string message, params object[] args) : base(string.Format(message, args)) { }

        /// <summary>
        ///     Creates a new instance of <see cref="VisualStudioOnlineApiException" /> , providing a string.Format for the message
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="innerException"> </param>
        /// <param name="args"> </param>
        public VisualStudioOnlineApiException(string message, Exception innerException, params object[] args)
            : base(string.Format(message, args), innerException) { }

        #endregion
    }
}