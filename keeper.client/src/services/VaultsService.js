import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async getMyVaults() {
    AppState.vaults = []
    const res = await api.get(`account/vaults`)
    AppState.vaults = res.data.map(v => new Vault(v))
    // logger.log(["Logging the AppState"], AppState.vaults)
  }
  async saveToVault(selectableValue, keepId) {
    selectableValue = selectableValue.replace(/[^\d]/g, '')
    const vaultId = selectableValue
    let vaultKeepData = {}
    vaultKeepData.vaultId = vaultId
    vaultKeepData.keepId = keepId
    const res = await api.post(`api/vaultKeeps`, vaultKeepData)
    AppState.vaultKeeps.push = new VaultKeep(res.data)
    logger.log(['Logging AppState'], AppState.vaultKeeps)
  }
  async getProfileVaults(profileId) {
    AppState.vaults = []
    const res = await api.get(`api/profiles/${profileId}/vaults`);
    AppState.vaults = res.data.map(v => new Vault(v));
  }

  async createVault(vaultData) {
    const res = await api.post(`api/vaults`, vaultData);
    AppState.vaults.push(new Vault(res.data))
    logger.log("[logging AppState Vaults]", AppState.vaults)
  }


}

export const vaultsService = new VaultsService();