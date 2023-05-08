namespace keeper.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repo.CreateKeep(keepData);
    keep.CreatedAt = DateTime.Now;
    keep.UpdatedAt = DateTime.Now;
    return keep;
  }
  internal List<Keep> GetAll()
  {
    List<Keep> keeps = _repo.GetAll();
    return keeps;
  }

  internal Keep GetOne(int keepId)
  {
    Keep keep = _repo.GetOne(keepId);
    if (keep == null) throw new Exception($"The keep you are tying to get, with the ID: {keepId}, does not exist!");
    return keep;
  }
  internal Keep EditKeep(Keep keepData)
  {
    Keep keep = GetOne(keepData.Id);
    if (keep.CreatorId != keepData.CreatorId) throw new Exception($"You are not authorized to edit the keep with the name: {keep.Name}");

    keep.Name = keepData.Name ?? keep.Name;
    keep.Description = keepData.Description ?? keep.Name;
    keep.Img = keepData.Img ?? keep.Img;

    _repo.EditKeep(keep);

    return keep;
  }

  internal string DeleteKeep(int keepId, string userId)
  {
    Keep keep = GetOne(keepId);
    if (keep.CreatorId != userId) throw new Exception($"You are not authorized to Delete the keep with the name: {keep.Name}");
    _repo.DeleteKeep(keepId);
    return $"You have deleted keep: {keep.Name}";
  }

  internal List<Keep> GetUsersKeeps(string profileId)
  {
    List<Keep> keeps = _repo.GetUsersKeeps(profileId);
    if (keeps == null) throw new Exception("This User does not have any Keeps");
    return keeps;
  }

  internal List<VaultedKeep> GetKeepsInVault(int vaultId)
  {
    List<VaultedKeep> keeps = _repo.GetKeepsInVault(vaultId);
    return keeps;
  }
}
