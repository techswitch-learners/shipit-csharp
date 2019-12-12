using System.Web.Http;
using ShipIt.Models.ApiModels;
using ShipIt.Repositories;

namespace ShipIt.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public CompanyResponse Get(string gcp)
        {
            var company = new Company(companyRepository.GetCompany(gcp));
            return new CompanyResponse(company);
        }
    }
}
