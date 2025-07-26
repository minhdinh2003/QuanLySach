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
        return author == null ? NotFound("Không tìm thấy tác giả") : Ok(author);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Author author)
    {
        try
        {
            if (_service.GetById(author.Id) != null)
                return BadRequest("Tác giả đã tồn tại với ID này");
            if (string.IsNullOrWhiteSpace(author.Name))
                return BadRequest("Tên tác giả không được để trống");
            
        }
        catch (Exception ex)
        {
            return BadRequest($"Lỗi khi tạo tác giả: {ex.Message}");
        }
        _service.Add(author);
        return Ok(new { message = "Tạo tác giả thành công" });
    }

    [HttpPut]
    public IActionResult Update([FromBody] Author author)
    {
        if (_service.GetById(author.Id) == null) return NotFound("Không tìm thấy tác giả");
        if (string.IsNullOrWhiteSpace(author.Name))
            return BadRequest("Tên tác giả không được để trống");
        _service.Update(author);
        return Ok(new { message = "Cập nhật tác giả thành công" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_service.GetById(id) == null) return NotFound("Không tìm thấy tác giả");
        _service.Delete(id);
        return Ok(new { message = "Tác giả đã được xóa" });
    }
}
