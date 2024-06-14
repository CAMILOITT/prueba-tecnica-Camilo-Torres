<script setup lang="ts">
import { ref, onMounted } from 'vue'
const refDialog = ref(null)

function openDialog() {
  refDialog.value.showModal()
}

</script>

<template>
  <button @click="openDialog" class="btn-save">
    guardar partida
  </button>
  <dialog ref="refDialog">

    <div v-if="true"></div>
    <div v-else></div>

  </dialog>
</template>

<style scoped>
.btn-save {
  position: absolute;
  top: 10px;
  right: 20px;
  padding: .5rem 1rem;
  background-color: #4CAF50;
}
</style>