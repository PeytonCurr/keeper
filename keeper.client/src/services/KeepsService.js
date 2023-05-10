import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getKeeps() {
    AppState.keeps = []
    const res = await api.get("api/keeps");
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  // async getOneKeep(keepId) {
  //   const res = await api.get(`api/keeps/${keepId}`)
  //   logger.log(res.data)
  //   AppState.activeKeep = new Keep(res.data)
  //   logger.log(["Logging AppState.activeKeep"], AppState.activeKeep)
  // }
  async increaseViews(keep) {
    const res = await api.get(`api/keeps/${keep.id}/increaseViews`)
    // logger.log(["logging res.data"], res.data)
  }

  async getProfileKeeps(profileId) {
    AppState.keeps = []
    const res = await api.get(`api/profiles/${profileId}/keeps`);
    // logger.log("[Logging Res.Data]", res.data);
    AppState.keeps = res.data.map(k => new Keep(k))
  }
}

export const keepsService = new KeepsService();