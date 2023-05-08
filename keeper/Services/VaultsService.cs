namespace keeper.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly KeepsService _keepsService;

  public VaultsService(VaultsRepository repo, VaultKeepsService vaultKeepsService, KeepsService keepsService)
  {
    _repo = repo;
    _vaultKeepsService = vaultKeepsService;
    _keepsService = keepsService;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repo.CreateVault(vaultData);
    vault.CreatedAt = DateTime.Now;
    vault.UpdatedAt = DateTime.Now;
    return vault;
  }

  internal Vault GetOne(int vaultId, string userId)
  {
    Vault vault = _repo.GetOne(vaultId);
    if (vault == null) throw new Exception($"The Vault at ID: {vaultId} does not exist!");
    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception("You are not Authorized to Get this Private Vault");
    return vault;
  }

  internal Vault EditVault(Vault vaultData)
  {
    Vault vault = GetOne(vaultData.Id, vaultData.CreatorId);
    if (vault.CreatorId != vaultData.CreatorId) throw new Exception("You do not have authorization to Edit the Vault named: vault.name");

    vault.Name = vaultData.Name ?? vault.Name;
    vault.Description = vaultData.Description ?? vault.Name;
    vault.Img = vaultData.Img ?? vault.Img;
    vault.IsPrivate = vaultData.IsPrivate != null ? vaultData.IsPrivate : vault.IsPrivate;

    _repo.EditVault(vault);

    return vault;
  }

  internal string DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetOne(vaultId, userId);
    if (vault.CreatorId != userId) throw new Exception($"You are not Authorized to delete the Vault named: {vault.Name}");

    _repo.DeleteVault(vaultId);

    return $"You have Deleted the Vault Named: {vault.Name}";
  }

  internal List<VaultedKeep> GetKeepsInVault(int vaultId, string userId)
  {
    Vault vault = GetOne(vaultId, userId);
    List<VaultedKeep> keeps = _keepsService.GetKeepsInVault(vaultId);
    return keeps;
  }

  internal List<Vault> GetUsersVaults(string profileId, string userId)
  {
    List<Vault> vaults = _repo.GetUsersVaults(profileId);
    if (vaults == null) throw new Exception("This User does not have any Vaults");

    List<Vault> filteredVaults = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userId);

    return filteredVaults;
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    List<Vault> vaults = _repo.GetMyVaults(userId);
    if (vaults == null) throw new Exception("You do not have any Vaults");
    return vaults;
  }
}
