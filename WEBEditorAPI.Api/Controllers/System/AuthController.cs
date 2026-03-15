using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Application.DTOs.System;
using WEBEditorAPI.Application.Interfaces;

namespace WEBEditorAPI.Api.Controllers.System
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUseCase<AuthRequest, AuthResponse> Login;
        public AuthController(IUseCase<AuthRequest, AuthResponse> login)
        {
            Login = login;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
        {
            var result = await Login.ExecuteAsync(request);
            return Ok(result);
        }
    }
}
