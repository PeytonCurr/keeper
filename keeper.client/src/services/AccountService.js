import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async editAccount(accountData) {
    accountData.id = AppState.account.id;
    accountData.email = AppState.account.email;
    accountData.createdAt = AppState.account.createdAt;
    accountData.updatedAt = AppState.account.updatedAt;
    const res = await api.put(`account`, accountData);
    AppState.account = new Account(res.data);
  }

}

export const accountService = new AccountService()
