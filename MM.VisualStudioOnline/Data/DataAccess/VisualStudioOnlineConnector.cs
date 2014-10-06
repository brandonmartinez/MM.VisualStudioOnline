using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PortableRest;
using MM.VisualStudioOnline.Models;

namespace MM.VisualStudioOnline.Data.DataAccess
{
    /// <summary>
    ///     A connector to the Visual Studio Online API
    /// </summary>
    /// <remarks>
    ///     More info: http://www.visualstudio.com/integrate/get-started/get-started-rest-basics-vsi
    /// </remarks>
    internal class VisualStudioOnlineConnector : RestClient, IVisualStudioOnlineConnector
    {
        private const string VISUAL_STUDIO_ONLINE_URL_TEMPLATE =
            @"https://{0}.visualstudio.com/DefaultCollection/_apis/";

        private const string VISUAL_STUDIO_ONLINE_API_VERSION = @"1.0-preview";

        private const string VISUAL_STUDIO_ONLINE_USER_AGENT = @"Skyline Visual Studio Online API Library";

        private const string VISUAL_STUDIO_ONLINE_ACCEPT_HEADER_TEMPLATE = @"application/json;api-version={0}";

        public VisualStudioOnlineConnector(string accountName, string username, string password)
        {
            BaseUrl = string.Format(VISUAL_STUDIO_ONLINE_URL_TEMPLATE, accountName);
            UserAgent = VISUAL_STUDIO_ONLINE_USER_AGENT;

            setHeaders(username, password);
        }

        #region IVisualStudioOnlineConnector Members

        public async Task<IEnumerable<T>> GetApiCollectionRequestAsync<T>(string apiResource,
            Action<RestRequest> requestBuilder = null) where T : class
        {
            return await requestAsync(apiResource, async request =>
            {
                var result = await ExecuteAsync<ApiCollection<T>>(request);

                return result != null ? result.Value : null;
            }, requestBuilder);
        }

        /// <summary>
        ///     Defines a method for sending a GET request asynchronously
        /// </summary>
        /// <param name="apiResource"> </param>
        /// <param name="requestBuilder"> </param>
        /// <typeparam name="T"> The type to return from the request </typeparam>
        /// <returns> </returns>
        public async Task<T> GetApiRequestAsync<T>(string apiResource, Action<RestRequest> requestBuilder = null)
            where T : class
        {
            return await requestAsync(apiResource, async request =>
            {
                var result = await ExecuteAsync<T>(request);

                return result;
            }, requestBuilder);
        }

        public async Task<ApiQueryResultCollection> GetApiQueryRequestAsync(string apiResource,
            Action<RestRequest> requestBuilder = null)
        {
            return await requestAsync(apiResource, async request =>
            {
                var result = await ExecuteAsync<ApiQueryResultCollection>(request);

                return result;
            }, requestBuilder);
        }

        public async Task<string> GetRawRequestAsync(string apiResource, Action<RestRequest> requestBuilder = null)
        {
            return await requestAsync(apiResource, async request =>
            {
                var result = await SendAsync<string>(request);

                return result.Content;
            }, requestBuilder);
        }

        #endregion

        private static async Task<T> requestAsync<T>(string apiResource, Func<RestRequest, Task<T>> execute,
            Action<RestRequest> requestBuilder) where T : class
        {
            var request = new RestRequest(apiResource);

            if(requestBuilder != null)
            {
                requestBuilder(request);
            }

            try
            {
                var result = await execute(request);
                if(result != null)
                {
                    return result;
                }
            }
            catch(Exception ex)
            {
                // TODO: log or something
            }

            return null;
        }

        private void setHeaders(string username, string password)
        {
            var authName = username + ":" + password;
            var authBytes = Encoding.UTF8.GetBytes(authName);
            var encodedAuth = Convert.ToBase64String(authBytes);

            AddHeader("Accept",
                string.Format(VISUAL_STUDIO_ONLINE_ACCEPT_HEADER_TEMPLATE, VISUAL_STUDIO_ONLINE_API_VERSION));
            AddHeader("Authorization", string.Format("Basic {0}", encodedAuth));
        }
    }
}