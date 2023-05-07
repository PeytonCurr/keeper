namespace keeper.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVK(VaultKeep vkData)
  {
    string sql = @"
INSERT INTO vaultKeeps
  (creatorId, vaultId, keepId)
values
  (@CreatorId, @VaultId, @KeepId);
  SELECT LAST_INSERT_ID()
;";
    int id = _db.ExecuteScalar<int>(sql, vkData);
    vkData.Id = id;
    return vkData;
  }

  internal List<VaultedKeep> GetKeepsInVault(int vaultId)
  {
    string sql = @"

SELECT
vk.*,
vaultedKeep.*,
COUNT(vk.keepId) AS kept,
acct.*
FROM vaultKeeps vk
JOIN keeps vaultedKeep ON vk.keepId = vaultedKeep.id
JOIN accounts acct ON acct.id = vaultedKeep.creatorId
WHERE vk.vaultId = @vaultId
GROUP BY (vk.id)
;";
    List<VaultedKeep> keeps = _db.Query<VaultKeep, VaultedKeep, Account, VaultedKeep>(sql, (vk, vaultedKeep, acct) =>
    {
      vaultedKeep.Creator = acct;
      vaultedKeep.VaultKeepId = vk.Id;
      return vaultedKeep;
    }, new { vaultId }).ToList();
    return keeps;
  }
}
