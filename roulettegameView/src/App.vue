<script setup lang="ts">
import { provide, reactive } from 'vue'
import InformationUser from './components/informationUser/InformationUser.vue'
import InitGame from './components/initGame/InitGame.vue'
import SaveGame from './components/saveGame/SaveGame.vue'
import TableBet from './components/tableBet/TableBet.vue'

const infoUser = reactive({
  name: '',
  amount: 0,
  gain: 0
})

function setName(name: string){
infoUser.name = name
}

function setMoney(money: number){
  infoUser.amount = money
}

provide('infoUser', { infoUser, setName, setMoney })

// onMounted(() => {
//   window.addEventListener('beforeunload', (event) => {
//     event.preventDefault();
//     event.returnValue = '¿Estás seguro de que quieres salir?';
//   });
// })

</script>

<template>
  <main class="main">
    <InformationUser />
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
