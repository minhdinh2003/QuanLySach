using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuanLySach.Models;
using QuanLySach.Services;

namespace QuanLySach.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(BookService service) : ControllerBase
{
    private readonly BookService _service = service;

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _service.GetById(id);
        return book == null ? NotFound("Không tìm thấy sách") : Ok(book);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Book book)
    { // kiểm tra tên sách có hợp lệ hay không trước khi thêm sách mới
        try
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                return BadRequest("Tiêu đề sách không được để trống");
            _service.Add(book);
            return StatusCode(201, new { message = "Sách được tạo thành công" });
        }
        catch (Exception ex)
        {
            return BadRequest("Tác giả không tồn tại");
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] Book book)
    {   // kiểm tra sách có tồn tại hay không và tên sách có hợp lệ hay không trước khi cập nhật

        if (string.IsNullOrWhiteSpace(book.Title))
            return BadRequest("Tiêu đề sách không được để trống");
        if (_service.GetById(book.Id) == null)
            return NotFound("Không tìm thấy sách");

        try
        {
            _service.Update(book);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest("Tác giả không tồn tại");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)// kiểm tra sách có tồn tại hay không rồi xóa sách theo id 
    {
        if (_service.GetById(id) == null) return NotFound("không tìm thấy sách");
        _service.Delete(id);
        return NoContent();
    }
}
