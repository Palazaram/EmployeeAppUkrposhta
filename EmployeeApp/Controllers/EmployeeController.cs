using EmployeeApp.Models;
using EmployeeApp.Repositories.CompanyRepository;
using EmployeeApp.Repositories.DepartmentRepository;
using EmployeeApp.Repositories.EmployeeRepository;
using EmployeeApp.Repositories.PositionRepository;
using EmployeeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employeeRepository;
        private readonly ICompany _companyRepository;
        private readonly IPosition _positionRepository;
        private readonly IDepartment _departmentRepository;

        public EmployeeController(IEmployee employeeRepository, ICompany company, 
            IPosition position, IDepartment department)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = company;
            _positionRepository = position;
            _departmentRepository = department;
        }

        public async Task<IActionResult> Index(string searchString = "", int? companyId = null, int? departmentId = null, int? positionId = null)
        {
            try
            {
                IEnumerable<Company> companies = await _companyRepository.GetAllCompaniesAsync();
                IEnumerable<Department> departments = await _departmentRepository.GetAllDepartmentsAsync();
                IEnumerable<Position> positions = await _positionRepository.GetAllPositionsAsync();
                IEnumerable<Employee> employees = await _employeeRepository.GetAllEmployeesAsync(searchString, companyId, departmentId, positionId);

                EmployeeIndexViewModel employeeIndexViewModel = new EmployeeIndexViewModel 
                {
                    Employees = employees,
                    Companies = companies,
                    Departments = departments,
                    Positions = positions,
                    SearchString = searchString,
                    CompanyId = companyId,
                    DepartmentId = departmentId,
                    PositionId = positionId
                };

                return View(employeeIndexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                IEnumerable<Company> companies = await _companyRepository.GetAllCompaniesAsync();
                IEnumerable<Position> positions = await _positionRepository.GetAllPositionsAsync();
                IEnumerable<Department> departments = await _departmentRepository.GetAllDepartmentsAsync();

                ViewBag.Companies = new SelectList(companies, "Id", "Name");
                ViewBag.Positions = new SelectList(positions, "Id", "Name");
                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                IEnumerable<Company> companies = await _companyRepository.GetAllCompaniesAsync();
                IEnumerable<Position> positions = await _positionRepository.GetAllPositionsAsync();
                IEnumerable<Department> departments = await _departmentRepository.GetAllDepartmentsAsync();

                ViewBag.Companies = new SelectList(companies, "Id", "Name");
                ViewBag.Positions = new SelectList(positions, "Id", "Name");
                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                if (id != null)
                {
                    Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                    if (employee != null)
                    {
                        return View(employee);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                await _employeeRepository.AddEmployeeAsync(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            try
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Employee employee)
        {
            try
            {
                if (employee.Id != null)
                {
                    await _employeeRepository.DeleteEmployeeAsync(employee.Id);
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Payroll(string searchString = "", int? companyId = null, int? departmentId = null, int? positionId = null)
        {
            try
            {
                IEnumerable<Company> companies = await _companyRepository.GetAllCompaniesAsync();
                IEnumerable<Department> departments = await _departmentRepository.GetAllDepartmentsAsync();
                IEnumerable<Position> positions = await _positionRepository.GetAllPositionsAsync();
                IEnumerable<Employee> employees = await _employeeRepository.GetAllEmployeesAsync(searchString, companyId, departmentId, positionId);

                EmployeeIndexViewModel employeeIndexViewModel = new EmployeeIndexViewModel
                {
                    Employees = employees,
                    Companies = companies,
                    Departments = departments,
                    Positions = positions,
                    SearchString = searchString,
                    CompanyId = companyId,
                    DepartmentId = departmentId,
                    PositionId = positionId
                };

                return View(employeeIndexViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ExportToTxt(string searchString = "", int? companyId = null, int? departmentId = null, int? positionId = null)
        {
            try
            {
                IEnumerable<Employee> employees = await _employeeRepository.GetAllEmployeesAsync(searchString, companyId, departmentId, positionId);

                // Сериализация данных в формат JSON
                string json = JsonConvert.SerializeObject(employees);

                // Задаем имя файла и тип контента
                string fileName = "EmployeeData.txt";
                string contentType = "application/json";

                // Сохраняем JSON в текстовый файл
                System.IO.File.WriteAllText(fileName, json);

                // Чтение файла и возврат как результата
                byte[] fileBytes = System.IO.File.ReadAllBytes(fileName);
                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}
