namespace keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVK(VaultKeep vkData)
  {
    Vault vault = _vaultsService.GetOne(vkData.VaultId, vkData.CreatorId);

    VaultKeep vk = _repo.CreateVK(vkData);
    vk.CreatedAt = DateTime.Now;
    vk.UpdatedAt = DateTime.Now;
    return vk;
  }
  internal string DeleteVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetOne(vaultKeepId);
    if (vaultKeep.CreatorId != userId) throw new Exception($"You are not Authorized to Delete the VaultKeep at ID: {vaultKeepId}");
    _repo.DeleteVaultKeep(vaultKeepId);
    return $"The VaultKeep at ID: {vaultKeepId} has been Deleted";
  }

  private VaultKeep GetOne(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repo.GetOne(vaultKeepId);
    if (vaultKeep == null) throw new Exception($"The VaultKeep with ID: {vaultKeepId} does not exist!");
    return vaultKeep;
  }
}
