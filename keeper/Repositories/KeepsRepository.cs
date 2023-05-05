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
  (name, description, img, views, kept, creatorId)
values
  (@name, @description, @img, @views, @kept, @creatorId);
  SELECT LAST_INSERT_ID()
;";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }
}