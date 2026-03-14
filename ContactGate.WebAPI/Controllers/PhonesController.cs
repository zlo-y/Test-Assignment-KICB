using ContactGate.Application.DTOs;
using ContactGate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactGate.WebAPI.Controllers;

// 
// Контроллер для управления телефонными номерами. Представляет CRUD операции для телефонных номеров.
// 
[ApiController]
[Route("api/[controller]")]
public class PhonesController : ControllerBase
{
    private readonly IPhoneNumberService _phoneService;

    public PhonesController(IPhoneNumberService phoneService)
    {
        _phoneService = phoneService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhoneNumberDto>>> GetAll()
    {
        var phones = await _phoneService.GetAllPhonesNumberAsync();
        return Ok(phones);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<PhoneNumberDto>>> GetByUserId(int userId)
    {
        var phones = await _phoneService.GetPhoneNumberByIdAsync(userId);
        return Ok(phones);
    }

    [HttpPost]
    public async Task<ActionResult<PhoneNumberDto>> Create(CreatePhoneNumberDto dto)
    {
        var phone = await _phoneService.CreatePhoneNumberAsync(dto);
        return Ok(phone);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePhoneNumberDto dto)
    {
        await _phoneService.UpdatePhoneNumberAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _phoneService.DeletePhoneNumberAsync(id);
        return NoContent();
    }
}