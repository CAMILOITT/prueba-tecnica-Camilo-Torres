<script setup lang="ts">
import { onMounted } from 'vue'
import InformationUser from './components/informationUser/InformationUser.vue'
import InitGame from './components/initGame/InitGame.vue'
import TableBet from './components/tableBet/TableBet.vue'
import SaveGame from './components/saveGame/SaveGame.vue'

// onMounted(() => {
//   window.addEventListener('beforeunload', (event) => {
//     event.preventDefault();
//     event.returnValue = '¿Estás seguro de que quieres salir?';
//   });
// })

</script>

<template>
  <main class="main">
    <InformationUser user="Camilo" :money="100" />
    <TableBet class="table" />
    <SaveGame />
    <InitGame/>
  </main>
</template>

<style scoped>
.main {
  width: 100dvw;
  height: 100dvh;
  background: green;
}
</style>
