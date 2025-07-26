using Microsoft.AspNetCore.Mvc;
using QuanLySach.Models;
using QuanLySach.Services;

namespace QuanLySach.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController(AuthorService service) : ControllerBase
{
    private readonly AuthorService _service = service;

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var author = _service.GetById(id);
        return author == null ? NotFound() : Ok(author);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Author author)
    {
        _service.Add(author);
        return Ok(new { message = "Tạo tác giả thành công" });
    }

    [HttpPut]
    public IActionResult Update([FromBody] Author author)
    {
        if (_service.GetById(author.Id) == null) return NotFound("Không tìm thấy tác giả");
        _service.Update(author);
        return Ok(new { message = "Cập nhật tác giả thành công" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_service.GetById(id) == null) return NotFound();
        _service.Delete(id);
        return Ok(new { message = "Author deleted" });
    }
}
