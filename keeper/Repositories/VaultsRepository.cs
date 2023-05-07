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
  internal void EditVault(Vault vault)
  {
    string sql = @"
UPDATE vaults
SET
name = @Name,
description = @description,
img = @Img,
isPrivate = @IsPrivate
WHERE id = @id
;";
    _db.Execute(sql, vault);
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = @"DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
    _db.Execute(sql, new { vaultId });
  }

  internal List<Vault> GetUsersVaults(string profileId)
  {
    string sql = @"
SELECT
profile.*,
v.*
FROM accounts profile
JOIN vaults v ON v.creatorId = profile.id
WHERE profile.id = @profileId
;";
    List<Vault> vaults = _db.Query<Profile, Vault, Vault>(sql, (profile, v) =>
    {
      v.Creator = profile;
      return v;
    }, new { profileId }).ToList();
    return vaults;
  }
}
