namespace keeper.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
INSERT INTO vaults
  (name, description, img, creatorId)
values
  (@name, @description, @img, @creatorId);
  SELECT LAST_INSERT_ID()
;";

    int Id = _db.ExecuteScalar<int>(sql, vaultData);
    vaultData.Id = Id;
    return vaultData;
  }

  internal Vault GetOne(int vaultId)
  {
    string sql = @"
SELECT
v.*,
acct.*
FROM vaults v
JOIN accounts acct ON v.CreatorId = acct.id
WHERE v.id = @vaultId
;";
    Vault vault = _db.Query<Vault, Account, Vault>(sql, (v, acct) =>
    {
      v.Creator = acct;
      return v;
    }, new { vaultId }).FirstOrDefault();
    return vault;
  }
}