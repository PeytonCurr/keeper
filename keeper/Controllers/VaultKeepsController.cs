namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVK([FromBody] VaultKeep vkData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vkData.CreatorId = userInfo.Id;
      VaultKeep vk = _vaultKeepsService.CreateVK(vkData);
      return Ok(vk);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
