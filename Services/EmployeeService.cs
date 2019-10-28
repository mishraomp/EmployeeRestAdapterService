using EmployeeRestAdapterService.Entity;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeRestAdapterService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<EmployeeEntity> _employees;

        public EmployeeService(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _employees = database.GetCollection<EmployeeEntity>(settings.EmployeeCollectionName);
        }
        public async Task<List<EmployeeEntity>> Get()
        {
            var result = await _employees.FindAsync(employee => true);
            return result.ToList();
        }


        public EmployeeEntity Get(string id) =>
            _employees.Find<EmployeeEntity>(employee => employee.Id == id).FirstOrDefault();


        public EmployeeEntity Create(EmployeeEntity employee)
        {
            _employees.InsertOne(employee);
            return employee;
        }

        public void Update(string id, EmployeeEntity employeeIn) =>
            _employees.ReplaceOne(employee => employee.Id == id, employeeIn);

        public void Remove(EmployeeEntity employeeIn) =>
            _employees.DeleteOne(employee => employee.Id == employeeIn.Id);

        public void Remove(string id) =>
            _employees.DeleteOne(employee => employee.Id == id);
    }

    public interface IEmployeeService
    {
        EmployeeEntity Create(EmployeeEntity employee);
        void Remove(string id);
        void Remove(EmployeeEntity employeeIn);
        void Update(string id, EmployeeEntity employeeIn);
        EmployeeEntity Get(string id);
        Task<List<EmployeeEntity>> Get();
    }
}
