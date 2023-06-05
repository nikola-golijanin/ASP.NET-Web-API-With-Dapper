using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly DapperContext _context;
    private readonly Lazy<ICompanyRepository> _companyRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;

    public RepositoryManager(DapperContext context)
    {
        _context = context;
        _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(_context));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_context));
    }

    public ICompanyRepository Company => _companyRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;
}