using AutoMapper;
using Xunit;

namespace DAO.Tests.ServicesTests
{
    public class AllProfilesTest : IClassFixture<DIFixture>
    {
        private readonly DIFixture _dIFixture;


        public AllProfilesTest(DIFixture fixture)
        {
            _dIFixture = fixture;
        }

        [Fact]
        public void TestProfiles()
        {
            _dIFixture.DIContainer.Locate<IConfigurationProvider>().AssertConfigurationIsValid();
        }

    }
}
