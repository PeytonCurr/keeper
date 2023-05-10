<template>
  Going to put all the Stuff from KeepDetails
</template>


<script lang="ts">
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';

export default {
  setup() {
    return {
      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm("Are you sure you want to Delete This Vault?")) {
            await vaultsService.deleteVault(vaultId);
            Pop.toast("You Vault was Successfully Deleted")
          }
        } catch (error) {
          Pop.error(error);
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped></style>