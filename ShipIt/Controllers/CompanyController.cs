using System.Collections.Generic;
using System.Web.Http;
using ShipIt.Exceptions;
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

        public void Post([FromBody]AddCompaniesRequest requestModel)
        {
            List<Company> companies = requestModel.companies;

            if (companies.Count == 0)
            {
                throw new MalformedRequestException("Expected at least one <company> tag");
            }

            companyRepository.AddCompanies(companies);
        }
    }
}
