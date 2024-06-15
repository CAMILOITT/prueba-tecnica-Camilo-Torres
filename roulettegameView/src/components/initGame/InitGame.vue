<script setup lang="ts">
import { inject, ref } from 'vue';
import { UserInformation, UserInject } from '../../type/user';

const { setName, setMoney } = inject('infoUser') as UserInject

const nameValue = ref()
const visibilityComponent = ref(true)
async function getUser() {
  fetch('https://localhost:44342/api/Player/GetPlayer', {
    method: 'POST',
    body: JSON.stringify({ name: `${nameValue.value}` }),
    headers: {
      'Content-Type': 'application/json'
    }
  }).then(res => res.json()).then((data: UserInformation) => {
    console.log(data);
    setName(data.name)
    setMoney(data.amount)
    visibilityComponent.value = false

  }).catch(e => console.log(e))
}

function initAnonymous() {
  setName("Anonymous")
  setMoney(100)
  visibilityComponent.value = false
}


</script>

<template>
  <div class="session" v-if="visibilityComponent">
    <h1>Bienvenido al juego de la ruleta</h1>
    <div class="login">
      <label for="Username">
        nombre de usuario:
        <input type="text" name="username" id="Username" :value="nameValue"
          @input="event => nameValue = `${(event?.target as HTMLInputElement)!.value as string}`">
      </label>
      <button @click="getUser">ingresar con cuenta</button>
    </div>
    <div class="separation"></div>
    <div class="anonymous">
      <button @click="initAnonymous">ingresar como anónimo</button>
    </div>
  </div>
</template>

<style>
.session {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background: black;
  padding: 1rem 2rem;
  border-radius: .5rem;

  & button {
    padding: .5rem 1rem;
  }
}

.session-hidden{
  display: none;
}

.mount {
  position: absolute;
}


.login {
  display: flex;
  flex-direction: column;
  gap: 1rem;

  &>label {
    display: flex;
    flex-direction: column;
    gap: .5rem;

    &>input {
      background: white;
      color: black;
      padding: .5rem 1rem;
    }
  }
}

.separation {
  border: 1px solid white;
}

.anonymous {
  display: flex;
  width: 100%;

  &>button {
    width: 100%;
  }
}
</style>