using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MM.VisualStudioOnline.Data;
using MM.VisualStudioOnline.Data.DataAccess;
using MM.VisualStudioOnline.IntegrationTests.Helpers;

namespace MM.VisualStudioOnline.IntegrationTests.Data
{
    public class ProjectRepositoryIntegrationTests
    {
        #region Nested type: GetBuildDefinitionsMethod

        [TestClass]
        public class GetBuildDefinitionsMethod
        {
            private IProjectsRepository _repository;

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
                var results = await _repository.GetProjectsAsync();

                // ** Assert **
                Assert.IsNotNull(results);
            }

            #endregion
        }

        #endregion
    }
}