using Core.Filter;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Core.CommandDto;
using Core.ModelDto;
using Infrastracture.Service;
using Core.Command.Update;

namespace My_Real_Estate.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAddress([FromQuery] AddressFilter filter)
    {
        var address = await _addressService.GetAddressDto(filter);
        if (address != null)
        {
            return Json(new { Result = address });
        }
        return NotFound(new { Message = "Address or Addresses not found" });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto addressDto)
    {
        var addressId = await _addressService.CreateAddressDto(addressDto);
        if (addressId == null)
        {
            return BadRequest(new { Message = "Create address fail" });
        }
        return Ok(new { AddressId = addressId });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddress updateAddress, string addressId)
    {
        bool update = await _addressService.UpdateAddress(updateAddress, addressId);
        if (!update)
        {
            return BadRequest(new { Message = "Address update fail" });
        }
        return Ok(new { Result = update });
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteAddress(string addressId)
    {
        bool delete = await _addressService.DeleteAddress(addressId);
        if (!delete)
        {
            return BadRequest(new { Message = "Address does not exist." });
        }
        return Ok(new { Result = delete });
    }
}
