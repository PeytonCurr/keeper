namespace keeper.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  internal Profile GetUserProfile(string profileId)
  {
    Profile profile = _repo.GetUserProfile(profileId);
    return profile;
  }
}
