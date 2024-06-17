<script setup lang="ts">
import { inject, ref } from 'vue';
import { UserInformation, UserInject } from '../../type/user';
const refDialogAccount = ref()
const { infoUser, setName, setMoney } = inject('infoUser') as UserInject

const nameValue = ref(infoUser.name);

async function saveGain() {
  if (!infoUser.name || infoUser.name === `Anonymous`) return

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
    (refDialogAccount.value as HTMLDialogElement).close()
  })
}


function openDialog() {
  console.dir(refDialogAccount);

  (refDialogAccount.value as HTMLDialogElement).showModal()
  if (!infoUser.name || infoUser.name === `Anonymous`) return
  saveGain()
}

</script>

<template>
  <button @click="openDialog" class="btn-save">
    guardar partida
  </button>

  <dialog ref="refDialogAccount" class="dialog" :open="false">
    <div v-if="!infoUser.name || infoUser.name === `Anonymous`" class="form-account">
      <label for="Username">
        nombre de usuario:
        <input type="text" name="username" id="Username" :value="nameValue"
          @input="event => nameValue = `${(event?.target as HTMLInputElement)!.value as string}`">
      </label>
      <button @click="saveGain" class="btn">crear cuenta</button>
      <button @click="refDialogAccount.close" class="btn">cancelar</button>
    </div>
    <div v-else class="load">
      ... cargando
    </div>
  </dialog>
</template>

<style scoped>
.load {
  padding: 1rem 2rem;
}

.btn {
  padding: .5rem 1rem;
  background-color: #4CAF50;
}

.form-account {
  display: grid;
  gap: 1rem;
}

.dialog {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 1rem;
  border-radius: 1rem;
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