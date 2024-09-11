using Bogus;
using dotnet_api.Data;
using System.Security.AccessControl;
namespace dotnet_api.FakeData
{
    public class FakeData
    {
        private static List<Advert> fakeAdvertData;
        private static List<User> fakeUsers;
        private static List<CV> fakeCVs;

        public static List<User> getUsers()
        {
            fakeUsers = new Faker<User>()
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .Generate(10);

        return fakeUsers; 
        }
    }
}
