namespace keeper.Repositories;

public class KeepsRepository

{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
INSERT INTO keeps
  (name, description, img, views, creatorId)
values
  (@name, @description, @img, @views, @creatorId);
  SELECT LAST_INSERT_ID()
;";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }

  internal List<Keep> GetAll()
  {
    string sql = @"
  SELECT
  k.*,
  COUNT(vk.keepId) AS kept,
  acct.*
  FROM keeps k
  JOIN accounts acct ON acct.id = k.creatorId
  LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
  GROUP BY (k.id)
  ;";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (k, acct) =>
    {
      k.Creator = acct;
      return k;
    }).ToList();
    return keeps;
  }

  internal Keep GetOne(int keepId)
  {
    string sql = @"
SELECT
k.*,
COUNT(vk.keepId) AS kept,
acct.*
FROM keeps k
JOIN accounts acct ON acct.id = k.creatorId
LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
WHERE k.id = @keepId
GROUP BY (k.id)
;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, acct) =>
    {
      k.Creator = acct;
      return k;
    }, new { keepId }).FirstOrDefault();
    return keep;
  }
  internal void EditKeep(Keep keep)
  {
    string sql = @"
UPDATE keeps
SET
name = @Name,
description = @description,
img = @Img,
views = @views
WHERE id = @Id
;";
    _db.Execute(sql, keep);
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = @"DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
    _db.Execute(sql, new { keepId });
  }

  internal List<Keep> GetUsersKeeps(string profileId)
  {
    string sql = @"
SELECT
profile.*,
k.*,
COUNT(vk.keepId) AS Kept
FROM accounts profile
JOIN keeps k ON k.creatorId = profile.id
LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
WHERE profile.id = @profileId
GROUP BY (k.id)
;";
    List<Keep> keeps = _db.Query<Profile, Keep, Keep>(sql, (profile, k) =>
    {
      k.Creator = profile;
      return k;
    }, new { profileId }).ToList();
    return keeps;
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
