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
    {
        try
        {
            _service.Add(book);
            return Ok(new { message = "Sách được tạo thành công" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] Book book)
    {
        if (_service.GetById(book.Id) == null) return NotFound("Không tìm thấy sách");
        _service.Update(book);
        return Ok(new { message = "Sách đã được cập nhật" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_service.GetById(id) == null) return NotFound("không tìm thấy sách");
        _service.Delete(id);
        return Ok(new { message = "Sách đã được xóa" });
    }
}
