namespace keeper.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account accountData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = accountData.Name ?? original.Name;
    original.Picture = accountData.Picture ?? original.Picture;
    original.CoverImg = accountData.CoverImg ?? original.CoverImg;
    Account account = _repo.Edit(accountData);
    return account;
  }
}
