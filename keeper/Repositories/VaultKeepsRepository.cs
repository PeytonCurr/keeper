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
}
