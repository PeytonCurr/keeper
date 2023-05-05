namespace keeper.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;
  private readonly VaultKeepsService _vaultKeepsService;

  public VaultsService(VaultsRepository repo, VaultKeepsService vaultKeepsService)
  {
    _repo = repo;
    _vaultKeepsService = vaultKeepsService;
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

  internal Vault EditVault(Vault vaultData)
  {
    Vault vault = GetOne(vaultData.Id);
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
    Vault vault = GetOne(vaultId);
    if (vault.CreatorId != userId) throw new Exception($"You are not Authorized to delete the Vault named: {vault.Name}");

    _repo.DeleteVault(vaultId);

    return $"You have Deleted the Vault Named: {vault.Name}";
  }

  internal List<VaultedKeep> GetKeepsInVault(int vaultId, string userId)
  {
    Vault vault = GetOne(vaultId);
    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception($"You are not Authorized to view the Vault ID: {vaultId}");
    List<VaultedKeep> keeps = _vaultKeepsService.GetKeepsInVault(vaultId);
    return keeps;
  }
}
