using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Dtos;
using MyWebApplication.Models;
using MyWebApplication.Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> GetUsers()
    {
        return Ok(await _userService.GetAllUsers());
    }

    [HttpGet("active")]
    public async Task<ActionResult<List<UserResponseDto>>> GetActive()
    {
        return Ok(await _userService.GetActiveUsers());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetId(int id)
    {
        if (id <= 0) return BadRequest();

        var user = await _userService.GetById(id);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> PostCreate([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new User { Name = dto.Name, Email = dto.Email };
        var createdUser = await _userService.Create(user);

        return CreatedAtAction(nameof(GetId), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new User { Name = dto.Name, Email = dto.Email, IsActive = dto.IsActive };
        var updatedUser = await _userService.Update(id, user);

        if (updatedUser == null) return NotFound();

        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest();
        if (!await _userService.SoftDelete(id)) return NotFound();

        return NoContent();
    }
}