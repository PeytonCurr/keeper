namespace keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;

  public VaultKeepsService(VaultKeepsRepository repo)
  {
    _repo = repo;
  }

  internal VaultKeep CreateVK(VaultKeep vkData)
  {
    VaultKeep vk = _repo.CreateVK(vkData);
    vk.CreatedAt = DateTime.Now;
    vk.UpdatedAt = DateTime.Now;
    return vk;
  }

  internal List<VaultedKeep> GetKeepsInVault(int vaultId)
  {
    List<VaultedKeep> keeps = _repo.GetKeepsInVault(vaultId);
    return keeps;
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
