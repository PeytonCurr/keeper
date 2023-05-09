export class VaultKeep {
  constructor(data) {
    this.id = data.id
    this.vaultId = data.vaultId
    this.keepId = data.keepId
    this.creatorId = data.creatorId
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
  }
}