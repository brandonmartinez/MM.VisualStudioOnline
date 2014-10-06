using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MM.VisualStudioOnline.Data;
using MM.VisualStudioOnline.Data.DataAccess;
using MM.VisualStudioOnline.IntegrationTests.Helpers;

namespace MM.VisualStudioOnline.IntegrationTests.Data
{
    public class WorkItemsRepositoryIntegrationTests
    {
        #region Nested type: GetWorkItemsAsyncMethod

        [TestClass]
        public class GetWorkItemsAsyncMethod
        {
            private IWorkItemsRepository _repository;

            #region Test Facts

            [TestInitialize]
            public void TestInitialize()
            {
                var connector = new VisualStudioOnlineConnector(Configuration.VisualStudioOnlineAccountName,
                    Configuration.VisualStudioOnlineUserName, Configuration.VisualStudioOnlinePassword);

                _repository = new VisualStudioOnlineApiRepository(connector);
            }

            [TestMethod]
            public async Task CanConnect()
            {
                // ** Arrange **

                // ** Act **
                var results = await _repository.GetWorkItemsQueryAsync("JetPubs-JetStream");

                // ** Assert **
                Assert.IsNotNull(results);
            }

            #endregion
        }

        #endregion
    }
}