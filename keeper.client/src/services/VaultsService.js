import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async getMyVaults() {
    const res = await api.get(`account/vaults`)
    AppState.vaults = res.data.map(v => new Vault(v))
    logger.log(["Logging the AppState"], AppState.vaults)
  }
}

export const vaultsService = new VaultsService();