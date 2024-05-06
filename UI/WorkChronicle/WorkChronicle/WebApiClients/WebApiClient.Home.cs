namespace WorkChronicle.WebApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeFirst.Models;
    using CodeFirst.Models.Entities;

    using ViewModels;

    public partial class WebApiClient
    {
        public List<TestTable> GetTestTables()
        {
            return Get<List<TestTable>>("/Table/GetList");
        }
    }
}
