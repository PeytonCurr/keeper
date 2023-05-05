namespace keeper.Models;

public class VaultKeep : RepoItem<int>
{
  public string CreatorId { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }
}
public class VaultedKeep : Keep
{
  public int VaultKeepId { get; set; }
}