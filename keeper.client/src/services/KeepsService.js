import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getKeeps() {
    const res = await api.get("api/keeps");
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  // async getOneKeep(keepId) {
  //   const res = await api.get(`api/keeps/${keepId}`)
  //   logger.log(res.data)
  //   AppState.activeKeep = new Keep(res.data)
  //   logger.log(["Logging AppState.activeKeep"], AppState.activeKeep)
  // }

}

export const keepsService = new KeepsService();