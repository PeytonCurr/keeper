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
  k.*,
  COUNT(vk.keepId) AS kept,
  vk.id AS VaultKeepId,
  acct.*
  FROM keeps k
  JOIN accounts acct ON acct.id = k.creatorId
  LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
  WHERE vk.vaultId = @vaultId
  GROUP BY (k.id)
;";
    List<VaultedKeep> keeps = _db.Query<VaultedKeep, Account, VaultedKeep>(sql, (k, acct) =>
    {
      k.Creator = acct;
      return k;
    }, new { vaultId }).ToList();
    return keeps;
  }
}
