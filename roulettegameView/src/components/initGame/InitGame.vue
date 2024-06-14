<script setup lang="ts">

function getUser() {

}

</script>

<template>
  <div class="session">
    <h1>Bienvenido al juego de la ruleta</h1>
    <div class="login">
      <label for="Username">
        nombre de usuario:
        <input type="text" name="username" id="Username">
      </label>
      <button>ingresar con cuenta</button>
    </div>
    <div class="separation"></div>
    <div class="anonymous">
      <button>ingresar como anónimo</button>
    </div>
    <!-- <div class="mount">
      <p>monto a abonar a la cuenta</p>
      <input type="number">
      <button>iniciar juego</button>
    </div> -->
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