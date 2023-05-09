import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async getMyVaults() {
    const res = await api.get(`account/vaults`)
    AppState.vaults = res.data.map(v => new Vault(v))
    // logger.log(["Logging the AppState"], AppState.vaults)
  }
  async saveToVault(vaultId, keepId) {
    let vaultKeepData = {}
    vaultKeepData.vaultId = vaultId
    vaultKeepData.keepId = keepId
    const res = await api.post(`api/vaultKeeps`, vaultKeepData)
    AppState.vaultKeeps.push = new VaultKeep(res.data)
    logger.log(['Logging AppState'], AppState.vaultKeeps)
  }

}

export const vaultsService = new VaultsService();