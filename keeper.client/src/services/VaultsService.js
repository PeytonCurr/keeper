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

  async createVault(vaultData, route) {
    const res = await api.post(`api/vaults`, vaultData);
    if (route.params.accountId == null && route.params.profileId == null) {
      AppState.vaults.push(new Vault(res.data))
    }
    if (res.data.creatorId == route.params.accountId) {
      AppState.vaults.push(new Vault(res.data))
    }
    // logger.log("[logging AppState Vaults]", AppState.vaults)
  }

  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    AppState.vaults = AppState.vaults.filter(v => v.id != vaultId)
  }
  async getActiveVault(vaultId) {
    AppState.activeKeep = null
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
  }

}

export const vaultsService = new VaultsService();