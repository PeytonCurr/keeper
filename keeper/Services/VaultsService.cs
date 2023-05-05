namespace keeper.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repo.CreateVault(vaultData);
    vault.CreatedAt = DateTime.Now;
    vault.UpdatedAt = DateTime.Now;
    return vault;
  }

  internal Vault GetOne(int vaultId)
  {
    Vault vault = _repo.GetOne(vaultId);
    if (vault == null) throw new Exception($"The Vault at ID: {vaultId} does not exist!");
    return vault;
  }
}
