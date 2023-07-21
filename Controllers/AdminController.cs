using BookManagement.BLL.Services;
using BookManagement.Entity.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.IO;



namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        IStudentService _studentService;
        IBookAllocationServices _bookAllocationServices;
        IReturnBookServices _returnBookServices;

        public AdminController(IStudentService studentService, IBookAllocationServices bookAllocationServices, IReturnBookServices returnBookServices)
        {
            _studentService = studentService;
            _bookAllocationServices = bookAllocationServices;
            _returnBookServices = returnBookServices;
        }

        [HttpGet("getallstudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await _studentService.GetAll();
            return Ok(response);
        }

        [HttpPost("addstudent")]
        public async Task<IActionResult> AddStudent(StudentDTO student)
        {
            await _studentService.Add(student);
            return Ok("Student Successfully Added!!!");
        }
        [HttpPost("deletestudent/{id}")]
        public async Task<IActionResult> RemoveStudent(Guid id)
        {
            await _studentService.Delete(id);
            return Ok("Student is removed Successfully ");
        }

        [HttpPost("updatestudent")]
        public async Task<IActionResult> EditStudent(Guid id, StudentDTO student)
        {
            await _studentService.Update(id, student);

            return Ok("User Updated Successfully !!!");

        }

        [HttpPost("allocatedbook")]
        public async Task<IActionResult> AllocateBook(BookAllocationDTO bookAllocationEntry)
        {
           if ( await _bookAllocationServices.AllocateBook(bookAllocationEntry))
            {
                return Ok("Book Allocation Successfully ");
            }


            return BadRequest("SomeThing Went Wrong");
          
        }

        [HttpGet("getallocatedbook")]
        public async Task<IActionResult> GetAllocateBook()
        {
          var allocatedBookList=  await _bookAllocationServices.GetAllAllocatedBook();
          return Ok(allocatedBookList);
        }

        [HttpGet("getallocatedbookdetails")]
        public async Task<IActionResult> GetAllocateBookDetails()
        {
            var allocatedBookList = await _bookAllocationServices.GetAllAllocatedBookWithDetails();
            return Ok(allocatedBookList);
        }


        [HttpPost("returnbook")]

        public async Task<IActionResult> ReturnBook(ReturnBookDTO returnBook)
        {
            if (await _returnBookServices.ReturnBook(returnBook))
            {
                return Ok("Book has been returned");
            }
            return BadRequest("Something Went wrong");
        }
    
    
    }
}