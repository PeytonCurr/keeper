namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth;

  public VaultsController(VaultsService vaultsService, Auth0Provider auth)
  {
    _vaultsService = vaultsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.CreateVault(vaultData);
      vault.Creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetOne(int vaultId)
  {
    try
    {
      Vault vault = _vaultsService.GetOne(vaultId);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPut("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      vaultData.Id = vaultId;
      Vault vault = _vaultsService.EditVault(vaultData);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
