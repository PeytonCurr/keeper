namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  public readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth, KeepsService keepsService, VaultsService vaultsService)
  {
    _profilesService = profilesService;
    _auth = auth;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetUserProfile(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetUserProfile(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetUsersKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetUsersKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> GetUsersVaults(string profileId)
  {
    try
    {
      List<Vault> vaults = _vaultsService.GetUsersVaults(profileId);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
