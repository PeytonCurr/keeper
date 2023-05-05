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
}
