import { Keep } from "./Keep.js"

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

export class VaultedKeep extends Keep {
  constructor(data) {
    super(data)
    this.vaultKeepId = data.vaultKeepId
  }
}