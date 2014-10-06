using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortableRest;
using MM.VisualStudioOnline.Models;

namespace MM.VisualStudioOnline.Data.DataAccess
{
    internal interface IVisualStudioOnlineConnector
    {
        /// <summary>
        ///     Defines a method for sending a GET request asynchronously
        /// </summary>
        /// <param name="apiResource"> </param>
        /// <param name="requestBuilder"> </param>
        /// <typeparam name="T"> The type to return from the request </typeparam>
        /// <returns> </returns>
        Task<IEnumerable<T>> GetApiCollectionRequestAsync<T>(string apiResource,
            Action<RestRequest> requestBuilder = null) where T : class;

        /// <summary>
        ///     Defines a method for sending a GET request asynchronously
        /// </summary>
        /// <param name="apiResource"> </param>
        /// <param name="requestBuilder"> </param>
        /// <typeparam name="T"> The type to return from the request </typeparam>
        /// <returns> </returns>
        Task<T> GetApiRequestAsync<T>(string apiResource, Action<RestRequest> requestBuilder = null) where T : class;

        /// <summary>
        ///     Defines a method for sending a GET request asynchronously
        /// </summary>
        /// <param name="apiResource"> </param>
        /// <param name="requestBuilder"> </param>
        /// <returns> </returns>
        Task<ApiQueryResultCollection> GetApiQueryRequestAsync(string apiResource,
            Action<RestRequest> requestBuilder = null);

        /// <summary>
        ///     Defines a method for sending a GET request asynchronously
        /// </summary>
        /// <param name="apiResource"> </param>
        /// <param name="requestBuilder"> </param>
        /// <typeparam name="T"> The type to return from the request </typeparam>
        /// <returns> </returns>
        Task<string> GetRawRequestAsync(string apiResource, Action<RestRequest> requestBuilder = null);
    }
}