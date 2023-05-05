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


}
