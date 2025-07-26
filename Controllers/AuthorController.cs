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
    { // kiểm tra tên tác giả có hợp lệ hay không trước khi thêm tác giả mới
        try
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                return BadRequest("Tên tác giả không được để trống");
            _service.Add(author);
            return StatusCode(201, new { message = "Tác giả được tạo thành công" }); 
        }
        catch (Exception ex)
        {
            return BadRequest($"Lỗi khi tạo tác giả: {ex.Message}");
        }
        
    }

    [HttpPut]
    public IActionResult Update([FromBody] Author author)
    { // kiểm tra tác giả có tồn tại hay không và tên tác giả có hợp lệ hay không trước khi cập nhật
        if (_service.GetById(author.Id) == null)
            return NotFound("Không tìm thấy tác giả");
        if (string.IsNullOrWhiteSpace(author.Name))
            return BadRequest("Tên tác giả không được để trống");
        _service.Update(author);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)// kiểm tra tác giả có tồn tại hay không rồi xóa tác giả theo id
    {
        if (_service.GetById(id) == null) return NotFound("Không tìm thấy tác giả");
        _service.Delete(id);
        return NoContent();
    }
}
