<script setup lang="ts">
import { ref, onMounted, inject } from 'vue'
import { UserInformation, UserInject } from '../../type/user';
const refDialog = ref()
const { infoUser, setName, setMoney } = inject('infoUser') as UserInject

const nameValue = ref(infoUser.name)
async function saveGain() {
  fetch('https://localhost:44342/api/Player/SaveAgain', {
    method: 'POST',
    body: JSON.stringify({ ...infoUser, name: nameValue.value }),
    headers: {
      'Content-Type': 'application/json'
    }
  }).then(res => res.json()).then((data: UserInformation) => {
    console.log(data);
    setName(data.name)
    setMoney(data.amount)
  }).catch(e => console.log(e)).finally(() => {
    (refDialog.value as HTMLDialogElement).close()
  })
}


function openDialog() {
  (refDialog.value as HTMLDialogElement).showModal()
  if (!infoUser.name || infoUser.name === `Anonymous`) return
  saveGain()
}

</script>

<template>
  <button @click="openDialog" class="btn-save">
    guardar partida
  </button>
  <dialog ref="refDialog" class="dialog">

    <div v-if="!infoUser.name || infoUser.name === `Anonymous`">
      <label for="Username">
        nombre de usuario:
        <input type="text" name="username" id="Username" :value="nameValue"
          @input="event => nameValue = `${(event?.target as HTMLInputElement)!.value as string}`">
      </label>
      <button @click="saveGain">ingresar con cuenta</button>
    </div>
    <div v-else>

      ... cargando
    </div>

  </dialog>
</template>

<style scoped>
.dialog {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 1rem;
  border-radius: 1rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

label {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

input {
  background-color: white;
  color: black;
  padding: .5rem;
}

.btn-save {
  position: absolute;
  top: 10px;
  right: 20px;
  padding: .5rem 1rem;
  background-color: #4CAF50;
}
</style>